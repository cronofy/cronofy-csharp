using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Cronofy.Test.CronofyAccountClientTests
{
    [TestFixture]
    public sealed class GetCalendars
    {
        private const string accessToken = "zyxvut987654";

        private CronofyAccountClient client;
        private StubHttpClient http;

        [SetUp]
        public void SetUp()
        {
            this.client = new CronofyAccountClient(accessToken);
            this.http = new StubHttpClient();

            client.HttpClient = http;
        }

        [Test]
        public void CanGetCalendars()
        {
            http.Stub(
                HttpGet
                .Url("https://api.cronofy.com/v1/calendars")
                .RequestHeader("Authorization", "Bearer " + accessToken)
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
      ""calendar_deleted"": false
    },
    {
      ""provider_name"": ""google"",
      ""profile_id"": ""pro_n23kjnwrw2"",
      ""profile_name"": ""example@cronofy.com"",
      ""calendar_id"": ""cal_n23kjnwrw2_n1k323nkj23"",
      ""calendar_name"": ""Work"",
      ""calendar_readonly"": true,
      ""calendar_deleted"": true
    },
    {
      ""provider_name"": ""apple"",
      ""profile_id"": ""pro_n23kjnkopy"",
      ""profile_name"": ""example@cronofy.com"",
      ""calendar_id"": ""cal_n23kjnkopy_3nkj23wejk1"",
      ""calendar_name"": ""Bank Holidays"",
      ""calendar_readonly"": true,
      ""calendar_deleted"": false
    }
  ]
}")
        );

            var calendars = client.GetCalendars();

            CollectionAssert.AreEqual(
                new List<Calendar> {
                    new Calendar {
                        Profile = new Profile {
                            ProviderName = "google",
                            ProfileId = "pro_n23kjnwrw2",
                            Name = "example@cronofy.com",
                        },
                        CalendarId = "cal_n23kjnwrw2_jsdfjksn234",
                        Name = "Home",
                        ReadOnly = false,
                        Deleted = false,
                    },
                    new Calendar {
                        Profile = new Profile {
                            ProviderName = "google",
                            ProfileId = "pro_n23kjnwrw2",
                            Name = "example@cronofy.com",
                        },
                        CalendarId = "cal_n23kjnwrw2_n1k323nkj23",
                        Name = "Work",
                        ReadOnly = true,
                        Deleted = true,
                    },
                    new Calendar {
                        Profile = new Profile {
                            ProviderName = "apple",
                            ProfileId = "pro_n23kjnkopy",
                            Name = "example@cronofy.com",
                        },
                        CalendarId = "cal_n23kjnkopy_3nkj23wejk1",
                        Name = "Bank Holidays",
                        ReadOnly = true,
                        Deleted = false,
                    },
                },
                calendars.ToList());
        }
    }
}
