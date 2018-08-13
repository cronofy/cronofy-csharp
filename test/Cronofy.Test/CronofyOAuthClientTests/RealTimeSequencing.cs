using System;
using NUnit.Framework;
using Cronofy.Requests;

namespace Cronofy.Test.CronofyOAuthClientTests
{
    [TestFixture]
    public sealed class RealTimeSequencing
    {
        private const string clientId = "abcdef123456";
        private const string clientSecret = "s3cr3t1v3";

        [Test]
        public void CanGetOAuthUrlWithAvailabilityTargetCalendarsAndHourFormat()
        {
            var start = new DateTime(2014, 8, 5, 15, 30, 0, DateTimeKind.Utc);
            var end = new DateTime(2014, 8, 5, 16, 30, 0, DateTimeKind.Utc);

            var client = new CronofyOAuthClient(clientId, clientSecret);
            var http = new StubHttpClient();

            client.HttpClient = http;

            var upsertEventRequestWithoutStartAndEnd = new UpsertEventRequestBuilder()
                .EventId("testEventId")
                .Summary("Test Summary")
                .Build();

            var sequenceBuilder = new SequenceRequestBuilder()
                .RequiredDuration(60)
                .Ordinal(1)
                .SequenceId("First Event")
                .AddRequiredParticipant("acc_567236000909002")
                .AddRequiredParticipant("acc_678347111010113");

            var availabilityRequest = new SequencedAvailabilityRequestBuilder()
                .AddSequence(sequenceBuilder)
                .AddAvailablePeriod(start, end)
                .Build();

            var expectedUrl = "http://test.com";

            http.Stub(
                HttpPost
                    .Url("https://api.cronofy.com/v1/real_time_sequencing")
                    .JsonRequest(@"
                    {
                      ""availablity"":{
                         ""sequence"":[
                              {
                                ""ordinal"":1,
                                ""sequence_id"":""First Event"",
                                ""participants"":[
                                  {
                                    ""members"":[
                                      { ""sub"":""acc_567236000909002"" },
                                      { ""sub"":""acc_678347111010113"" }
                                    ],
                                ""required"":""all""
                              }
                            ],
                            ""required_duration"":{
                              ""minutes"":60
                            },
                            ""available_periods"":[ ]
                          }
                        ],
                        ""available_periods"":[
                          {
                            ""start"":""2014-08-05 15:30:00Z"",
                            ""end"":""2014-08-05 16:30:00Z""
                          }
                        ]
                      },
                      ""client_id"":""abcdef123456"",
                      ""client_secret"":""s3cr3t1v3"",
                      ""oauth"":{
                        ""redirect_uri"":""http://example.com/redirectUri"",
                        ""scope"":""test_scope""
                      },
                      ""event"":{
                        ""event_id"":""testEventId"",
                        ""summary"":""Test Summary""
                      },
                      ""target_calendars"":[
                        {
                          ""sub"":""sub"",
                          ""calendar_id"":""calendarId""
                        }
                      ],
                      ""formatting"":{
                        ""hour_format"":""H""
                      },
                      ""tzid"":""Etc/UTC""
                    }")
                    .ResponseCode(200)
                    .ResponseBodyFormat("{{\"url\":\"{0}\"}}", expectedUrl)
            );

            var request = new RealTimeSequencingRequestBuilder()
                .OAuthDetails("http://example.com/redirectUri", "test_scope")
                .Timezone("Etc/UTC")
                .UpsertEventRequest(upsertEventRequestWithoutStartAndEnd)
                .SequencedAvailabilityRequest(availabilityRequest)
                .AddTargetCalendar("sub", "calendarId")
                .HourFormat("H")
                .Build();

            var actualUrl = client.RealTimeSequencing(request);

            Assert.AreEqual(expectedUrl, actualUrl);
        }
    }
}
