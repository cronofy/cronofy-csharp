namespace Cronofy.Requests
{
    using System;
    using Newtonsoft.Json;

    internal class DisableRealTimeSchedulingRequest
    {
        [JsonProperty("display_message")]
        public string DisplayMessage { get; set; }
    }
}
