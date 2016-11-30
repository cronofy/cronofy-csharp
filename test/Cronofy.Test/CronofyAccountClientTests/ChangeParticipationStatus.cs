using System;
using NUnit.Framework;

namespace Cronofy.Test.CronofyAccountClientTests
{
    internal sealed class ChangeParticipationStatus : Base
    {
        private const string calendarId = "cal_123456_abcdef";

        private const string eventId = "qTtZdczOccgaPncGJaCiLg";

        [TestCase(ParticipationStatus.Accepted, "accepted")]
        [TestCase(ParticipationStatus.Tentative, "tentative")]
        [TestCase(ParticipationStatus.Declined, "declined")]
        public void CanDeleteEvent(ParticipationStatus status, string expectedStatus)
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
