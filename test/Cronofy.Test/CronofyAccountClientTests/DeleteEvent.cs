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
    }
}
