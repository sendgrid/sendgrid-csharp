namespace SendGrid.Permissions
{
    using System.Collections.Generic;
    using System.Linq;

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
            this.Name = name;
            this.AllowedOptions = ScopeOptions.AllCrud.ToArray();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SendGridPermissionScope"/> class.
        /// </summary>
        /// <param name="name">The name of the scope</param>
        /// <param name="allowedOptions">The allowed options e.g. create, delete, read, update</param>
        internal SendGridPermissionScope(string name, params string[] allowedOptions)
            : this(name)
        {
            this.AllowedOptions = allowedOptions;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SendGridPermissionScope"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="allowedOptions">The allowed options e.g. create, delete, read, update.</param>
        internal SendGridPermissionScope(string name, IEnumerable<string> allowedOptions)
         : this(name, allowedOptions.ToArray())
        {
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; }

        /// <summary>
        /// Gets the allowed options.
        /// </summary>
        /// <value>
        /// The allowed options.
        /// </value>
        public string[] AllowedOptions { get; internal set; }

        /// <summary>
        /// Gets the sub scopes.
        /// </summary>
        /// <value>
        /// The sub scopes.
        /// </value>
        public IEnumerable<ISendGridPermissionScope> SubScopes { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this scope can only appear in the admin API Key scopes.
        /// </summary>
        /// <value>
        /// <c>true</c> if [admin only]; otherwise, <c>false</c>.
        /// </value>
        public bool IsAdminOnly { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this instance is mutually exclusive.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is mutually exclusive; otherwise, <c>false</c>.
        /// </value>
        public bool IsMutuallyExclusive { get; internal set; }

        /// <summary>
        /// Builds the specified options.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="prefix">The prefix.</param>
        /// <returns>
        /// The list of scopes for this permission based on the <paramref name="options" /> with an optional <paramref name="prefix" />
        /// </returns>
        public IEnumerable<string> Build(ScopeOptions options, string prefix = null)
        {
            var setOptions = this.AllowedOptions.Join(options, allowed => allowed, set => set, (a, s) => a);

            var scopes = new List<string>();

            var name = prefix == null ? this.Name : $"{prefix}.{this.Name}";
            foreach (var so in setOptions)
            {
                scopes.Add($"{name}.{so}");
            }

            if (this.SubScopes != null && this.SubScopes.Any())
            {
                foreach (var subScope in this.SubScopes)
                {
                    scopes.AddRange(subScope.Build(options, name));
                }
            }

            return scopes;
        }
    }
}
