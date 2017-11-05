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
        /// The raw scopes to be added to the final list of scopes.
        /// </summary>
        private IList<string> rawScopes;

        /// <summary>
        /// Initializes a new instance of the <see cref="SendGridPermissionsBuilder"/> class.
        /// </summary>
        public SendGridPermissionsBuilder()
        {
            this.rawScopes = new List<string>();
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
                { typeof(Devices), new Devices() },
                { typeof(EmailActivity), new EmailActivity() },
                { typeof(Geo), new Geo() },
                { typeof(IpManagement), new IpManagement() },
                { typeof(MailSettings), new MailSettings() },
                { typeof(Mail), new Mail() },
                { typeof(MailboxProviders), new MailboxProviders() },
                { typeof(MarketingCampaigns), new MarketingCampaigns() },
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
                { typeof(Whitelabel), new Whitelabel() },
            };
        }

        /// <summary>
        /// The scopes
        /// </summary>
        internal IDictionary<ISendGridPermissionScope, ScopeOptions> AddedScopes { get; }

        /// <summary>
        /// The scope map
        /// </summary>
        public IDictionary<Type, ISendGridPermissionScope> AllScopesMap { get; }

        /// <summary>
        /// Builds the list of API Key scopes based on the permissions that have been added to this instance.
        /// </summary>
        /// <returns>A list of strings representing the scope names.</returns>
        public IEnumerable<string> Build()
        {
            var scopes = this.AddedScopes
                .SelectMany(x => x.Key.Build(x.Value))

            .Concat(this.rawScopes)
            .OrderBy(x => x);


            //.OrderBy(x => x);
            //.Select(x => new
            //{
            //    Name = x.Key.Name,
            //    Scopes = x.Key.Build(x.Value); //.OrderBy(s => s)
            //})

            //.se

            //.OrderBy(x => x.Name);
            return scopes;//scopes.SelectMany(x => x.Scopes);
        }

        /// <summary>
        /// Builds the list of API Key scopes based on the permissions that have been added to this instance using <paramref name="allowedScopes" /> as a whitelist filter.
        /// </summary>
        /// <param name="allowedScopes">The allowed scopes.</param>
        /// <returns>
        /// A list of strings representing the scope names.
        /// </returns>
        //public IEnumerable<string> Build(IEnumerable<string> allowedScopes)
        //{
        //    var builtScopes = this.Build().ToArray();
        //    var filteredScopes = allowedScopes.Join(
        //        builtScopes,
        //        allowed => allowed,
        //        built => built,
        //        (a, b) => a);
        //    return filteredScopes;
        //}

        /// <summary>
        /// Adds the permissions for the specified <typeparamref name="TScope"/>
        /// </summary>
        /// <typeparam name="TScope">The type of the scope.</typeparam>
        /// <param name="options">The options.</param>
        /// <returns>The builder instance with the permissions added.</returns>
        public SendGridPermissionsBuilder AddPermissionsFor<TScope>(ScopeOptions options)
            where TScope : SendGridPermissionScope
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
        /// Adds the scope.
        /// </summary>
        /// <param name="scope">The scope.</param>
        /// <returns>The builder instance with the scope added.</returns>
        public SendGridPermissionsBuilder AddScope(string scope)
        {
            this.rawScopes.Add(scope);
            return this;
        }
    }
}
