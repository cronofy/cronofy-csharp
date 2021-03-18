namespace Cronofy.Test.CronofyAccountClientTests
{
    using System.Collections.Generic;
    using NUnit.Framework;

    internal sealed class GetChannels : Base
    {
        [Test]
        public void CanGetChannels()
        {
            this.Http.Stub(
                HttpGet
                    .Url("https://api.cronofy.com/v1/channels")
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .ResponseCode(200)
                    .ResponseBody(@"{
  ""channels"": [
    {
      ""channel_id"": ""chn_54cf7c7cb4ad4c1027000001"",
      ""callback_url"": ""https://example.com/callback"",
      ""filters"": {}
    }
  ]
}"));

            var channels = this.Client.GetChannels();

            Assert.AreEqual(
                new List<Channel>
                {
                    new Channel
                    {
                        Id = "chn_54cf7c7cb4ad4c1027000001",
                        CallbackUrl = "https://example.com/callback",
                        Filters = new Channel.ChannelFilters(),
                    },
                },
                channels);
        }
    }
}
