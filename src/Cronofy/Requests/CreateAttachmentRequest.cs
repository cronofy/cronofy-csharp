namespace Cronofy.Requests
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the serialization of a create attachment request.
    /// </summary>
    public sealed class CreateAttachmentRequest
    {
        /// <summary>
        /// Gets or sets the summary of the attachment to be created.
        /// </summary>
        /// <value>
        /// The summary of the attachment to be created.
        /// </value>
        [JsonProperty("attachment")]
        public AttachmentSummary Attachment { get; set; }

        /// <summary>
        /// Gets or sets the list of subscriptions to be added to the attachment.
        /// </summary>
        /// <value>
        /// The list of subscriptions to be added to the attachment.
        /// </value>
        [JsonProperty("subscriptions")]
        public IEnumerable<RequestSubscription> Subscriptions { get; set; }

        /// <summary>
        /// Class for the serialization of attachment summary.
        /// </summary>
        public sealed class AttachmentSummary
        {
            /// <summary>
            /// Gets or sets the file name for the attachment.
            /// </summary>
            /// <value>
            /// The file name for the attachment.
            /// </value>
            [JsonProperty("file_name")]
            public string FileName { get; set; }

            /// <summary>
            /// Gets or sets the MIME content type for the attachment.
            /// </summary>
            /// <value>
            /// The MIME content type for the attachment.
            /// </value>
            [JsonProperty("content_type")]
            public string ContentType { get; set; }

            /// <summary>
            /// Gets or sets the Base64-encoded content of the attachment.
            /// </summary>
            /// <value>
            /// The Base64-encoded content of the attachment.
            /// </value>
            [JsonProperty("base64_content")]
            public string Base64Content { get; set; }
        }

        /// <summary>
        /// Class for the serialization of attachment subscriptions.
        /// </summary>
        public sealed class RequestSubscription
        {
            /// <summary>
            /// Gets or sets the type of the subscription.
            /// </summary>
            /// <value>
            /// The type of the subscription.
            /// </value>
            [JsonProperty("type")]
            public string Type { get; set; }

            /// <summary>
            /// Gets or sets the destination URI Cronofy will call when the subscription is triggered.
            /// </summary>
            /// <value>
            /// The destination URI Cronofy will call when the subscription is triggered.
            /// </value>
            [JsonProperty("uri")]
            public string Uri { get; set; }

            /// <summary>
            /// Gets or sets the interactions subscribed to.
            /// </summary>
            /// <value>
            /// The interactions subscribed to.
            /// </value>
            [JsonProperty("interactions")]
            public IEnumerable<Interaction> Interactions { get; set; }

            /// <summary>
            /// Class for the serialization of event subscription interactions.
            /// </summary>
            public sealed class Interaction
            {
                /// <summary>
                /// Gets or sets the type of the interaction.
                /// </summary>
                /// <value>
                /// The type of the interaction.
                /// </value>
                [JsonProperty("type")]
                public string Type { get; set; }
            }
        }
    }
}
