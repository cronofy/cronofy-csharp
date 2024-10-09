namespace Cronofy.Responses
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the deserialization of a list availability rules response.
    /// </summary>
    internal sealed class ListAvailabilityRulesResponse
    {
        /// <summary>
        /// Gets or sets the list of discovered availability rules.
        /// </summary>
        /// <value>
        /// The list of discovered availability rules.
        /// </value>
        [JsonProperty("availability_rules")]
        public IEnumerable<AvailabilityRuleResponse> AvailabilityRules { get; set; }
    }
}
