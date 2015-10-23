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
    }
}
