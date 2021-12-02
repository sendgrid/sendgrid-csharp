namespace SendGrid.Permissions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A builder for constructing a list of API Key permissions scopes
    /// </summary>
    public sealed partial class SendGridPermissionsBuilder
    {
        private readonly List<Func<string, bool>> excludeFilters = new();

        private readonly List<string> addedScopes = new();

        private readonly HashSet<string> allScopes;

        /// <summary>
        /// Initializes a new instance of the <see cref="SendGridPermissionsBuilder"/> class.
        /// </summary>
        public SendGridPermissionsBuilder()
        {
            this.allScopes = new HashSet<string>(this.allPermissions.SelectMany(x => x.Value));
        }


        /// <summary>
        /// Builds the list of API Key scopes based on the permissions that have been added to this instance.
        /// </summary>
        /// <returns>A list of strings representing the scope names.</returns>
        public IEnumerable<string> Build()
        {
            var scopes = this.addedScopes.ToList();

            foreach (var f in this.excludeFilters)
            {
                scopes.RemoveAll(x => f(x));
            }

            return scopes.Distinct().ToArray();
        }

        /// <summary>
        /// Adds the permissions for the specified <paramref name="permission"/>
        /// </summary>
        /// <param name="permission">The permission group to add scopes for.</param>
        /// <param name="options">The <see cref="ScopeOptions"/> indicating read-only or all scopes for a given permission group.</param>
        /// <returns>The builder instance with the permissions added.</returns>
        public SendGridPermissionsBuilder AddPermissionsFor(SendGridPermission permission, ScopeOptions options = ScopeOptions.All)
        {
            var scopesToAdd = BuildScopes(permission, options);
            ThrowIfViolatesMutualExclusivity(scopesToAdd);
            this.addedScopes.AddRange(scopesToAdd);
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
            return Include(scopes.ToArray());
        }

        /// <summary>
        /// Adds the <paramref name="scopes"/> to the builder.
        /// </summary>
        /// <param name="scopes">The list of scopes to include.</param>
        /// <returns>The builder instance with the scopes included.</returns>
        public SendGridPermissionsBuilder Include(params string[]? scopes)
        {
            if (scopes is null || !scopes.Any())
            {
                return this;
            }

            ThrowIfViolatesMutualExclusivity(scopes);

            foreach (var scope in scopes)
            {
                if (!IsValidScope(scope))
                {
                    var ex = new InvalidOperationException($"The provided scope '{scope}' is not valid. See the API permissions docs for a list of valid scopes.")
                    {
                        HelpLink = "https://sendgrid.api-docs.io/v3.0/api-key-permissions/api-key-permissions"
                    };
                    throw ex;
                }
            }

            this.addedScopes.AddRange(scopes);
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
        private string[] BuildScopes(SendGridPermission permission, ScopeOptions requestedOptions)
        {
            if (requestedOptions == ScopeOptions.ReadOnly)
            {
                return allPermissions[permission].Where(x => x.EndsWith(".read")).ToArray();
            }

            return allPermissions[permission].ToArray();
        }

        private void ThrowIfViolatesMutualExclusivity(IEnumerable<string> scopes)
        {
            if ((scopes.Any(x => IsMutualyExclusive(x)) && this.addedScopes.Any(x => !IsMutualyExclusive(x)))
                ||
                (this.addedScopes.Any(x => IsMutualyExclusive(x)) && scopes.Any(x => !IsMutualyExclusive(x))))
            {
                throw new InvalidOperationException($"An API Key can either have billing permissions or any other set of permissions but not both.")
                {
                    HelpLink = "https://sendgrid.api-docs.io/v3.0/api-key-permissions/api-key-permissions#Billing"
                };
            }
        }

        private bool IsValidScope(string scope) => this.allScopes.Contains(scope);

        private bool IsMutualyExclusive(string? scope) => scope?.StartsWith("billing") ?? false;
    }
}
