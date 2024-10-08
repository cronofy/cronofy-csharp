namespace Cronofy.Test.CronofyAccountClientTests
{
    using NUnit.Framework;

    internal sealed class ListAvailabilityRules : Base
    {
        [Test]
        public void CanListAvailabilityRules()
        {
            this.Http.Stub(
                HttpGet
                    .Url("https://api.cronofy.com/v1/availability_rules")
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .ResponseCode(200)
                    .ResponseBody(@"
                        {
                            ""availability_rules"": [
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
                                },
                                {
                                    ""availability_rule_id"": ""another_rule"",
                                    ""tzid"": ""Europe/London"",
                                    ""calendar_ids"": [
                                        ""cal_n23kjnwrw2_jsdfjksn234"",
                                        ""cal_n23kjnwrw2_th53tksn567"",
                                    ],
                                    ""weekly_periods"": [
                                        {
                                            ""day"": ""saturday"",
                                            ""start_time"": ""09:00"",
                                            ""end_time"": ""17:30""
                                        },
                                        {
                                            ""day"": ""sunday"",
                                            ""start_time"": ""11:00"",
                                            ""end_time"": ""17:40""
                                        }
                                    ]
                                }
                            ]
                        }
                    "));

            var actualResponse = this.Client.GetAvailabilityRules();

            var expectedResponse = new[]
            {
                new AvailabilityRule
                {
                    AvailabilityRuleId = "default",
                    TimeZoneId = "America/Chicago",
                    CalendarIds = new[] { "cal_n23kjnwrw2_jsdfjksn234" },
                    WeeklyPeriods = new[]
                    {
                        new AvailabilityRule.WeeklyPeriod
                        {
                            Day = "monday",
                            StartTime = "09:30",
                            EndTime = "12:30",
                        },
                        new AvailabilityRule.WeeklyPeriod
                        {
                            Day = "monday",
                            StartTime = "14:00",
                            EndTime = "17:00",
                        },
                        new AvailabilityRule.WeeklyPeriod
                        {
                            Day = "wednesday",
                            StartTime = "09:30",
                            EndTime = "12:30",
                        },
                    },
                },
                new AvailabilityRule
                {
                    AvailabilityRuleId = "another_rule",
                    TimeZoneId = "Europe/London",
                    CalendarIds = new[]
                    {
                      "cal_n23kjnwrw2_jsdfjksn234",
                      "cal_n23kjnwrw2_th53tksn567",
                    },
                    WeeklyPeriods = new[]
                    {
                        new AvailabilityRule.WeeklyPeriod
                        {
                            Day = "saturday",
                            StartTime = "09:00",
                            EndTime = "17:30",
                        },
                        new AvailabilityRule.WeeklyPeriod
                        {
                            Day = "sunday",
                            StartTime = "11:00",
                            EndTime = "17:40",
                        },
                    },
                },
            };

            Assert.AreEqual(expectedResponse, actualResponse);
        }
    }
}
