using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Reflection.Emit;

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
                    "\"location\":{{\"description\":\"{5}\"}}" +
                    "}}",
                    eventId,
                    summary,
                    description,
                    startTimeString,
                    endTimeString,
                    locationDescription)
                .ResponseCode(202)
            );

            var builder = new UpsertEventRequestBuilder()
                .EventId(eventId)
                .Summary(summary)
                .Description(description)
                .Start(new DateTime(2014, 8, 5, 15, 30, 0, DateTimeKind.Utc))
                .End(new DateTime(2014, 8, 5, 17, 0, 0, DateTimeKind.Utc))
                .Location(locationDescription);

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
                    "\"end\":{{\"time\":\"{4}\",\"tzid\":\"{5}\"}}" +
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
    }
}
