namespace Cronofy
{
    using System;

    /// <summary>
    /// Class for providing options to a revoke authorization request.
    /// </summary>
    public sealed class RevokeAuthorizationOptions
    {
        /// <summary>
        /// Gets or sets the sub of the OAuth authorization to be revoked.
        /// </summary>
        /// <value>
        /// The sub of the OAuth authorization to be revoked.
        /// </value>
        public string Sub { get; set; }

        /// <summary>
        /// Gets or sets the token of the OAuth authorization to be revoked.
        /// </summary>
        /// <value>
        /// The token of the OAuth authorization to be revoked.
        /// </value>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets whether PII erasure will be requested alongside the
        /// revocation.
        /// </summary>
        /// <value>
        /// Whether PII erasure will be requested alongside the revocation.
        /// </value>
        public bool? RequestPiiErasure { get; set; }
    }
}
