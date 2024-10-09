namespace Cronofy.Responses
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the deserialization of a get availability rule response.
    /// </summary>
    internal sealed class GetAvailabilityRuleResponse
    {
        /// <summary>
        /// Gets or sets the availability rule.
        /// </summary>
        /// <value>
        /// The availability rule.
        /// </value>
        [JsonProperty("availability_rule")]
        public AvailabilityRuleResponse AvailabilityRule { get; set; }
    }
}
