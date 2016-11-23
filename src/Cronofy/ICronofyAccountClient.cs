namespace Cronofy
{
    using System;
    using System.Collections.Generic;
    using Cronofy.Requests;

    /// <summary>
    /// Interface for a Cronofy client that interacts with an account's
    /// calendars and events.
    /// </summary>
    public interface ICronofyAccountClient
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
        /// <remarks>
        /// TODO Validation exceptions.
        /// </remarks>
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
        /// <remarks>
        /// TODO Validation exceptions.
        /// </remarks>
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
        /// <remarks>
        /// TODO Validation exceptions.
        /// </remarks>
        void DeleteEvent(string calendarId, string eventId);

        /// <summary>
        /// Deletes an external event from the account's calendars.
        /// </summary>
        /// <param name="calendarId">
        /// The ID of the calendar the event should be deleted to, must not be
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
        /// <remarks>
        /// TODO Validation exceptions.
        /// </remarks>
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
        /// <remarks>
        /// TODO Validation exceptions.
        /// </remarks>
        void ChangeParticipationStatus(string calendarId, string eventUid, PartipationStatus status);

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
        /// <remarks>
        /// TODO Validation exceptions.
        /// </remarks>
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
        /// <remarks>
        /// TODO Validation exceptions.
        /// </remarks>
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
        /// <remarks>
        /// TODO Validation exceptions.
        /// </remarks>
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
        ElevatedPermissionsResponse ElevatedPermissions(ElevatedPermissionsBuilder builder);

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
    }
}
