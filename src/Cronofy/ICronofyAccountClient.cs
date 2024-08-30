namespace Cronofy
{
    using System;
    using System.Collections.Generic;
    using Cronofy.Requests;
    using Cronofy.Responses;

    /// <summary>
    /// Interface for a Cronofy client that interacts with an account's
    /// calendars and events.
    /// </summary>
    public interface ICronofyAccountClient : ICronofyUserInfoClient
    {
        /// <summary>
        /// Gets the details of the account.
        /// </summary>
        /// <returns>
        /// The <see cref="Account"/>.
        /// </returns>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        Account GetAccount();

        /// <summary>
        /// Gets the profiles belonging to the account.
        /// </summary>
        /// <returns>
        /// The account's <see cref="Profile"/>s.
        /// </returns>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        IEnumerable<Profile> GetProfiles();

        /// <summary>
        /// Gets the calendars belonging to the account.
        /// </summary>
        /// <returns>
        /// The account's <see cref="Calendar"/>s.
        /// </returns>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        IEnumerable<Calendar> GetCalendars();

        /// <summary>
        /// Creates a new calendar for the account.
        /// </summary>
        /// <param name="profileId">
        /// The ID of the profile to create the calendar within, must not be
        /// empty.
        /// </param>
        /// <param name="name">
        /// The name to give the new calendar, must not be empty.
        /// </param>
        /// <returns>
        /// The new <see cref="Calendar"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="profileId"/> or <paramref name="name"/>
        /// are empty.
        /// </exception>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        Calendar CreateCalendar(string profileId, string name);

        /// <summary>
        /// Creates a new calendar for the account.
        /// </summary>
        /// <param name="profileId">
        /// The ID of the profile to create the calendar within, must not be
        /// empty.
        /// </param>
        /// <param name="name">
        /// The name to give the new calendar, must not be empty.
        /// </param>
        /// <param name="color">
        /// The color to give the new calendar, must not be empty.
        /// </param>
        /// <returns>
        /// The new <see cref="Calendar"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="profileId"/> or <paramref name="name"/>
        /// are empty.
        /// </exception>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        Calendar CreateCalendar(string profileId, string name, string color);

        /// <summary>
        /// Gets the events belonging to the account.
        /// </summary>
        /// <returns>
        /// The account's <see cref="Event"/>s.
        /// </returns>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        IEnumerable<Event> GetEvents();

        /// <summary>
        /// Gets the events belonging to the account.
        /// </summary>
        /// <param name="builder">
        /// The builder from which to get the parameters for the request, must
        /// not be null.
        /// </param>
        /// <returns>
        /// The account's <see cref="Event"/>s.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="builder"/> is null.
        /// </exception>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        IEnumerable<Event> GetEvents(IBuilder<GetEventsRequest> builder);

        /// <summary>
        /// Gets the events belonging to the account.
        /// </summary>
        /// <param name="request">
        /// The parameters for the request, must not be null.
        /// </param>
        /// <returns>
        /// The account's <see cref="Event"/>s.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="request"/> is null.
        /// </exception>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        IEnumerable<Event> GetEvents(GetEventsRequest request);

        /// <summary>
        /// Gets the free busy information belonging to the account.
        /// </summary>
        /// <returns>
        /// The account's <see cref="FreeBusy"/>s.
        /// </returns>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        IEnumerable<FreeBusy> GetFreeBusy();

        /// <summary>
        /// Gets the free busy information belonging to the account.
        /// </summary>
        /// <param name="builder">
        /// The builder from which to get the parameters for the request, must
        /// not be null.
        /// </param>
        /// <returns>
        /// The account's <see cref="FreeBusy"/>s.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="builder"/> is null.
        /// </exception>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        IEnumerable<FreeBusy> GetFreeBusy(IBuilder<GetFreeBusyRequest> builder);

        /// <summary>
        /// Gets the free busy information belonging to the account.
        /// </summary>
        /// <param name="request">
        /// The parameters for the request, must not be null.
        /// </param>
        /// <returns>
        /// The account's <see cref="FreeBusy"/>s.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="request"/> is null.
        /// </exception>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        IEnumerable<FreeBusy> GetFreeBusy(GetFreeBusyRequest request);

        /// <summary>
        /// Performs a batch request for the account.
        /// </summary>
        /// <param name="batchBuilder">
        /// The builder from which to create a
        /// <see cref="Requests.BatchRequest"/>, must not be null.
        /// </param>
        /// <returns>
        /// The result of a the batch request.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="batchBuilder"/> is null.
        /// </exception>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        BatchResponse BatchRequest(IBuilder<BatchRequest> batchBuilder);

        /// <summary>
        /// Performs a batch request for the account.
        /// </summary>
        /// <param name="batchRequest">
        /// The details of the request to make, must not be null.
        /// </param>
        /// <returns>
        /// The result of a the batch request.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="batchRequest"/> is null.
        /// </exception>
        /// <exception cref="BatchWithErrorsException">
        /// Thrown if any of the batch entries receives an error in response.
        /// </exception>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        BatchResponse BatchRequest(BatchRequest batchRequest);

        /// <summary>
        /// Upserts an event to the account's calendar.
        /// </summary>
        /// <param name="calendarId">
        /// The ID of the calendar the event should be upserted to, must not be
        /// empty.
        /// </param>
        /// <param name="eventBuilder">
        /// The builder from which to create a <see cref="UpsertEventRequest"/>,
        /// must not be null.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="calendarId"/> is empty,
        /// <paramref name="eventBuilder"/> is null, or if
        /// <paramref name="eventBuilder"/> is not in a state which creates a
        /// valid <see cref="UpsertEventRequest"/>.
        /// </exception>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        void UpsertEvent(string calendarId, IBuilder<UpsertEventRequest> eventBuilder);

        /// <summary>
        /// Upserts an event to the account's calendar.
        /// </summary>
        /// <param name="calendarId">
        /// The ID of the calendar the event should be upserted to, must not be
        /// empty.
        /// </param>
        /// <param name="eventRequest">
        /// The details of the event to create, must not be null.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="calendarId"/> is empty, or
        /// <paramref name="eventRequest"/> is null.
        /// </exception>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        void UpsertEvent(string calendarId, UpsertEventRequest eventRequest);

        /// <summary>
        /// Deletes an event from the account's calendars.
        /// </summary>
        /// <param name="calendarId">
        /// The ID of the calendar the event should be upserted to, must not be
        /// empty.
        /// </param>
        /// <param name="eventId">
        /// The ID of the event to delete, must not be empty.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="calendarId"/> or
        /// <paramref name="eventId"/> are empty.
        /// </exception>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        void DeleteEvent(string calendarId, string eventId);

        /// <summary>
        /// Deletes all events you are managing from the account's calendars.
        /// </summary>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        void DeleteAllEvents();

        /// <summary>
        /// Deletes all events you are managing from the specified calendars.
        /// </summary>
        /// <param name="calendarIds">
        /// The IDs for the calendars to delete events from.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="calendarIds"/> is empty.
        /// </exception>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        void DeleteAllEventsForCalendars(params string[] calendarIds);

        /// <summary>
        /// Deletes an external event from the account's calendars.
        /// </summary>
        /// <param name="calendarId">
        /// The ID of the calendar the event should be deleted from, must not be
        /// empty.
        /// </param>
        /// <param name="eventUid">
        /// The ID of the external event to delete, must not be empty.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="calendarId"/> or
        /// <paramref name="eventUid"/> are empty.
        /// </exception>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        void DeleteExternalEvent(string calendarId, string eventUid);

        /// <summary>
        /// Changes the participation status of an event.
        /// </summary>
        /// <param name="calendarId">
        /// The ID of the calendar the event should be upserted to, must not be
        /// empty.
        /// </param>
        /// <param name="eventUid">
        /// The ID of the external event to delete, must not be empty.
        /// </param>
        /// <param name="status">
        /// The status to change the event to.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="calendarId"/> or
        /// <paramref name="eventUid"/> are empty.
        /// </exception>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        void ChangeParticipationStatus(string calendarId, string eventUid, ParticipationStatus status);

        /// <summary>
        /// Creates a notification channel.
        /// </summary>
        /// <param name="callbackUrl">
        /// The callback URL for the channel, must not be empty.
        /// </param>
        /// <returns>
        /// The created channel.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="callbackUrl"/> is empty.
        /// </exception>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        Channel CreateChannel(string callbackUrl);

        /// <summary>
        /// Creates a notification channel.
        /// </summary>
        /// <param name="channelBuilder">
        /// The builder from which to create a
        /// <see cref="CreateChannelRequest"/>, must not be null.
        /// </param>
        /// <returns>
        /// The created channel.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="channelBuilder"/> is null.
        /// </exception>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        Channel CreateChannel(IBuilder<CreateChannelRequest> channelBuilder);

        /// <summary>
        /// Creates a notification channel.
        /// </summary>
        /// <param name="channelRequest">
        /// The details of the channel to create, must not be null.
        /// </param>
        /// <returns>
        /// The created channel.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="channelRequest"/> is null.
        /// </exception>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        Channel CreateChannel(CreateChannelRequest channelRequest);

        /// <summary>
        /// Gets the active notification channels for the account.
        /// </summary>
        /// <returns>
        /// The account's <see cref="Channel"/>s.
        /// </returns>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        IEnumerable<Channel> GetChannels();

        /// <summary>
        /// Close the notification channel.
        /// </summary>
        /// <param name="channelId">
        /// The ID of the notification channel to close, must not be empty.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="channelId"/> is empty.
        /// </exception>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        void CloseChannel(string channelId);

        /// <summary>
        /// Generates a request for Elevated permissions for an account.
        /// </summary>
        /// <param name="builder">
        /// The builder from which to get the parameters for the request, must
        /// not be null.
        /// </param>
        /// <returns>
        /// The account's <see cref="Cronofy.ElevatedPermissionsResponse"/>s.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="builder"/> is null.
        /// </exception>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        ElevatedPermissionsResponse ElevatedPermissions(IBuilder<ElevatedPermissionsRequest> builder);

        /// <summary>
        /// Generates a request for Elevated permissions for an account.
        /// </summary>
        /// <param name="request">
        /// The parameters for the request, must not be null.
        /// </param>
        /// <returns>
        /// The account's <see cref="Cronofy.ElevatedPermissionsResponse"/>s.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="request"/> is null.
        /// </exception>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        ElevatedPermissionsResponse ElevatedPermissions(ElevatedPermissionsRequest request);

        /// <summary>
        /// Gets the availability for a given set of criteria.
        /// </summary>
        /// <param name="builder">
        /// A builder for the criteria for the availability request, must not be <c>null</c>.
        /// </param>
        /// <returns>
        /// The <see cref="AvailablePeriod"/>s which match the set of criteria.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="builder"/> is null.
        /// </exception>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        IEnumerable<AvailablePeriod> GetAvailability(IBuilder<AvailabilityRequest> builder);

        /// <summary>
        /// Gets the availability for a given set of criteria.
        /// </summary>
        /// <param name="request">
        /// The criteria for the availability request, must not be <c>null</c>.
        /// </param>
        /// <returns>
        /// The <see cref="AvailablePeriod"/>s which match the set of criteria.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="request"/> is null.
        /// </exception>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        IEnumerable<AvailablePeriod> GetAvailability(AvailabilityRequest request);

        /// <summary>
        /// Gets the availability for a given set of criteria.
        /// </summary>
        /// <param name="builder">
        /// A builder for the criteria for the availability request, must not be <c>null</c>.
        /// </param>
        /// <returns>
        /// The <see cref="AvailableSequences"/> which match the set of criteria.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="builder"/> is null.
        /// </exception>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        AvailableSequences GetSequencedAvailability(IBuilder<SequencedAvailabilityRequest> builder);

        /// <summary>
        /// Gets the availability for a given set of criteria.
        /// </summary>
        /// <param name="request">
        /// The criteria for the availability request, must not be <c>null</c>.
        /// </param>
        /// <returns>
        /// The <see cref="AvailableSequences"/> which match the set of criteria.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="request"/> is null.
        /// </exception>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        AvailableSequences GetSequencedAvailability(SequencedAvailabilityRequest request);

        /// <summary>
        /// Creates a link token for the current account.
        /// </summary>
        /// <returns>
        /// A link token for the current account.
        /// </returns>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        string CreateLinkToken();

        /// <summary>
        /// Revokes the authorization for the specified profile.
        /// </summary>
        /// <param name="profileId">
        /// The ID of the profile to revoke access to, must not be empty.
        /// </param>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        void RevokeProfileAuthorization(string profileId);

        /// <summary>
        /// Creates a URL which the end-user can use to link a conferencing service to their Cronofy Account.
        /// </summary>
        /// <param name="conferencingServiceAuthorizationRequest">
        /// The details of the authorization request.
        /// </param>
        /// <returns>The URL which the end-user should visit.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="conferencingServiceAuthorizationRequest"/> if null, or it doesn't contain a Redirect URI.
        /// </exception>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        string GetConferencingServiceAuthorizationUrl(ConferencingServiceAuthorizationRequest conferencingServiceAuthorizationRequest);
    }
}
