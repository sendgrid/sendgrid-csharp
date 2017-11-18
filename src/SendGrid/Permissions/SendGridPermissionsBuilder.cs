namespace SendGrid.Permissions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Scopes;

    /// <summary>
    /// A builder for constructing a list of API Key permissions scopes
    /// </summary>
    public class SendGridPermissionsBuilder
    {
        /// <summary>
        /// The filters
        /// </summary>
        private readonly IList<Func<string, bool>> filters;

        /// <summary>
        /// Initializes a new instance of the <see cref="SendGridPermissionsBuilder"/> class.
        /// </summary>
        public SendGridPermissionsBuilder()
        {
            this.filters = new List<Func<string, bool>>();
            this.AddedScopes = new Dictionary<ISendGridPermissionScope, ScopeOptions>();
            this.AllScopesMap = new Dictionary<Type, ISendGridPermissionScope>
            {
                { typeof(AccessSettings), new AccessSettings() },
                { typeof(Alerts), new Alerts() },
                { typeof(ApiKeys), new ApiKeys() },
                { typeof(AsmGroups), new AsmGroups() },
                { typeof(Billing), new Billing() },
                { typeof(Browsers), new Browsers() },
                { typeof(Categories), new Categories() },
                { typeof(Clients), new Clients() },
                { typeof(Credentials), new Credentials() },
                { typeof(Devices), new Devices() },
                { typeof(EmailActivity), new EmailActivity() },
                { typeof(Geo), new Geo() },
                { typeof(IpManagement), new IpManagement() },
                { typeof(MailSettings), new MailSettings() },
                { typeof(Mail), new Mail() },
                { typeof(MailboxProviders), new MailboxProviders() },
                { typeof(MarketingCampaigns), new MarketingCampaigns() },
                { typeof(Newsletter), new Newsletter() },
                { typeof(PartnerSettings), new PartnerSettings() },
                { typeof(ScheduledSends), new ScheduledSends() },
                { typeof(Stats), new Stats() },
                { typeof(Subusers), new Subusers() },
                { typeof(Suppression), new Suppression() },
                { typeof(Suppressions), new Suppressions() },
                { typeof(Teammates), new Teammates() },
                { typeof(Templates), new Templates() },
                { typeof(Tracking), new Tracking() },
                { typeof(UserSettings), new UserSettings() },
                { typeof(Webhooks), new Webhooks() },
                { typeof(Whitelabel), new Whitelabel() },
            };
        }

        /// <summary>
        /// Gets the scope type map
        /// </summary>
        public IDictionary<Type, ISendGridPermissionScope> AllScopesMap { get; }

        /// <summary>
        /// Gets the scopes that have been added to this builder instance
        /// </summary>
        /// <value>
        /// The added scopes.
        /// </value>
        internal IDictionary<ISendGridPermissionScope, ScopeOptions> AddedScopes { get; }

        /// <summary>
        /// Builds the list of API Key scopes based on the permissions that have been added to this instance.
        /// </summary>
        /// <returns>A list of strings representing the scope names.</returns>
        public IEnumerable<string> Build()
        {
            var scopes = this.AddedScopes
                .SelectMany(x => x.Key.Build(x.Value))
                .ToList();

            foreach (var f in this.filters)
            {
                scopes.RemoveAll(x => f(x));
            }

            return scopes;
        }

        /// <summary>
        /// Adds the permissions for the specified <typeparamref name="TScope"/>
        /// </summary>
        /// <typeparam name="TScope">The type of the scope.</typeparam>
        /// <param name="options">The options.</param>
        /// <returns>The builder instance with the permissions added.</returns>
        public SendGridPermissionsBuilder AddPermissionsFor<TScope>(ScopeOptions options)
            where TScope : ISendGridPermissionScope
        {
            var scope = this.AllScopesMap[typeof(TScope)];

            if ((scope.IsMutuallyExclusive && this.AddedScopes.Any()) || this.AddedScopes.Any(x => x.Key.IsMutuallyExclusive))
            {
                throw new InvalidOperationException($"{scope.Name} permissions are mutually exclusive from all others. An API Key can either have {scope.Name} Permissions, or any other set of Permissions.");
            }

            this.AddedScopes[this.AllScopesMap[typeof(TScope)]] = options;
            return this;
        }

        /// <summary>
        /// Adds the permissions for the specified <typeparamref name="TScope"/> with all possible scopes requested
        /// </summary>
        /// <typeparam name="TScope">The type of the scope.</typeparam>
        /// <returns>The builder instance with the permissions added.</returns>
        public SendGridPermissionsBuilder AddPermissionsFor<TScope>()
            where TScope : SendGridPermissionScope
        {
            return this.AddPermissionsFor<TScope>(ScopeOptions.All);
        }

        /// <summary>
        /// Adds the filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The builder instance with the scope added.</returns>
        public SendGridPermissionsBuilder Exclude(Func<string, bool> filter)
        {
            this.filters.Add(filter);
            return this;
        }
    }
}
