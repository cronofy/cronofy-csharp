namespace Cronofy.Test.CronofyAccountClientTests
{
    using NUnit.Framework;

    internal sealed class ChangeParticipationStatus : Base
    {
        private const string CalendarId = "cal_123456_abcdef";

        private const string EventId = "qTtZdczOccgaPncGJaCiLg";

        [TestCase(ParticipationStatus.Accepted, "accepted")]
        [TestCase(ParticipationStatus.Tentative, "tentative")]
        [TestCase(ParticipationStatus.Declined, "declined")]
        public void CanChangeParticipationStatus(ParticipationStatus status, string expectedStatus)
        {
            this.Http.Stub(
                HttpPost
                    .Url("https://api.cronofy.com/v1/calendars/" + CalendarId + "/events/" + EventId + "/participation_status")
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBodyFormat(@"{{""status"":""{0}""}}", expectedStatus)
                    .ResponseCode(202));

            this.Client.ChangeParticipationStatus(CalendarId, EventId, status);
        }

        [Test]
        public void ChangeParticipationStatusThrowsCronofyResponseExceptionOnFailure()
        {
            this.Http.Stub(
                HttpPost
                    .Url("https://api.cronofy.com/v1/calendars/" + CalendarId + "/events/" + EventId + "/participation_status")
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBody("{\"status\":\"declined\"}")
                    .ResponseCode(404));

            var exception = Assert.Throws<CronofyResponseException>(() => this.Client.ChangeParticipationStatus(CalendarId, EventId, ParticipationStatus.Declined));

            Assert.AreEqual(exception.Message, "Not found");
            Assert.AreEqual(exception.Response.Code, 404);
        }
    }
}
