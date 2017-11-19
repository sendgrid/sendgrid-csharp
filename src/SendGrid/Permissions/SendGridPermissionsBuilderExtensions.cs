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
        /// Creates the read-only stats permission list.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The builder instance with the permissions added.
        /// </returns>
        public static SendGridPermissionsBuilder CreateReadOnlyStats(this SendGridPermissionsBuilder builder)
        {
            builder.AddPermissionsFor<EmailActivity>(ScopeOptions.ReadOnly);
            builder.AddPermissionsFor<Stats>(ScopeOptions.ReadOnly);
            builder.AddPermissionsFor<Devices>(ScopeOptions.ReadOnly);
            builder.AddPermissionsFor<Geo>(ScopeOptions.ReadOnly);
            builder.AddPermissionsFor<Browsers>(ScopeOptions.ReadOnly);
            builder.AddPermissionsFor<MailboxProviders>(ScopeOptions.ReadOnly);
            return builder;
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
        /// Creates the read-only whitelabel permission list.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The builder instance with the permissions added.
        /// </returns>
        public static SendGridPermissionsBuilder CreateReadOnlyWhitelabels(this SendGridPermissionsBuilder builder)
        {
            builder.AddPermissionsFor<Whitelabel>(ScopeOptions.ReadOnly);
            return builder;
        }

        /// <summary>
        /// Creates the full access whitelabel permission list.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The builder instance with the permissions added.
        /// </returns>
        public static SendGridPermissionsBuilder CreateFullAccessWhitelabels(this SendGridPermissionsBuilder builder)
        {
            builder.AddPermissionsFor<Whitelabel>();
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
            builder.AddPermissionsFor<Webhooks>(ScopeOptions.ReadOnly);
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
            builder.AddPermissionsFor<Webhooks>();
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
                .AddPermissionsFor<Alerts>()
                .AddPermissionsFor<AsmGroups>()
                .AddPermissionsFor<IpManagement>(ScopeOptions.ReadOnly)
                .Exclude(s => s.StartsWith("ips") && s != "ips.pools.ips.read")
                .AddPermissionsFor<Mail>()
                .AddPermissionsFor<EmailActivity>()
                .AddPermissionsFor<MailSettings>().Exclude(x => x == "mail_settings.read")
                .AddPermissionsFor<PartnerSettings>().Exclude(x => x == "partner_settings.read")
                .AddPermissionsFor<MarketingCampaigns>()
                .AddPermissionsFor<Tracking>().Exclude(x => x == "tracking_settings.read")
                .AddPermissionsFor<Webhooks>().Exclude(x =>
                    x == "user.webhooks.create" || x == "user.webhooks.delete" || x == "user.webhooks.read" ||
                    x == "user.webhooks.update")
                .AddPermissionsFor<Stats>()
                .AddPermissionsFor<Categories>()
                .AddPermissionsFor<Devices>()
                .AddPermissionsFor<Clients>()
                .AddPermissionsFor<Geo>()
                .AddPermissionsFor<MailboxProviders>()
                .AddPermissionsFor<Browsers>()
                .AddPermissionsFor<Templates>()
                .Exclude(x => x.StartsWith("templates.versions.activate") && !x.EndsWith("create"))
                .AddPermissionsFor<UserSettings>().Exclude(
                    x => x.StartsWith("user.account")
                         || x.StartsWith("user.credits")
                         || x.StartsWith("user.email")
                         || x.StartsWith("user.multifactor_authentication")
                         || x.StartsWith("user.password")
                         || x.StartsWith("user.profile")
                         || x.StartsWith("user.username"))
                .AddPermissionsFor<ApiKeys>()
                .AddPermissionsFor<ScheduledSends>()
                .AddPermissionsFor<AccessSettings>()
                .AddPermissionsFor<Whitelabel>()
                .AddPermissionsFor<Suppression>().Exclude(
                    x => x.StartsWith("suppression") && (
                             x.Contains("bounces")
                             || x.Contains("invalid_emails")
                             || x.Contains("spam_reports")
                             || x.Contains("unsubscribes")
                             || x.Contains("blocks")));

            return builder;
        }
    }
}
