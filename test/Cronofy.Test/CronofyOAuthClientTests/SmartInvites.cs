using System;
using NUnit.Framework;
using Cronofy.Requests;

namespace Cronofy.Test.CronofyOAuthClientTests
{
    using System.Linq;

    [TestFixture]
    public sealed class SmartInvites
    {
        private const string clientId = "abcdef123456";
        private const string clientSecret = "s3cr3t1v3";

        private string callbackUrl = "http://example.com/callbackUrl";

        private string inviteId = "testEventId";
        private string summary = "Test Summary";
        private DateTimeOffset start = new DateTime(2014, 8, 5, 15, 30, 0, DateTimeKind.Utc);
        private DateTimeOffset end = new DateTime(2014, 8, 5, 16, 30, 0, DateTimeKind.Utc);

        private SmartInviteEventRequest upsertEventRequest;

        private CronofyOAuthClient client;
        private StubHttpClient http;

        [SetUp]
        public void SetUp()
        {
            this.client = new CronofyOAuthClient(clientId, clientSecret);
            this.http = new StubHttpClient();

            client.HttpClient = http;

            this.upsertEventRequest = new SmartInviteEventRequestBuilder()
                .Summary(summary)
                .Start(start)
                .End(end)
                .Build();
        }

        [Test]
        public void CanCreateInvite()
        {
            http.Stub(
                HttpPost
                    .Url("https://api.cronofy.com/v1/smart_invites")
                    .RequestHeader("Authorization", string.Format("Bearer {0}", clientSecret))
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBody(
                        @"{""method"":""request"",""smart_invite_id"":""testEventId"",""callback_url"":""http://example.com/callbackUrl"",""recipient"":{""email"":""example@example.com""},""event"":{""summary"":""Test Summary"",""start"":{""time"":""2014-08-05 15:30:00Z"",""tzid"":""Etc/UTC""},""end"":{""time"":""2014-08-05 16:30:00Z"",""tzid"":""Etc/UTC""}}}")
                    .ResponseCode(200)
                    .ResponseBody(@"{
                      ""recipient"": {
                        ""email"": ""cronofy@example.com"",
                        ""status"": ""pending""
                      },
                      ""method"": ""request"",
                      ""smart_invite_id"": ""your-unique-identifier-for-invite"",
                      ""callback_url"": ""https://example.yourapp.com/cronofy/smart_invite/notifications"",
                      ""event"": {
                        ""summary"": ""Board meeting"",
                        ""description"": ""Discuss plans for the next quarter."",
                        ""start"": ""2017-10-05T09:30:00Z"",
                        ""end"": ""2017-10-05T10:00:00Z"",
                        ""tzid"": ""Europe/London"",
                        ""location"": {
                          ""description"": ""Board room""
                        }
                      },
                      ""attachments"": {
                        ""icalendar"": ""BEGIN:VCALENDAR\nVERSION:2.0...""
                      }
                    }")
            );

            var smartInviteRequest = new SmartInviteRequestBuilder()
                .Method("request")
                .CallbackUrl(callbackUrl)
                .InviteId(inviteId)
                .Recipient("example@example.com")
                .Event(upsertEventRequest)
                .Build();

            var actual = client.CreateInvite(smartInviteRequest);

            Assert.AreEqual("your-unique-identifier-for-invite", actual.SmartInviteId);
            Assert.AreEqual("https://example.yourapp.com/cronofy/smart_invite/notifications", actual.CallbackUrl);
            Assert.AreEqual("pending", actual.Recipient.Status);
            Assert.AreEqual("request", actual.Method);
            Assert.AreEqual("cronofy@example.com", actual.Recipient.Email);
            Assert.AreEqual("BEGIN:VCALENDAR\nVERSION:2.0...", actual.Attachments.ICalendar);
        }

        [Test]
        public void CanGetEventDetails()
        {
            http.Stub(
                HttpGet
                    .Url("https://api.cronofy.com/v1/smart_invites?smart_invite_id=example-event-id&recipient_email=cronofy%40example.com")
                    .RequestHeader("Authorization", string.Format("Bearer {0}", clientSecret))
                    .ResponseCode(200)
                    .ResponseBody(@"{
                      ""recipient"": {
                        ""email"": ""cronofy@example.com"",
                        ""status"": ""pending""
                      },
                      ""replies"": [
                       {
                         ""email"": ""person1@example.com"",
                         ""status"": ""accepted""
                       },
                       {
                         ""email"": ""person2@example.com"",
                         ""status"": ""declined""
                       }
                      ],
                      ""smart_invite_id"": ""your-unique-identifier-for-invite"",
                      ""callback_url"": ""https://example.yourapp.com/cronofy/smart_invite/notifications"",
                      ""method"": ""request"",
                      ""event"": {
                        ""summary"": ""Board meeting"",
                        ""description"": ""Discuss plans for the next quarter."",
                        ""start"": ""2017-10-05T09:30:00Z"",
                        ""end"": ""2017-10-05T10:00:00Z"",
                        ""tzid"": ""Europe/London"",
                        ""location"": {
                          ""description"": ""Board room""
                        }
                      },
                      ""attachments"": {
                        ""icalendar"": ""BEGIN:VCALENDAR\nVERSION:2.0...""
                      }
                    }")
            );

            var actual = client.GetSmartInvite("example-event-id", "cronofy@example.com");

            Assert.AreEqual("your-unique-identifier-for-invite", actual.SmartInviteId);
            Assert.AreEqual("https://example.yourapp.com/cronofy/smart_invite/notifications", actual.CallbackUrl);
            Assert.AreEqual("pending", actual.Recipient.Status);
            Assert.AreEqual("request", actual.Method);
            Assert.AreEqual("cronofy@example.com", actual.Recipient.Email);
            Assert.AreEqual("BEGIN:VCALENDAR\nVERSION:2.0...", actual.Attachments.ICalendar);
            Assert.AreEqual(2, actual.Replies.Count());

            var reply1 = actual.Replies.FirstOrDefault();
            Assert.NotNull(reply1);
            Assert.AreEqual("person1@example.com", reply1.Email);
            Assert.AreEqual("accepted", reply1.Status);

            var reply2 = actual.Replies.LastOrDefault();
            Assert.NotNull(reply2);
            Assert.AreEqual("person2@example.com", reply2.Email);
            Assert.AreEqual("declined", reply2.Status);
        }

    }
}
