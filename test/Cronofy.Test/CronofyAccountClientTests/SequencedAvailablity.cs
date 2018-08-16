using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Cronofy.Test.CronofyAccountClientTests
{
    internal sealed class SequencedAvailability : Base
    {
        [Test]
        public void CanPerformSequenceAvailabilityQuery()
        {
            const string requestBody = @"
                {
                  ""sequence"":[
                    {
                      ""ordinal"": 1,
                      ""sequence_id"": ""First Event"",
                      ""participants"":[
                        {
                          ""members"":[
                            { ""sub"":""acc_567236000909002"" },
                            { ""sub"":""acc_678347111010113"" }
                          ],
                          ""required"":""all""
                        }
                      ],
                      ""required_duration"":{ ""minutes"":60 },
                      ""available_periods"":[ ]
                    }
                  ],
                  ""available_periods"":[
                    {
                      ""start"":""2017-01-03 09:00:00Z"",
                      ""end"":""2017-01-03 18:00:00Z""
                    },
                    {
                      ""start"":""2017-01-04 09:00:00Z"",
                      ""end"":""2017-01-04 18:00:00Z""
                    }
                  ]
                }";

            var sequenceBuilder = new SequenceRequestBuilder()
                .RequiredDuration(60)
                .Ordinal(1)
                .SequenceId("First Event")
                .AddRequiredParticipant("acc_567236000909002")
                .AddRequiredParticipant("acc_678347111010113");

            var builder = new SequencedAvailabilityRequestBuilder()
                .AddSequence(sequenceBuilder)
                .AddAvailablePeriod(
                    new DateTimeOffset(2017, 1, 3, 9, 0, 0, TimeSpan.Zero),
                    new DateTimeOffset(2017, 1, 3, 18, 0, 0, TimeSpan.Zero))
                .AddAvailablePeriod(
                    new DateTimeOffset(2017, 1, 4, 9, 0, 0, TimeSpan.Zero),
                    new DateTimeOffset(2017, 1, 4, 18, 0, 0, TimeSpan.Zero));

            const string responseBody = @"
            {
              ""sequences"":[
                {
                  ""sequence"":[
                    {
                      ""sequence_id"":""123"",
                      ""start"": ""2018-08-15T09:00:00Z"",
                      ""end"": ""2018-08-15T10:00:00Z"",
                      ""participants"":[ { ""sub"":""acc_567236000909002"" } ]
                    },
                    {
                      ""sequence_id"":""456"",
                      ""start"": ""2018-08-15T10:00:00Z"",
                      ""end"": ""2018-08-15T11:00:00Z"",
                      ""participants"":[ { ""sub"":""acc_678347111010113"" } ]
                    }
                  ]
                },
                {
                  ""sequence"":[
                    {
                      ""sequence_id"":""123"",
                      ""start"": ""2018-08-15T11:00:00Z"",
                      ""end"": ""2018-08-15T12:00:00Z"",
                      ""participants"":[ { ""sub"":""acc_567236000909002"" } ]
                    },
                    {
                      ""sequence_id"":""456"",
                      ""start"": ""2018-08-15T12:00:00Z"",
                      ""end"": ""2018-08-15T13:00:00Z"",
                      ""participants"":[ { ""sub"":""acc_678347111010113"" } ]
                    }
                  ]
                }
              ]
            }";

            Http.Stub(
                HttpPost
                    .Url("https://api.cronofy.com/v1/sequenced_availability")
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .JsonRequest(requestBody)
                    .ResponseCode(200)
                    .ResponseBody(responseBody)
            );

            var availability = Client.GetSequencedAvailability(builder);

            var expected = new AvailableSequences {
                Sequences = new IEnumerable<AvailableSequences.SequenceItem>[] {
                    new AvailableSequences.SequenceItem[] {
                        new AvailableSequences.SequenceItem
                        {
                            SequenceId = "123",
                            Start = new DateTimeOffset(2018, 8, 15, 9, 0, 0, TimeSpan.Zero),
                            End = new DateTimeOffset(2018, 8, 15, 10, 0, 0, TimeSpan.Zero),
                            Participants = new[]
                            {
                                new AvailablePeriod.Participant { Sub = "acc_567236000909002" },
                            },
                        },
                        new AvailableSequences.SequenceItem
                        {
                            SequenceId = "456",
                            Start = new DateTimeOffset(2018, 8, 15, 10, 0, 0, TimeSpan.Zero),
                            End = new DateTimeOffset(2018, 8, 15, 11, 0, 0, TimeSpan.Zero),
                            Participants = new[]
                            {
                                new AvailablePeriod.Participant { Sub = "acc_678347111010113" },
                            },
                        }
                    },
                    new[] {
                        new AvailableSequences.SequenceItem
                        {
                            SequenceId = "123",
                            Start = new DateTimeOffset(2018, 8, 15, 11, 0, 0, TimeSpan.Zero),
                            End = new DateTimeOffset(2018, 8, 15, 12, 0, 0, TimeSpan.Zero),
                            Participants = new[]
                            {
                                new AvailablePeriod.Participant { Sub = "acc_567236000909002" },
                            },
                        },
                        new AvailableSequences.SequenceItem
                        {
                            SequenceId = "456",
                            Start = new DateTimeOffset(2018, 8, 15, 12, 0, 0, TimeSpan.Zero),
                            End = new DateTimeOffset(2018, 8, 15, 13, 0, 0, TimeSpan.Zero),
                            Participants = new[]
                            {
                                new AvailablePeriod.Participant { Sub = "acc_678347111010113" },
                            },
                        }
                    }
                }
            };

            Assert.AreEqual(expected.Sequences, availability.Sequences);
        }
    }
}