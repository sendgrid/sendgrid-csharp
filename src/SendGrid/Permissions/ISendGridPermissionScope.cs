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
        /// Gets the scopes asscociated with this permission.
        /// </summary>
        /// <value>
        /// The scopes.
        /// </value>
        IEnumerable<string> Scopes { get; }
    }
}
