namespace Cronofy.Responses
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the deserialization of an upsert availability rules response.
    /// </summary>
    internal sealed class UpsertAvailabilityRuleResponse
    {
        /// <summary>
        /// Gets or sets the created or updated availability rule.
        /// </summary>
        /// <value>
        /// The created or updated availability rule.
        /// </value>
        [JsonProperty("availability_rule")]
        public AvailabilityRuleResponse AvailabilityRule { get; set; }
    }
}
