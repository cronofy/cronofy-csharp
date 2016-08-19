namespace Cronofy.Test.CronofyAccountClientTests
{
    using NUnit.Framework;

    internal sealed class ElevatedPermissions : Base
    {
        [Test]
        public void CanRequestElevatedPermissions()
        {
            const string CalendarId = "cal_102324034530";
            const string PermissionLevel = "unrestricted";
            const string RedirectUri = "http://example.local/redirect";

            var builder = new ElevatedPermissionsBuilder()
                .RedirectUri(RedirectUri)
                .AddCalendarPermission(CalendarId, PermissionLevel);


            Http.Stub(
                HttpDelete
                    .Url("https://api.cronofy.com/v1/permissions")
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBodyFormat(@"{{
                        ""permissions_request"": {{
                            ""redirect_url"":""http://example.local/redirect"",
                            ""permissions"": [ ""calendar_id"":""cal_102324034530"", ""permission_level"":""unrestricted"" ]
                        }}
					}}")
            		.ResponseBody(@"{{ ""permissions_request"": {{ ""url"": ""http://example.local/response"" }} }}")
                    .ResponseCode(202)
            );

            var result = Client.ElevatedPermissions(builder);

            Assert.NotNull(result);
            Assert.AreEqual(result.Url, "http://example.local/response");
        }
    }
}