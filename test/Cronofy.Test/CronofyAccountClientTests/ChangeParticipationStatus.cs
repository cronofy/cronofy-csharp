using System;
using NUnit.Framework;

namespace Cronofy.Test.CronofyAccountClientTests
{
    internal sealed class ChangeParticipationStatus : Base
    {
        private const string calendarId = "cal_123456_abcdef";

        private const string eventId = "qTtZdczOccgaPncGJaCiLg";

        [TestCase(PartipationStatus.Accepted, "accepted")]
        [TestCase(PartipationStatus.Tentative, "tentative")]
        [TestCase(PartipationStatus.Declined, "declined")]
        public void CanDeleteEvent(PartipationStatus status, string expectedStatus)
        {
            Http.Stub(
                HttpPost
                    .Url("https://api.cronofy.com/v1/calendars/" + calendarId + "/events/" + eventId + "/participation_status")
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBodyFormat(@"{{""status"":""{0}""}}", expectedStatus)
                    .ResponseCode(202)
            );

            Client.ChangeParticipationStatus(calendarId, eventId, status);
        }
    }
}
