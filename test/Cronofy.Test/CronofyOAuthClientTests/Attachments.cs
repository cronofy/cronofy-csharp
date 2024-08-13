namespace Cronofy.Test.CronofyOAuthClientTests
{
    using NUnit.Framework;

    [TestFixture]
    public sealed class Attachments
    {
        private const string ClientId = "abcdef123456";
        private const string ClientSecret = "s3cr3t1v3";

        private CronofyOAuthClient client;
        private StubHttpClient http;

        [SetUp]
        public void SetUp()
        {
            this.client = new CronofyOAuthClient(ClientId, ClientSecret);
            this.http = new StubHttpClient();

            this.client.HttpClient = this.http;
        }

        [Test]
        public void CanCreateAttachment()
        {
            const string AttachmentId = "attachment-123";
            const string FileName = "file_name.txt";
            const string ContentType = "plain/text";
            const string Base64Content = "VGVzdGluZyBGaWxlIENvbnRlbnQ=";
            const string MD5 = "ac79653edeb65ab5563585f2d5f14fe9";

            this.http.Stub(
                HttpPost
                    .Url("https://api.cronofy.com/v1/attachments")
                    .RequestHeader("Authorization", string.Format("Bearer {0}", ClientSecret))
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBodyFormat(
                        "{{\"attachment\":{{\"file_name\":\"{0}\",\"content_type\":\"{1}\",\"base64_content\":\"{2}\"}}}}",
                        FileName, ContentType, Base64Content)
                    .ResponseCode(200)
                    .ResponseBodyFormat(
                        "{{\"attachment\":{{\"attachment_id\":\"{0}\",\"file_name\":\"{1}\",\"content_type\":\"{2}\",\"md5\":\"{3}\"}}}}",
                        AttachmentId, FileName, ContentType, MD5));

            var actualAttachment = this.client.CreateAttachment(new Requests.CreateAttachmentRequest
            {
                Attachment = new Requests.CreateAttachmentRequest.AttachmentSummary
                {
                    FileName = FileName,
                    ContentType = ContentType,
                    Base64Content = Base64Content,
                },
            });

            var expectedAttachment = new Attachment
            {
                AttachmentId = AttachmentId,
                FileName = FileName,
                ContentType = ContentType,
                MD5 = MD5,
            };

            Assert.AreEqual(expectedAttachment, actualAttachment);
        }
    }
}
