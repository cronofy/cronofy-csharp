namespace Cronofy
{
    using System;
    using System.Collections.Generic;
    using Cronofy.Requests;

    /// <summary>
    /// Class to build an real time scheduling request.
    /// </summary>
    public sealed class RealTimeSchedulingRequestBuilder : IBuilder<RealTimeSchedulingRequest>
    {
        /// <summary>
        /// The oauth details for the request.
        /// </summary>
        private IBuilder<RealTimeSchedulingRequest.OAuthDetails> oauthBuilder;

        /// <summary>
        /// The event details builder for the request.
        /// </summary>
        private IBuilder<UpsertEventRequest> upsertEventRequestBuilder;

        /// <summary>
        /// The availability details builder for the request.
        /// </summary>
        private IBuilder<AvailabilityRequest> availabilityRequestBuilder;

        /// <summary>
        /// The target calendars builder for the request.
        /// </summary>
        private IList<RealTimeSchedulingRequest.TargetCalendar> targetCalendars;

        /// <summary>
        /// The tzid for the request.
        /// </summary>
        private string tzid;

        /// <summary>
        /// The hour format for the request.
        /// </summary>
        private string hourFormat;

        /// <summary>
        /// The callback URL for the request.
        /// </summary>
        private string callbackUrl;

        /// <summary>
        /// The completed URL for the request.
        /// </summary>
        private string completedUrl;

        /// <summary>
        /// The callback URL for the request.
        /// </summary>
        private string callbackCompletedUrl;

        /// <summary>
        /// The URL for the request when no suitable times are found.
        /// </summary>
        private string noTimesSuitableUrl;

        /// <summary>
        /// The URL for the request when no suitable times are displayed on-screen.
        /// </summary>
        private string noTimesDisplayedUrl;

        /// <summary>
        /// Sets the OAuth details of the request.
        /// </summary>
        /// <param name="redirectUri">
        /// The redirect uri for the request's oauth details, must not be blank.
        /// </param>
        /// <param name="scope">
        /// The scope for the request's oauth details, must not be blank.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="RealTimeSchedulingRequestBuilder"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="redirectUri"/> or <paramref name="scope"/> are empty.
        /// </exception>
        public RealTimeSchedulingRequestBuilder OAuthDetails(string redirectUri, string scope)
        {
            return this.OAuthDetails(redirectUri, scope, null);
        }

        /// <summary>
        /// Sets the Timezone id of the request.
        /// </summary>
        /// <param name="tzid">
        /// The timezone to use for the request, must not be blank.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="RealTimeSchedulingRequestBuilder"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="tzid"/> is empty.
        /// </exception>
        public RealTimeSchedulingRequestBuilder Timezone(string tzid)
        {
            Preconditions.NotBlank("tzid", tzid);

            this.tzid = tzid;

            return this;
        }

        /// <summary>
        /// Sets the hour format of the request.
        /// </summary>
        /// <param name="hourFormat">
        /// The hour format to use for the request, must not be blank.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="RealTimeSchedulingRequestBuilder"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="hourFormat"/> is empty.
        /// </exception>
        public RealTimeSchedulingRequestBuilder HourFormat(string hourFormat)
        {
            Preconditions.NotBlank("hourFormat", hourFormat);

            this.hourFormat = hourFormat;

            return this;
        }

        /// <summary>
        /// Sets the OAuth details of the request.
        /// </summary>
        /// <param name="redirectUri">
        /// The redirect uri for the request's oauth details, must not be blank.
        /// </param>
        /// <param name="scope">
        /// The scope for the request's oauth details.
        /// </param>
        /// <param name="state">
        /// The state for the request's oauth details.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="RealTimeSchedulingRequestBuilder"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="redirectUri"/> is empty.
        /// </exception>
        public RealTimeSchedulingRequestBuilder OAuthDetails(string redirectUri, string scope, string state)
        {
            Preconditions.NotBlank("redirectUri", redirectUri);

            var oauthDetails = new RealTimeSchedulingRequest.OAuthDetails
            {
                RedirectUri = redirectUri,
                Scope = scope,
                State = state,
            };

            this.oauthBuilder = Builder.Wrap(oauthDetails);

            return this;
        }

        /// <summary>
        /// Sets the OAuth details of the request.
        /// </summary>
        /// <param name="oauthBuilder">
        /// The builder for <see cref="RealTimeSchedulingBaseRequest.OAuthDetails"/>.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="RealTimeSchedulingRequestBuilder"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="oauthBuilder"/> is empty.
        /// </exception>
        public RealTimeSchedulingRequestBuilder OAuthDetails(IBuilder<RealTimeSchedulingBaseRequest.OAuthDetails> oauthBuilder)
        {
            Preconditions.NotNull("oauthBuilder", oauthBuilder);

            this.oauthBuilder = oauthBuilder;

            return this;
        }

        /// <summary>
        /// Sets the event details of the request.
        /// </summary>
        /// <param name="upsertEventRequestBuilder">
        /// The event details builder for the request, must not be null.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="RealTimeSchedulingRequestBuilder"/>.
        /// </returns>
        public RealTimeSchedulingRequestBuilder UpsertEventRequestBuilder(IBuilder<UpsertEventRequest> upsertEventRequestBuilder)
        {
            Preconditions.NotNull("upsertEventRequestBuilder", upsertEventRequestBuilder);

            this.upsertEventRequestBuilder = upsertEventRequestBuilder;

            return this;
        }

        /// <summary>
        /// Sets the event details of the request.
        /// </summary>
        /// <param name="upsertEventRequest">
        /// The event details for the request, must not be null.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="RealTimeSchedulingRequestBuilder"/>.
        /// </returns>
        public RealTimeSchedulingRequestBuilder UpsertEventRequest(UpsertEventRequest upsertEventRequest)
        {
            Preconditions.NotNull("event", upsertEventRequest);

            this.upsertEventRequestBuilder = Builder.Wrap(upsertEventRequest);

            return this;
        }

        /// <summary>
        /// Sets the availability details of the request.
        /// </summary>
        /// <param name="availabilityRequestBuilder">
        /// The availability details builder for the request, must not be null.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="RealTimeSchedulingRequestBuilder"/>.
        /// </returns>
        public RealTimeSchedulingRequestBuilder AvailabilityRequestBuilder(IBuilder<AvailabilityRequest> availabilityRequestBuilder)
        {
            Preconditions.NotNull("availabilityRequestBuilder", availabilityRequestBuilder);

            this.availabilityRequestBuilder = availabilityRequestBuilder;

            return this;
        }

        /// <summary>
        /// Sets the availability details of the request.
        /// </summary>
        /// <param name="availabilityRequest">
        /// The event details for the request, must not be null.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="RealTimeSchedulingRequestBuilder"/>.
        /// </returns>
        public RealTimeSchedulingRequestBuilder AvailabilityRequest(AvailabilityRequest availabilityRequest)
        {
            Preconditions.NotNull("availability", availabilityRequest);

            this.availabilityRequestBuilder = Builder.Wrap(availabilityRequest);

            return this;
        }

        /// <summary>
        /// Adds a target calendar to the request.
        /// </summary>
        /// <param name="sub">
        /// The sub for the target calendar.
        /// </param>
        /// <param name="calendarId">
        /// The target calendar's id.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="RealTimeSchedulingRequestBuilder"/>.
        /// </returns>
        public RealTimeSchedulingRequestBuilder AddTargetCalendar(string sub, string calendarId)
        {
            Preconditions.NotBlank("sub", sub);
            Preconditions.NotBlank("calendarId", calendarId);

            if (this.targetCalendars == null)
            {
                this.targetCalendars = new List<RealTimeSchedulingRequest.TargetCalendar>();
            }

            this.targetCalendars.Add(new RealTimeSchedulingRequest.TargetCalendar
            {
                Sub = sub,
                CalendarId = calendarId,
            });

            return this;
        }

        /// <summary>
        /// Sets the target calendar details of the request.
        /// </summary>
        /// <param name="targetCalendars">
        /// The target calendars.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="RealTimeSchedulingRequestBuilder"/>.
        /// </returns>
        public RealTimeSchedulingRequestBuilder TargetCalendars(IList<RealTimeSchedulingRequest.TargetCalendar> targetCalendars)
        {
            this.targetCalendars = targetCalendars;

            return this;
        }

        /// <summary>
        /// Sets the callback URL for the request.
        /// </summary>
        /// <param name="callbackUrl">
        /// The callback URL to use for the request, must not be blank.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="RealTimeSchedulingRequestBuilder"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="callbackUrl"/> is empty.
        /// </exception>
        public RealTimeSchedulingRequestBuilder CallbackUrl(string callbackUrl)
        {
            Preconditions.NotBlank(nameof(callbackUrl), callbackUrl);

            this.callbackUrl = callbackUrl;

            return this;
        }

        /// <summary>
        /// Sets the callback URLs for the request.
        /// </summary>
        /// <param name="callbackCompletedUrl">
        /// The completed URL to use for the request callback.
        /// </param>
        /// <param name="noTimesSuitableUrl">
        /// The URL to use for the request when no suitable times are found.
        /// </param>
        /// <param name="noTimesDisplayedUrl">
        /// The URL to use for the request when no times are displayed on-screen.
        /// </param>
        public RealTimeSchedulingRequestBuilder CallbackUrls(string callbackCompletedUrl = null, string noTimesSuitableUrl = null, string noTimesDisplayedUrl = null)
        {
            this.callbackCompletedUrl = callbackCompletedUrl;
            this.noTimesSuitableUrl = noTimesSuitableUrl;
            this.noTimesDisplayedUrl = noTimesDisplayedUrl;

            return this;
        }

        /// <summary>
        /// Sets the redirect URLs for the request.
        /// </summary>
        /// <param name="completedUrl">
        /// The completed URL to use for the request, must not be blank.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="RealTimeSchedulingRequestBuilder"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="completedUrl"/> is empty.
        /// </exception>
        public RealTimeSchedulingRequestBuilder RedirectUrls(string completedUrl)
        {
            Preconditions.NotBlank(nameof(completedUrl), completedUrl);

            this.completedUrl = completedUrl;

            return this;
        }

        /// <inheritdoc />
        public RealTimeSchedulingRequest Build()
        {
            var request = new RealTimeSchedulingRequest
            {
                OAuth = this.oauthBuilder.Build(),
                Event = this.upsertEventRequestBuilder.Build(),
                TargetCalendars = this.targetCalendars,
                Tzid = this.tzid,
                CallbackUrl = this.callbackUrl
            };

            if (this.completedUrl != null)
            {
                request.RedirectUrls = new RealTimeSchedulingRequest.RedirectUrlsInfo
                {
                    CompletedUrl = this.completedUrl,
                };
            }

            if (this.noTimesSuitableUrl != null || this.noTimesDisplayedUrl != null || this.callbackCompletedUrl != null)
            {
                request.CallbackUrls = new RealTimeSchedulingRequest.CallbackUrlsInfo
                {
                    NoTimesSuitableUrl = this.noTimesSuitableUrl,
                    NoTimesDisplayedUrl = this.noTimesDisplayedUrl,
                    CallbackCompletedUrl = this.callbackCompletedUrl
                };
            }

            if (this.availabilityRequestBuilder != null)
            {
                request.Availability = this.availabilityRequestBuilder.Build();
            }

            if (this.hourFormat != null)
            {
                request.Formatting = new SchedulingFormatting
                {
                    HourFormat = this.hourFormat,
                };
            }

            return request;
        }
    }
}
