using System;
using NUnit.Framework;

namespace Cronofy.Test.CronofyAccountClientTests
{
    internal sealed class CloseChannel : Base
    {
        [Test]
        public void CanCloseChannel()
        {
            const string channelId = "chn_54cf7c7cb4ad4c1027000001";

            Http.Stub(
                HttpDelete
                    .Url("https://api.cronofy.com/v1/channels/" + channelId)
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .ResponseCode(202)
            );

            Client.CloseChannel(channelId);
        }
    }
}
