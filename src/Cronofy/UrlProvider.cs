namespace Cronofy
{
    /// <summary>
    /// Class for providing URLs.
    /// </summary>
    public sealed class UrlProvider
    {

        // new url here for /v1/availability_rules

        /// <summary>
        /// The URL format of the OAuth authorization endpoint.
        /// </summary>
        private const string AuthorizationUrlFormat = "https://app{0}.cronofy.com/oauth/authorize";

        /// <summary>
        /// The URL format of the Enterprise Connect OAuth authorization endpoint.
        /// </summary>
        private const string EnterpriseConnectAuthorizationUrlFormat = "https://app{0}.cronofy.com/enterprise_connect/oauth/authorize";

        /// <summary>
        /// The URL format of the OAuth token endpoint.
        /// </summary>
        private const string TokenUrlFormat = "https://app{0}.cronofy.com/oauth/token";

        /// <summary>
        /// The URL format of the OAuth token revocation endpoint.
        /// </summary>
        private const string TokenRevocationUrlFormat = "https://app{0}.cronofy.com/oauth/token/revoke";

        /// <summary>
        /// The URL format of the user info endpoint.
        /// </summary>
        private const string UserInfoUrlFormat = "https://api{0}.cronofy.com/v1/userinfo";

        /// <summary>
        /// The URL format of the resources endpoint.
        /// </summary>
        private const string ResourcesUrlFormat = "https://api{0}.cronofy.com/v1/resources";

        /// <summary>
        /// The URL format of the service account user authorization endpoint.
        /// </summary>
        private const string AuthorizeWithServiceAccountUrlFormat = "https://api{0}.cronofy.com/v1/service_account_authorizations";

        /// <summary>
        /// The URL of the account endpoint.
        /// </summary>
        private const string AccountUrlFormat = "https://api{0}.cronofy.com/v1/account";

        /// <summary>
        /// The URL of the profiles endpoint.
        /// </summary>
        private const string ProfilesUrlFormat = "https://api{0}.cronofy.com/v1/profiles";

        /// <summary>
        /// The URL of the list calendars endpoint.
        /// </summary>
        private const string CalendarsUrlFormat = "https://api{0}.cronofy.com/v1/calendars";

        /// <summary>
        /// The URL of the free-busy endpoint.
        /// </summary>
        private const string FreeBusyUrlFormat = "https://api{0}.cronofy.com/v1/free_busy";

        /// <summary>
        /// The URL of the read events endpoint.
        /// </summary>
        private const string EventsUrlFormat = "https://api{0}.cronofy.com/v1/events";

        /// <summary>
        /// The URL format for the managed event endpoint.
        /// </summary>
        private const string ManagedEventUrlFormatFormat = "https://api{0}.cronofy.com/v1/calendars/{{0}}/events";

        /// <summary>
        /// The URL format for the participation status endpoint.
        /// </summary>
        private const string ParticipationStatusUrlFormatFormat = "https://api{0}.cronofy.com/v1/calendars/{{0}}/events/{{1}}/participation_status";

        /// <summary>
        /// The URL of the channels endpoint.
        /// </summary>
        private const string ChannelsUrlFormat = "https://api{0}.cronofy.com/v1/channels";

        /// <summary>
        /// The URL format for the channel endpoint.
        /// </summary>
        private const string ChannelUrlFormatFormat = "https://api{0}.cronofy.com/v1/channels/{{0}}";

        /// <summary>
        /// The URL format for the elevated permissions endpoint.
        /// </summary>
        private const string PermissionsUrlFormat = "https://api{0}.cronofy.com/v1/permissions";

        /// <summary>
        /// The URL of the availability endpoint.
        /// </summary>
        private const string AvailabilityUrlFormat = "https://api{0}.cronofy.com/v1/availability";

        /// <summary>
        /// The URL for the add to calendar endpoint.
        /// </summary>
        private const string AddToCalendarUrlFormat = "https://api{0}.cronofy.com/v1/add_to_calendar";

        /// <summary>
        /// The URL for the real time scheduling endpoint.
        /// </summary>
        private const string RealTimeSchedulingUrlFormat = "https://api{0}.cronofy.com/v1/real_time_scheduling";

        /// <summary>
        /// The URL for the real time scheduling endpoint.
        /// </summary>
        private const string RealTimeSchedulingByIdUrlFormat = "https://api{0}.cronofy.com/v1/real_time_scheduling/{{0}}";

        /// <summary>
        /// The URL format for the real time scheduling disable endpoint.
        /// </summary>
        private const string DisableRealTimeSchedulingUrlFormatFormat = "https://api{0}.cronofy.com/v1/real_time_scheduling/{{0}}/disable";

        /// <summary>
        /// The URL of the link tokens endpoint.
        /// </summary>
        private const string LinkTokensUrlFormat = "https://api{0}.cronofy.com/v1/link_tokens";

        /// <summary>
        /// The URL of the revoke profile authorization endpoint.
        /// </summary>
        private const string RevokeProfileAuthorizationUrlFormatFormat = "https://api{0}.cronofy.com/v1/profiles/{{0}}/revoke";

        /// <summary>
        /// The URL of the Smart Invite endpoint.
        /// </summary>
        private const string SmartInviteUrlFormat = "https://api{0}.cronofy.com/v1/smart_invites";

        /// <summary>
        /// The URL of the batch request endpoint.
        /// </summary>
        private const string BatchUrlFormat = "https://api{0}.cronofy.com/v1/batch";

        /// <summary>
        /// The URL of the application calendars endpoint.
        /// </summary>
        private const string ApplicationCalendarsUrlFormat = "https://api{0}.cronofy.com/v1/application_calendars";

        /// <summary>
        /// The URL of the real time sequencing endpoint.
        /// </summary>
        private const string RealTimeSequencingFormat = "https://api{0}.cronofy.com/v1/real_time_sequencing";

        /// <summary>
        /// The URL of the sequenced availability endpoint.
        /// </summary>
        private const string SequencedAvailablityFormat = "https://api{0}.cronofy.com/v1/sequenced_availability";

        /// <summary>
        /// The URL of the application verification submission endpoint.
        /// </summary>
        private const string ApplicationVerificationUrlFormat = "https://api{0}.cronofy.com/v1/application_verification";

        /// <summary>
        /// The URL of the application provisioning endpoint.
        /// </summary>
        private const string ProvisionApplicationUrlFormat = "https://api{0}.cronofy.com/v1/applications";

        /// <summary>
        /// The URL of the Element Tokens endpoint.
        /// </summary>
        private const string ElementTokensUrlFormat = "https://api{0}.cronofy.com/v1/element_tokens";

        /// <summary>
        /// The URL of the Conferencing Services Authorization endpoint.
        /// </summary>
        private const string ConferencingServiceAuthorizationUrlFormat = "https://api{0}.cronofy.com/v1/conferencing_service_authorizations";

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlProvider"/> class.
        /// </summary>
        /// <param name="dataCenter">
        /// The data center the <see cref="UrlProvider"/> is for.
        /// </param>
        internal UrlProvider(string dataCenter)
        {
            string suffix = string.Empty;

            if (dataCenter != string.Empty)
            {
                suffix = "-" + dataCenter;
            }

            this.AuthorizationUrl = string.Format(AuthorizationUrlFormat, suffix);
            this.EnterpriseConnectAuthorizationUrl = string.Format(EnterpriseConnectAuthorizationUrlFormat, suffix);
            this.TokenUrl = string.Format(TokenUrlFormat, suffix);
            this.TokenRevocationUrl = string.Format(TokenRevocationUrlFormat, suffix);
            this.UserInfoUrl = string.Format(UserInfoUrlFormat, suffix);
            this.ResourcesUrl = string.Format(ResourcesUrlFormat, suffix);
            this.AuthorizeWithServiceAccountUrl = string.Format(AuthorizeWithServiceAccountUrlFormat, suffix);
            this.AccountUrl = string.Format(AccountUrlFormat, suffix);
            this.ProfilesUrl = string.Format(ProfilesUrlFormat, suffix);
            this.CalendarsUrl = string.Format(CalendarsUrlFormat, suffix);
            this.FreeBusyUrl = string.Format(FreeBusyUrlFormat, suffix);
            this.EventsUrl = string.Format(EventsUrlFormat, suffix);
            this.ManagedEventUrlFormat = string.Format(ManagedEventUrlFormatFormat, suffix);
            this.ParticipationStatusUrlFormat = string.Format(ParticipationStatusUrlFormatFormat, suffix);
            this.ChannelsUrl = string.Format(ChannelsUrlFormat, suffix);
            this.ChannelUrlFormat = string.Format(ChannelUrlFormatFormat, suffix);
            this.PermissionsUrl = string.Format(PermissionsUrlFormat, suffix);
            this.AvailabilityUrl = string.Format(AvailabilityUrlFormat, suffix);
            this.AddToCalendarUrl = string.Format(AddToCalendarUrlFormat, suffix);
            this.RealTimeSchedulingUrl = string.Format(RealTimeSchedulingUrlFormat, suffix);
            this.RealTimeSchedulingByIdUrl = string.Format(RealTimeSchedulingByIdUrlFormat, suffix);
            this.DisableRealTimeSchedulingUrlFormat = string.Format(DisableRealTimeSchedulingUrlFormatFormat, suffix);
            this.LinkTokensUrl = string.Format(LinkTokensUrlFormat, suffix);
            this.SmartInviteUrl = string.Format(SmartInviteUrlFormat, suffix);
            this.RevokeProfileAuthorizationUrlFormat = string.Format(RevokeProfileAuthorizationUrlFormatFormat, suffix);
            this.BatchUrl = string.Format(BatchUrlFormat, suffix);
            this.ApplicationCalendarsUrl = string.Format(ApplicationCalendarsUrlFormat, suffix);
            this.RealTimeSequencingUrl = string.Format(RealTimeSequencingFormat, suffix);
            this.SequencedAvailabilityUrl = string.Format(SequencedAvailablityFormat, suffix);
            this.ApplicationVerificationUrl = string.Format(ApplicationVerificationUrlFormat, suffix);
            this.ProvisionApplicationUrl = string.Format(ProvisionApplicationUrlFormat, suffix);
            this.ElementTokensUrl = string.Format(ElementTokensUrlFormat, suffix);
            this.ConferencingServiceAuthorizationUrl = string.Format(ConferencingServiceAuthorizationUrlFormat, suffix);
            // transform url
        }

        // new public url getter

        /// <summary>
        /// Gets the authorization URL.
        /// </summary>
        /// <value>
        /// The authorization URL.
        /// </value>
        public string AuthorizationUrl
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the Enterprise Connect authorization URL.
        /// </summary>
        /// <value>
        /// The Enterprise Connect authorization URL.
        /// </value>
        public string EnterpriseConnectAuthorizationUrl
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the OAuth token URL.
        /// </summary>
        /// <value>
        /// The OAuth token URL.
        /// </value>
        public string TokenUrl
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the OAuth token revocation URL.
        /// </summary>
        /// <value>
        /// The OAuth token revocation URL.
        /// </value>
        public string TokenRevocationUrl
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the userinfo URL.
        /// </summary>
        /// <value>
        /// The userinfo URL.
        /// </value>
        public string UserInfoUrl
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the resources URL.
        /// </summary>
        /// <value>
        /// The resources URL.
        /// </value>
        public string ResourcesUrl
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the authorize with service account URL.
        /// </summary>
        /// <value>
        /// The authorize with service account URL.
        /// </value>
        public string AuthorizeWithServiceAccountUrl
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the account URL.
        /// </summary>
        /// <value>
        /// The account URL.
        /// </value>
        public string AccountUrl
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the profiles URL.
        /// </summary>
        /// <value>
        /// The profiles URL.
        /// </value>
        public string ProfilesUrl
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the calendars URL.
        /// </summary>
        /// <value>
        /// The calendars URL.
        /// </value>
        public string CalendarsUrl
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the free busy URL.
        /// </summary>
        /// <value>
        /// The free busy URL.
        /// </value>
        public string FreeBusyUrl
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the events URL.
        /// </summary>
        /// <value>
        /// The events URL.
        /// </value>
        public string EventsUrl
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the managed event URL format.
        /// </summary>
        /// <value>
        /// The managed event URL format.
        /// </value>
        public string ManagedEventUrlFormat
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the participation status URL format.
        /// </summary>
        /// <value>
        /// The participation status URL format.
        /// </value>
        public string ParticipationStatusUrlFormat
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the channels URL.
        /// </summary>
        /// <value>
        /// The channels URL.
        /// </value>
        public string ChannelsUrl
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the channel URL format.
        /// </summary>
        /// <value>
        /// The channel URL format.
        /// </value>
        public string ChannelUrlFormat
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the permissions URL.
        /// </summary>
        /// <value>
        /// The permissions URL.
        /// </value>
        public string PermissionsUrl
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the availability URL.
        /// </summary>
        /// <value>
        /// The availability URL.
        /// </value>
        public string AvailabilityUrl
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the add to calendar URL.
        /// </summary>
        /// <value>
        /// The add to calendar URL.
        /// </value>
        public string AddToCalendarUrl
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the real time scheduling URL.
        /// </summary>
        /// <value>
        /// The real time scheduling URL.
        /// </value>
        public string RealTimeSchedulingUrl
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the real time scheduling by ID URL.
        /// </summary>
        /// <value>
        /// The real time scheduling by ID URL.
        /// </value>
        public string RealTimeSchedulingByIdUrl
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the real time scheduling disable URL format.
        /// </summary>
        /// <value>
        /// The real time scheduling disable URL format.
        /// </value>
        public string DisableRealTimeSchedulingUrlFormat
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the link tokens URL.
        /// </summary>
        /// <value>
        /// The link tokens URL.
        /// </value>
        public string LinkTokensUrl
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the revoke profile authorization URL format.
        /// </summary>
        /// <value>
        /// The revoke profile authorization URL format.
        /// </value>
        public string RevokeProfileAuthorizationUrlFormat
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the Smart Invite URL.
        /// </summary>
        /// <value>The Smart Invite url.</value>
        public string SmartInviteUrl { get; private set; }

        /// <summary>
        /// Gets the batch request URL.
        /// </summary>
        /// <value>
        /// The batch request URL.
        /// </value>
        public string BatchUrl
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the application calendars URL.
        /// </summary>
        /// <value>The application calendars URL.</value>
        public string ApplicationCalendarsUrl { get; private set; }

        /// <summary>
        /// Gets the real time sequencing URL.
        /// </summary>
        /// <value>The real time sequencing URL.</value>
        public string RealTimeSequencingUrl { get; private set; }

        /// <summary>
        /// Gets the sequenced availability URL.
        /// </summary>
        /// <value>The sequenced availability URL.</value>
        public string SequencedAvailabilityUrl { get; private set; }

        /// <summary>
        /// Gets the application verification submission URL.
        /// </summary>
        /// <value>The application verification submission URL.</value>
        public string ApplicationVerificationUrl { get; private set; }

        /// <summary>
        /// Gets the application provisioning URL.
        /// </summary>
        /// <value>The application provisioning URL.</value>
        public string ProvisionApplicationUrl { get; private set; }

        /// <summary>
        /// Gets the element tokens URL.
        /// </summary>
        /// <value>The element tokens URL.</value>
        public string ElementTokensUrl { get; private set; }

        /// <summary>
        /// Gets the conferencing service authorization URL.
        /// </summary>
        /// <value>The conferencing service authorization URL.</value>
        public string ConferencingServiceAuthorizationUrl { get; private set; }
    }
}
