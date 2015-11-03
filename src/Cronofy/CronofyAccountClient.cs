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
        /// The URL of the free-busy endpoint.
        /// </summary>
        private const string FreeBusyUrl = "https://api.cronofy.com/v1/free_busy";

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
            var builder = new GetEventsRequestBuilder();

            return this.GetEvents(builder);
        }

        /// <inheritdoc/>
        public IEnumerable<Event> GetEvents(IBuilder<GetEventsRequest> builder)
        {
            Preconditions.NotNull("builder", builder);

            var request = builder.Build();

            return this.GetEvents(request);
        }

        /// <inheritdoc/>
        public IEnumerable<Event> GetEvents(GetEventsRequest request)
        {
            Preconditions.NotNull("request", request);

            var httpRequest = new HttpRequest();

            httpRequest.Method = "GET";
            httpRequest.Url = ReadEventsUrl;
            httpRequest.AddOAuthAuthorization(this.accessToken);

            // TODO Support more parameters
            httpRequest.QueryString.Add("tzid", request.TimeZoneId);
            httpRequest.QueryString.Add("localized_times", "true");

            if (request.From.HasValue)
            {
                httpRequest.QueryString.Add("from", request.From.ToString());
            }

            if (request.To.HasValue)
            {
                httpRequest.QueryString.Add("to", request.To.ToString());
            }

            if (request.LastModified.HasValue)
            {
                httpRequest.QueryString.Add("last_modified", request.LastModified.Value.ToString("u"));
            }

            return new PagedResultsIterator<ReadEventsResponse, Event>(
                this.HttpClient,
                this.accessToken,
                httpRequest);
        }

        /// <inheritdoc/>
        public IEnumerable<FreeBusy> GetFreeBusy()
        {
            var builder = new GetFreeBusyRequestBuilder();

            return this.GetFreeBusy(builder);
        }

        /// <inheritdoc/>
        public IEnumerable<FreeBusy> GetFreeBusy(IBuilder<GetFreeBusyRequest> builder)
        {
            Preconditions.NotNull("builder", builder);

            var request = builder.Build();

            return this.GetFreeBusy(request);
        }

        /// <inheritdoc/>
        public IEnumerable<FreeBusy> GetFreeBusy(GetFreeBusyRequest request)
        {
            Preconditions.NotNull("request", request);

            var httpRequest = new HttpRequest();

            httpRequest.Method = "GET";
            httpRequest.Url = FreeBusyUrl;
            httpRequest.AddOAuthAuthorization(this.accessToken);

            // TODO Support more parameters
            httpRequest.QueryString.Add("tzid", request.TimeZoneId);
            httpRequest.QueryString.Add("localized_times", "true");

            if (request.From.HasValue)
            {
                httpRequest.QueryString.Add("from", request.From.ToString());
            }

            if (request.To.HasValue)
            {
                httpRequest.QueryString.Add("to", request.To.ToString());
            }

            return new PagedResultsIterator<FreeBusyResponse, FreeBusy>(
                this.HttpClient,
                this.accessToken,
                httpRequest);
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
        /// <typeparam name="TResponse">
        /// The type of response returned by the paged result set.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the items within the paged result set.
        /// </typeparam>
        internal sealed class PagedResultsIterator<TResponse, TResult> : IEnumerable<TResult>
            where TResponse : IPagedResultsResponse<TResult>
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
            private readonly TResponse firstPage;

            /// <summary>
            /// Initializes a new instance of the
            /// <see cref="Cronofy.CronofyAccountClient.PagedResultsIterator{TResponse,TResult}"/>
            /// class.
            /// </summary>
            /// <param name="httpClient">
            /// The HTTP client to use for requests, must not be null.
            /// </param>
            /// <param name="accessToken">
            /// The access token for the OAuth authorization for the account,
            /// must not be empty.
            /// </param>
            /// <param name="firstRequest">
            /// The request for the first page of results, must not be null.
            /// </param>
            /// <exception cref="System.ArgumentException">
            /// Thrown if <paramref name="httpClient"/> or
            /// <paramref name="firstRequest"/> are null, of if
            /// <paramref name="accessToken"/> is empty.
            /// </exception>
            public PagedResultsIterator(IHttpClient httpClient, string accessToken, HttpRequest firstRequest)
            {
                Preconditions.NotNull("httpClient", httpClient);
                Preconditions.NotEmpty("accessToken", accessToken);
                Preconditions.NotNull("firstRequest", firstRequest);

                this.httpClient = httpClient;
                this.accessToken = accessToken;

                // Eagerly fetch the first page to hit access token and validation issues.
                this.firstPage = this.httpClient.GetJsonResponse<TResponse>(firstRequest);
            }

            /// <inheritdoc/>
            public IEnumerator<TResult> GetEnumerator()
            {
                return this.GetResults().GetEnumerator();
            }

            /// <inheritdoc/>
            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            /// <summary>
            /// Gets all the events from the result set.
            /// </summary>
            /// <returns>
            /// All the events from the result set.
            /// </returns>
            private IEnumerable<TResult> GetResults()
            {
                return this.GetPages().SelectMany(page => page.GetResults());
            }

            /// <summary>
            /// Gets all the pages from the result set.
            /// </summary>
            /// <returns>
            /// All the pages from the result set.
            /// </returns>
            private IEnumerable<TResponse> GetPages()
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
            private TResponse GetNextPageResponse(IPagedResultsResponse<TResult> currentPage)
            {
                var request = new HttpRequest();

                request.Method = "GET";
                request.Url = currentPage.Pages.NextPageUrl;
                request.AddOAuthAuthorization(this.accessToken);

                return this.httpClient.GetJsonResponse<TResponse>(request);
            }
        }
    }
}
