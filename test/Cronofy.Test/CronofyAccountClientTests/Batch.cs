using System;
using NUnit.Framework;

namespace Cronofy.Test.CronofyAccountClientTests
{
    internal sealed class Batch : Base
    {
        [Test]
        public void CanUpsertEvent()
        {
            this.Http.Stub(
                HttpPost
                    .Url("https://api.cronofy.com/v1/batch")
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .JsonRequest(@"
                        {
                            ""batch"": [
                                {
                                    ""method"": ""POST"",
                                    ""relative_url"": ""/v1/calendars/cal_n23kjnwrw2_jsdfjksn234/events"",
                                    ""data"": {
                                        ""event_id"": ""qTtZdczOccgaPncGJaCiLg"",
                                        ""summary"": ""Board meeting"",
                                        ""description"": ""Discuss plans for the next quarter."",
                                        ""start"": { ""time"": ""2014-08-05 15:30:00Z"", ""tzid"": ""Etc/UTC"" },
                                        ""end"": { ""time"": ""2014-08-05 17:00:00Z"", ""tzid"": ""Etc/UTC"" },
                                        ""location"": {
                                            ""description"": ""Board room""
                                        }
                                    }
                                }
                            ]
                        }
                    ")
                    .ResponseCode(207)
                    .ResponseBody(@"
                        {
                            ""batch"": [
                                { ""status"": 202 }
                            ]
                        }
                    ")
            );

            var batchBuilder = new BatchRequestBuilder();

            var upsertBuilder = new UpsertEventRequestBuilder()
                .EventId("qTtZdczOccgaPncGJaCiLg")
                .Summary("Board meeting")
                .Description("Discuss plans for the next quarter.")
                .Start(new DateTime(2014, 8, 5, 15, 30, 0, DateTimeKind.Utc))
                .End(new DateTime(2014, 8, 5, 17, 0, 0, DateTimeKind.Utc))
                .Location("Board room");

            batchBuilder.UpsertEvent("cal_n23kjnwrw2_jsdfjksn234", upsertBuilder);

            this.Client.BatchRequest(batchBuilder);
        }

        [Test]
        public void CanDeleteEvent()
        {
            this.Http.Stub(
                HttpPost
                    .Url("https://api.cronofy.com/v1/batch")
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .JsonRequest(@"
                        {
                            ""batch"": [
                                {
                                    ""method"": ""DELETE"",
                                    ""relative_url"": ""/v1/calendars/cal_n23kjnwrw2_jsdfjksn234/events"",
                                    ""data"": {
                                        ""event_id"": ""qTtZdczOccgaPncGJaCiLg""
                                    }
                                }
                            ]
                        }
                    ")
                    .ResponseCode(207)
                    .ResponseBody(@"
                        {
                            ""batch"": [
                                { ""status"": 202 }
                            ]
                        }
                    ")
            );

            var batchBuilder = new BatchRequestBuilder();

            batchBuilder.DeleteEvent("cal_n23kjnwrw2_jsdfjksn234", "qTtZdczOccgaPncGJaCiLg");

            this.Client.BatchRequest(batchBuilder);
        }
    }
}
