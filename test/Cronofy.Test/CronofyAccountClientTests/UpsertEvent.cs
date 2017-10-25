using System;
using NUnit.Framework;

namespace Cronofy.Test.CronofyAccountClientTests
{
    internal sealed class UpsertEvent : Base
    {
        private const string calendarId = "cal_123456_abcdef";

        [Test]
        public void CanUpsertEvent()
        {
            const string eventId = "qTtZdczOccgaPncGJaCiLg";
            const string summary = "Board meeting";
            const string description = "Discuss plans for the next quarter";
            const string startTimeString = "2014-08-05 15:30:00Z";
            const string endTimeString = "2014-08-05 17:00:00Z";
            const string locationDescription = "Board room";
            const string transparency = Transparency.Opaque;

            Http.Stub(
                HttpPost
                .Url("https://api.cronofy.com/v1/calendars/" + calendarId + "/events")
                .RequestHeader("Authorization", "Bearer " + AccessToken)
                .RequestHeader("Content-Type", "application/json; charset=utf-8")
                .RequestBodyFormat(
                    "{{\"event_id\":\"{0}\"," +
                    "\"summary\":\"{1}\"," +
                    "\"description\":\"{2}\"," +
                    "\"start\":{{\"time\":\"{3}\",\"tzid\":\"Etc/UTC\"}}," +
                    "\"end\":{{\"time\":\"{4}\",\"tzid\":\"Etc/UTC\"}}," +
                    "\"location\":{{\"description\":\"{5}\"}}," +
                    "\"transparency\":\"{6}\"" +
                    "}}",
                    eventId,
                    summary,
                    description,
                    startTimeString,
                    endTimeString,
                    locationDescription,
                    transparency)
                .ResponseCode(202)
            );

            var builder = new UpsertEventRequestBuilder()
                .EventId(eventId)
                .Summary(summary)
                .Description(description)
                .Start(new DateTime(2014, 8, 5, 15, 30, 0, DateTimeKind.Utc))
                .End(new DateTime(2014, 8, 5, 17, 0, 0, DateTimeKind.Utc))
                .Location(locationDescription)
                .Transparency(transparency);

            Client.UpsertEvent(calendarId, builder);
        }

        [Test]
        public void CanUpsertExternalEvent()
        {
            const string eventUid = "external_event_id";
            const string summary = "Board meeting";
            const string description = "Discuss plans for the next quarter";
            const string startTimeString = "2014-08-05 15:30:00Z";
            const string endTimeString = "2014-08-05 17:00:00Z";
            const string locationDescription = "Board room";
            const string transparency = Transparency.Opaque;

            Http.Stub(
                HttpPost
                .Url("https://api.cronofy.com/v1/calendars/" + calendarId + "/events")
                .RequestHeader("Authorization", "Bearer " + AccessToken)
                .RequestHeader("Content-Type", "application/json; charset=utf-8")
                .RequestBodyFormat(
                    "{{\"event_uid\":\"{0}\"," +
                    "\"summary\":\"{1}\"," +
                    "\"description\":\"{2}\"," +
                    "\"start\":{{\"time\":\"{3}\",\"tzid\":\"Etc/UTC\"}}," +
                    "\"end\":{{\"time\":\"{4}\",\"tzid\":\"Etc/UTC\"}}," +
                    "\"location\":{{\"description\":\"{5}\"}}," +
                    "\"transparency\":\"{6}\"" +
                    "}}",
                    eventUid,
                    summary,
                    description,
                    startTimeString,
                    endTimeString,
                    locationDescription,
                    transparency)
                .ResponseCode(202)
            );

            var builder = new UpsertEventRequestBuilder()
                .EventUid(eventUid)
                .Summary(summary)
                .Description(description)
                .Start(new DateTime(2014, 8, 5, 15, 30, 0, DateTimeKind.Utc))
                .End(new DateTime(2014, 8, 5, 17, 0, 0, DateTimeKind.Utc))
                .Location(locationDescription)
                .Transparency(transparency);

            Client.UpsertEvent(calendarId, builder);
        }

        [Test]
        public void CanUpsertEventWithoutLocation()
        {
            const string eventId = "qTtZdczOccgaPncGJaCiLg";
            const string summary = "Board meeting";
            const string description = "Discuss plans for the next quarter";
            const string startTimeString = "2014-08-05 15:30:00Z";
            const string endTimeString = "2014-08-05 17:00:00Z";

            Http.Stub(
                HttpPost
                .Url("https://api.cronofy.com/v1/calendars/" + calendarId + "/events")
                .RequestHeader("Authorization", "Bearer " + AccessToken)
                .RequestHeader("Content-Type", "application/json; charset=utf-8")
                .RequestBodyFormat(
                    "{{\"event_id\":\"{0}\"," +
                    "\"summary\":\"{1}\"," +
                    "\"description\":\"{2}\"," +
                    "\"start\":{{\"time\":\"{3}\",\"tzid\":\"Etc/UTC\"}}," +
                    "\"end\":{{\"time\":\"{4}\",\"tzid\":\"Etc/UTC\"}}" +
                    "}}",
                    eventId,
                    summary,
                    description,
                    startTimeString,
                    endTimeString)
                .ResponseCode(202)
            );

            var builder = new UpsertEventRequestBuilder()
                .EventId(eventId)
                .Summary(summary)
                .Description(description)
                .Start(new DateTime(2014, 8, 5, 15, 30, 0, DateTimeKind.Utc))
                .End(new DateTime(2014, 8, 5, 17, 0, 0, DateTimeKind.Utc));

            Client.UpsertEvent(calendarId, builder);
        }

        [Test]
        public void CanUpsertWithGeoLocation()
        {
            const string eventId = "qTtZdczOccgaPncGJaCiLg";
            const string summary = "Board meeting";
            const string description = "Discuss plans for the next quarter";
            const string startTimeString = "2014-08-05 15:30:00Z";
            const string endTimeString = "2014-08-05 17:00:00Z";
            const string locationDescription = "Board room";
            const string locationLatitude = "1.2345";
            const string locationLongitude = "0.1234";

            Http.Stub(
                HttpPost
                .Url("https://api.cronofy.com/v1/calendars/" + calendarId + "/events")
                .RequestHeader("Authorization", "Bearer " + AccessToken)
                .RequestHeader("Content-Type", "application/json; charset=utf-8")
                .RequestBodyFormat(
                    "{{\"event_id\":\"{0}\"," +
                    "\"summary\":\"{1}\"," +
                    "\"description\":\"{2}\"," +
                    "\"start\":{{\"time\":\"{3}\",\"tzid\":\"Etc/UTC\"}}," +
                    "\"end\":{{\"time\":\"{4}\",\"tzid\":\"Etc/UTC\"}}," +
                    "\"location\":{{\"description\":\"{5}\",\"lat\":\"{6}\",\"long\":\"{7}\"}}" +
                    "}}",
                    eventId,
                    summary,
                    description,
                    startTimeString,
                    endTimeString,
                    locationDescription,
                    locationLatitude,
                    locationLongitude)
                .ResponseCode(202)
            );

            var builder = new UpsertEventRequestBuilder()
                .EventId(eventId)
                .Summary(summary)
                .Description(description)
                .Start(new DateTime(2014, 8, 5, 15, 30, 0, DateTimeKind.Utc))
                .End(new DateTime(2014, 8, 5, 17, 0, 0, DateTimeKind.Utc))
                .Location(locationDescription, locationLatitude, locationLongitude);

            Client.UpsertEvent(calendarId, builder);
        }

        [Test]
        public void CanUpsertEventWithTimeZoneId()
        {
            const string eventId = "qTtZdczOccgaPncGJaCiLg";
            const string summary = "Board meeting";
            const string description = "Discuss plans for the next quarter";
            const string startTimeString = "2014-08-05 15:30:00Z";
            const string endTimeString = "2014-08-05 17:00:00Z";
            const string timeZoneId = "Europe/London";

            Http.Stub(
                HttpPost
                .Url("https://api.cronofy.com/v1/calendars/" + calendarId + "/events")
                .RequestHeader("Authorization", "Bearer " + AccessToken)
                .RequestHeader("Content-Type", "application/json; charset=utf-8")
                .RequestBodyFormat(
                    "{{\"event_id\":\"{0}\"," +
                    "\"summary\":\"{1}\"," +
                    "\"description\":\"{2}\"," +
                    "\"start\":{{\"time\":\"{3}\",\"tzid\":\"{5}\"}}," +
                    "\"end\":{{\"time\":\"{4}\",\"tzid\":\"{5}\"}}," +
                    "\"tzid\":\"{5}\"" +
                    "}}",
                    eventId,
                    summary,
                    description,
                    startTimeString,
                    endTimeString,
                    timeZoneId)
                .ResponseCode(202)
            );

            var builder = new UpsertEventRequestBuilder()
                .EventId(eventId)
                .Summary(summary)
                .Description(description)
                .Start(new DateTime(2014, 8, 5, 15, 30, 0, DateTimeKind.Utc))
                .End(new DateTime(2014, 8, 5, 17, 0, 0, DateTimeKind.Utc))
                .TimeZoneId(timeZoneId);

            Client.UpsertEvent(calendarId, builder);
        }

        [Test]
        public void CanUpsertEventWithSeparateTimeZoneIds()
        {
            const string eventId = "qTtZdczOccgaPncGJaCiLg";
            const string summary = "Board meeting";
            const string description = "Discuss plans for the next quarter";
            const string startTimeString = "2014-08-05 15:30:00Z";
            const string endTimeString = "2014-08-05 17:00:00Z";
            const string startTimeZoneId = "Europe/London";
            const string endTimeZoneId = "America/Chicago";

            Http.Stub(
                HttpPost
                .Url("https://api.cronofy.com/v1/calendars/" + calendarId + "/events")
                .RequestHeader("Authorization", "Bearer " + AccessToken)
                .RequestHeader("Content-Type", "application/json; charset=utf-8")
                .RequestBodyFormat(
                    "{{\"event_id\":\"{0}\"," +
                    "\"summary\":\"{1}\"," +
                    "\"description\":\"{2}\"," +
                    "\"start\":{{\"time\":\"{3}\",\"tzid\":\"{4}\"}}," +
                    "\"end\":{{\"time\":\"{5}\",\"tzid\":\"{6}\"}}" +
                    "}}",
                    eventId,
                    summary,
                    description,
                    startTimeString,
                    startTimeZoneId,
                    endTimeString,
                    endTimeZoneId)
                .ResponseCode(202)
            );

            var builder = new UpsertEventRequestBuilder()
                .EventId(eventId)
                .Summary(summary)
                .Description(description)
                .Start(new DateTime(2014, 8, 5, 15, 30, 0, DateTimeKind.Utc))
                .StartTimeZoneId(startTimeZoneId)
                .End(new DateTime(2014, 8, 5, 17, 0, 0, DateTimeKind.Utc))
                .EndTimeZoneId(endTimeZoneId);

            Client.UpsertEvent(calendarId, builder);
        }

        [Test]
        public void CanUpsertAllDayEvent()
        {
            const string eventId = "qTtZdczOccgaPncGJaCiLg";
            const string summary = "Board meeting";
            const string description = "Discuss plans for the next quarter";
            const string startTimeString = "2014-08-05";
            const string endTimeString = "2014-08-06";

            Http.Stub(
                HttpPost
                .Url("https://api.cronofy.com/v1/calendars/" + calendarId + "/events")
                .RequestHeader("Authorization", "Bearer " + AccessToken)
                .RequestHeader("Content-Type", "application/json; charset=utf-8")
                .RequestBodyFormat(
                    "{{\"event_id\":\"{0}\"," +
                    "\"summary\":\"{1}\"," +
                    "\"description\":\"{2}\"," +
                    "\"start\":{{\"time\":\"{3}\",\"tzid\":\"Etc/UTC\"}}," +
                    "\"end\":{{\"time\":\"{4}\",\"tzid\":\"Etc/UTC\"}}" +
                    "}}",
                    eventId,
                    summary,
                    description,
                    startTimeString,
                    endTimeString)
                .ResponseCode(202)
            );

            var builder = new UpsertEventRequestBuilder()
                .EventId(eventId)
                .Summary(summary)
                .Description(description)
                .Start(new Date(2014, 8, 5))
                .End(new Date(2014, 8, 6));

            Client.UpsertEvent(calendarId, builder);
        }

        [Test]
        public void CanUpsertAttendees()
        {
            const string eventId = "qTtZdczOccgaPncGJaCiLg";
            const string summary = "Board meeting";
            const string description = "Discuss plans for the next quarter";
            const string startTimeString = "2014-08-05";
            const string endTimeString = "2014-08-06";

            Http.Stub(
                HttpPost
                .Url("https://api.cronofy.com/v1/calendars/" + calendarId + "/events")
                .RequestHeader("Authorization", "Bearer " + AccessToken)
                .RequestHeader("Content-Type", "application/json; charset=utf-8")
                .RequestBodyFormat(
                    "{{\"event_id\":\"{0}\"," +
                    "\"attendees\":{{\"invite\":[{{\"email\":\"test@attendee.com\",\"display_name\":\"Test attendee\"}}],\"remove\":[{{\"email\":\"remove@attendee.com\",\"display_name\":\"Test removal\"}}]}}," +
                    "\"summary\":\"{1}\"," +
                    "\"description\":\"{2}\"," +
                    "\"start\":{{\"time\":\"{3}\",\"tzid\":\"Etc/UTC\"}}," +
                    "\"end\":{{\"time\":\"{4}\",\"tzid\":\"Etc/UTC\"}}" +
                    "}}",
                    eventId,
                    summary,
                    description,
                    startTimeString,
                    endTimeString)
                .ResponseCode(202)
            );

            var builder = new UpsertEventRequestBuilder()
                .EventId(eventId)
                .Summary(summary)
                .Description(description)
                .Start(new Date(2014, 8, 5))
                .End(new Date(2014, 8, 6))
                .AddAttendee("test@attendee.com", "Test attendee")
                .RemoveAttendee("remove@attendee.com", "Test removal");

            Client.UpsertEvent(calendarId, builder);
        }

        [Test]
        public void CanUpsertReminders()
        {
            const string eventId = "qTtZdczOccgaPncGJaCiLg";
            const string summary = "Board meeting";
            const string description = "Discuss plans for the next quarter";
            const string startTimeString = "2014-08-05";
            const string endTimeString = "2014-08-06";

            var reminders = new[] { 10, 0, 30 };

            Http.Stub(
                HttpPost
                .Url("https://api.cronofy.com/v1/calendars/" + calendarId + "/events")
                .RequestHeader("Authorization", "Bearer " + AccessToken)
                .RequestHeader("Content-Type", "application/json; charset=utf-8")
                .RequestBodyFormat(
                    "{{\"event_id\":\"{0}\"," +
                    "\"summary\":\"{1}\"," +
                    "\"description\":\"{2}\"," +
                    "\"start\":{{\"time\":\"{3}\",\"tzid\":\"Etc/UTC\"}}," +
                    "\"end\":{{\"time\":\"{4}\",\"tzid\":\"Etc/UTC\"}}," +
                    "\"reminders\":[{{\"minutes\":10}},{{\"minutes\":0}},{{\"minutes\":30}}]" +
                    "}}",
                    eventId,
                    summary,
                    description,
                    startTimeString,
                    endTimeString)
                .ResponseCode(202)
            );

            var builder = new UpsertEventRequestBuilder()
                .EventId(eventId)
                .Summary(summary)
                .Description(description)
                .Start(new Date(2014, 8, 5))
                .End(new Date(2014, 8, 6))
                .Reminders(reminders);

            Client.UpsertEvent(calendarId, builder);
        }

        [Test]
        public void CanUpsertUrl()
        {
            const string eventId = "qTtZdczOccgaPncGJaCiLg";
            const string summary = "Board meeting";
            const string description = "Discuss plans for the next quarter";
            const string startTimeString = "2014-08-05";
            const string endTimeString = "2014-08-06";
            const string url = "http://example.com";

            Http.Stub(
                HttpPost
                .Url("https://api.cronofy.com/v1/calendars/" + calendarId + "/events")
                .RequestHeader("Authorization", "Bearer " + AccessToken)
                .RequestHeader("Content-Type", "application/json; charset=utf-8")
                .RequestBodyFormat(
                    "{{\"event_id\":\"{0}\"," +
                    "\"summary\":\"{1}\"," +
                    "\"description\":\"{2}\"," +
                    "\"start\":{{\"time\":\"{3}\",\"tzid\":\"Etc/UTC\"}}," +
                    "\"end\":{{\"time\":\"{4}\",\"tzid\":\"Etc/UTC\"}}," +
                    "\"url\":\"{5}\"" +
                    "}}",
                    eventId,
                    summary,
                    description,
                    startTimeString,
                    endTimeString,
                    url)
                .ResponseCode(202)
            );

            var builder = new UpsertEventRequestBuilder()
                .EventId(eventId)
                .Summary(summary)
                .Description(description)
                .Start(new Date(2014, 8, 5))
                .End(new Date(2014, 8, 6))
                .Url(url);

            Client.UpsertEvent(calendarId, builder);
        }

        [Test]
        public void CanUpsertExplicitNullUrl()
        {
            const string eventId = "qTtZdczOccgaPncGJaCiLg";
            const string summary = "Board meeting";
            const string description = "Discuss plans for the next quarter";
            const string startTimeString = "2014-08-05";
            const string endTimeString = "2014-08-06";

            Http.Stub(
                HttpPost
                .Url("https://api.cronofy.com/v1/calendars/" + calendarId + "/events")
                .RequestHeader("Authorization", "Bearer " + AccessToken)
                .RequestHeader("Content-Type", "application/json; charset=utf-8")
                .RequestBodyFormat(
                    "{{\"event_id\":\"{0}\"," +
                    "\"summary\":\"{1}\"," +
                    "\"description\":\"{2}\"," +
                    "\"start\":{{\"time\":\"{3}\",\"tzid\":\"Etc/UTC\"}}," +
                    "\"end\":{{\"time\":\"{4}\",\"tzid\":\"Etc/UTC\"}}," +
                    "\"url\":null" +
                    "}}",
                    eventId,
                    summary,
                    description,
                    startTimeString,
                    endTimeString)
                .ResponseCode(202)
            );

            var builder = new UpsertEventRequestBuilder()
                .EventId(eventId)
                .Summary(summary)
                .Description(description)
                .Start(new Date(2014, 8, 5))
                .End(new Date(2014, 8, 6))
                .Url(null);

            Client.UpsertEvent(calendarId, builder);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void CantUpsertWithInvalidTransparency()
        {
            const string eventId = "qTtZdczOccgaPncGJaCiLg";
            const string summary = "Board meeting";
            const string startTimeString = "2014-08-05 15:30:00Z";
            const string endTimeString = "2014-08-05 17:00:00Z";
            const string transparency = Transparency.Unknown;

            Http.Stub(
                HttpPost
                .Url("https://api.cronofy.com/v1/calendars/" + calendarId + "/events")
                .RequestHeader("Authorization", "Bearer " + AccessToken)
                .RequestHeader("Content-Type", "application/json; charset=utf-8")
                .RequestBodyFormat(
                    "{{\"event_id\":\"{0}\"," +
                    "\"summary\":\"{1}\"," +
                    "\"start\":{{\"time\":\"{2}\",\"tzid\":\"Etc/UTC\"}}," +
                    "\"end\":{{\"time\":\"{3}\",\"tzid\":\"Etc/UTC\"}}," +
                    "\"transparency\":\"{4}\"" +
                    "}}",
                    eventId,
                    summary,
                    startTimeString,
                    endTimeString,
                    transparency)
                .ResponseCode(202)
            );

            var builder = new UpsertEventRequestBuilder()
                .EventId(eventId)
                .Summary(summary)
                .Start(new DateTime(2014, 8, 5, 15, 30, 0, DateTimeKind.Utc))
                .End(new DateTime(2014, 8, 5, 17, 0, 0, DateTimeKind.Utc))
                .Transparency(transparency);

            Client.UpsertEvent(calendarId, builder);
        }

        [Test]
        public void CanUpsertColor()
        {
            const string eventId = "qTtZdczOccgaPncGJaCiLg";
            const string summary = "Board meeting";
            const string description = "Discuss plans for the next quarter";
            const string startTimeString = "2014-08-05";
            const string endTimeString = "2014-08-06";
            const string color = "#49BED8";

            Http.Stub(
                HttpPost
                .Url("https://api.cronofy.com/v1/calendars/" + calendarId + "/events")
                .RequestHeader("Authorization", "Bearer " + AccessToken)
                .RequestHeader("Content-Type", "application/json; charset=utf-8")
                .RequestBodyFormat(
                    "{{\"event_id\":\"{0}\"," +
                    "\"summary\":\"{1}\"," +
                    "\"description\":\"{2}\"," +
                    "\"start\":{{\"time\":\"{3}\",\"tzid\":\"Etc/UTC\"}}," +
					"\"end\":{{\"time\":\"{4}\",\"tzid\":\"Etc/UTC\"}}," +
                    "\"color\":\"{5}\"" +
                    "}}",
                    eventId,
                    summary,
                    description,
                    startTimeString,
                    endTimeString,
                    color)
                .ResponseCode(202)
            );

            var builder = new UpsertEventRequestBuilder()
                .EventId(eventId)
                .Summary(summary)
                .Description(description)
                .Start(new Date(2014, 8, 5))
                .End(new Date(2014, 8, 6))
                .Color(color);

            Client.UpsertEvent(calendarId, builder);
        }
    }
}
