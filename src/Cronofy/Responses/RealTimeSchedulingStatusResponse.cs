namespace Cronofy.Responses
{
    using System;
    using Newtonsoft.Json;

    internal class RealTimeSchedulingStatusResponse
    {
        [JsonProperty("real_time_scheduling")]
        public RealTimeSequencingStatusResponseContent RealTimeScheduling { get; set; }

        public RealTimeSchedulingLinkStatus ToRealTimeSchedulingLinkStatus()
        {
            return new RealTimeSchedulingLinkStatus
            {
                RealTimeSchedulingId = this.RealTimeScheduling.RealTimeSchedulingId,
                Url = this.RealTimeScheduling.Url,
                Status = (RealTimeSchedulingLinkStatus.LinkStatus)Enum.Parse(typeof(RealTimeSchedulingLinkStatus.LinkStatus), this.RealTimeScheduling.Status, true),
                Event = this.RealTimeScheduling.Event.ToEvent(),
            };
        }

        internal class RealTimeSequencingStatusResponseContent
        {
            [JsonProperty("real_time_scheduling_id")]
            public string RealTimeSchedulingId { get; set; }

            [JsonProperty("url")]
            public string Url { get; set; }

            [JsonProperty("status")]
            public string Status { get; set; }

            [JsonProperty("event")]
            public ReadEventsResponse.EventResponse Event { get; set; }
        }
    }
}
