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

        internal sealed class AttachmentSummary
        {
            [JsonProperty("attachment_id")]
            public string AttachmentId { get; set; }

            [JsonProperty("file_name")]
            public string FileName { get; set; }

            [JsonProperty("content_type")]
            public string ContentType { get; set; }

            [JsonProperty("md5")]
            public string MD5 { get; set; }
        }

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
    }
}
