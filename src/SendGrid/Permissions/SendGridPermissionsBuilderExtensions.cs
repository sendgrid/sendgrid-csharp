namespace SendGrid.Permissions
{
    using Scopes;

    /// <summary>
    /// Extension methods for creating permissions for common API key use cases
    /// </summary>
    public static class SendGridPermissionsBuilderExtensions
    {
        /// <summary>
        /// Creates the full access mail send permission list.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The builder instance with the permissions added.
        /// </returns>
        public static SendGridPermissionsBuilder CreateReadOnlyMailSend(this SendGridPermissionsBuilder builder)
        {
            return builder.AddPermissionsFor<Mail>(ScopeOptions.ReadOnly);
        }

        /// <summary>
        /// Creates the full access mail send permission list.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The builder instance with the permissions added.
        /// </returns>
        public static SendGridPermissionsBuilder CreateFullAccessMailSend(this SendGridPermissionsBuilder builder)
        {
            return builder.AddPermissionsFor<Mail>();
        }

        /// <summary>
        /// Creates the read-only alerts permission list.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The builder instance with the permissions added.
        /// </returns>
        public static SendGridPermissionsBuilder CreateReadOnlyAlerts(this SendGridPermissionsBuilder builder)
        {
            return builder.AddPermissionsFor<Alerts>(ScopeOptions.ReadOnly);
        }

        /// <summary>
        /// Creates the full access alerts permission list.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The builder instance with the permissions added.
        /// </returns>
        public static SendGridPermissionsBuilder CreateFullAccessAlerts(this SendGridPermissionsBuilder builder)
        {
            return builder.AddPermissionsFor<Alerts>();
        }       

        /// <summary>
        /// Creates the read-only suppressions permission list.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The builder instance with the permissions added.
        /// </returns>
        public static SendGridPermissionsBuilder CreateReadOnlySuppressions(this SendGridPermissionsBuilder builder)
        {
            builder.AddPermissionsFor<Suppressions>(ScopeOptions.ReadOnly);
            return builder;
        }

        /// <summary>
        /// Creates the full access suppressions permission list.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The builder instance with the permissions added.
        /// </returns>
        public static SendGridPermissionsBuilder CreateFullAccessSuppressions(this SendGridPermissionsBuilder builder)
        {
            builder.AddPermissionsFor<Suppressions>();
            return builder;
        }

        /// <summary>
        /// Creates the read-only Domain Authentication (formerly Whitelabel) permission list.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The builder instance with the permissions added.
        /// </returns>
        public static SendGridPermissionsBuilder CreateReadOnlyDomainAuthentication(this SendGridPermissionsBuilder builder)
        {
            builder.AddPermissionsFor<DomainAuthentication>(ScopeOptions.ReadOnly);
            return builder;
        }

        /// <summary>
        /// Creates the full access Domain Authentication (formerly Whitelabel) permission list.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The builder instance with the permissions added.
        /// </returns>
        public static SendGridPermissionsBuilder CreateFullAccessDomainAuthentication(this SendGridPermissionsBuilder builder)
        {
            builder.AddPermissionsFor<DomainAuthentication>();
            return builder;
        }

        /// <summary>
        /// Creates the read-only IP management permission list.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The builder instance with the permissions added.
        /// </returns>
        public static SendGridPermissionsBuilder CreateReadOnlyIpManagement(this SendGridPermissionsBuilder builder)
        {
            builder.AddPermissionsFor<IpManagement>(ScopeOptions.ReadOnly);
            builder.Exclude(x => x.StartsWith("ips.pools.ips."));
            return builder;
        }

        /// <summary>
        /// Creates the full access IP management permission list.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The builder instance with the permissions added.
        /// </returns>
        public static SendGridPermissionsBuilder CreateFullAccessIpManagement(this SendGridPermissionsBuilder builder)
        {
            builder.AddPermissionsFor<IpManagement>();
            builder.Exclude(x => x.StartsWith("ips.pools.ips.") && (x.EndsWith("update") || x.EndsWith("read")));
            builder.Exclude(x => x.StartsWith("ips.warmup.") && x.EndsWith("update"));
            return builder;
        }

        /// <summary>
        /// Creates the read-only templates permission list.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The builder instance with the permissions added.
        /// </returns>
        public static SendGridPermissionsBuilder CreateReadOnlyTemplates(this SendGridPermissionsBuilder builder)
        {
            builder.AddPermissionsFor<Templates>(ScopeOptions.ReadOnly);
            return builder;
        }

        /// <summary>
        /// Creates the full access templates permission list.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The builder instance with the permissions added.
        /// </returns>
        public static SendGridPermissionsBuilder CreateFullAccessTemplates(this SendGridPermissionsBuilder builder)
        {
            builder.AddPermissionsFor<Templates>();
            return builder;
        }

        /// <summary>
        /// Creates the read-only user.webhooks.parse permission list.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The builder instance with the permissions added.
        /// </returns>
        public static SendGridPermissionsBuilder CreateReadOnlyInboundParse(this SendGridPermissionsBuilder builder)
        {
            builder.AddPermissionsFor<Webhook>(ScopeOptions.ReadOnly);
            builder.Exclude(x => !x.StartsWith("user.webhooks.parse"));
            return builder;
        }

        /// <summary>
        /// Creates the full access user.webhooks.parse permission list.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The builder instance with the permissions added.
        /// </returns>
        public static SendGridPermissionsBuilder CreateFullAccessInboundParse(this SendGridPermissionsBuilder builder)
        {
            builder.AddPermissionsFor<Webhook>();
            builder.Exclude(x => !x.StartsWith("user.webhooks.parse"));
            return builder;
        }

        /// <summary>
        /// Creates the read-only mail_settings permission list.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The builder instance with the permissions added.
        /// </returns>
        public static SendGridPermissionsBuilder CreateReadOnlyMailSettings(this SendGridPermissionsBuilder builder)
        {
            builder.AddPermissionsFor<MailSettings>(ScopeOptions.ReadOnly);
            return builder;
        }

        /// <summary>
        /// Creates the full access mail_settings permission list.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The builder instance with the permissions added.
        /// </returns>
        public static SendGridPermissionsBuilder CreateFullAccessMailSettings(this SendGridPermissionsBuilder builder)
        {
            builder.AddPermissionsFor<MailSettings>();
            return builder;
        }

        /// <summary>
        /// Creates the read-only marketing_campaigns permission list.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The builder instance with the permissions added.
        /// </returns>
        public static SendGridPermissionsBuilder CreateReadOnlyMarketingCampaigns(this SendGridPermissionsBuilder builder)
        {
            builder.AddPermissionsFor<MarketingCampaigns>(ScopeOptions.ReadOnly);
            return builder;
        }

        /// <summary>
        /// Creates the full access marketing_campaigns permission list.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The builder instance with the permissions added.
        /// </returns>
        public static SendGridPermissionsBuilder CreateFullAccessMarketingCampaigns(this SendGridPermissionsBuilder builder)
        {
            builder.AddPermissionsFor<MarketingCampaigns>();
            builder.AddPermissionsFor<PartnerSettings>();
            builder.Exclude(x => x.StartsWith("partner_settings") && !x.EndsWith("new_relic.read"));
            return builder;
        }

        /// <summary>
        /// Creates the full admin permissions.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The builder instance with the permissions added.
        /// </returns>
        public static SendGridPermissionsBuilder CreateAdminPermissions(this SendGridPermissionsBuilder builder)
        {
            builder
                .AddPermissionsFor<ReverseDNS>()
                .AddPermissionsFor<ApiKeys>()
                .AddPermissionsFor<Alerts>()
                .AddPermissionsFor<AsmGroups>()
                .AddPermissionsFor<Categories>()
                .AddPermissionsFor<Clients>()
                .AddPermissionsFor<Credentials>()
                .AddPermissionsFor<IpManagement>()
                .AddPermissionsFor<Mail>()                
                .AddPermissionsFor<MailSettings>()
                .AddPermissionsFor<MarketingCampaigns>()
                .AddPermissionsFor<Newsletter>()
                .AddPermissionsFor<PartnerSettings>()
                .AddPermissionsFor<Stats>()
                .AddPermissionsFor<Subusers>()
                .AddPermissionsFor<Suppressions>()
                .AddPermissionsFor<Templates>()
                .AddPermissionsFor<Tracking>()                
                .AddPermissionsFor<UserSettings>()
                .AddPermissionsFor<ScheduledSends>()
                .AddPermissionsFor<Webhook>()                    
                .AddPermissionsFor<DomainAuthentication>();               

            return builder;
        }
    }
}
