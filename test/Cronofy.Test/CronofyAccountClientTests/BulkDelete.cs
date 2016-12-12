using NUnit.Framework;

namespace Cronofy.Test.CronofyAccountClientTests
{
    internal sealed class BulkDelete : Base
    {
        [Test]
        public void CanDeleteAll()
        {
            Http.Stub(
                HttpDelete
                    .Url("https://api.cronofy.com/v1/events")
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBody("{\"delete_all\":true}")
                    .ResponseCode(202)
            );

            Client.DeleteAllEvents();
        }

        [Test]
        public void CanDeleteAllForCalendars()
        {
            const string calendar1 = "cal_1234_5678";
            const string calendar2 = "cal_8765_4321";
            
            Http.Stub(
                HttpDelete
                    .Url("https://api.cronofy.com/v1/events")
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBodyFormat("{{\"calendar_ids\":[\"{0}\",\"{1}\"]}}", calendar1, calendar2)
                    .ResponseCode(202)
            );

            Client.DeleteAllEventsForCalendars(calendar1, calendar2);
        }
    }
}
