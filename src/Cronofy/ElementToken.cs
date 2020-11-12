namespace Cronofy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A token for authorizing UI Elements.
    /// </summary>
    public sealed class ElementToken
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ElementToken" /> class.
        /// </summary>
        /// <param name="token">The token that is passed to Elements to authenticate them.</param>
        /// <param name="origin">The permitted Origin the token can be used with.</param>
        /// <param name="permissions">The array of permissions granted.</param>
        /// <param name="expiresIn">The number of seconds the token can be used for.</param>
        public ElementToken(string token, string origin, string[] permissions, int expiresIn)
        {
            this.Token = token;
            this.Origin = origin;
            this.Permissions = permissions;
            this.ExpiresIn = expiresIn;
        }

        /// <summary>
        /// Gets the array of permissions granted.
        /// </summary>
        /// <value>The array of permissions granted.</value>
        public string[] Permissions { get; }

        /// <summary>
        /// Gets the permitted Origin the token can be used with.
        /// </summary>
        /// <value>The permitted Origin the token can be used with.</value>
        public string Origin { get; }

        /// <summary>
        /// Gets the token that is passed to Elements to authenticate them.
        /// </summary>
        /// <value>The token that is passed to Elements to authenticate them.</value>
        public string Token { get; }

        /// <summary>
        /// Gets the number of seconds the token can be used for.
        /// </summary>
        /// <value>The number of seconds the token can be used for.</value>
        public int ExpiresIn { get; }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is ElementToken token &&
                   this.Permissions.SequenceEqual(token.Permissions) &&
                   this.Origin == token.Origin &&
                   this.Token == token.Token &&
                   this.ExpiresIn == token.ExpiresIn;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 1736317884;
            hashCode = (hashCode * -1521134295) + EqualityComparer<string[]>.Default.GetHashCode(this.Permissions);
            hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(this.Origin);
            hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(this.Token);
            hashCode = (hashCode * -1521134295) + this.ExpiresIn.GetHashCode();
            return hashCode;
        }
    }
}
