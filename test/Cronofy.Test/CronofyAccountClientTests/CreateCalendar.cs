using System;
using NUnit.Framework;

namespace Cronofy.Test.CronofyAccountClientTests
{
    internal sealed class CreateCalendar : Base
    {
        [Test]
        public void CanCreateCalendar()
        {
            const string ProfileId = "pro_n23kjnwrw2";
            const string CalendarName = "New Calendar";

            Http.Stub(
                HttpPost
                    .Url("https://api.cronofy.com/v1/calendars")
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBodyFormat("{{\"profile_id\":\"{0}\",\"name\":\"{1}\"}}", ProfileId, CalendarName)
                    .ResponseCode(200)
                .ResponseBodyFormat(@"{{
  ""calendar"": {{
    ""provider_name"": ""google"",
    ""profile_id"": ""{0}"",
    ""profile_name"": ""example@cronofy.com"",
    ""calendar_id"": ""cal_n23kjnwrw2_sakdnawerd3"",
    ""calendar_name"": ""{1}"",
    ""calendar_readonly"": false,
    ""calendar_deleted"": false
  }}
}}", ProfileId, CalendarName)
        );

            var calendar = Client.CreateCalendar(ProfileId, CalendarName);

            Assert.AreEqual(
                new Calendar {
                    Profile = new Calendar.ProfileSummary {
                        ProviderName = "google",
                        ProfileId = ProfileId,
                        Name = "example@cronofy.com",
                    },
                    CalendarId = "cal_n23kjnwrw2_sakdnawerd3",
                    Name = CalendarName,
                    ReadOnly = false,
                    Deleted = false,
                },
                calendar);
        }

        [Test]
        public void CanCreateCalendarWithColor()
        {
            const string ProfileId = "pro_n23kjnwrw2";
            const string CalendarName = "New Calendar";
            const string Color = "#49BED8";

            Http.Stub(
                HttpPost
                    .Url("https://api.cronofy.com/v1/calendars")
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBodyFormat("{{\"profile_id\":\"{0}\",\"name\":\"{1}\",\"color\":\"{2}\"}}", ProfileId, CalendarName, Color)
                    .ResponseCode(200)
                .ResponseBodyFormat(@"{{
  ""calendar"": {{
    ""provider_name"": ""google"",
    ""profile_id"": ""{0}"",
    ""profile_name"": ""example@cronofy.com"",
    ""calendar_id"": ""cal_n23kjnwrw2_sakdnawerd3"",
    ""calendar_name"": ""{1}"",
    ""calendar_readonly"": false,
    ""calendar_deleted"": false
  }}
}}", ProfileId, CalendarName)
        );

            var calendar = Client.CreateCalendar(ProfileId, CalendarName, Color);

            Assert.AreEqual(
                new Calendar {
                    Profile = new Calendar.ProfileSummary {
                        ProviderName = "google",
                        ProfileId = ProfileId,
                        Name = "example@cronofy.com",
                    },
                    CalendarId = "cal_n23kjnwrw2_sakdnawerd3",
                    Name = CalendarName,
                    ReadOnly = false,
                    Deleted = false,
                },
                calendar);
        }
    }
}
