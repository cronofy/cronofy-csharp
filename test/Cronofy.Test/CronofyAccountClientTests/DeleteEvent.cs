using System;
using NUnit.Framework;

namespace Cronofy.Test.CronofyAccountClientTests
{
    internal sealed class DeleteEvent : Base
    {
        private const string calendarId = "cal_123456_abcdef";

        [Test]
        public void CanDeleteEvent()
        {
            const string eventId = "qTtZdczOccgaPncGJaCiLg";

            Http.Stub(
                HttpDelete
                    .Url("https://api.cronofy.com/v1/calendars/" + calendarId + "/events")
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBodyFormat(@"{{""event_id"":""{0}""}}", eventId)
                    .ResponseCode(202)
            );

            Client.DeleteEvent(calendarId, eventId);
        }
    }
}
