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
        /// Gets or sets the weekly recurring periods for the availability rule.
        /// </summary>
        /// <value>
        /// The weekly recurring periods for the availability rule.
        /// </value>
        [JsonProperty("availability_rules")]
        public IEnumerable<AvailabilityRuleResponse> AvailabilityRules { get; set; }
    }
}
