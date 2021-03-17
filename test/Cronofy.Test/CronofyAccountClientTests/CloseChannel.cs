namespace Cronofy.Test.CronofyAccountClientTests
{
    using NUnit.Framework;

    internal sealed class CloseChannel : Base
    {
        [Test]
        public void CanCloseChannel()
        {
            const string channelId = "chn_54cf7c7cb4ad4c1027000001";

            this.Http.Stub(
                HttpDelete
                    .Url("https://api.cronofy.com/v1/channels/" + channelId)
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .ResponseCode(202));

            this.Client.CloseChannel(channelId);
        }
    }
}
