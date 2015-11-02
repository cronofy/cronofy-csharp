namespace Cronofy.Responses
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface for a response that is part of a paged result set.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the items within the paged result set.
    /// </typeparam>
    internal interface IPagedResultsResponse<T>
    {
        /// <summary>
        /// Gets the paging information for the response.
        /// </summary>
        /// <value>
        /// The paging information for the response.
        /// </value>
        PagesResponse Pages { get; }

        /// <summary>
        /// Gets the results from the page.
        /// </summary>
        /// <returns>
        /// The results from the page.
        /// </returns>
        IEnumerable<T> GetResults();
    }
}
