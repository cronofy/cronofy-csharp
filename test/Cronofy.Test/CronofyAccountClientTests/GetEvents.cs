using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.NetworkInformation;

namespace Cronofy.Test.CronofyAccountClientTests
{
    [TestFixture]
    public sealed class GetEvents
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
        public void CanGetEvents()
        {
            http.Stub(
                HttpGet
                .Url("https://api.cronofy.com/v1/events?tzid=Etc%2FUTC&localized_times=true")
                .RequestHeader("Authorization", "Bearer " + accessToken)
                .ResponseCode(200)
                .ResponseBody(SingleEventResponseBody)
            );

            var events = client.GetEvents();

            CollectionAssert.AreEqual(SingleEventResultCollection, events);
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
    ""total"": 1
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
                        ParticipationStatus = AttendeeStatus.NeedsAction,
                        Transparency = Transparency.Opaque,
                        EventStatus = EventStatus.Confirmed,
                        Categories = new string[] {},
                        Created = new DateTime(2014, 9, 1, 8, 0, 1, DateTimeKind.Utc),
                        Updated = new DateTime(2014, 9, 1, 9, 24, 16, DateTimeKind.Utc),
                        Attendees = new[] {
                            new Attendee {
                                Email = "example@cronofy.com",
                                DisplayName = "Example Person",
                                Status = AttendeeStatus.NeedsAction,
                            }
                        },
                    }
                },
                events);
        }

        [Test]
        public void CanGetPagedEvents()
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

            http.Stub(
                HttpGet
                .Url("https://api.cronofy.com/v1/events/pages/08a07b034306679e")
                .RequestHeader("Authorization", "Bearer " + accessToken)
                .ResponseCode(200)
                .ResponseBody(
                    @"{
  ""pages"": {
    ""current"": 2,
    ""total"": 2
  },
  ""events"": [
    {
      ""calendar_id"": ""cal_U9uuErStTG@EAAAB_IsAsykA2DBTWqQTf-f0kJw"",
      ""event_uid"": ""evt_external_54008b1a4a41730f8d5c6040"",
      ""summary"": ""Company Retreat"",
      ""description"": ""Escape to the country"",
      ""start"": ""2014-12-06"",
      ""end"": ""2014-12-08"",
      ""deleted"": false,
      ""participation_status"": ""needs_action"",
      ""transparency"": ""opaque"",
      ""event_status"": ""confirmed"",
      ""categories"": [],
      ""attendees"": [
        {
          ""email"": ""example+other@cronofy.com"",
          ""display_name"": ""Other Person"",
          ""status"": ""needs_action""
        }
      ],
      ""created"": ""2014-09-01T09:00:01Z"",
      ""updated"": ""2014-09-01T10:24:16Z""
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
                        ParticipationStatus = AttendeeStatus.NeedsAction,
                        Transparency = Transparency.Opaque,
                        EventStatus = EventStatus.Confirmed,
                        Categories = new string[] {},
                        Created = new DateTime(2014, 9, 1, 8, 0, 1, DateTimeKind.Utc),
                        Updated = new DateTime(2014, 9, 1, 9, 24, 16, DateTimeKind.Utc),
                        Attendees = new[] {
                            new Attendee {
                                Email = "example@cronofy.com",
                                DisplayName = "Example Person",
                                Status = AttendeeStatus.NeedsAction,
                            }
                        },
                    },
                    new Event {
                        CalendarId = "cal_U9uuErStTG@EAAAB_IsAsykA2DBTWqQTf-f0kJw",
                        EventUid = "evt_external_54008b1a4a41730f8d5c6040",
                        Summary = "Company Retreat",
                        Description = "Escape to the country",
                        Start = new EventTime(new Date(2014, 12, 6), "Etc/UTC"),
                        End = new EventTime(new Date(2014, 12, 8), "Etc/UTC"),
                        Location = null,
                        Deleted = false,
                        ParticipationStatus = AttendeeStatus.NeedsAction,
                        Transparency = Transparency.Opaque,
                        EventStatus = EventStatus.Confirmed,
                        Categories = new string[] {},
                        Created = new DateTime(2014, 9, 1, 9, 0, 1, DateTimeKind.Utc),
                        Updated = new DateTime(2014, 9, 1, 10, 24, 16, DateTimeKind.Utc),
                        Attendees = new[] {
                            new Attendee {
                                Email = "example+other@cronofy.com",
                                DisplayName = "Other Person",
                                Status = AttendeeStatus.NeedsAction,
                            }
                        },
                    },
                },
                events);
        }

        [Test]
        public void CanGetEventsWithinDates()
        {
            AssertParameter(
                "from=2015-10-20&to=2015-10-30",
                b => b.From(2015, 10, 20).To(2015, 10, 30));
        }

        [Test]
        public void CanGetEventsSinceLastModified()
        {
            var lastModified = DateTime.UtcNow.AddMinutes(-15);

            AssertParameter(
                "last_modified=" + UrlBuilder.EncodeParameter(lastModified.ToString("u")),
                b => b.LastModified(lastModified));
        }

        [Test]
        public void CanGetEventsThatHaveBeenDeleted()
        {
            AssertParameter("include_deleted=true", b => b.IncludeDeleted(true));
        }

        [Test]
        public void CanGetEventsThatHaveBeenMoved()
        {
            AssertParameter("include_moved=true", b => b.IncludeMoved(true));
        }

        [Test]
        public void CanGetEventsThatAreManaged()
        {
            AssertParameter("include_managed=true", b => b.IncludeManaged(true));
        }

        [Test]
        public void CanGetOnlyEventsThatAreManaged()
        {
            AssertParameter("only_managed=true", b => b.OnlyManaged(true));
        }

        private void AssertParameter(string keyValue, Action<GetEventsRequestBuilder> builderAction)
        {
            http.Stub(
                HttpGet
                .Url("https://api.cronofy.com/v1/events?tzid=Etc%2FUTC&localized_times=true&" + keyValue)
                .RequestHeader("Authorization", "Bearer " + accessToken)
                .ResponseCode(200)
                .ResponseBody(SingleEventResponseBody)
            );

            var builder = new GetEventsRequestBuilder();

            builderAction.Invoke(builder);

            var events = client.GetEvents(builder);

            CollectionAssert.AreEqual(SingleEventResultCollection, events);
        }

        const string SingleEventResponseBody = @"{
  ""pages"": {
    ""current"": 1,
    ""total"": 1
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
}";

        private static readonly List<Event> SingleEventResultCollection = new List<Event> {
            new Event {
                CalendarId = "cal_U9uuErStTG@EAAAB_IsAsykA2DBTWqQTf-f0kJw",
                EventUid = "evt_external_54008b1a4a41730f8d5c6037",
                Summary = "Company Retreat",
                Description = "Escape to the country",
                Start = new EventTime(new Date(2014, 9, 6), "Etc/UTC"),
                End = new EventTime(new Date(2014, 9, 8), "Etc/UTC"),
                Location = null,
                Deleted = false,
                ParticipationStatus = AttendeeStatus.NeedsAction,
                Transparency = Transparency.Opaque,
                EventStatus = EventStatus.Confirmed,
                Categories = new string[] {

                },
                Created = new DateTime(2014, 9, 1, 8, 0, 1, DateTimeKind.Utc),
                Updated = new DateTime(2014, 9, 1, 9, 24, 16, DateTimeKind.Utc),
                Attendees = new[] {
                    new Attendee {
                        Email = "example@cronofy.com",
                        DisplayName = "Example Person",
                        Status = AttendeeStatus.NeedsAction,
                    }
                },
            }
        };
    }
}
