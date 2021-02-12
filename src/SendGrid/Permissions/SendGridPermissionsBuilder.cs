namespace SendGrid.Permissions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A builder for constructing a list of API Key permissions scopes
    /// </summary>
    public class SendGridPermissionsBuilder
    {

        private readonly IList<Func<string, bool>> excludeFilters;

        private readonly List<string> includedScopes;

        private readonly IDictionary<ISendGridPermissionScope, ScopeOptions> addedPermissions;


        /// <summary>
        /// Initializes a new instance of the <see cref="SendGridPermissionsBuilder"/> class.
        /// </summary>
        public SendGridPermissionsBuilder()
        {
            this.excludeFilters = new List<Func<string, bool>>();
            this.includedScopes = new List<string>();
            this.addedPermissions = new Dictionary<ISendGridPermissionScope, ScopeOptions>();
        }


        /// <summary>
        /// Builds the list of API Key scopes based on the permissions that have been added to this instance.
        /// </summary>
        /// <returns>A list of strings representing the scope names.</returns>
        public IEnumerable<string> Build()
        {
            var scopes = this.addedPermissions
                .SelectMany(x => BuildScopes(x.Key, x.Value))
                .Concat(this.includedScopes)
                .ToList();


            foreach (var f in this.excludeFilters)
            {
                scopes.RemoveAll(x => f(x));
            }

            return scopes.Distinct().ToArray();
        }

        /// <summary>
        /// Adds the permissions for the specified <typeparamref name="TPermissionGroup"/>
        /// </summary>
        /// <typeparam name="TPermissionGroup">The permission group to add permissions for of the scope.</typeparam>
        /// <param name="options">The <see cref="ScopeOptions"/> indicating read-only or all scopes for a given permission group.</param>
        /// <returns>The builder instance with the permissions added.</returns>
        public SendGridPermissionsBuilder AddPermissionsFor<TPermissionGroup>(ScopeOptions options = ScopeOptions.All)
            where TPermissionGroup : ISendGridPermissionScope, new()
        {
            var permission = new TPermissionGroup();

            if ((permission.IsMutuallyExclusive && this.addedPermissions.Any()) || this.addedPermissions.Any(x => x.Key.IsMutuallyExclusive))
            {
                throw new InvalidOperationException($"{permission.Name} permissions are mutually exclusive from all others. An API Key can either have {permission.Name} Permissions, or any other set of Permissions.");
            }

            this.addedPermissions[permission] = options;
            return this;
        }

        /// <summary>
        /// Adds an exclusion filter the will not emit any scopes that the filter matches.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The builder instance with the exclusion filter applied.</returns>
        public SendGridPermissionsBuilder Exclude(Func<string, bool> filter)
        {
            this.excludeFilters.Add(filter);
            return this;
        }

        /// <summary>
        /// Adds the <paramref name="scopes"/> to the builder.
        /// </summary>
        /// <param name="scopes">The list of scopes to include.</param>
        /// <returns>The builder instance with the scopes included.</returns>
        public SendGridPermissionsBuilder Include(IEnumerable<string> scopes)
        {
            if (scopes is null || !scopes.Any())
            {
                return this;
            }

            this.includedScopes.AddRange(scopes);
            return this;
        }

        /// <summary>
        /// Adds the <paramref name="scopes"/> to the builder.
        /// </summary>
        /// <param name="scopes">The list of scopes to include.</param>
        /// <returns>The builder instance with the scopes included.</returns>
        public SendGridPermissionsBuilder Include(params string[] scopes)
        {
            if (scopes is null || !scopes.Any())
            {
                return this;
            }

            this.includedScopes.AddRange(scopes);
            return this;
        }

        /// <summary>
        /// Builds the specified options.
        /// </summary>
        /// <param name="permission">The permission group to build the scopes list for.</param>
        /// <param name="requestedOptions">The options.</param>        
        /// <returns>
        /// A final list of scopes to use for this permission filtered by the requested options
        /// </returns>
        private string[] BuildScopes(ISendGridPermissionScope permission, ScopeOptions requestedOptions)
        {
            if (requestedOptions == ScopeOptions.ReadOnly)
            {
                return permission.Scopes.Where(x => x.EndsWith(".read")).ToArray();
            }

            return permission.Scopes.ToArray();
        }
    }
}
