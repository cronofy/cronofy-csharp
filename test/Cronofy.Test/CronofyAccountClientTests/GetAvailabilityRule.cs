namespace Cronofy.Test.CronofyAccountClientTests
{
    using System;
    using NUnit.Framework;

    internal sealed class GetAvailabilityRule : Base
    {
        private const string AvailabilityRuleId = "my_really_cool_rule_id";

        [Test]
        public void CanGetAvailabilityRule()
        {
            this.Http.Stub(
                HttpGet
                    .Url("https://api.cronofy.com/v1/availability_rules/" + AvailabilityRuleId)
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .ResponseCode(200)
                    .ResponseBodyFormat(
                        @"
                            {{
                                ""availability_rule"": {{
                                    ""availability_rule_id"": ""{0}"",
                                    ""tzid"": ""America/Chicago"",
                                    ""calendar_ids"": [
                                        ""cal_n23kjnwrw2_jsdfjksn234""
                                    ],
                                    ""weekly_periods"": [
                                        {{
                                            ""day"": ""monday"",
                                            ""start_time"": ""09:30"",
                                            ""end_time"": ""12:30""
                                        }},
                                        {{
                                            ""day"": ""monday"",
                                            ""start_time"": ""14:00"",
                                            ""end_time"": ""17:00""
                                        }},
                                        {{
                                            ""day"": ""wednesday"",
                                            ""start_time"": ""09:30"",
                                            ""end_time"": ""12:30""
                                        }}
                                    ]
                                }}
                            }}
                        ", AvailabilityRuleId));

            var actualResponse = this.Client.GetAvailabilityRule(AvailabilityRuleId);

            var expectedResponse = new AvailabilityRule
            {
                AvailabilityRuleId = AvailabilityRuleId,
                TimeZoneId = "America/Chicago",
                CalendarIds = new[] { "cal_n23kjnwrw2_jsdfjksn234" },
                WeeklyPeriods = new[]
                {
                    new AvailabilityRule.WeeklyPeriod
                    {
                        Day = DayOfWeek.Monday,
                        StartTime = "09:30",
                        EndTime = "12:30",
                    },
                    new AvailabilityRule.WeeklyPeriod
                    {
                        Day = DayOfWeek.Monday,
                        StartTime = "14:00",
                        EndTime = "17:00",
                    },
                    new AvailabilityRule.WeeklyPeriod
                    {
                        Day = DayOfWeek.Wednesday,
                        StartTime = "09:30",
                        EndTime = "12:30",
                    },
                },
            };

            Assert.AreEqual(expectedResponse, actualResponse);
        }
    }
}
