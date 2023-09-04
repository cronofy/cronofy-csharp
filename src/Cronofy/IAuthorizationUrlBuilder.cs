namespace Cronofy
{
    using System.Collections.Generic;

    /// <summary>
    /// Builder for creating authorization URLs.
    /// </summary>
    public interface IAuthorizationUrlBuilder
    {
        /// <summary>
        /// Generates an authorization URL based on the current state of the
        /// builder.
        /// </summary>
        /// <returns>
        /// An authorization URL based on the current state of the builder.
        /// </returns>
        string Build();

        /// <summary>
        /// Sets the scope the authorization URL will request from the user.
        /// </summary>
        /// <param name="scope">
        /// The scope to request from the user, must not be empty.
        /// </param>
        /// <returns>
        /// A reference to the builder.
        /// </returns>
        /// <exception cref="System.ArgumentException">
        /// Thrown if <paramref name="scope"/> is empty.
        /// </exception>
        IAuthorizationUrlBuilder Scope(params string[] scope);

        /// <summary>
        /// Sets the scope the authorization URL will request from the user.
        /// </summary>
        /// <param name="scope">
        /// The scope to request from the user, must not be empty.
        /// </param>
        /// <returns>
        /// A reference to the builder.
        /// </returns>
        /// <exception cref="System.ArgumentException">
        /// Thrown if <paramref name="scope"/> is empty.
        /// </exception>
        IAuthorizationUrlBuilder Scope(IEnumerable<string> scope);

        /// <summary>
        /// Sets the state to be passed through the authorization process.
        /// </summary>
        /// <param name="state">
        /// The state to be passed through the authorization process, must
        /// not be null or empty.
        /// </param>
        /// <returns>
        /// A reference to the builder.
        /// </returns>
        /// <exception cref="System.ArgumentException">
        /// Thrown if <paramref name="state"/> is null or empty.
        /// </exception>
        IAuthorizationUrlBuilder State(string state);

        /// <summary>
        /// Sets the avoid linking parameter for the OAuth authorization
        /// process.
        /// </summary>
        /// <param name="avoidLinking">
        /// If set to <c>true</c>, avoid linking calendars during the OAuth
        /// authorization process.
        /// </param>
        /// <returns>
        /// A reference to the builder.
        /// </returns>
        IAuthorizationUrlBuilder AvoidLinking(bool avoidLinking);

        /// <summary>
        /// Sets the URL to be an Enterprise Connect OAuth authorization
        /// URL.
        /// </summary>
        /// <returns>
        /// A reference to the builder.
        /// </returns>
        IAuthorizationUrlBuilder EnterpriseConnect();

        /// <summary>
        /// Sets the scope the authorization URL will request from the Enterprise
        /// Connect user.
        /// </summary>
        /// <param name="scope">
        /// The scope to request from the Enterprise Connect user, must not be empty.
        /// </param>
        /// <returns>
        /// A reference to the builder.
        /// </returns>
        /// <exception cref="System.ArgumentException">
        /// Thrown if <paramref name="scope"/> is empty.
        /// </exception>
        IAuthorizationUrlBuilder EnterpriseConnectScope(params string[] scope);

        /// <summary>
        /// Sets the scope the authorization URL will request from the Enterprise
        /// Connect user.
        /// </summary>
        /// <param name="scope">
        /// The scope to request from the Enterprise Connect user, must not be empty.
        /// </param>
        /// <returns>
        /// A reference to the builder.
        /// </returns>
        /// <exception cref="System.ArgumentException">
        /// Thrown if <paramref name="scope"/> is empty.
        /// </exception>
        IAuthorizationUrlBuilder EnterpriseConnectScope(IEnumerable<string> scope);

        /// <summary>
        /// Sets the link token parameter for the OAuth authorization
        /// process.
        /// </summary>
        /// <param name="linkToken">
        /// The link token to use for the OAuth authorization process.
        /// </param>
        /// <returns>
        /// A reference to the builder.
        /// </returns>
        IAuthorizationUrlBuilder LinkToken(string linkToken);

        /// <summary>
        /// Sets the provider name parameter for the OAuth authorization
        /// process.
        /// </summary>
        /// <param name="providerName">
        /// The provider name to use for the OAuth authorization process.
        /// </param>
        /// <returns>
        /// A reference to the builder.
        /// </returns>
        IAuthorizationUrlBuilder ProviderName(string providerName);
    }
}
