namespace Cronofy.Responses
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the deserialization of an account response.
    /// </summary>
    internal sealed class AccountResponse
    {
        /// <summary>
        /// Gets or sets the details of the account.
        /// </summary>
        /// <value>
        /// The details of the account.
        /// </value>
        [JsonProperty("account")]
        public AccountDetailResponse Account { get; set; }

        /// <summary>
        /// Converts the response into a <see cref="Cronofy.Account"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="Cronofy.Account"/> based upon the response.
        /// </returns>
        public Account ToAccount()
        {
            if (this.Account == null)
            {
                return null;
            }

            return this.Account.ToAccount();
        }

        /// <summary>
        /// Class for the deserialization of an account response.
        /// </summary>
        internal sealed class AccountDetailResponse
        {
            /// <summary>
            /// Gets or sets the ID of the account.
            /// </summary>
            /// <value>
            /// The ID of the account.
            /// </value>
            [JsonProperty("account_id")]
            public string AccountId { get; set; }

            /// <summary>
            /// Gets or sets the email of the account.
            /// </summary>
            /// <value>
            /// The email of the account.
            /// </value>
            [JsonProperty("email")]
            public string Email { get; set; }

            /// <summary>
            /// Gets or sets the name of the account.
            /// </summary>
            /// <value>
            /// The name of the account.
            /// </value>
            [JsonProperty("name")]
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the default time zone identifier of the account.
            /// </summary>
            /// <value>
            /// The default time zone identifier of the account.
            /// </value>
            [JsonProperty("default_tzid")]
            public string DefaultTimeZoneId { get; set; }

            /// <summary>
            /// Gets or sets the scopes granted for the account.
            /// </summary>
            /// <value>
            /// The scopes granted for the account.
            /// </value>
            [JsonProperty("scope")]
            public string Scope { get; set; }

            /// <summary>
            /// Converts the response into a <see cref="Cronofy.Account"/>.
            /// </summary>
            /// <returns>
            /// A <see cref="Cronofy.Account"/> based upon the response.
            /// </returns>
            public Account ToAccount()
            {
                return new Account
                {
                    Id = this.AccountId,
                    Email = this.Email,
                    Name = this.Name,
                    DefaultTimeZoneId = this.DefaultTimeZoneId,
                    Scope = this.Scope.Split(' '),
                };
            }
        }
    }
}
