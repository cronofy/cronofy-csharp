namespace Cronofy
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Cronofy;
    using Cronofy.Requests;
    using Cronofy.Responses;

    /// <summary>
    /// Class for a Cronofy client that interacts with an account's calendars
    /// and events.
    /// </summary>
    public sealed class CronofyAccountClient : ICronofyAccountClient
    {
        /// <summary>
        /// The URL of the list calendars endpoint.
        /// </summary>
        private const string CalendarsUrl = "https://api.cronofy.com/v1/calendars";

        /// <summary>
        /// The URL of the read events endpoint.
        /// </summary>
        private const string ReadEventsUrl = "https://api.cronofy.com/v1/events";

        /// <summary>
        /// The URL format for the managed event endpoint.
        /// </summary>
        private const string ManagedEventUrlFormat = "https://api.cronofy.com/v1/calendars/{0}/events";

        /// <summary>
        /// The access token for the OAuth authorization for the account.
        /// </summary>
        private readonly string accessToken;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Cronofy.CronofyAccountClient"/> class.
        /// </summary>
        /// <param name="accessToken">
        /// The access token for the OAuth authorization for the account, must
        /// not be empty.
        /// </param>
        /// <exception cref="System.ArgumentException">
        /// Thrown if <paramref name="accessToken"/> is null or empty.
        /// </exception>
        public CronofyAccountClient(string accessToken)
        {
            Preconditions.NotEmpty("accessToken", accessToken);

            this.accessToken = accessToken;
            this.HttpClient = new ConcreteHttpClient();
        }

        /// <summary>
        /// Gets or sets the HTTP client.
        /// </summary>
        /// <value>
        /// The HTTP client.
        /// </value>
        /// <remarks>
        /// Intend for test purposes only.
        /// </remarks>
        internal IHttpClient HttpClient { get; set; }

        /// <inheritdoc/>
        public IEnumerable<Calendar> GetCalendars()
        {
            var request = new HttpRequest();

            request.Method = "GET";
            request.Url = CalendarsUrl;
            request.AddOAuthAuthorization(this.accessToken);

            var calendarsResponse = this.HttpClient.GetJsonResponse<CalendarsResponse>(request);

            return calendarsResponse.Calendars.Select(c => c.ToCalendar());
        }

        /// <inheritdoc/>
        public IEnumerable<Event> GetEvents()
        {
            var request = new HttpRequest();

            request.Method = "GET";
            request.Url = ReadEventsUrl;
            request.AddOAuthAuthorization(this.accessToken);

            // TODO Support parameters
            request.QueryString.Add("tzid", "Etc/UTC");
            request.QueryString.Add("localized_times", "true");

            // Eagerly fetch the first page to hit access token and validation issues.
            var response = this.HttpClient.GetJsonResponse<ReadEventsResponse>(request);

            return new GetEventsIterator(this.HttpClient, this.accessToken, response);
        }

        /// <inheritdoc/>
        public void UpsertEvent(string calendarId, IBuilder<UpsertEventRequest> eventBuilder)
        {
            Preconditions.NotEmpty("calendarId", calendarId);
            Preconditions.NotNull("eventBuilder", eventBuilder);

            var request = eventBuilder.Build();

            this.UpsertEvent(calendarId, request);
        }

        /// <inheritdoc/>
        public void UpsertEvent(string calendarId, UpsertEventRequest eventRequest)
        {
            Preconditions.NotEmpty("calendarId", calendarId);
            Preconditions.NotNull("eventRequest", eventRequest);

            var request = new HttpRequest();

            request.Method = "POST";
            request.Url = string.Format(ManagedEventUrlFormat, calendarId);
            request.AddOAuthAuthorization(this.accessToken);
            request.SetJsonBody(eventRequest);

            var response = this.HttpClient.GetResponse(request);

            if (response.Code != 202)
            {
                // TODO More useful exceptions for validation errors
                throw new CronofyException("Request failed");
            }
        }

        /// <inheritdoc/>
        public void DeleteEvent(string calendarId, string eventId)
        {
            Preconditions.NotEmpty("calendarId", calendarId);
            Preconditions.NotEmpty("eventId", eventId);

            var request = new HttpRequest();

            request.Method = "DELETE";
            request.Url = string.Format(ManagedEventUrlFormat, calendarId);
            request.AddOAuthAuthorization(this.accessToken);

            var requestBody = new { event_id = eventId };
            request.SetJsonBody(requestBody);

            var response = this.HttpClient.GetResponse(request);

            if (response.Code != 202)
            {
                // TODO More useful exceptions for validation errors
                throw new CronofyException("Request failed");
            }
        }

        /// <summary>
        /// Iterator for a paged events response.
        /// </summary>
        internal sealed class GetEventsIterator : IEnumerable<Event>
        {
            /// <summary>
            /// The HTTP client to perform requests with.
            /// </summary>
            private readonly IHttpClient httpClient;

            /// <summary>
            /// The access token for the OAuth authorization for the account.
            /// </summary>
            private readonly string accessToken;

            /// <summary>
            /// The first page of the events response.
            /// </summary>
            private readonly ReadEventsResponse firstPage;

            /// <summary>
            /// Initializes a new instance of the
            /// <see cref="Cronofy.CronofyAccountClient.GetEventsIterator"/>
            /// class.
            /// </summary>
            /// <param name="httpClient">
            /// The HTTP client to use for requests, must not be null.
            /// </param>
            /// <param name="accessToken">
            /// The access token for the OAuth authorization for the account,
            /// must not be empty.
            /// </param>
            /// <param name="firstPage">
            /// The first page of events, must not be null.
            /// </param>
            /// <exception cref="System.ArgumentException">
            /// Thrown if <paramref name="httpClient"/> or
            /// <paramref name="firstPage"/> are null, of if
            /// <paramref name="accessToken"/> is empty.
            /// </exception>
            public GetEventsIterator(IHttpClient httpClient, string accessToken, ReadEventsResponse firstPage)
            {
                Preconditions.NotNull("httpClient", httpClient);
                Preconditions.NotEmpty("accessToken", accessToken);
                Preconditions.NotNull("firstPage", firstPage);

                this.httpClient = httpClient;
                this.accessToken = accessToken;
                this.firstPage = firstPage;
            }

            /// <inheritdoc/>
            public IEnumerator<Event> GetEnumerator()
            {
                return this.GetEvents().GetEnumerator();
            }

            /// <inheritdoc/>
            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            /// <summary>
            /// Gets the events from a page response.
            /// </summary>
            /// <param name="response">
            /// The page response to extract the events from.
            /// </param>
            /// <returns>
            /// The events from the page response.
            /// </returns>
            private static IEnumerable<Event> GetEventsFromPage(ReadEventsResponse response)
            {
                return response.Events.Select(e => e.ToEvent());
            }

            /// <summary>
            /// Gets all the events from the result set.
            /// </summary>
            /// <returns>
            /// All the events from the result set.
            /// </returns>
            private IEnumerable<Event> GetEvents()
            {
                return this.GetPages().SelectMany(GetEventsFromPage);
            }

            /// <summary>
            /// Gets all the pages from the result set.
            /// </summary>
            /// <returns>
            /// All the pages from the result set.
            /// </returns>
            private IEnumerable<ReadEventsResponse> GetPages()
            {
                var currentPage = this.firstPage;

                while (true)
                {
                    yield return currentPage;

                    if (currentPage.Pages.NextPageUrl == null)
                    {
                        break;
                    }

                    currentPage = this.GetNextPageResponse(currentPage);
                }
            }

            /// <summary>
            /// Gets the next page response.
            /// </summary>
            /// <param name="currentPage">
            /// The response for the current page.
            /// </param>
            /// <returns>
            /// The next page response.
            /// </returns>
            private ReadEventsResponse GetNextPageResponse(ReadEventsResponse currentPage)
            {
                var request = new HttpRequest();

                request.Method = "GET";
                request.Url = currentPage.Pages.NextPageUrl;
                request.AddOAuthAuthorization(this.accessToken);

                return this.httpClient.GetJsonResponse<ReadEventsResponse>(request);
            }
        }
    }
}
