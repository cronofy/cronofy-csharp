namespace Cronofy.Test.CronofyAccountClientTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;

    internal sealed class GetEvents : Base
    {
        [Test]
        public void CanGetEvents()
        {
            this.Http.Stub(
                HttpGet
                .Url("https://api.cronofy.com/v1/events?tzid=Etc%2FUTC&localized_times=true")
                .RequestHeader("Authorization", "Bearer " + AccessToken)
                .ResponseCode(200)
                .ResponseBody(SingleEventResponseBody));

            var events = this.Client.GetEvents();

            CollectionAssert.AreEqual(SingleEventResultCollection, events);
        }

        [Test]
        public void CanGetEventWithConferencing()
        {
            this.Http.Stub(
                HttpGet
                .Url("https://api.cronofy.com/v1/events?tzid=Etc%2FUTC&localized_times=true")
                .RequestHeader("Authorization", "Bearer " + AccessToken)
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
      ""recurring"": true,
      ""conferencing"": {
        ""provider_name"": ""zoom"",
        ""join_url"": ""https://example.com/joinurl""
      },
      ""series_identifier"": ""identifier"",
      ""participation_status"": ""needs_action"",
      ""transparency"": ""opaque"",
      ""status"": ""confirmed"",
      ""categories"": [],
      ""created"": ""2014-09-01T08:00:01Z"",
      ""updated"": ""2014-09-01T09:24:16Z""
    }
  ]
}"));

            var events = this.Client.GetEvents();

            CollectionAssert.AreEqual(
                new List<Event>
                {
                    new Event
                    {
                        CalendarId = "cal_U9uuErStTG@EAAAB_IsAsykA2DBTWqQTf-f0kJw",
                        EventUid = "evt_external_54008b1a4a41730f8d5c6037",
                        Summary = "Company Retreat",
                        Description = "Escape to the country",
                        Start = new EventTime(new Date(2014, 9, 6), "Etc/UTC"),
                        End = new EventTime(new Date(2014, 9, 8), "Etc/UTC"),
                        Location = null,
                        Deleted = false,
                        Recurring = true,
                        SeriesIdentifier = "identifier",
                        ParticipationStatus = AttendeeStatus.NeedsAction,
                        Transparency = Transparency.Opaque,
                        EventStatus = EventStatus.Confirmed,
                        Categories = new string[] { },
                        Created = new DateTime(2014, 9, 1, 8, 0, 1, DateTimeKind.Utc),
                        Updated = new DateTime(2014, 9, 1, 9, 24, 16, DateTimeKind.Utc),
                        ConferencingInfo = new Conferencing
                        {
                          ProviderName = "zoom",
                          JoinUrl = "https://example.com/joinurl",
                        },
                    },
                },
                events);
        }

        [Test]
        public void CanGetEventWithoutLocation()
        {
            this.Http.Stub(
                HttpGet
                .Url("https://api.cronofy.com/v1/events?tzid=Etc%2FUTC&localized_times=true")
                .RequestHeader("Authorization", "Bearer " + AccessToken)
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
      ""recurring"": true,
      ""series_identifier"": ""identifier"",
      ""participation_status"": ""needs_action"",
      ""transparency"": ""opaque"",
      ""status"": ""confirmed"",
      ""categories"": [],
      ""attendees"": [
        {
          ""email"": ""example@cronofy.com"",
          ""display_name"": ""Example Person"",
          ""status"": ""needs_action""
        }
      ],
      ""created"": ""2014-09-01T08:00:01Z"",
      ""updated"": ""2014-09-01T09:24:16Z"",
      ""options"": {
        ""delete"": true,
        ""update"": true,
        ""change_participation_status"": true
      }
    }
  ]
}"));

            var events = this.Client.GetEvents();

            CollectionAssert.AreEqual(
                new List<Event>
                {
                    new Event
                    {
                        CalendarId = "cal_U9uuErStTG@EAAAB_IsAsykA2DBTWqQTf-f0kJw",
                        EventUid = "evt_external_54008b1a4a41730f8d5c6037",
                        Summary = "Company Retreat",
                        Description = "Escape to the country",
                        Start = new EventTime(new Date(2014, 9, 6), "Etc/UTC"),
                        End = new EventTime(new Date(2014, 9, 8), "Etc/UTC"),
                        Location = null,
                        Deleted = false,
                        Recurring = true,
                        SeriesIdentifier = "identifier",
                        ParticipationStatus = AttendeeStatus.NeedsAction,
                        Transparency = Transparency.Opaque,
                        EventStatus = EventStatus.Confirmed,
                        Categories = new string[] { },
                        Created = new DateTime(2014, 9, 1, 8, 0, 1, DateTimeKind.Utc),
                        Updated = new DateTime(2014, 9, 1, 9, 24, 16, DateTimeKind.Utc),
                        Attendees = new[]
                        {
                            new Attendee
                            {
                                Email = "example@cronofy.com",
                                DisplayName = "Example Person",
                                Status = AttendeeStatus.NeedsAction,
                            },
                        },
                        Options = new EventOptions()
                        {
                            Delete = true,
                            Update = true,
                            ChangeParticipationStatus = true,
                        },
                    },
                },
                events);
        }

        [Test]
        public void CanGetRecurringEventWithGeoLocation()
        {
            this.Http.Stub(
                HttpGet
                .Url("https://api.cronofy.com/v1/events?tzid=Etc%2FUTC&localized_times=true")
                .RequestHeader("Authorization", "Bearer " + AccessToken)
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
      ""recurring"": true,
      ""series_identifier"": ""identifier"",
      ""participation_status"": ""needs_action"",
      ""transparency"": ""opaque"",
      ""status"": ""confirmed"",
      ""categories"": [],
      ""location"": {
        ""description"": ""The Country"",
        ""lat"": ""1.2345"",
        ""long"": ""0.1234""
      },
      ""attendees"": [
        {
          ""email"": ""example@cronofy.com"",
          ""display_name"": ""Example Person"",
          ""status"": ""needs_action""
        }
      ],
      ""created"": ""2014-09-01T08:00:01Z"",
      ""updated"": ""2014-09-01T09:24:16Z"",
      ""options"": {
        ""delete"": true,
        ""update"": true,
        ""change_participation_status"": true
      }
    }
  ]
}"));

            var events = this.Client.GetEvents();

            CollectionAssert.AreEqual(
                new List<Event>
                {
                    new Event
                    {
                        CalendarId = "cal_U9uuErStTG@EAAAB_IsAsykA2DBTWqQTf-f0kJw",
                        EventUid = "evt_external_54008b1a4a41730f8d5c6037",
                        Summary = "Company Retreat",
                        Description = "Escape to the country",
                        Start = new EventTime(new Date(2014, 9, 6), "Etc/UTC"),
                        End = new EventTime(new Date(2014, 9, 8), "Etc/UTC"),
                        Location = new Location("The Country", "1.2345", "0.1234"),
                        Deleted = false,
                        Recurring = true,
                        SeriesIdentifier = "identifier",
                        ParticipationStatus = AttendeeStatus.NeedsAction,
                        Transparency = Transparency.Opaque,
                        EventStatus = EventStatus.Confirmed,
                        Categories = new string[] { },
                        Created = new DateTime(2014, 9, 1, 8, 0, 1, DateTimeKind.Utc),
                        Updated = new DateTime(2014, 9, 1, 9, 24, 16, DateTimeKind.Utc),
                        Attendees = new[]
                        {
                            new Attendee
                            {
                                Email = "example@cronofy.com",
                                DisplayName = "Example Person",
                                Status = AttendeeStatus.NeedsAction,
                            },
                        },
                        Options = new EventOptions()
                        {
                            Delete = true,
                            Update = true,
                            ChangeParticipationStatus = true,
                        },
                    },
                },
                events);
        }

        [Test]
        public void CanGetNonRecurringEventWithGeoLocation()
        {
            this.Http.Stub(
                HttpGet
                .Url("https://api.cronofy.com/v1/events?tzid=Etc%2FUTC&localized_times=true")
                .RequestHeader("Authorization", "Bearer " + AccessToken)
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
      ""recurring"": false,
      ""participation_status"": ""needs_action"",
      ""transparency"": ""opaque"",
      ""status"": ""confirmed"",
      ""categories"": [],
      ""location"": {
        ""description"": ""The Country"",
        ""lat"": ""1.2345"",
        ""long"": ""0.1234""
      },
      ""attendees"": [
        {
          ""email"": ""example@cronofy.com"",
          ""display_name"": ""Example Person"",
          ""status"": ""needs_action""
        }
      ],
      ""created"": ""2014-09-01T08:00:01Z"",
      ""updated"": ""2014-09-01T09:24:16Z"",
      ""options"": {
        ""delete"": true,
        ""update"": true,
        ""change_participation_status"": true
      }
    }
  ]
}"));

            var events = this.Client.GetEvents();

            CollectionAssert.AreEqual(
                new List<Event>
                {
                    new Event
                    {
                        CalendarId = "cal_U9uuErStTG@EAAAB_IsAsykA2DBTWqQTf-f0kJw",
                        EventUid = "evt_external_54008b1a4a41730f8d5c6037",
                        Summary = "Company Retreat",
                        Description = "Escape to the country",
                        Start = new EventTime(new Date(2014, 9, 6), "Etc/UTC"),
                        End = new EventTime(new Date(2014, 9, 8), "Etc/UTC"),
                        Location = new Location("The Country", "1.2345", "0.1234"),
                        Deleted = false,
                        Recurring = false,
                        ParticipationStatus = AttendeeStatus.NeedsAction,
                        Transparency = Transparency.Opaque,
                        EventStatus = EventStatus.Confirmed,
                        Categories = new string[] { },
                        Created = new DateTime(2014, 9, 1, 8, 0, 1, DateTimeKind.Utc),
                        Updated = new DateTime(2014, 9, 1, 9, 24, 16, DateTimeKind.Utc),
                        Attendees = new[]
                        {
                            new Attendee
                            {
                                Email = "example@cronofy.com",
                                DisplayName = "Example Person",
                                Status = AttendeeStatus.NeedsAction,
                            },
                        },
                        Options = new EventOptions()
                        {
                            Delete = true,
                            Update = true,
                            ChangeParticipationStatus = true,
                        },
                    },
                },
                events);
        }

        [Test]
        public void CanGetTransparentEvent()
        {
            this.Http.Stub(
                HttpGet
                .Url("https://api.cronofy.com/v1/events?tzid=Etc%2FUTC&localized_times=true")
                .RequestHeader("Authorization", "Bearer " + AccessToken)
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
      ""recurring"": true,
      ""series_identifier"": ""identifier"",
      ""participation_status"": ""needs_action"",
      ""transparency"": ""transparent"",
      ""status"": ""confirmed"",
      ""categories"": [],
      ""attendees"": [
        {
          ""email"": ""example@cronofy.com"",
          ""display_name"": ""Example Person"",
          ""status"": ""needs_action""
        }
      ],
      ""created"": ""2014-09-01T08:00:01Z"",
      ""updated"": ""2014-09-01T09:24:16Z"",
      ""options"": {
        ""delete"": true,
        ""update"": true,
        ""change_participation_status"": true
      }
    }
  ]
}"));

            var events = this.Client.GetEvents();

            CollectionAssert.AreEqual(
                new List<Event>
                {
                    new Event
                    {
                        CalendarId = "cal_U9uuErStTG@EAAAB_IsAsykA2DBTWqQTf-f0kJw",
                        EventUid = "evt_external_54008b1a4a41730f8d5c6037",
                        Summary = "Company Retreat",
                        Description = "Escape to the country",
                        Start = new EventTime(new Date(2014, 9, 6), "Etc/UTC"),
                        End = new EventTime(new Date(2014, 9, 8), "Etc/UTC"),
                        Location = null,
                        Deleted = false,
                        Recurring = true,
                        SeriesIdentifier = "identifier",
                        ParticipationStatus = AttendeeStatus.NeedsAction,
                        Transparency = Transparency.Transparent,
                        EventStatus = EventStatus.Confirmed,
                        Categories = new string[] { },
                        Created = new DateTime(2014, 9, 1, 8, 0, 1, DateTimeKind.Utc),
                        Updated = new DateTime(2014, 9, 1, 9, 24, 16, DateTimeKind.Utc),
                        Attendees = new[]
                        {
                            new Attendee
                            {
                                Email = "example@cronofy.com",
                                DisplayName = "Example Person",
                                Status = AttendeeStatus.NeedsAction,
                            },
                        },
                        Options = new EventOptions()
                        {
                            Delete = true,
                            Update = true,
                            ChangeParticipationStatus = true,
                        },
                    },
                },
                events);
        }

        [Test]
        public void CanGetEventWithMeetingUrl()
        {
            this.Http.Stub(
                HttpGet
                .Url("https://api.cronofy.com/v1/events?tzid=Etc%2FUTC&localized_times=true")
                .RequestHeader("Authorization", "Bearer " + AccessToken)
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
      ""recurring"": true,
      ""series_identifier"": ""identifier"",
      ""participation_status"": ""needs_action"",
      ""transparency"": ""transparent"",
      ""status"": ""confirmed"",
      ""categories"": [],
      ""attendees"": [
        {
          ""email"": ""example@cronofy.com"",
          ""display_name"": ""Example Person"",
          ""status"": ""needs_action""
        }
      ],
      ""created"": ""2014-09-01T08:00:01Z"",
      ""updated"": ""2014-09-01T09:24:16Z"",
      ""options"": {
        ""delete"": true,
        ""update"": true,
        ""change_participation_status"": true
      },
      ""meeting_url"": ""https://meet.example.com/ABCD1234""
    }
  ]
}"));

            var events = this.Client.GetEvents();

            CollectionAssert.AreEqual(
                new List<Event>
                {
                    new Event
                    {
                        CalendarId = "cal_U9uuErStTG@EAAAB_IsAsykA2DBTWqQTf-f0kJw",
                        EventUid = "evt_external_54008b1a4a41730f8d5c6037",
                        Summary = "Company Retreat",
                        Description = "Escape to the country",
                        Start = new EventTime(new Date(2014, 9, 6), "Etc/UTC"),
                        End = new EventTime(new Date(2014, 9, 8), "Etc/UTC"),
                        Location = null,
                        Deleted = false,
                        Recurring = true,
                        SeriesIdentifier = "identifier",
                        ParticipationStatus = AttendeeStatus.NeedsAction,
                        Transparency = Transparency.Transparent,
                        EventStatus = EventStatus.Confirmed,
                        Categories = new string[] { },
                        Created = new DateTime(2014, 9, 1, 8, 0, 1, DateTimeKind.Utc),
                        Updated = new DateTime(2014, 9, 1, 9, 24, 16, DateTimeKind.Utc),
                        Attendees = new[]
                        {
                            new Attendee
                            {
                                Email = "example@cronofy.com",
                                DisplayName = "Example Person",
                                Status = AttendeeStatus.NeedsAction,
                            },
                        },
                        Options = new EventOptions()
                        {
                            Delete = true,
                            Update = true,
                            ChangeParticipationStatus = true,
                        },
                        MeetingUrl = "https://meet.example.com/ABCD1234",
                    },
                },
                events);
        }

        [Test]
        public void CanGetEventWithOldAuditTimes()
        {
            this.Http.Stub(
                HttpGet
                .Url("https://api.cronofy.com/v1/events?tzid=Etc%2FUTC&localized_times=true")
                .RequestHeader("Authorization", "Bearer " + AccessToken)
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
      ""status"": ""confirmed"",
      ""categories"": [],
      ""attendees"": [
        {
          ""email"": ""example@cronofy.com"",
          ""display_name"": ""Example Person"",
          ""status"": ""needs_action""
        }
      ],
      ""created"": ""0000-12-29T00:00:00Z"",
      ""updated"": ""0000-12-29T00:00:00Z""
    }
  ]
}"));

            var events = this.Client.GetEvents();

            CollectionAssert.AreEqual(
              new List<Event>
              {
                new Event
                {
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
                    Categories = new string[] { },
                    Created = DateTime.MinValue.ToUniversalTime(),
                    Updated = DateTime.MinValue.ToUniversalTime(),
                    Attendees = new[]
                    {
                        new Attendee
                        {
                            Email = "example@cronofy.com",
                            DisplayName = "Example Person",
                            Status = AttendeeStatus.NeedsAction,
                        },
                    },
                },
              },
              events);
        }

        [Test]
        public void CanGetGoogleEventIds()
        {
            this.Http.Stub(
                HttpGet
                .Url("https://api.cronofy.com/v1/events?tzid=Etc%2FUTC&localized_times=true&google_event_ids=true")
                .RequestHeader("Authorization", "Bearer " + AccessToken)
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
      ""google_event_id"": ""goog.12345"",
      ""summary"": ""Company Retreat"",
      ""description"": ""Escape to the country"",
      ""start"": ""2014-09-06"",
      ""end"": ""2014-09-08"",
      ""deleted"": false,
      ""recurring"": true,
      ""series_identifier"": ""identifier"",
      ""participation_status"": ""needs_action"",
      ""transparency"": ""transparent"",
      ""status"": ""confirmed"",
      ""categories"": [],
      ""attendees"": [
        {
          ""email"": ""example@cronofy.com"",
          ""display_name"": ""Example Person"",
          ""status"": ""needs_action""
        }
      ],
      ""created"": ""2014-09-01T08:00:01Z"",
      ""updated"": ""2014-09-01T09:24:16Z"",
      ""options"": {
        ""delete"": true,
        ""update"": true,
        ""change_participation_status"": true
      }
    }
  ]
}"));

            var builder = new GetEventsRequestBuilder();

            builder.GoogleEventIds(true);

            var events = this.Client.GetEvents(builder);

            Assert.AreEqual("goog.12345", events.First().GoogleEventId);

            CollectionAssert.AreEqual(
                new List<Event>
                {
                    new Event
                    {
                        CalendarId = "cal_U9uuErStTG@EAAAB_IsAsykA2DBTWqQTf-f0kJw",
                        EventUid = "evt_external_54008b1a4a41730f8d5c6037",
                        GoogleEventId = "goog.12345",
                        Summary = "Company Retreat",
                        Description = "Escape to the country",
                        Start = new EventTime(new Date(2014, 9, 6), "Etc/UTC"),
                        End = new EventTime(new Date(2014, 9, 8), "Etc/UTC"),
                        Location = null,
                        Deleted = false,
                        Recurring = true,
                        SeriesIdentifier = "identifier",
                        ParticipationStatus = AttendeeStatus.NeedsAction,
                        Transparency = Transparency.Transparent,
                        EventStatus = EventStatus.Confirmed,
                        Categories = new string[] { },
                        Created = new DateTime(2014, 9, 1, 8, 0, 1, DateTimeKind.Utc),
                        Updated = new DateTime(2014, 9, 1, 9, 24, 16, DateTimeKind.Utc),
                        Attendees = new[]
                        {
                            new Attendee
                            {
                                Email = "example@cronofy.com",
                                DisplayName = "Example Person",
                                Status = AttendeeStatus.NeedsAction,
                            },
                        },
                        Options = new EventOptions()
                        {
                            Delete = true,
                            Update = true,
                            ChangeParticipationStatus = true,
                        },
                    },
                },
                events);
        }

        [Test]
        public void CanGetPagedEvents()
        {
            this.Http.Stub(
                HttpGet
                .Url("https://api.cronofy.com/v1/events?tzid=Etc%2FUTC&localized_times=true")
                .RequestHeader("Authorization", "Bearer " + AccessToken)
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
      ""status"": ""confirmed"",
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
}"));

            this.Http.Stub(
                HttpGet
                .Url("https://api.cronofy.com/v1/events/pages/08a07b034306679e")
                .RequestHeader("Authorization", "Bearer " + AccessToken)
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
      ""status"": ""confirmed"",
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
}"));

            var events = this.Client.GetEvents();

            CollectionAssert.AreEqual(
                new List<Event>
                {
                    new Event
                    {
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
                        Categories = new string[] { },
                        Created = new DateTime(2014, 9, 1, 8, 0, 1, DateTimeKind.Utc),
                        Updated = new DateTime(2014, 9, 1, 9, 24, 16, DateTimeKind.Utc),
                        Attendees = new[]
                        {
                            new Attendee
                            {
                                Email = "example@cronofy.com",
                                DisplayName = "Example Person",
                                Status = AttendeeStatus.NeedsAction,
                            },
                        },
                    },
                    new Event
                    {
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
                        Categories = new string[] { },
                        Created = new DateTime(2014, 9, 1, 9, 0, 1, DateTimeKind.Utc),
                        Updated = new DateTime(2014, 9, 1, 10, 24, 16, DateTimeKind.Utc),
                        Attendees = new[]
                        {
                            new Attendee
                            {
                                Email = "example+other@cronofy.com",
                                DisplayName = "Other Person",
                                Status = AttendeeStatus.NeedsAction,
                            },
                        },
                    },
                },
                events);
        }

        [Test]
        public void CanGetEventsWithinDates()
        {
            this.AssertParameter(
                "from=2015-10-20&to=2015-10-30",
                b => b.From(2015, 10, 20).To(2015, 10, 30));
        }

        [Test]
        public void CanGetEventsSinceLastModified()
        {
            var lastModified = DateTime.UtcNow.AddMinutes(-15);

            this.AssertParameter(
                "last_modified=" + Encode(lastModified.ToString("u")),
                b => b.LastModified(lastModified));
        }

        [Test]
        public void CanGetEventsThatHaveBeenDeleted()
        {
            this.AssertParameter("include_deleted=true", b => b.IncludeDeleted(true));
        }

        [Test]
        public void CanGetEventsThatHaveBeenMoved()
        {
            this.AssertParameter("include_moved=true", b => b.IncludeMoved(true));
        }

        [Test]
        public void CanGetEventsThatAreManaged()
        {
            this.AssertParameter("include_managed=true", b => b.IncludeManaged(true));
        }

        [Test]
        public void CanGetOnlyEventsThatAreManaged()
        {
            this.AssertParameter("only_managed=true", b => b.OnlyManaged(true));
        }

        [Test]
        public void CanGetEventsWithIncludeGeo()
        {
            this.AssertParameter("include_geo=true", b => b.IncludeGeo(true));
        }

        [Test]
        public void CanGetEventsWithinOneCalendar()
        {
            const string calendarId = "cal_U9uuErStTG@EAAAB_IsAsykA2DBTWqQTf-f0kJw";

            this.AssertParameter(
                Encode("calendar_ids[]", calendarId),
                b => b.CalendarId(calendarId));
        }

        [Test]
        public void CanGetEventsWithinMultipleCalendars()
        {
            var calendarIds = new List<string>
            {
                "cal_U9uuErStTG@EAAAB_IsAsykA2DBTWqQTf-f0kJw",
                "cal_U@y23bStTFV7AAAB_iWTeH8WOCDOIW@us5gRzww",
            };

            var expectedKeyValue =
                Encode("calendar_ids[]", calendarIds[0]) + "&" +
                Encode("calendar_ids[]", calendarIds[1]);

            this.AssertParameter(
                expectedKeyValue,
                b => b.CalendarIds(calendarIds));

            this.AssertParameter(
                expectedKeyValue,
                b => b.CalendarIds(calendarIds.ToArray()));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void CanGetPrivateEvents(bool eventPrivate)
        {
            this.Http.Stub(
                HttpGet
                .Url("https://api.cronofy.com/v1/events?tzid=Etc%2FUTC&localized_times=true")
                .RequestHeader("Authorization", "Bearer " + AccessToken)
                .ResponseCode(200)
                .ResponseBody(
string.Format(
    @"{{
  ""pages"": {{
    ""current"": 1,
    ""total"": 1
  }},
  ""events"": [
    {{
      ""calendar_id"": ""cal_U9uuErStTG@EAAAB_IsAsykA2DBTWqQTf-f0kJw"",
      ""event_uid"": ""evt_external_54008b1a4a41730f8d5c6037"",
      ""summary"": ""Company Retreat"",
      ""description"": ""Escape to the country"",
      ""start"": ""2014-09-06"",
      ""end"": ""2014-09-08"",
      ""deleted"": false,
      ""recurring"": true,
      ""series_identifier"": ""identifier"",
      ""participation_status"": ""needs_action"",
      ""transparency"": ""opaque"",
      ""status"": ""confirmed"",
      ""categories"": [],
      ""attendees"": [
        {{
          ""email"": ""example@cronofy.com"",
          ""display_name"": ""Example Person"",
          ""status"": ""needs_action""
        }}
      ],
      ""created"": ""2014-09-01T08:00:01Z"",
      ""updated"": ""2014-09-01T09:24:16Z"",
      ""options"": {{
        ""delete"": true,
        ""update"": true,
        ""change_participation_status"": true
      }},
      ""event_private"": {0}
    }}
  ]
}}", eventPrivate.ToString().ToLower())));

            var events = this.Client.GetEvents();

            CollectionAssert.AreEqual(
                new List<Event>
                {
                    new Event
                    {
                        CalendarId = "cal_U9uuErStTG@EAAAB_IsAsykA2DBTWqQTf-f0kJw",
                        EventUid = "evt_external_54008b1a4a41730f8d5c6037",
                        Summary = "Company Retreat",
                        Description = "Escape to the country",
                        Start = new EventTime(new Date(2014, 9, 6), "Etc/UTC"),
                        End = new EventTime(new Date(2014, 9, 8), "Etc/UTC"),
                        Location = null,
                        Deleted = false,
                        Recurring = true,
                        SeriesIdentifier = "identifier",
                        ParticipationStatus = AttendeeStatus.NeedsAction,
                        Transparency = Transparency.Opaque,
                        EventStatus = EventStatus.Confirmed,
                        Categories = new string[] { },
                        Created = new DateTime(2014, 9, 1, 8, 0, 1, DateTimeKind.Utc),
                        Updated = new DateTime(2014, 9, 1, 9, 24, 16, DateTimeKind.Utc),
                        Attendees = new[]
                        {
                            new Attendee
                            {
                                Email = "example@cronofy.com",
                                DisplayName = "Example Person",
                                Status = AttendeeStatus.NeedsAction,
                            },
                        },
                        Options = new EventOptions()
                        {
                            Delete = true,
                            Update = true,
                            ChangeParticipationStatus = true,
                        },
                        EventPrivate = eventPrivate,
                    },
                },
                events);
        }

        private void AssertParameter(string keyValue, Action<GetEventsRequestBuilder> builderAction)
        {
            this.Http.Stub(
                HttpGet
                .Url("https://api.cronofy.com/v1/events?tzid=Etc%2FUTC&localized_times=true&" + keyValue)
                .RequestHeader("Authorization", "Bearer " + AccessToken)
                .ResponseCode(200)
                .ResponseBody(SingleEventResponseBody));

            var builder = new GetEventsRequestBuilder();

            builderAction.Invoke(builder);

            var events = this.Client.GetEvents(builder);

            CollectionAssert.AreEqual(SingleEventResultCollection, events);
        }

        private const string SingleEventResponseBody = @"{
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
      ""status"": ""confirmed"",
      ""categories"": [],
      ""attendees"": [
        {
          ""email"": ""example@cronofy.com"",
          ""display_name"": ""Example Person"",
          ""status"": ""needs_action""
        }
      ],
      ""organizer"": {
        ""email"": ""example@cronofy.com"",
        ""display_name"": ""Example Person"",
      },
      ""created"": ""2014-09-01T08:00:01Z"",
      ""updated"": ""2014-09-01T09:24:16Z""
    }
  ]
}";

        private static readonly List<Event> SingleEventResultCollection = new List<Event>
        {
            new Event
            {
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
                Categories = new string[] { },
                Created = new DateTime(2014, 9, 1, 8, 0, 1, DateTimeKind.Utc),
                Updated = new DateTime(2014, 9, 1, 9, 24, 16, DateTimeKind.Utc),
                Attendees = new[]
                {
                    new Attendee
                    {
                        Email = "example@cronofy.com",
                        DisplayName = "Example Person",
                        Status = AttendeeStatus.NeedsAction,
                    },
                },
                Organizer = new Organizer
                {
                    Email = "example@cronofy.com",
                    DisplayName = "Example Person",
                },
            },
        };

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
