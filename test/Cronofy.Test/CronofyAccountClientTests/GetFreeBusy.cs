using NUnit.Framework;
using System.Collections.Generic;

namespace Cronofy.Test.CronofyAccountClientTests
{
    [TestFixture]
    public sealed class GetFreeBusy
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
        public void CanGetFreeBusy()
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
                    } 
                },
                events);
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
            http.Stub(
                HttpGet
                .Url("https://api.cronofy.com/v1/free_busy?tzid=Etc%2FUTC&localized_times=true&from=2015-10-20&to=2015-10-30")
                .RequestHeader("Authorization", "Bearer " + accessToken)
                .ResponseCode(200)
                .ResponseBody(
                    @"{
  ""pages"": {
    ""current"": 1,
    ""total"": 1
  },
  ""free_busy"": [
    {
      ""calendar_id"": ""cal_U9uuErStTG@EAAAB_IsAsykA2DBTWqQTf-f0kJw"",
      ""start"": ""2014-09-06"",
      ""end"": ""2014-09-08"",
      ""deleted"": false,
      ""free_busy_status"": ""busy""
    }
  ]
}")
        );

            var builder = new GetFreeBusyRequestBuilder()
                .From(2015, 10, 20)
                .To(2015, 10, 30);

            var events = client.GetFreeBusy(builder);

            CollectionAssert.AreEqual(
                new List<FreeBusy> {
                    new FreeBusy {
                        CalendarId = "cal_U9uuErStTG@EAAAB_IsAsykA2DBTWqQTf-f0kJw",
                        Start = new EventTime(new Date(2014, 9, 6), "Etc/UTC"),
                        End = new EventTime(new Date(2014, 9, 8), "Etc/UTC"),
                        FreeBusyStatus = FreeBusyStatus.Busy,
                    },
                },
                events);
        }
    }
}
