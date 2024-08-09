namespace Cronofy
{
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the deserialization of an attachment summary
    /// response.
    /// </summary>
    public sealed class Attachment
    {
        /// <summary>
        /// Gets or sets the attachment ID.
        /// </summary>
        /// <value>
        /// The ID of the attachment.
        /// </value>
        [JsonProperty("attachment_id")]
        public string AttachmentId { get; set; }

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
        /// Gets or sets the MD5 hash of the attachment content.
        /// </summary>
        /// <value>
        /// The MD5 hash of the attachment content.
        /// </value>
        [JsonProperty("md5")]
        public string MD5 { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj is Attachment && this.Equals((Attachment)obj);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.AttachmentId.GetHashCode();
        }

        /// <summary>
        /// Determines whether the specified <see cref="Cronofy.Attachment"/>
        /// is equal to the current <see cref="Cronofy.Attachment"/>.
        /// </summary>
        /// <param name="other">
        /// The <see cref="Cronofy.Attachment"/> to compare with the current
        /// <see cref="Cronofy.Attachment"/>.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="Cronofy.Attachment"/> is
        /// equal to the current <see cref="Cronofy.Attachment"/>; otherwise,
        /// <c>false</c>.
        /// </returns>
        public bool Equals(Attachment other)
        {
            return this.AttachmentId == other.AttachmentId && this.FileName == other.FileName
             && this.ContentType == other.ContentType && this.MD5 == other.MD5;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format(
                "<{0} AttachmentId={1}, FileName={2}>",
                this.GetType(),
                this.AttachmentId,
                this.FileName);
        }
    }
}
