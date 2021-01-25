namespace SendGrid.Permissions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// A builder for constructing a list of API Key permissions scopes
    /// </summary>
    public class SendGridPermissionsBuilder
    {
        /// <summary>
        /// The filters
        /// </summary>
        private readonly IList<Func<string, bool>> excludeFilters;

        /// <summary>
        /// Gets the scopes that have been added to this builder instance
        /// </summary>
        /// <value>
        /// The added scopes.
        /// </value>
        private IDictionary<ISendGridPermissionScope, ScopeOptions> addedPermissions;


        /// <summary>
        /// Initializes a new instance of the <see cref="SendGridPermissionsBuilder"/> class.
        /// </summary>
        public SendGridPermissionsBuilder()
        {
            this.excludeFilters = new List<Func<string, bool>>();
            this.addedPermissions = new Dictionary<ISendGridPermissionScope, ScopeOptions>();
        }


        /// <summary>
        /// Builds the list of API Key scopes based on the permissions that have been added to this instance.
        /// </summary>
        /// <returns>A list of strings representing the scope names.</returns>
        public IEnumerable<string> Build()
        {
            var scopes = this.addedPermissions
                .SelectMany(x => x.Key.Build(x.Value))
                .ToList();

            foreach (var f in this.excludeFilters)
            {
                scopes.RemoveAll(x => f(x));
            }

            return scopes.Distinct().ToArray();
        }

        /// <summary>
        /// Adds the permissions for the specified <typeparamref name="TScope"/>
        /// </summary>
        /// <typeparam name="TScope">The type of the scope.</typeparam>
        /// <param name="options">The o.</param>
        /// <returns>The builder instance with the permissions added.</returns>
        public SendGridPermissionsBuilder AddPermissionsFor<TScope>(ScopeOptions options)
            where TScope : ISendGridPermissionScope, new()
        {
            var scope = new TScope();

            if ((scope.IsMutuallyExclusive && this.addedPermissions.Any()) || this.addedPermissions.Any(x => x.Key.IsMutuallyExclusive))
            {
                throw new InvalidOperationException($"{scope.Name} permissions are mutually exclusive from all others. An API Key can either have {scope.Name} Permissions, or any other set of Permissions.");
            }

            this.addedPermissions[scope] = options;
            return this;
        }

        /// <summary>
        /// Adds the permissions for the specified <typeparamref name="TScope"/> with all possible scopes requested
        /// </summary>
        /// <typeparam name="TScope">The type of the scope.</typeparam>
        /// <returns>The builder instance with the permissions added.</returns>
        public SendGridPermissionsBuilder AddPermissionsFor<TScope>()
            where TScope : ISendGridPermissionScope, new()
        {
            return this.AddPermissionsFor<TScope>(ScopeOptions.All);
        }

        /// <summary>
        /// Filters the emitted scopes based on the scopes available to the current API key.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The builder instance with the filter applied.</returns>
        public async Task<SendGridPermissionsBuilder> FilterByCurrentApiKeyAsync(ISendGridClient client, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await client.RequestAsync(SendGridClient.Method.GET, null, null, "scopes", cancellationToken);
            var body = await response.DeserializeResponseBodyAsync(response.Body);
            var userScopesJArray = (body["scopes"] as JArray);
            var includedScopes = userScopesJArray.Values<string>().ToArray();
            this.Exclude(scope => !includedScopes.Contains(scope));
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
    }
}
