namespace Cronofy.Responses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the deserialization of an availability response.
    /// </summary>
    internal sealed class AttachmentResponse
    {
        /// <summary>
        /// Gets or sets the available periods of the response.
        /// </summary>
        /// <value>
        /// The available periods of the response.
        /// </value>
        [JsonProperty("attachment")]
        public AttachmentSummary Attachment { get; set; }

        /// <summary>
        /// Converts the response into an attachment.
        /// </summary>
        /// <returns>The response as an attachment.</returns>
        public Attachment ToAttachment()
        {
            return new Attachment
            {
                AttachmentId = this.Attachment.AttachmentId,
                FileName = this.Attachment.FileName,
                ContentType = this.Attachment.ContentType,
                MD5 = this.Attachment.MD5,
            };
        }

        /// <summary>
        /// Class for the serialization of attachment summary.
        /// </summary>
        internal sealed class AttachmentSummary
        {
            /// <summary>
            /// Gets or sets the id of the attachment.
            /// </summary>
            /// <value>
            /// The id of the attachment.
            /// </value>
            [JsonProperty("attachment_id")]
            public string AttachmentId { get; set; }

            /// <summary>
            /// Gets or sets the name of the attachment file.
            /// </summary>
            /// <value>
            /// The name of the attachment file.
            /// </value>
            [JsonProperty("file_name")]
            public string FileName { get; set; }

            /// <summary>
            /// Gets or sets the MIME content type of the attachment.
            /// </summary>
            /// <value>
            /// The MIME content type of the attachment.
            /// </value>
            [JsonProperty("content_type")]
            public string ContentType { get; set; }

            /// <summary>
            /// Gets or sets the MD5 hash of the attachment file content.
            /// </summary>
            /// <value>
            /// The MD5 hash of the attachment file content.
            /// </value>
            [JsonProperty("md5")]
            public string MD5 { get; set; }
        }
    }
}
