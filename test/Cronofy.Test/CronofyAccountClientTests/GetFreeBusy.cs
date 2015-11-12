using NUnit.Framework;
using System.Collections.Generic;
using System;

namespace Cronofy.Test.CronofyAccountClientTests
{
    [TestFixture]
    public sealed class GetFreeBusy
    {
        private const string accessToken = "zyxvut987654";

        private CronofyAccountClient client;
        private StubHttpClient http;

        const string BasicResponseBody = @"{
  ""pages"": {
    ""current"": 1,
    ""total"": 1
  },
  ""free_busy"": [
    {
      ""calendar_id"": ""cal_U9uuErStTG@EAAAB_IsAsykA2DBTWqQTf-f0kJw"",
      ""start"": ""2014-09-06"",
      ""end"": ""2014-09-08"",
      ""free_busy_status"": ""busy""
    }
  ]
}";

        private static readonly List<FreeBusy> BasicResponseCollection = new List<FreeBusy> {
            new FreeBusy {
                CalendarId = "cal_U9uuErStTG@EAAAB_IsAsykA2DBTWqQTf-f0kJw",
                Start = new EventTime(new Date(2014, 9, 6), "Etc/UTC"),
                End = new EventTime(new Date(2014, 9, 8), "Etc/UTC"),
                FreeBusyStatus = FreeBusyStatus.Busy,
            }
        };

        [SetUp]
        public void SetUp()
        {
            this.client = new CronofyAccountClient(accessToken);
            this.http = new StubHttpClient();

            client.HttpClient = http;
        }

        [Test]
        public void CanGetFreeBusy()
        {
            http.Stub(
                HttpGet
                    .Url("https://api.cronofy.com/v1/free_busy?tzid=Etc%2FUTC&localized_times=true")
                    .RequestHeader("Authorization", "Bearer " + accessToken)
                    .ResponseCode(200)
                    .ResponseBody(BasicResponseBody)
            );

            var events = client.GetFreeBusy();

            CollectionAssert.AreEqual(BasicResponseCollection, events);
        }

        [Test]
        public void CanGetPagedFreeBusy()
        {
            http.Stub(
                HttpGet
                .Url("https://api.cronofy.com/v1/free_busy?tzid=Etc%2FUTC&localized_times=true")
                .RequestHeader("Authorization", "Bearer " + accessToken)
                .ResponseCode(200)
                .ResponseBody(
                    @"{
  ""pages"": {
    ""current"": 1,
    ""total"": 2,
    ""next_page"": ""https://api.cronofy.com/v1/free_busy/pages/08a07b034306679e""
  },
  ""free_busy"": [
    {
      ""calendar_id"": ""cal_U9uuErStTG@EAAAB_IsAsykA2DBTWqQTf-f0kJw"",
      ""start"": ""2014-09-06"",
      ""end"": ""2014-09-08"",
      ""free_busy_status"": ""busy""
    }
  ]
}")
        );

            http.Stub(
                HttpGet
                .Url("https://api.cronofy.com/v1/free_busy/pages/08a07b034306679e")
                .RequestHeader("Authorization", "Bearer " + accessToken)
                .ResponseCode(200)
                .ResponseBody(
                    @"{
  ""pages"": {
    ""current"": 2,
    ""total"": 2
  },
  ""free_busy"": [
    {
      ""calendar_id"": ""cal_U9uuErStTG@EAAAB_IsAsykA2DBTWqQTf-f0kJw"",
      ""start"": ""2014-12-06"",
      ""end"": ""2014-12-08"",
      ""free_busy_status"": ""tentative""
    }
  ]
}")
        );

            var events = client.GetFreeBusy();

            CollectionAssert.AreEqual(
                new List<FreeBusy> {
                    new FreeBusy {
                        CalendarId = "cal_U9uuErStTG@EAAAB_IsAsykA2DBTWqQTf-f0kJw",
                        Start = new EventTime(new Date(2014, 9, 6), "Etc/UTC"),
                        End = new EventTime(new Date(2014, 9, 8), "Etc/UTC"),
                        FreeBusyStatus = FreeBusyStatus.Busy,
                    },
                    new FreeBusy {
                        CalendarId = "cal_U9uuErStTG@EAAAB_IsAsykA2DBTWqQTf-f0kJw",
                        Start = new EventTime(new Date(2014, 12, 6), "Etc/UTC"),
                        End = new EventTime(new Date(2014, 12, 8), "Etc/UTC"),
                        FreeBusyStatus = FreeBusyStatus.Tentative,
                    },
                },
                events);
        }

        [Test]
        public void CanGetFreeBusyWithinDates()
        {
            AssertParameter(
                "from=2015-10-20&to=2015-10-30",
                b => b.From(2015, 10, 20).To(2015, 10, 30));
        }

        [Test]
        public void CanGetFreeBusyIncludingManagedEvents()
        {
            AssertParameter("include_managed=true", b => b.IncludeManaged(true));
        }

        [Test]
        public void CanGetFreeBusyWithinMultipleCalendars()
        {
            var calendarIds = new List<string>
            {
                "cal_U9uuErStTG@EAAAB_IsAsykA2DBTWqQTf-f0kJw",
                "cal_U@y23bStTFV7AAAB_iWTeH8WOCDOIW@us5gRzww",
            };

            var expectedKeyValue =
                Encode("calendar_ids[]", calendarIds[0]) + "&" +
                Encode("calendar_ids[]", calendarIds[1]);

            AssertParameter(
                expectedKeyValue,
                b => b.CalendarIds(calendarIds));

            AssertParameter(
                expectedKeyValue,
                b => b.CalendarIds(calendarIds.ToArray()));
        }

        private void AssertParameter(string keyValue, Action<GetFreeBusyRequestBuilder> builderAction)
        {
            http.Stub(
                HttpGet
                .Url("https://api.cronofy.com/v1/free_busy?tzid=Etc%2FUTC&localized_times=true&" + keyValue)
                .RequestHeader("Authorization", "Bearer " + accessToken)
                .ResponseCode(200)
                .ResponseBody(BasicResponseBody)
            );

            var builder = new GetFreeBusyRequestBuilder();

            builderAction.Invoke(builder);

            var events = client.GetFreeBusy(builder);

            CollectionAssert.AreEqual(BasicResponseCollection, events);
        }

        private static string Encode(string value)
        {
            return UrlBuilder.EncodeParameter(value);
        }

        private static string Encode(string key, string value)
        {
            return Encode(key) + "=" + Encode(value);
        }
    }
}
