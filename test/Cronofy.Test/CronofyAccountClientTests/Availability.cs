using System;
using NUnit.Framework;

namespace Cronofy.Test.CronofyAccountClientTests
{
    internal sealed class Availability : Base
    {
        [Test]
        public void CanPerformAvailabilityQuery()
        {
            const string requestBody = @"
                {
                  ""participants"": [
                    {
                      ""members"": [
                        { ""sub"": ""acc_567236000909002"" },
                        { ""sub"": ""acc_678347111010113"" }
                      ],
                      ""required"": ""all""
                    }
                  ],
                  ""required_duration"": { ""minutes"": 60 },
                  ""available_periods"": [
                    {
                      ""start"": ""2017-01-03 09:00:00Z"",
                      ""end"": ""2017-01-03 18:00:00Z""
                    },
                    {
                      ""start"": ""2017-01-04 09:00:00Z"",
                      ""end"": ""2017-01-04 18:00:00Z""
                    }
                  ]
                }";

            var builder = new AvailabilityRequestBuilder()
                .RequiredDuration(60)
                .AddAvailablePeriod(
                    new DateTimeOffset(2017, 1, 3, 9, 0, 0, TimeSpan.Zero),
                    new DateTimeOffset(2017, 1, 3, 18, 0, 0, TimeSpan.Zero))
                .AddAvailablePeriod(
                    new DateTimeOffset(2017, 1, 4, 9, 0, 0, TimeSpan.Zero),
                    new DateTimeOffset(2017, 1, 4, 18, 0, 0, TimeSpan.Zero))
                .AddRequiredParticipant("acc_567236000909002")
                .AddRequiredParticipant("acc_678347111010113");

            const string responseBody = @"
                {
                  ""available_periods"": [
                    {
                      ""start"": ""2017-01-03T09:00:00Z"",
                      ""end"": ""2017-01-03T11:00:00Z"",
                      ""participants"": [
                        { ""sub"": ""acc_567236000909002"" },
                        { ""sub"": ""acc_678347111010113"" }
                      ]
                    },
                    {
                      ""start"": ""2017-01-03T14:00:00Z"",
                      ""end"": ""2017-01-03T16:00:00Z"",
                      ""participants"": [
                        { ""sub"": ""acc_567236000909002"" },
                        { ""sub"": ""acc_678347111010113"" }
                      ]
                    },
                    {
                      ""start"": ""2017-01-04T11:00:00Z"",
                      ""end"": ""2017-01-04T17:00:00Z"",
                      ""participants"": [
                        { ""sub"": ""acc_567236000909002"" },
                        { ""sub"": ""acc_678347111010113"" }
                      ]
                    },
                  ]
                }";

            Http.Stub(
                HttpPost
                    .Url("https://api.cronofy.com/v1/availability")
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .JsonRequest(requestBody)
                    .ResponseCode(200)
                    .ResponseBody(responseBody)
            );

            var availability = Client.GetAvailability(builder);

            var expected = new[]
            {
                new AvailablePeriod
                {
                    Start = new DateTimeOffset(2017, 1, 3, 9, 0, 0, TimeSpan.Zero),
                    End = new DateTimeOffset(2017, 1, 3, 11, 0, 0, TimeSpan.Zero),
                    Participants = new[]
                    {
                        new AvailablePeriod.Participant { Sub = "acc_567236000909002" },
                        new AvailablePeriod.Participant { Sub = "acc_678347111010113" },
                    },
                },
                new AvailablePeriod
                {
                    Start = new DateTimeOffset(2017, 1, 3, 14, 0, 0, TimeSpan.Zero),
                    End = new DateTimeOffset(2017, 1, 3, 16, 0, 0, TimeSpan.Zero),
                    Participants = new[]
                    {
                        new AvailablePeriod.Participant { Sub = "acc_567236000909002" },
                        new AvailablePeriod.Participant { Sub = "acc_678347111010113" },
                    },
                },
                new AvailablePeriod
                {
                    Start = new DateTimeOffset(2017, 1, 4, 11, 0, 0, TimeSpan.Zero),
                    End = new DateTimeOffset(2017, 1, 4, 17, 0, 0, TimeSpan.Zero),
                    Participants = new[]
                    {
                        new AvailablePeriod.Participant { Sub = "acc_567236000909002" },
                        new AvailablePeriod.Participant { Sub = "acc_678347111010113" },
                    },
                },
            };

            Assert.AreEqual(expected, availability);
        }

        [Test]
        public void CanPerformComplexAvailabilityQuery()
        {
            const string requestBody = @"
                {
                  ""participants"": [
                    {
                      ""members"": [
                        { ""sub"": ""acc_567236000909002"" },
                        {
                          ""sub"": ""acc_678347111010113"",
                          ""available_periods"": [
                            {
                              ""start"": ""2017-01-03 09:00:00Z"",
                              ""end"": ""2017-01-03 12:00:00Z""
                            }
                          ]
                        }
                      ],
                      ""required"": ""all""
                    },
                    {
                      ""members"": [
                        { ""sub"": ""acc_678910200909001"" },
                        { ""sub"": ""acc_879082061010114"" }
                      ],
                      ""required"": 1
                    }
                  ],
                  ""required_duration"": { ""minutes"": 60 },
                  ""available_periods"": [
                    {
                      ""start"": ""2017-01-03 09:00:00Z"",
                      ""end"": ""2017-01-03 18:00:00Z""
                    }
                  ]
                }";

            var member = new AvailabilityMemberBuilder()
                .Sub("acc_678347111010113")
                .AddAvailablePeriod(
                    new DateTimeOffset(2017, 1, 3, 9, 0, 0, TimeSpan.Zero),
                    new DateTimeOffset(2017, 1, 3, 12, 0, 0, TimeSpan.Zero));

            var requiredGroup = new ParticipantGroupBuilder()
                .AddMember("acc_567236000909002")
                .AddMember(member)
                .AllRequired();

            var representedGroup = new ParticipantGroupBuilder()
                .AddMember("acc_678910200909001")
                .AddMember("acc_879082061010114")
                .Required(1);

            var builder = new AvailabilityRequestBuilder()
                .RequiredDuration(60)
                .AddAvailablePeriod(
                    new DateTimeOffset(2017, 1, 3, 9, 0, 0, TimeSpan.Zero),
                    new DateTimeOffset(2017, 1, 3, 18, 0, 0, TimeSpan.Zero))
                .AddParticipantGroup(requiredGroup)
                .AddParticipantGroup(representedGroup);

            const string responseBody = @"
                {
                  ""available_periods"": [
                    {
                      ""start"": ""2017-01-03T09:00:00Z"",
                      ""end"": ""2017-01-03T11:00:00Z"",
                      ""participants"": [
                        { ""sub"": ""acc_567236000909002"" },
                        { ""sub"": ""acc_678347111010113"" },
                        { ""sub"": ""acc_6789010200909001"" }
                      ]
                    }
                  ]
                }";

            Http.Stub(
                HttpPost
                    .Url("https://api.cronofy.com/v1/availability")
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .JsonRequest(requestBody)
                    .ResponseCode(200)
                    .ResponseBody(responseBody)
            );

            var availability = Client.GetAvailability(builder);

            var expected = new[]
            {
                new AvailablePeriod
                {
                    Start = new DateTimeOffset(2017, 1, 3, 9, 0, 0, TimeSpan.Zero),
                    End = new DateTimeOffset(2017, 1, 3, 11, 0, 0, TimeSpan.Zero),
                    Participants = new[]
                    {
                        new AvailablePeriod.Participant { Sub = "acc_567236000909002" },
                        new AvailablePeriod.Participant { Sub = "acc_678347111010113" },
                        new AvailablePeriod.Participant { Sub = "acc_6789010200909001" },
                    },
                },
            };

            Assert.AreEqual(expected, availability);
        }
    }
}