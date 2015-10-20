using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Cronofy.Test.CronofyClientTests
{
	[TestFixture]
	public sealed class GetEvents
	{
		private const string clientId = "abcdef123456";
		private const string clientSecret = "s3cr3t1v3";
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
		public void CanGetEvents()
		{
			http.Stub(
				HttpGet
				.Url("https://api.cronofy.com/v1/events?tzid=Etc%2FUTC")
				.RequestHeader("Authorization", "Bearer " + accessToken)
				.ResponseCode(200)
				.ResponseBody(
					@"{
  ""pages"": {
    ""current"": 1,
    ""total"": 2,
    ""next_page"": ""https://api.cronofy.com/v1/events/pages/08a07b034306679e""
  },
  ""events"": [
    {
      ""calendar_id"": ""cal_U9uuErStTG@EAAAB_IsAsykA2DBTWqQTf-f0kJw"",
      ""event_uid"": ""evt_external_54008b1a4a41730f8d5c6037"",
      ""summary"": ""Company Retreat"",
      ""description"": ""Escape to the country"",
      ""start"": ""2014-09-06"",
      ""end"": ""2014-09-08"",
      ""deleted"": false,
      ""location"": {
        ""description"": ""Beach""
      },
      ""participation_status"": ""needs_action"",
      ""transparency"": ""opaque"",
      ""event_status"": ""confirmed"",
      ""categories"": [],
      ""attendees"": [
        {
          ""email"": ""example@cronofy.com"",
          ""display_name"": ""Example Person"",
          ""status"": ""needs_action""
        }
      ],
      ""created"": ""2014-09-01T08:00:01Z"",
      ""updated"": ""2014-09-01T09:24:16Z""
    }
  ]
}")
			);

			var events = client.GetEvents();

			CollectionAssert.AreEqual(
				new List<Event> {
					new Event {
						CalendarId = "cal_U9uuErStTG@EAAAB_IsAsykA2DBTWqQTf-f0kJw",
						EventUid = "evt_external_54008b1a4a41730f8d5c6037",
						Summary = "Company Retreat",
						Description = "Escape to the country",
						Start = new EventTime(new Date(2014, 9, 6), "Etc/UTC"),
						End = new EventTime(new Date(2014, 9, 8), "Etc/UTC"),
						Location = new Location {
							Description = "Beach",
						},
						Deleted = false,
						ParticipationStatus = Participation.NeedsAction,
						Transparency = Transparency.Opaque,
						EventStatus = EventStatus.Confirmed,
						Created = new DateTime(2014, 9, 1, 8, 0, 1, DateTimeKind.Utc),
						Updated = new DateTime(2014, 9, 1, 9, 24, 16, DateTimeKind.Utc),
					} 
				},
				events);
		}

		[Test]
		public void CanGetEventWithoutLocation()
		{
			http.Stub(
				HttpGet
				.Url("https://api.cronofy.com/v1/events?tzid=Etc%2FUTC&localized_times=true")
				.RequestHeader("Authorization", "Bearer " + accessToken)
				.ResponseCode(200)
				.ResponseBody(
					@"{
  ""pages"": {
    ""current"": 1,
    ""total"": 2,
    ""next_page"": ""https://api.cronofy.com/v1/events/pages/08a07b034306679e""
  },
  ""events"": [
    {
      ""calendar_id"": ""cal_U9uuErStTG@EAAAB_IsAsykA2DBTWqQTf-f0kJw"",
      ""event_uid"": ""evt_external_54008b1a4a41730f8d5c6037"",
      ""summary"": ""Company Retreat"",
      ""description"": ""Escape to the country"",
      ""start"": ""2014-09-06"",
      ""end"": ""2014-09-08"",
      ""deleted"": false,
      ""participation_status"": ""needs_action"",
      ""transparency"": ""opaque"",
      ""event_status"": ""confirmed"",
      ""categories"": [],
      ""attendees"": [
        {
          ""email"": ""example@cronofy.com"",
          ""display_name"": ""Example Person"",
          ""status"": ""needs_action""
        }
      ],
      ""created"": ""2014-09-01T08:00:01Z"",
      ""updated"": ""2014-09-01T09:24:16Z""
    }
  ]
}")
			);

			var events = client.GetEvents();

			CollectionAssert.AreEqual(
				new List<Event> {
					new Event {
						CalendarId = "cal_U9uuErStTG@EAAAB_IsAsykA2DBTWqQTf-f0kJw",
						EventUid = "evt_external_54008b1a4a41730f8d5c6037",
						Summary = "Company Retreat",
						Description = "Escape to the country",
						Start = new EventTime(new Date(2014, 9, 6), "Etc/UTC"),
						End = new EventTime(new Date(2014, 9, 8), "Etc/UTC"),
						Location = null,
						Deleted = false,
						ParticipationStatus = Participation.NeedsAction,
						Transparency = Transparency.Opaque,
						EventStatus = EventStatus.Confirmed,
						Created = new DateTime(2014, 9, 1, 8, 0, 1, DateTimeKind.Utc),
						Updated = new DateTime(2014, 9, 1, 9, 24, 16, DateTimeKind.Utc),
					}
				},
				events);
		}
	}
}
