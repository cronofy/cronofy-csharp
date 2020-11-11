using System.Collections.Generic;
using Newtonsoft.Json;

/// <summary>
/// A request for an Element Token.
/// </summary>
public sealed class ElementTokenRequest
{
    /// <summary>
    /// Gets or sets the array of permissions the token will be granted.
    /// </summary>
    /// <value>The array of permissions the token will be granted.</value>
    [JsonProperty("permissions")]
    public IEnumerable<string> Permissions { get; set; }

    /// <summary>
    /// Gets or sets the array of subs to identify the accounts the token is allowed to access.
    /// For Elements such as Agenda and CalendarSync, this will be one sub, for Elements such as the Slot Picker and Availability Viewer there may be many.
    /// </summary>
    /// <value>
    /// The array of subs to identify the accounts the token is allowed to access.
    /// For Elements such as Agenda and CalendarSync, this will be one sub, for Elements such as the Slot Picker and Availability Viewer there may be many.
    /// </value>
    [JsonProperty("subs")]
    public IEnumerable<string> Subs { get; set; }

    /// <summary>
    /// Gets or sets the Origin of the application where the Element will be used.
    /// </summary>
    /// <value>The Origin of the application where the Element will be used.</value>
    [JsonProperty("origin")]
    public string Origin { get; set; }

    /// <summary>
    /// Gets the version of the request payload.
    /// </summary>
    /// <value>
    /// The version of the request payload.
    /// </value>
    [JsonProperty("version")]
    public string Version { get; } = "1";
}
