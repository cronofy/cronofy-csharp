namespace Cronofy
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Requests;
    using Responses;

    /// <summary>
    /// Class for a Cronofy client that interacts with an account's calendars
    /// and events.
    /// </summary>
    public sealed class CronofyAccountClient : CronofyAccessTokenClient, ICronofyAccountClient
    {
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
        public CronofyAccountClient(string accessToken) : base(accessToken)
        {
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Cronofy.CronofyAccountClient"/> class.
        /// </summary>
        /// <param name="accessToken">
        /// The access token for the OAuth authorization for the account, must
        /// not be empty.
        /// </param>
        /// <param name="dataCentre">
        /// The data centre to use.
        /// </param>
        /// <exception cref="System.ArgumentException">
        /// Thrown if <paramref name="accessToken"/> is <code>null</code> or
        /// empty.
        /// </exception>
        public CronofyAccountClient(string accessToken, string dataCentre)
            : base(accessToken, dataCentre)
        {
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Cronofy.CronofyAccountClient"/> class.
        /// </summary>
        /// <param name="accessToken">
        /// The access token for the OAuth authorization for the account, must
        /// not be empty.
        /// </param>
        /// <param name="dataCentre">
        /// The data centre to use, must not be <code>null</code>.
        /// </param>
        /// <exception cref="System.ArgumentException">
        /// Thrown if <paramref name="accessToken"/> is <code>null</code> or
        /// empty, or if <paramref name="dataCentre"/> is <code>null</code>.
        /// </exception>
        public CronofyAccountClient(string accessToken, DataCentre dataCentre)
            : base(accessToken, dataCentre)
        {
        }

        /// <inheritdoc/>
        public Account GetAccount()
        {
            var request = new HttpRequest();

            request.Method = "GET";
            request.Url = this.UrlProvider.AccountUrl;
            request.AddOAuthAuthorization(this.AccessToken);

            var response = this.HttpClient.GetJsonResponse<AccountResponse>(request);

            return response.ToAccount();
        }

        /// <inheritdoc/>
        public IEnumerable<Profile> GetProfiles()
        {
            var request = new HttpRequest();

            request.Method = "GET";
            request.Url = this.UrlProvider.ProfilesUrl;
            request.AddOAuthAuthorization(this.AccessToken);

            var response = this.HttpClient.GetJsonResponse<ProfilesResponse>(request);

            return response.Profiles.Select(p => p.ToProfile());
        }

        /// <inheritdoc/>
        public IEnumerable<Calendar> GetCalendars()
        {
            var request = new HttpRequest();

            request.Method = "GET";
            request.Url = this.UrlProvider.CalendarsUrl;
            request.AddOAuthAuthorization(this.AccessToken);

            var calendarsResponse = this.HttpClient.GetJsonResponse<CalendarsResponse>(request);

            return calendarsResponse.Calendars.Select(c => c.ToCalendar());
        }

        /// <inheritdoc/>
        public Calendar CreateCalendar(string profileId, string name)
        {
            Preconditions.NotEmpty("profileId", profileId);
            Preconditions.NotEmpty("name", name);

            var calendarRequest = new CreateCalendarRequest
            {
                ProfileId = profileId,
                Name = name,
            };

            return this.CreateCalendar(calendarRequest);
        }

        /// <inheritdoc/>
        public Calendar CreateCalendar(string profileId, string name, string color)
        {
            Preconditions.NotEmpty("profileId", profileId);
            Preconditions.NotEmpty("name", name);
            Preconditions.NotEmpty("color", color);

            var calendarRequest = new CreateCalendarRequest
            {
                ProfileId = profileId,
                Name = name,
                Color = color,
            };

            return this.CreateCalendar(calendarRequest);
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
            httpRequest.Url = this.UrlProvider.EventsUrl;
            httpRequest.AddOAuthAuthorization(this.AccessToken);

            httpRequest.QueryString.Add("tzid", request.TimeZoneId);
            httpRequest.QueryString.Add("localized_times", true);
            httpRequest.QueryString.Add("from", request.From);
            httpRequest.QueryString.Add("to", request.To);
            httpRequest.QueryString.Add("last_modified", request.LastModified);
            httpRequest.QueryString.Add("include_deleted", request.IncludeDeleted);
            httpRequest.QueryString.Add("include_moved", request.IncludeMoved);
            httpRequest.QueryString.Add("include_managed", request.IncludeManaged);
            httpRequest.QueryString.Add("only_managed", request.OnlyManaged);
            httpRequest.QueryString.Add("include_geo", request.IncludeGeo);
            httpRequest.QueryString.Add("google_event_ids", request.GoogleEventIds);
            httpRequest.QueryString.Add("calendar_ids[]", request.CalendarIds);

            return new PagedResultsIterator<ReadEventsResponse, Event>(
                this.HttpClient,
                this.AccessToken,
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
            httpRequest.Url = this.UrlProvider.FreeBusyUrl;
            httpRequest.AddOAuthAuthorization(this.AccessToken);

            httpRequest.QueryString.Add("tzid", request.TimeZoneId);
            httpRequest.QueryString.Add("localized_times", true);
            httpRequest.QueryString.Add("from", request.From);
            httpRequest.QueryString.Add("to", request.To);
            httpRequest.QueryString.Add("include_managed", request.IncludeManaged);
            httpRequest.QueryString.Add("calendar_ids[]", request.CalendarIds);

            return new PagedResultsIterator<FreeBusyResponse, FreeBusy>(
                this.HttpClient,
                this.AccessToken,
                httpRequest);
        }

        /// <inheritdoc/>
        public BatchResponse BatchRequest(IBuilder<BatchRequest> batchBuilder)
        {
            Preconditions.NotNull("batchBuilder", batchBuilder);

            var request = batchBuilder.Build();

            return this.BatchRequest(request);
        }

        /// <inheritdoc/>
        public BatchResponse BatchRequest(BatchRequest batchRequest)
        {
            var request = new HttpRequest();

            request.Method = "POST";
            request.Url = this.UrlProvider.BatchUrl;
            request.AddOAuthAuthorization(this.AccessToken);
            request.SetJsonBody(batchRequest);

            var response = this.HttpClient.GetJsonResponse<BatchResponse>(request);

            for (int i = 0; i < response.Batch.Length; i++)
            {
                response.Batch[i].Request = batchRequest.Batch[i];
            }

            if (response.HasErrors)
            {
                var message = string.Format("Batch contains {0} errors", response.Errors.Count);
                throw new BatchWithErrorsException(message, response);
            }

            return response;
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
            request.Url = string.Format(this.UrlProvider.ManagedEventUrlFormat, calendarId);
            request.AddOAuthAuthorization(this.AccessToken);
            request.SetJsonBody(eventRequest);

            this.HttpClient.GetValidResponse(request);
        }

        /// <inheritdoc/>
        public void DeleteEvent(string calendarId, string eventId)
        {
            Preconditions.NotEmpty("calendarId", calendarId);
            Preconditions.NotEmpty("eventId", eventId);

            var request = new HttpRequest();

            request.Method = "DELETE";
            request.Url = string.Format(this.UrlProvider.ManagedEventUrlFormat, calendarId);
            request.AddOAuthAuthorization(this.AccessToken);

            var requestBody = new DeleteEventRequest { EventId = eventId };
            request.SetJsonBody(requestBody);

            this.HttpClient.GetValidResponse(request);
        }

        /// <inheritdoc/>
        public void DeleteAllEvents()
        {
            var request = new HttpRequest();

            request.Method = "DELETE";
            request.Url = this.UrlProvider.EventsUrl;
            request.AddOAuthAuthorization(this.AccessToken);

            var requestBody = new { delete_all = true };
            request.SetJsonBody(requestBody);

            var response = this.HttpClient.GetResponse(request);

            if (response.Code != 202)
            {
                // TODO More useful exceptions for validation errors
                throw new CronofyException("Request failed");
            }
        }

        /// <inheritdoc/>
        public void DeleteAllEventsForCalendars(params string[] calendarIds)
        {
            Preconditions.NotEmpty("calendarIds", calendarIds);

            var request = new HttpRequest();

            request.Method = "DELETE";
            request.Url = this.UrlProvider.EventsUrl;
            request.AddOAuthAuthorization(this.AccessToken);

            var requestBody = new { calendar_ids = calendarIds };
            request.SetJsonBody(requestBody);

            var response = this.HttpClient.GetResponse(request);

            if (response.Code != 202)
            {
                // TODO More useful exceptions for validation errors
                throw new CronofyException("Request failed");
            }
        }

        /// <inheritdoc/>
        public void DeleteExternalEvent(string calendarId, string eventUid)
        {
            Preconditions.NotEmpty("calendarId", calendarId);
            Preconditions.NotEmpty("eventUid", eventUid);

            var request = new HttpRequest();

            request.Method = "DELETE";
            request.Url = string.Format(this.UrlProvider.ManagedEventUrlFormat, calendarId);
            request.AddOAuthAuthorization(this.AccessToken);

            var requestBody = new DeleteExternalEventRequest { EventUid = eventUid };
            request.SetJsonBody(requestBody);

            var response = this.HttpClient.GetResponse(request);

            if (response.Code != 202)
            {
                throw new CronofyException("Request failed");
            }
        }

        /// <inheritdoc/>
        public void ChangeParticipationStatus(string calendarId, string eventUid, ParticipationStatus status)
        {
            Preconditions.NotEmpty("calendarId", calendarId);
            Preconditions.NotEmpty("eventUid", eventUid);

            var request = new HttpRequest();

            request.Method = "POST";
            request.Url = string.Format(this.UrlProvider.ParticipationStatusUrlFormat, calendarId, eventUid);
            request.AddOAuthAuthorization(this.AccessToken);

            var requestBody = new { status = status.ToString().ToLower() };
            request.SetJsonBody(requestBody);

            var response = this.HttpClient.GetResponse(request);

            if (response.Code != 202)
            {
                throw new CronofyException("Request failed");
            }
        }

        /// <inheritdoc/>
        public ElevatedPermissionsResponse ElevatedPermissions(IBuilder<ElevatedPermissionsRequest> builder)
        {
            Preconditions.NotNull("builder", builder);

            var request = builder.Build();

            return this.ElevatedPermissions(request);
        }

        /// <inheritdoc/>
        public ElevatedPermissionsResponse ElevatedPermissions(ElevatedPermissionsRequest permissionsRequest)
        {
            Preconditions.NotNull("permissionsRequesr", permissionsRequest);

            var request = new HttpRequest();

            request.Method = "POST";
            request.Url = this.UrlProvider.PermissionsUrl;
            request.AddOAuthAuthorization(this.AccessToken);
            request.SetJsonBody(permissionsRequest);

            var response = this.HttpClient.GetJsonResponse<Responses.ElevatedPermissionsResponse>(request);
            return response.ToElevatedPermissions();
        }

        /// <inheritdoc/>
        public Channel CreateChannel(string callbackUrl)
        {
            Preconditions.NotEmpty("callbackUrl", callbackUrl);

            var builder = new CreateChannelBuilder()
                .CallbackUrl(callbackUrl);

            return this.CreateChannel(builder);
        }

        /// <inheritdoc/>
        public Channel CreateChannel(IBuilder<CreateChannelRequest> channelBuilder)
        {
            Preconditions.NotNull("channelBuilder", channelBuilder);

            var request = channelBuilder.Build();

            return this.CreateChannel(request);
        }

        /// <inheritdoc/>
        public Channel CreateChannel(CreateChannelRequest channelRequest)
        {
            Preconditions.NotNull("channelRequest", channelRequest);

            var request = new HttpRequest();

            request.Method = "POST";
            request.Url = this.UrlProvider.ChannelsUrl;
            request.AddOAuthAuthorization(this.AccessToken);
            request.SetJsonBody(channelRequest);

            var response = this.HttpClient.GetJsonResponse<ChannelResponse>(request);

            return response.ToChannel();
        }

        /// <inheritdoc/>
        public IEnumerable<Channel> GetChannels()
        {
            var request = new HttpRequest();

            request.Method = "GET";
            request.Url = this.UrlProvider.ChannelsUrl;
            request.AddOAuthAuthorization(this.AccessToken);

            var response = this.HttpClient.GetJsonResponse<ChannelsResponse>(request);

            return response.Channels.Select(c => c.ToChannel());
        }

        /// <inheritdoc/>
        public void CloseChannel(string channelId)
        {
            Preconditions.NotEmpty("channelId", channelId);

            var request = new HttpRequest();

            request.Method = "DELETE";
            request.Url = string.Format(this.UrlProvider.ChannelUrlFormat, channelId);
            request.AddOAuthAuthorization(this.AccessToken);

            this.HttpClient.GetValidResponse(request);
        }

        /// <inheritdoc/>
        public IEnumerable<AvailablePeriod> GetAvailability(IBuilder<AvailabilityRequest> builder)
        {
            Preconditions.NotNull("builder", builder);

            var request = builder.Build();

            return this.GetAvailability(request);
        }

        /// <inheritdoc/>
        public IEnumerable<AvailablePeriod> GetAvailability(AvailabilityRequest availabilityRequest)
        {
            Preconditions.NotNull("availabilityRequest", availabilityRequest);

            var request = new HttpRequest();

            request.Method = "POST";
            request.Url = this.UrlProvider.AvailabilityUrl;
            request.AddOAuthAuthorization(this.AccessToken);
            request.SetJsonBody(availabilityRequest);

            var response = this.HttpClient.GetJsonResponse<AvailabilityResponse>(request);

            return response.AvailablePeriods.Select(ap => ap.ToAvailablePeriod());
        }

        /// <inheritdoc/>
        public AvailableSequences GetSequencedAvailability(IBuilder<SequencedAvailabilityRequest> builder)
        {
            Preconditions.NotNull("builder", builder);

            var request = builder.Build();

            return this.GetSequencedAvailability(request);
        }

        /// <inheritdoc/>
        public AvailableSequences GetSequencedAvailability(SequencedAvailabilityRequest availabilityRequest)
        {
            Preconditions.NotNull("availabilityRequest", availabilityRequest);

            var request = new HttpRequest();

            request.Method = "POST";
            request.Url = this.UrlProvider.SequencedAvailabilityUrl;
            request.AddOAuthAuthorization(this.AccessToken);
            request.SetJsonBody(availabilityRequest);

            var response = this.HttpClient.GetJsonResponse<SequencedAvailabilityResponse>(request);
            return response.ToSequence();
        }

        /// <inheritdoc/>
        public string CreateLinkToken()
        {
            var request = new HttpRequest();

            request.Method = "POST";
            request.Url = this.UrlProvider.LinkTokensUrl;
            request.AddOAuthAuthorization(this.AccessToken);

            var response = this.HttpClient.GetJsonResponse<LinkTokenResponse>(request);

            return response.LinkToken;
        }

        /// <inheritdoc/>
        public void RevokeProfileAuthorization(string profileId)
        {
            Preconditions.NotEmpty("profileId", profileId);

            var request = new HttpRequest();

            request.Method = "POST";
            request.Url = string.Format(this.UrlProvider.RevokeProfileAuthorizationUrlFormat, profileId);
            request.AddOAuthAuthorization(this.AccessToken);

            this.HttpClient.GetValidResponse(request);
        }

        /// <summary>
        /// Creates a calendar.
        /// </summary>
        /// <param name="calendarRequest">
        /// The calendar request from which to make the calendar.
        /// </param>
        /// <returns>The created calendar.</returns>
        private Calendar CreateCalendar(CreateCalendarRequest calendarRequest)
        {
            var request = new HttpRequest();

            request.Method = "POST";
            request.Url = this.UrlProvider.CalendarsUrl;
            request.AddOAuthAuthorization(this.AccessToken);

            request.SetJsonBody(calendarRequest);

            var response = this.HttpClient.GetJsonResponse<CreateCalendarResponse>(request);

            return response.ToCalendar();
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
