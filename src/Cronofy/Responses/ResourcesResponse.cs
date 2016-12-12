namespace Cronofy.Responses
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the deserialization of a resources response.
    /// </summary>
    internal sealed class ResourcesResponse
    {
        /// <summary>
        /// Gets or sets the array of resources.
        /// </summary>
        /// <value>
        /// The array of resources.
        /// </value>
        [JsonProperty("resources")]
        public ResourceResponse[] Resources { get; set; }

        /// <summary>
        /// Class for the deserialization of a resource.
        /// </summary>
        internal sealed class ResourceResponse
        {
            /// <summary>
            /// Gets or sets the email of the resource.
            /// </summary>
            [JsonProperty("email")]
            public string Email { get; set; }

            /// <summary>
            /// Gets or sets the name of the resource.
            /// </summary>
            [JsonProperty("name")]
            public string Name { get; set; }

            /// <summary>
            /// Converts the response into a <see cref="Cronofy.Resource"/>.
            /// </summary>
            /// <returns>
            /// A <see cref="Cronofy.Resource"/> based upon the response.
            /// </returns>
            public Resource ToResource()
            {
                return new Resource
                {
                    Email = this.Email,
                    Name = this.Name
                };
            }
        }
    }
}
