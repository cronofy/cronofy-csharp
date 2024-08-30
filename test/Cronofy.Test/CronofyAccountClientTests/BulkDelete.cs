namespace Cronofy.Test.CronofyAccountClientTests
{
    using NUnit.Framework;

    internal sealed class BulkDelete : Base
    {
        [Test]
        public void CanDeleteAll()
        {
            this.Http.Stub(
                HttpDelete
                    .Url("https://api.cronofy.com/v1/events")
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBody("{\"delete_all\":true}")
                    .ResponseCode(202));

            this.Client.DeleteAllEvents();
        }

        [Test]
        public void CanDeleteAllForCalendars()
        {
            const string calendar1 = "cal_1234_5678";
            const string calendar2 = "cal_8765_4321";

            this.Http.Stub(
                HttpDelete
                    .Url("https://api.cronofy.com/v1/events")
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBodyFormat("{{\"calendar_ids\":[\"{0}\",\"{1}\"]}}", calendar1, calendar2)
                    .ResponseCode(202));

            this.Client.DeleteAllEventsForCalendars(calendar1, calendar2);
        }

        [Test]
        public void DeleteAllThrowsCronofyResponseExceptionOnFailure()
        {
            // Realistically this method should not result in validation errors as there are no user-provided inputs.
            // But we can simulate error case handling here anyway to ensure this method is handling validation failures.
            this.Http.Stub(
                HttpDelete
                    .Url("https://api.cronofy.com/v1/events")
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBody("{\"delete_all\":true}")
                    .ResponseCode(422)
                    .ResponseBody(
                        "{\"errors:\":{\"delete_all\":[{\"key\":\"errors.must_be_boolean\",\"description\":\"must be a boolean value, either true or false\"}}"));

            var exception = Assert.Throws<CronofyResponseException>(() => this.Client.DeleteAllEvents());

            Assert.AreEqual(exception.Message, "Validation failed");
            Assert.AreEqual(exception.Response.Code, 422);
            Assert.AreEqual(
                exception.Response.Body,
                "{\"errors:\":{\"delete_all\":[{\"key\":\"errors.must_be_boolean\",\"description\":\"must be a boolean value, either true or false\"}}");
        }

        [Test]
        public void DeleteAllForCalendarsThrowsCronofyResponseExceptionOnFailure()
        {
            const string calendar1 = "cal_1234_5678";
            const string calendar2 = "cal_8765_4321";

            this.Http.Stub(
                HttpDelete
                    .Url("https://api.cronofy.com/v1/events")
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBodyFormat("{{\"calendar_ids\":[\"{0}\",\"{1}\"]}}", calendar1, calendar2)
                    .ResponseCode(422)
                    .ResponseBody(
                        "{\"errors:\":{\"calendar_ids\":[{\"key\":\"errors.invalid_calendar_ids\",\"description\":\"One or more of the calendar IDs provided was invalid\"}}"));

            var exception = Assert.Throws<CronofyResponseException>(() => this.Client.DeleteAllEventsForCalendars(calendar1, calendar2));

            Assert.AreEqual(exception.Message, "Validation failed");
            Assert.AreEqual(exception.Response.Code, 422);
            Assert.AreEqual(
                exception.Response.Body,
                "{\"errors:\":{\"calendar_ids\":[{\"key\":\"errors.invalid_calendar_ids\",\"description\":\"One or more of the calendar IDs provided was invalid\"}}");
        }
    }
}
