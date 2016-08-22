using Newtonsoft.Json;

namespace Cronofy.Responses
{
    internal sealed class ElevatedPermissionsResponse
    {
        [JsonProperty("permission")]
        public PermissionsRequestResponse PermissionsRequest { get; set; }

        internal sealed class PermissionsRequestResponse
        {
            [JsonProperty("url")]
            public string Url { get; set; }
        }

        public ElevatedPermissions ToElevatedPermissions()
        {
            return new ElevatedPermissions()
            {
                Url = this.PermissionsRequest.Url
            };
        }
    }
}