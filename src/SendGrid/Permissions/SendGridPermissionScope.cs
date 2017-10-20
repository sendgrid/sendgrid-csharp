
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
            this.AllowedOptions = allowedOptions.ToArray();
        }

        public string Name { get; }

        public string[] AllowedOptions { get; protected set; }

        public IEnumerable<ISendGridPermissionScope> SubScopes { get; internal set; }

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
