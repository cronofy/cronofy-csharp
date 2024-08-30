namespace Cronofy.Test.CronofyAccountClientTests
{
    using NUnit.Framework;

    internal sealed class DeleteEvent : Base
    {
        private const string CalendarId = "cal_123456_abcdef";

        [Test]
        public void CanDeleteEvent()
        {
            const string eventId = "qTtZdczOccgaPncGJaCiLg";

            this.Http.Stub(
                HttpDelete
                    .Url("https://api.cronofy.com/v1/calendars/" + CalendarId + "/events")
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBodyFormat(@"{{""event_id"":""{0}""}}", eventId)
                    .ResponseCode(202));

            this.Client.DeleteEvent(CalendarId, eventId);
        }

        [Test]
        public void CanDeleteExternalEvent()
        {
            const string eventUid = "external_event_id";

            this.Http.Stub(
                HttpDelete
                    .Url("https://api.cronofy.com/v1/calendars/" + CalendarId + "/events")
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBodyFormat(@"{{""event_uid"":""{0}""}}", eventUid)
                    .ResponseCode(202));

            this.Client.DeleteExternalEvent(CalendarId, eventUid);
        }

        [Test]
        public void DeleteEventThrowsCronofyResponseExceptionOnFailure()
        {
            const string eventId = "qTtZdczOccgaPncGJaCiLg";

            // Assume the calendar could not be found
            this.Http.Stub(
                HttpDelete
                    .Url("https://api.cronofy.com/v1/calendars/" + CalendarId + "/events")
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBodyFormat(@"{{""event_id"":""{0}""}}", eventId)
                    .ResponseCode(404));

            var exception = Assert.Throws<CronofyResponseException>(() => this.Client.DeleteEvent(CalendarId, eventId));

            Assert.AreEqual(exception.Message, "Not found");
            Assert.AreEqual(exception.Response.Code, 404);
        }

        [Test]
        public void DeleteExternalEventThrowsCronofyResponseExceptionOnFailure()
        {
            const string eventUid = "external_event_id";

            this.Http.Stub(
                HttpDelete
                    .Url("https://api.cronofy.com/v1/calendars/" + CalendarId + "/events")
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBodyFormat(@"{{""event_uid"":""{0}""}}", eventUid)
                    .ResponseCode(422)
                    .ResponseBody(
                        "{\"errors\":{\"event_uid\":[{\"key\":\"errors.must_be_external_event_uid\",\"description\":\"event uid must be a valid external event uid\"}]}"));

            var exception = Assert.Throws<CronofyResponseException>(() => this.Client.DeleteExternalEvent(CalendarId, eventUid));

            Assert.AreEqual(exception.Message, "Validation failed");
            Assert.AreEqual(exception.Response.Code, 422);
            Assert.AreEqual(
                exception.Response.Body,
                "{\"errors\":{\"event_uid\":[{\"key\":\"errors.must_be_external_event_uid\",\"description\":\"event uid must be a valid external event uid\"}]}");
        }
    }
}
