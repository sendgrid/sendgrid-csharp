namespace SendGrid.Permissions
{
    using System.Collections.Generic;

    /// <summary>
    /// A contract for a API Key permission scope
    /// </summary>
    public interface ISendGridPermissionScope
    {
        string Name { get; }

        /// <summary>
        /// Gets the sub scopes.
        /// </summary>
        /// <value>
        /// The sub scopes.
        /// </value>
        IEnumerable<ISendGridPermissionScope> SubScopes { get; }

        /// <summary>
        /// Builds the specified options.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="prefix">The prefix.</param>
        /// <returns></returns>
        IEnumerable<string> Build(ScopeOptions options, string prefix = null);
    }
}
