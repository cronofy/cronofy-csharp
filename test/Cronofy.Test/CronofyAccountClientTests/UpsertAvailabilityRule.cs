namespace Cronofy.Test.CronofyAccountClientTests
{
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
                    "));

            var actualResponse = this.Client.UpsertAvailabilityRule(new Requests.UpsertAvailabilityRuleRequest
            {
                AvailabilityRuleId = "default",
                TimeZoneId = "America/Chicago",
                CalendarIds = new[] { "cal_n23kjnwrw2_jsdfjksn234" },
                WeeklyPeriods = new[]
                {
                    new Requests.UpsertAvailabilityRuleRequest.WeeklyPeriod
                    {
                        Day = "monday",
                        StartTime = "09:30",
                        EndTime = "12:30",
                    },
                    new Requests.UpsertAvailabilityRuleRequest.WeeklyPeriod
                    {
                        Day = "monday",
                        StartTime = "14:00",
                        EndTime = "17:00",
                    },
                    new Requests.UpsertAvailabilityRuleRequest.WeeklyPeriod
                    {
                        Day = "wednesday",
                        StartTime = "09:30",
                        EndTime = "12:30",
                    },
                },
            });

            var expectedResponse = new AvailabilityRule
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
            };

            Assert.AreEqual(expectedResponse, actualResponse);
        }
    }
}
