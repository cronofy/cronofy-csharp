using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Cronofy.Test.CronofyAccountClientTests
{
    internal sealed class GetCalendars : Base
    {
        [Test]
        public void CanGetCalendars()
        {
            Http.Stub(
                HttpGet
                .Url("https://api.cronofy.com/v1/calendars")
                .RequestHeader("Authorization", "Bearer " + AccessToken)
                .ResponseCode(200)
                .ResponseBody(
                    @"{
  ""calendars"": [
    {
      ""provider_name"": ""google"",
      ""profile_id"": ""pro_n23kjnwrw2"",
      ""profile_name"": ""example@cronofy.com"",
      ""calendar_id"": ""cal_n23kjnwrw2_jsdfjksn234"",
      ""calendar_name"": ""Home"",
      ""calendar_readonly"": false,
      ""calendar_deleted"": false,
      ""calendar_primary"": true
    },
    {
      ""provider_name"": ""google"",
      ""profile_id"": ""pro_n23kjnwrw2"",
      ""profile_name"": ""example@cronofy.com"",
      ""calendar_id"": ""cal_n23kjnwrw2_n1k323nkj23"",
      ""calendar_name"": ""Work"",
      ""calendar_readonly"": true,
      ""calendar_deleted"": true,
      ""calendar_primary"": false
    },
    {
      ""provider_name"": ""apple"",
      ""profile_id"": ""pro_n23kjnkopy"",
      ""profile_name"": ""example@cronofy.com"",
      ""calendar_id"": ""cal_n23kjnkopy_3nkj23wejk1"",
      ""calendar_name"": ""Bank Holidays"",
      ""calendar_readonly"": true,
      ""calendar_deleted"": false,
      ""calendar_primary"": false
    }
  ]
}")
        );

            var calendars = Client.GetCalendars();

            CollectionAssert.AreEqual(
                new List<Calendar> {
                    new Calendar {
                        Profile = new Calendar.ProfileSummary {
                            ProviderName = "google",
                            ProfileId = "pro_n23kjnwrw2",
                            Name = "example@cronofy.com",
                        },
                        CalendarId = "cal_n23kjnwrw2_jsdfjksn234",
                        Name = "Home",
                        ReadOnly = false,
                        Deleted = false,
                        Primary = true,
                    },
                    new Calendar {
                        Profile = new Calendar.ProfileSummary {
                            ProviderName = "google",
                            ProfileId = "pro_n23kjnwrw2",
                            Name = "example@cronofy.com",
                        },
                        CalendarId = "cal_n23kjnwrw2_n1k323nkj23",
                        Name = "Work",
                        ReadOnly = true,
                        Deleted = true,
                        Primary = false,
                    },
                    new Calendar {
                        Profile = new Calendar.ProfileSummary {
                            ProviderName = "apple",
                            ProfileId = "pro_n23kjnkopy",
                            Name = "example@cronofy.com",
                        },
                        CalendarId = "cal_n23kjnkopy_3nkj23wejk1",
                        Name = "Bank Holidays",
                        ReadOnly = true,
                        Deleted = false,
                        Primary = false,
                    },
                },
                calendars.ToList());
        }
    }
}
