namespace SendGrid.Permissions
{
    using System.Collections.Generic;
    using System.Linq;

    /// <inheritdoc />
    /// <summary>
    /// Represents an API Key permission scope
    /// </summary>
    public class SendGridPermissionScope : ISendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SendGridPermissionScope"/> class.
        /// </summary>
        /// <param name="name">The name of the scope</param>
        internal SendGridPermissionScope(string name)
        {
            Name = name;
        }

        /// <inheritdoc/>
        public string Name { get; }

        /// <inheritdoc/>
        public IEnumerable<string> Scopes { get; internal set; }

        /// <inheritdoc/>
        public bool IsMutuallyExclusive { get; protected internal set; }
    }
}
