using System;
using Cronofy.Requests;
using Cronofy.Responses;
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

            var upsertRequest = new UpsertEventRequestBuilder()
                .EventId("qTtZdczOccgaPncGJaCiLg")
                .Summary("Board meeting")
                .Description("Discuss plans for the next quarter.")
                .Start(new DateTime(2014, 8, 5, 15, 30, 0, DateTimeKind.Utc))
                .End(new DateTime(2014, 8, 5, 17, 0, 0, DateTimeKind.Utc))
                .Location("Board room")
                .Build();

            batchBuilder.UpsertEvent("cal_n23kjnwrw2_jsdfjksn234", upsertRequest);

            var response = this.Client.BatchRequest(batchBuilder);

            var expected = new BatchResponse.EntryResponse
            {
                Status = 202,
                Request = new BatchRequest.EntryBuilder()
                    .Method("POST")
                    .RelativeUrl("/v1/calendars/cal_n23kjnwrw2_jsdfjksn234/events")
                    .Data(upsertRequest)
                    .Build(),
            };

            Assert.AreEqual(expected, response.Batch[0]);
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

            var response = this.Client.BatchRequest(batchBuilder);

            var expected = new BatchResponse.EntryResponse
            {
                Status = 202,
                Request = new BatchRequest.EntryBuilder()
                    .Method("DELETE")
                    .RelativeUrl("/v1/calendars/cal_n23kjnwrw2_jsdfjksn234/events")
                    .Data(new DeleteEventRequest { EventId = "qTtZdczOccgaPncGJaCiLg" })
                    .Build(),
            };

            Assert.AreEqual(expected, response.Batch[0]);
        }

        [Test]
        public void CanDeleteExternalEvent()
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
                                        ""event_uid"": ""evt_external_1234abcd""
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

            batchBuilder.DeleteExternalEvent("cal_n23kjnwrw2_jsdfjksn234", "evt_external_1234abcd");

            var response = this.Client.BatchRequest(batchBuilder);

            var expected = new BatchResponse.EntryResponse
            {
                Status = 202,
                Request = new BatchRequest.EntryBuilder()
                    .Method("DELETE")
                    .RelativeUrl("/v1/calendars/cal_n23kjnwrw2_jsdfjksn234/events")
                    .Data(new DeleteExternalEventRequest { EventUid = "evt_external_1234abcd" })
                    .Build(),
            };

            Assert.AreEqual(expected, response.Batch[0]);
        }

        [Test]
        public void RaiseExceptionOnPartialSuccess()
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
                                },
                                {
                                    ""method"": ""DELETE"",
                                    ""relative_url"": ""/v1/calendars/cal_n23kjnwrw2_jsdfjksn235/events"",
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
                                { ""status"": 404 },
                                { ""status"": 202 }
                            ]
                        }
                    ")
            );

            var batchBuilder = new BatchRequestBuilder();

            batchBuilder.DeleteEvent("cal_n23kjnwrw2_jsdfjksn234", "qTtZdczOccgaPncGJaCiLg");
            batchBuilder.DeleteEvent("cal_n23kjnwrw2_jsdfjksn235", "qTtZdczOccgaPncGJaCiLg");

            var exception = Assert.Throws<BatchWithErrorsException>(() => this.Client.BatchRequest(batchBuilder));
            var response = exception.Response;

            var firstEntryResponse = new BatchResponse.EntryResponse
            {
                Status = 404,
                Request = new BatchRequest.EntryBuilder()
                    .Method("DELETE")
                    .RelativeUrl("/v1/calendars/cal_n23kjnwrw2_jsdfjksn234/events")
                    .Data(new DeleteEventRequest { EventId = "qTtZdczOccgaPncGJaCiLg" })
                    .Build(),
            };

            var secondEntryResponse = new BatchResponse.EntryResponse
            {
                Status = 202,
                Request = new BatchRequest.EntryBuilder()
                    .Method("DELETE")
                    .RelativeUrl("/v1/calendars/cal_n23kjnwrw2_jsdfjksn235/events")
                    .Data(new DeleteEventRequest { EventId = "qTtZdczOccgaPncGJaCiLg" })
                    .Build(),
            };

            Assert.IsTrue(response.HasErrors);
            Assert.AreEqual(new[] { firstEntryResponse, secondEntryResponse }, response.Batch);
            Assert.AreEqual(new[] { firstEntryResponse }, response.Errors);
        }
    }
}
