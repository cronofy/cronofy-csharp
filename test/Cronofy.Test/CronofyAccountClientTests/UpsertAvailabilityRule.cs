namespace Cronofy.Test.CronofyAccountClientTests
{
    using System;
    using NUnit.Framework;

    internal sealed class UpsertAvailabilityRule : Base
    {
        [Test]
        public void CanUpsertAvailabilityRule()
        {
            this.Http.Stub(
                HttpPost
                    .Url("https://api.cronofy.com/v1/availability_rules")
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .JsonRequest(@"
                        {
                            ""availability_rule_id"": ""default"",
                            ""tzid"": ""America/Chicago"",
                            ""calendar_ids"": [
                                ""cal_n23kjnwrw2_jsdfjksn234""
                            ],
                            ""weekly_periods"": [
                                {
                                    ""day"": ""monday"",
                                    ""start_time"": ""09:30"",
                                    ""end_time"": ""12:30""
                                },
                                {
                                    ""day"": ""monday"",
                                    ""start_time"": ""14:00"",
                                    ""end_time"": ""17:00""
                                },
                                {
                                    ""day"": ""wednesday"",
                                    ""start_time"": ""09:30"",
                                    ""end_time"": ""12:30""
                                }
                            ]
                        }
                    ")
                    .ResponseCode(200)
                    .ResponseBody(@"
                        {
                            ""availability_rule"": {
                                ""availability_rule_id"": ""default"",
                                ""tzid"": ""America/Chicago"",
                                ""calendar_ids"": [
                                    ""cal_n23kjnwrw2_jsdfjksn234""
                                ],
                                ""weekly_periods"": [
                                    {
                                        ""day"": ""monday"",
                                        ""start_time"": ""09:30"",
                                        ""end_time"": ""12:30""
                                    },
                                    {
                                        ""day"": ""monday"",
                                        ""start_time"": ""14:00"",
                                        ""end_time"": ""17:00""
                                    },
                                    {
                                        ""day"": ""wednesday"",
                                        ""start_time"": ""09:30"",
                                        ""end_time"": ""12:30""
                                    }
                                ]
                            }
                        }
                    "));

            var updatedAvailabilityRuleState = new AvailabilityRule
            {
                AvailabilityRuleId = "default",
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

            var actualResponse = this.Client.UpsertAvailabilityRule(updatedAvailabilityRuleState);

            Assert.AreEqual(updatedAvailabilityRuleState, actualResponse);
        }

        [Test]
        public void CanUpsertAvailabilityRuleWithMissingCalendarIds()
        {
            this.Http.Stub(
                HttpPost
                    .Url("https://api.cronofy.com/v1/availability_rules")
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .JsonRequest(@"
                        {
                            ""availability_rule_id"": ""default"",
                            ""tzid"": ""America/Chicago"",
                            ""weekly_periods"": [
                                {
                                    ""day"": ""monday"",
                                    ""start_time"": ""09:30"",
                                    ""end_time"": ""12:30""
                                },
                                {
                                    ""day"": ""monday"",
                                    ""start_time"": ""14:00"",
                                    ""end_time"": ""17:00""
                                },
                                {
                                    ""day"": ""wednesday"",
                                    ""start_time"": ""09:30"",
                                    ""end_time"": ""12:30""
                                }
                            ]
                        }
                    ")
                    .ResponseCode(200)
                    .ResponseBody(@"
                        {
                            ""availability_rule"": {
                                ""availability_rule_id"": ""default"",
                                ""tzid"": ""America/Chicago"",
                                ""weekly_periods"": [
                                    {
                                        ""day"": ""monday"",
                                        ""start_time"": ""09:30"",
                                        ""end_time"": ""12:30""
                                    },
                                    {
                                        ""day"": ""monday"",
                                        ""start_time"": ""14:00"",
                                        ""end_time"": ""17:00""
                                    },
                                    {
                                        ""day"": ""wednesday"",
                                        ""start_time"": ""09:30"",
                                        ""end_time"": ""12:30""
                                    }
                                ]
                            }
                        }
                    "));

            var updatedAvailabilityRuleState = new AvailabilityRule
            {
                AvailabilityRuleId = "default",
                TimeZoneId = "America/Chicago",
                CalendarIds = null,
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

            var actualResponse = this.Client.UpsertAvailabilityRule(updatedAvailabilityRuleState);

            Assert.AreEqual(updatedAvailabilityRuleState, actualResponse);
        }
    }
}
