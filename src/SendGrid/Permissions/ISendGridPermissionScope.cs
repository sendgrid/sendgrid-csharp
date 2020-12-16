namespace SendGrid.Permissions
{
    using System.Collections.Generic;

    /// <summary>
    /// A contract for a API Key permission scope
    /// </summary>
    public interface ISendGridPermissionScope
    {
        /// <summary>
        /// Gets the name of this permission
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
        /// Gets the scopes asscociated with this permission.
        /// </summary>
        /// <value>
        /// The scopes.
        /// </value>
        IEnumerable<string> Scopes { get; }

        /// <summary>
        /// Builds the specified options.
        /// </summary>
        /// <param name="requestedOptions">The options.</param>        
        /// <returns>
        /// A final list of scopes to use for this permission filtered by the requested options
        /// </returns>
        IEnumerable<string> Build(ScopeOptions requestedOptions);
    }
}
