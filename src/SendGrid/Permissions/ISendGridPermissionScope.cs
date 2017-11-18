namespace SendGrid.Permissions
{
    using System.Collections.Generic;

    /// <summary>
    /// A contract for a API Key permission scope
    /// </summary>
    public interface ISendGridPermissionScope
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is mutually exclusive.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is mutually exclusive; otherwise, <c>false</c>.
        /// </value>
        bool IsMutuallyExclusive { get; }

        /// <summary>
        /// Gets the sub scopes.
        /// </summary>
        /// <value>
        /// The sub scopes.
        /// </value>
        IEnumerable<ISendGridPermissionScope> SubScopes { get; }

        /// <summary>
        /// Gets the allowed options.
        /// </summary>
        /// <value>
        /// The allowed options.
        /// </value>
        ScopeOptions AllowedOptions { get; }

        /// <summary>
        /// Builds the specified options.
        /// </summary>
        /// <param name="requestedOptions">The options.</param>
        /// <param name="prefix">The prefix.</param>
        /// <returns>The list of scopes for this permission based on the <paramref name="requestedOptions"/> with an optional <paramref name="prefix"/></returns>
        IEnumerable<string> Build(ScopeOptions requestedOptions, string prefix = null);
    }
}
