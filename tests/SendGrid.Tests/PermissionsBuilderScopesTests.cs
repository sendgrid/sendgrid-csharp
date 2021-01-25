namespace SendGrid.Tests
{
    using System.Linq;
    using Permissions;
    using SendGrid.Permissions.Scopes;
    using Xunit;

    public class PermissionsBuilderScopesTests
    {
        [Fact]
        public void CreateAlerts()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.AddPermissionsFor<Alerts>();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 4, "Alerts should have 4 scopes");
            Assert.Contains("alerts.create", scopes);
            Assert.Contains("alerts.delete", scopes);
            Assert.Contains("alerts.read", scopes);
            Assert.Contains("alerts.update", scopes);
        }

        [Fact]
        public void CreateApiKeys()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.AddPermissionsFor<ApiKeys>();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 4, "Api Keys should have 4 scopes");
            Assert.Contains("api_keys.create", scopes);
            Assert.Contains("api_keys.delete", scopes);
            Assert.Contains("api_keys.read", scopes);
            Assert.Contains("api_keys.update", scopes);
        }

        [Fact]
        public void CreateAsmGroups()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.AddPermissionsFor<AsmGroups>();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 4, "Asm Groups should have 4 scopes");
            Assert.Contains("asm.groups.create", scopes);
            Assert.Contains("asm.groups.delete", scopes);
            Assert.Contains("asm.groups.read", scopes);
            Assert.Contains("asm.groups.update", scopes);
        }

        [Fact]
        public void CreateBilling()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.AddPermissionsFor<Billing>();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 4, "Billing should have 4 scopes");
            Assert.Contains("billing.create", scopes);
            Assert.Contains("billing.delete", scopes);
            Assert.Contains("billing.read", scopes);
            Assert.Contains("billing.update", scopes);
        }

        [Fact]
        public void CreateCategories()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.AddPermissionsFor<Categories>();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 6, "Categories should have 6 scopes");
            Assert.Contains("categories.create", scopes);
            Assert.Contains("categories.delete", scopes);
            Assert.Contains("categories.read", scopes);
            Assert.Contains("categories.update", scopes);
            Assert.Contains("categories.stats.read", scopes);
            Assert.Contains("categories.stats.sums.read", scopes);
        }

        [Fact]
        public void CreateStats()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.AddPermissionsFor<Stats>();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 12, "read-only stats should have 7 scopes");
            Assert.Contains("email_activity.read", scopes);
            Assert.Contains("stats.read", scopes);
            Assert.Contains("stats.global.read", scopes);
            Assert.Contains("browsers.stats.read", scopes);
            Assert.Contains("devices.stats.read", scopes);
            Assert.Contains("geo.stats.read", scopes);
            Assert.Contains("mailbox_providers.stats.read", scopes);
            Assert.Contains("clients.desktop.stats.read", scopes);
            Assert.Contains("clients.phone.stats.read", scopes);
            Assert.Contains("clients.stats.read", scopes);
            Assert.Contains("clients.tablet.stats.read", scopes);
            Assert.Contains("clients.webmail.stats.read", scopes);
        }

        [Fact]
        public void CreateReadOnlySupressions()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.AddPermissionsFor<IpManagement>();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 14, "IPs should have 14 scopes");
            Assert.Contains("ips.assigned.read", scopes);
            Assert.Contains("ips.read", scopes);
            Assert.Contains("ips.pools.create", scopes);
            Assert.Contains("ips.pools.delete", scopes);
            Assert.Contains("ips.pools.read", scopes);
            Assert.Contains("ips.pools.update", scopes);
            Assert.Contains("ips.pools.ips.create", scopes);
            Assert.Contains("ips.pools.ips.delete", scopes);
            Assert.Contains("ips.pools.ips.read", scopes);
            Assert.Contains("ips.pools.ips.update", scopes);
            Assert.Contains("ips.warmup.create", scopes);
            Assert.Contains("ips.warmup.delete", scopes);
            Assert.Contains("ips.warmup.read", scopes);
            Assert.Contains("ips.warmup.update", scopes);
        }

        [Fact]
        public void CreateMailSettings()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.AddPermissionsFor<MailSettings>();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 12, "Mail Settings should have 12 scopes");
            Assert.Contains("mail_settings.address_whitelist.read", scopes);
            Assert.Contains("mail_settings.address_whitelist.update", scopes);
            Assert.Contains("mail_settings.bounce_purge.read", scopes);
            Assert.Contains("mail_settings.bounce_purge.update", scopes);
            Assert.Contains("mail_settings.footer.read", scopes);
            Assert.Contains("mail_settings.footer.update", scopes);
            Assert.Contains("mail_settings.forward_bounce.read", scopes);
            Assert.Contains("mail_settings.forward_bounce.update", scopes);
            Assert.Contains("mail_settings.forward_spam.read", scopes);
            Assert.Contains("mail_settings.forward_spam.update", scopes);
            Assert.Contains("mail_settings.template.read", scopes);
            Assert.Contains("mail_settings.template.update", scopes);
        }

        [Fact]
        public void CreateMail()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.AddPermissionsFor<Mail>();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 5, "Mail should have 5 scopes");
            Assert.Contains("mail.batch.create", scopes);
            Assert.Contains("mail.batch.delete", scopes);
            Assert.Contains("mail.batch.read", scopes);
            Assert.Contains("mail.batch.update", scopes);
            Assert.Contains("mail.send", scopes);
        }

        [Fact]
        public void CreateMarketingCampaigns()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.AddPermissionsFor<MarketingCampaigns>();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 4, "Marketing Campaigns should have 4 scopes");
            Assert.Contains("marketing_campaigns.create", scopes);
            Assert.Contains("marketing_campaigns.delete", scopes);
            Assert.Contains("marketing_campaigns.read", scopes);
            Assert.Contains("marketing_campaigns.update", scopes);
        }

        [Fact]
        public void CreatePartnerSettings()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.AddPermissionsFor<PartnerSettings>();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 3, "Partner Settings should have 3 scopes");
            Assert.Contains("partner_settings.new_relic.read", scopes);
            Assert.Contains("partner_settings.new_relic.update", scopes);
            Assert.Contains("partner_settings.read", scopes);
        }

        [Fact]
        public void CreateScheduledSends()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.AddPermissionsFor<ScheduledSends>();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 4, "Scheduled Sends should have 4 scopes");
            Assert.Contains("user.scheduled_sends.create", scopes);
            Assert.Contains("user.scheduled_sends.delete", scopes);
            Assert.Contains("user.scheduled_sends.read", scopes);
            Assert.Contains("user.scheduled_sends.update", scopes);

        }

        [Fact]
        public void CreateSubusers()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.AddPermissionsFor<Subusers>();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 21, "Subusers should have 21 scopes");
            Assert.Contains("subusers.create", scopes);
            Assert.Contains("subusers.delete", scopes);
            Assert.Contains("subusers.read", scopes);
            Assert.Contains("subusers.update", scopes);
            Assert.Contains("subusers.credits.create", scopes);
            Assert.Contains("subusers.credits.delete", scopes);
            Assert.Contains("subusers.credits.read", scopes);
            Assert.Contains("subusers.credits.update", scopes);
            Assert.Contains("subusers.credits.remaining.create", scopes);
            Assert.Contains("subusers.credits.remaining.delete", scopes);
            Assert.Contains("subusers.credits.remaining.read", scopes);
            Assert.Contains("subusers.credits.remaining.update", scopes);
            Assert.Contains("subusers.monitor.create", scopes);
            Assert.Contains("subusers.monitor.delete", scopes);
            Assert.Contains("subusers.monitor.read", scopes);
            Assert.Contains("subusers.monitor.update", scopes);
            Assert.Contains("subusers.reputations.read", scopes);
            Assert.Contains("subusers.stats.read", scopes);
            Assert.Contains("subusers.stats.monthly.read", scopes);
            Assert.Contains("subusers.stats.sums.read", scopes);
            Assert.Contains("subusers.summary.read", scopes);
        }

        [Fact]
        public void CreateSuppressions()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.AddPermissionsFor<Suppressions>();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 24, "Suppressions should have 24 scopes");
            Assert.Contains("suppression.create", scopes);
            Assert.Contains("suppression.delete", scopes);
            Assert.Contains("suppression.read", scopes);
            Assert.Contains("suppression.update", scopes);
            Assert.Contains("suppression.bounces.create", scopes);
            Assert.Contains("suppression.bounces.read", scopes);
            Assert.Contains("suppression.bounces.update", scopes);
            Assert.Contains("suppression.bounces.delete", scopes);
            Assert.Contains("suppression.blocks.create", scopes);
            Assert.Contains("suppression.blocks.read", scopes);
            Assert.Contains("suppression.blocks.update", scopes);
            Assert.Contains("suppression.blocks.delete", scopes);
            Assert.Contains("suppression.invalid_emails.create", scopes);
            Assert.Contains("suppression.invalid_emails.read", scopes);
            Assert.Contains("suppression.invalid_emails.update", scopes);
            Assert.Contains("suppression.invalid_emails.delete", scopes);
            Assert.Contains("suppression.spam_reports.create", scopes);
            Assert.Contains("suppression.spam_reports.read", scopes);
            Assert.Contains("suppression.spam_reports.update", scopes);
            Assert.Contains("suppression.spam_reports.delete", scopes);
            Assert.Contains("suppression.unsubscribes.create", scopes);
            Assert.Contains("suppression.unsubscribes.read", scopes);
            Assert.Contains("suppression.unsubscribes.update", scopes);
            Assert.Contains("suppression.unsubscribes.delete", scopes);
        }

        [Fact]
        public void CreateTeammates()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.AddPermissionsFor<Teammates>();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 4, "Teammates should have 4 scopes");
            Assert.Contains("teammates.create", scopes);
            Assert.Contains("teammates.read", scopes);
            Assert.Contains("teammates.update", scopes);
            Assert.Contains("teammates.delete", scopes);
        }

        [Fact]
        public void CreateTemplates()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.AddPermissionsFor<Templates>();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 12, "Templates should have 12 scopes");
            Assert.Contains("templates.create", scopes);
            Assert.Contains("templates.delete", scopes);
            Assert.Contains("templates.read", scopes);
            Assert.Contains("templates.update", scopes);
            Assert.Contains("templates.versions.activate.create", scopes);
            Assert.Contains("templates.versions.activate.delete", scopes);
            Assert.Contains("templates.versions.activate.read", scopes);
            Assert.Contains("templates.versions.activate.update", scopes);
            Assert.Contains("templates.versions.create", scopes);
            Assert.Contains("templates.versions.delete", scopes);
            Assert.Contains("templates.versions.read", scopes);
            Assert.Contains("templates.versions.update", scopes);
        }

        [Fact]
        public void CreateTracking()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.AddPermissionsFor<Tracking>();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 9, "Tracking should have 9 scopes");
            Assert.Contains("tracking_settings.click.read", scopes);
            Assert.Contains("tracking_settings.click.update", scopes);
            Assert.Contains("tracking_settings.google_analytics.read", scopes);
            Assert.Contains("tracking_settings.google_analytics.update", scopes);
            Assert.Contains("tracking_settings.open.read", scopes);
            Assert.Contains("tracking_settings.open.update", scopes);
            Assert.Contains("tracking_settings.read", scopes);
            Assert.Contains("tracking_settings.subscription.read", scopes);
            Assert.Contains("tracking_settings.subscription.update", scopes);
        }

        [Fact]
        public void CreateUserSettings()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.AddPermissionsFor<UserSettings>();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 20, "User Settings should have 20 scopes");
            Assert.Contains("user.account.read", scopes);
            Assert.Contains("user.credits.read", scopes);
            Assert.Contains("user.email.create", scopes);
            Assert.Contains("user.email.delete", scopes);
            Assert.Contains("user.email.read", scopes);
            Assert.Contains("user.email.update", scopes);
            Assert.Contains("user.multifactor_authentication.create", scopes);
            Assert.Contains("user.multifactor_authentication.delete", scopes);
            Assert.Contains("user.multifactor_authentication.read", scopes);
            Assert.Contains("user.multifactor_authentication.update", scopes);
            Assert.Contains("user.password.read", scopes);
            Assert.Contains("user.password.update", scopes);
            Assert.Contains("user.profile.read", scopes);
            Assert.Contains("user.profile.update", scopes);
            Assert.Contains("user.settings.enforced_tls.read", scopes);
            Assert.Contains("user.settings.enforced_tls.update", scopes);
            Assert.Contains("user.timezone.read", scopes);
            Assert.Contains("user.timezone.update", scopes);
            Assert.Contains("user.username.read", scopes);
            Assert.Contains("user.username.update", scopes);
        }

        [Fact]
        public void CreateWebhooks()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.AddPermissionsFor<Webhook>();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 10, "Webhook should have 10 scopes");
            Assert.Contains("user.webhooks.event.settings.read", scopes);
            Assert.Contains("user.webhooks.event.settings.update", scopes);
            Assert.Contains("user.webhooks.event.test.create", scopes);
            Assert.Contains("user.webhooks.event.test.read", scopes);
            Assert.Contains("user.webhooks.event.test.update", scopes);
            Assert.Contains("user.webhooks.parse.settings.create", scopes);
            Assert.Contains("user.webhooks.parse.settings.delete", scopes);
            Assert.Contains("user.webhooks.parse.settings.read", scopes);
            Assert.Contains("user.webhooks.parse.settings.update", scopes);
            Assert.Contains("user.webhooks.parse.stats.read", scopes);
        }

        [Fact]
        public void CreateDomainAuthentication()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.AddPermissionsFor<DomainAuthentication>();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 4, "Domain Authentication should have 4 scopes");
            Assert.Contains("whitelabel.create", scopes);
            Assert.Contains("whitelabel.delete", scopes);
            Assert.Contains("whitelabel.read", scopes);
            Assert.Contains("whitelabel.update", scopes);
        }

        [Fact]
        public void CreateReverseDNS()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.AddPermissionsFor<ReverseDNS>();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 5, "Reverse DNS should have 5 scopes");
            Assert.Contains("access_settings.activity.read", scopes);
            Assert.Contains("access_settings.whitelist.create", scopes);
            Assert.Contains("access_settings.whitelist.delete", scopes);
            Assert.Contains("access_settings.whitelist.read", scopes);
            Assert.Contains("access_settings.whitelist.update", scopes);
        }

        [Fact]
        public void CreateAdminApiKeyScopes()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.AddPermissionsFor<Admin>();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 187, "Admin should have 187 scopes");

            Assert.Contains("access_settings.activity.read", scopes);
            Assert.Contains("access_settings.whitelist.create", scopes);
            Assert.Contains("access_settings.whitelist.delete", scopes);
            Assert.Contains("access_settings.whitelist.read", scopes);
            Assert.Contains("access_settings.whitelist.update", scopes);
            Assert.Contains("alerts.create", scopes);
            Assert.Contains("alerts.delete", scopes);
            Assert.Contains("alerts.read", scopes);
            Assert.Contains("alerts.update", scopes);
            Assert.Contains("api_keys.create", scopes);
            Assert.Contains("api_keys.delete", scopes);
            Assert.Contains("api_keys.read", scopes);
            Assert.Contains("api_keys.update", scopes);
            Assert.Contains("asm.groups.create", scopes);
            Assert.Contains("asm.groups.delete", scopes);
            Assert.Contains("asm.groups.read", scopes);
            Assert.Contains("asm.groups.update", scopes);
            Assert.Contains("billing.create", scopes);
            Assert.Contains("billing.delete", scopes);
            Assert.Contains("billing.read", scopes);
            Assert.Contains("billing.update", scopes);
            Assert.Contains("browsers.stats.read", scopes);
            Assert.Contains("categories.create", scopes);
            Assert.Contains("categories.delete", scopes);
            Assert.Contains("categories.read", scopes);
            Assert.Contains("categories.stats.read", scopes);
            Assert.Contains("categories.stats.sums.read", scopes);
            Assert.Contains("categories.update", scopes);
            Assert.Contains("clients.desktop.stats.read", scopes);
            Assert.Contains("clients.phone.stats.read", scopes);
            Assert.Contains("clients.stats.read", scopes);
            Assert.Contains("clients.tablet.stats.read", scopes);
            Assert.Contains("clients.webmail.stats.read", scopes);
            Assert.Contains("devices.stats.read", scopes);
            Assert.Contains("email_activity.read", scopes);
            Assert.Contains("geo.stats.read", scopes);
            Assert.Contains("ips.assigned.read", scopes);
            Assert.Contains("ips.pools.create", scopes);
            Assert.Contains("ips.pools.delete", scopes);
            Assert.Contains("ips.pools.ips.create", scopes);
            Assert.Contains("ips.pools.ips.delete", scopes);
            Assert.Contains("ips.pools.ips.read", scopes);
            Assert.Contains("ips.pools.ips.update", scopes);
            Assert.Contains("ips.pools.read", scopes);
            Assert.Contains("ips.pools.update", scopes);
            Assert.Contains("ips.read", scopes);
            Assert.Contains("ips.warmup.create", scopes);
            Assert.Contains("ips.warmup.delete", scopes);
            Assert.Contains("ips.warmup.read", scopes);
            Assert.Contains("ips.warmup.update", scopes);
            Assert.Contains("mail_settings.address_whitelist.read", scopes);
            Assert.Contains("mail_settings.address_whitelist.update", scopes);
            Assert.Contains("mail_settings.bounce_purge.read", scopes);
            Assert.Contains("mail_settings.bounce_purge.update", scopes);
            Assert.Contains("mail_settings.footer.read", scopes);
            Assert.Contains("mail_settings.footer.update", scopes);
            Assert.Contains("mail_settings.forward_bounce.read", scopes);
            Assert.Contains("mail_settings.forward_bounce.update", scopes);
            Assert.Contains("mail_settings.forward_spam.read", scopes);
            Assert.Contains("mail_settings.forward_spam.update", scopes);
            Assert.Contains("mail_settings.plain_content.read", scopes);
            Assert.Contains("mail_settings.plain_content.update", scopes);
            Assert.Contains("mail_settings.read", scopes);
            Assert.Contains("mail_settings.template.read", scopes);
            Assert.Contains("mail_settings.template.update", scopes);
            Assert.Contains("mail.batch.create", scopes);
            Assert.Contains("mail.batch.delete", scopes);
            Assert.Contains("mail.batch.read", scopes);
            Assert.Contains("mail.batch.update", scopes);
            Assert.Contains("mail.send", scopes);
            Assert.Contains("mailbox_providers.stats.read", scopes);
            Assert.Contains("marketing_campaigns.create", scopes);
            Assert.Contains("marketing_campaigns.delete", scopes);
            Assert.Contains("marketing_campaigns.read", scopes);
            Assert.Contains("marketing_campaigns.update", scopes);
            Assert.Contains("partner_settings.new_relic.read", scopes);
            Assert.Contains("partner_settings.new_relic.update", scopes);
            Assert.Contains("partner_settings.read", scopes);
            Assert.Contains("stats.global.read", scopes);
            Assert.Contains("stats.read", scopes);
            Assert.Contains("subusers.create", scopes);
            Assert.Contains("subusers.credits.create", scopes);
            Assert.Contains("subusers.credits.delete", scopes);
            Assert.Contains("subusers.credits.read", scopes);
            Assert.Contains("subusers.credits.remaining.create", scopes);
            Assert.Contains("subusers.credits.remaining.delete", scopes);
            Assert.Contains("subusers.credits.remaining.read", scopes);
            Assert.Contains("subusers.credits.remaining.update", scopes);
            Assert.Contains("subusers.credits.update", scopes);
            Assert.Contains("subusers.delete", scopes);
            Assert.Contains("subusers.monitor.create", scopes);
            Assert.Contains("subusers.monitor.delete", scopes);
            Assert.Contains("subusers.monitor.read", scopes);
            Assert.Contains("subusers.monitor.update", scopes);
            Assert.Contains("subusers.read", scopes);
            Assert.Contains("subusers.reputations.read", scopes);
            Assert.Contains("subusers.stats.monthly.read", scopes);
            Assert.Contains("subusers.stats.read", scopes);
            Assert.Contains("subusers.stats.sums.read", scopes);
            Assert.Contains("subusers.summary.read", scopes);
            Assert.Contains("subusers.update", scopes);
            Assert.Contains("suppression.blocks.create", scopes);
            Assert.Contains("suppression.blocks.delete", scopes);
            Assert.Contains("suppression.blocks.read", scopes);
            Assert.Contains("suppression.blocks.update", scopes);
            Assert.Contains("suppression.bounces.create", scopes);
            Assert.Contains("suppression.bounces.delete", scopes);
            Assert.Contains("suppression.bounces.read", scopes);
            Assert.Contains("suppression.bounces.update", scopes);
            Assert.Contains("suppression.create", scopes);
            Assert.Contains("suppression.delete", scopes);
            Assert.Contains("suppression.invalid_emails.create", scopes);
            Assert.Contains("suppression.invalid_emails.delete", scopes);
            Assert.Contains("suppression.invalid_emails.read", scopes);
            Assert.Contains("suppression.invalid_emails.update", scopes);
            Assert.Contains("suppression.read", scopes);
            Assert.Contains("suppression.spam_reports.create", scopes);
            Assert.Contains("suppression.spam_reports.delete", scopes);
            Assert.Contains("suppression.spam_reports.read", scopes);
            Assert.Contains("suppression.spam_reports.update", scopes);
            Assert.Contains("suppression.unsubscribes.create", scopes);
            Assert.Contains("suppression.unsubscribes.delete", scopes);
            Assert.Contains("suppression.unsubscribes.read", scopes);
            Assert.Contains("suppression.unsubscribes.update", scopes);
            Assert.Contains("suppression.update", scopes);
            Assert.Contains("teammates.create", scopes);
            Assert.Contains("teammates.read", scopes);
            Assert.Contains("teammates.update", scopes);
            Assert.Contains("teammates.delete", scopes);
            Assert.Contains("templates.create", scopes);
            Assert.Contains("templates.delete", scopes);
            Assert.Contains("templates.read", scopes);
            Assert.Contains("templates.update", scopes);
            Assert.Contains("templates.versions.activate.create", scopes);
            Assert.Contains("templates.versions.activate.delete", scopes);
            Assert.Contains("templates.versions.activate.read", scopes);
            Assert.Contains("templates.versions.activate.update", scopes);
            Assert.Contains("templates.versions.create", scopes);
            Assert.Contains("templates.versions.delete", scopes);
            Assert.Contains("templates.versions.read", scopes);
            Assert.Contains("templates.versions.update", scopes);
            Assert.Contains("tracking_settings.click.read", scopes);
            Assert.Contains("tracking_settings.click.update", scopes);
            Assert.Contains("tracking_settings.google_analytics.read", scopes);
            Assert.Contains("tracking_settings.google_analytics.update", scopes);
            Assert.Contains("tracking_settings.open.read", scopes);
            Assert.Contains("tracking_settings.open.update", scopes);
            Assert.Contains("tracking_settings.read", scopes);
            Assert.Contains("tracking_settings.subscription.read", scopes);
            Assert.Contains("tracking_settings.subscription.update", scopes);
            Assert.Contains("user.account.read", scopes);
            Assert.Contains("user.credits.read", scopes);
            Assert.Contains("user.email.create", scopes);
            Assert.Contains("user.email.delete", scopes);
            Assert.Contains("user.email.read", scopes);
            Assert.Contains("user.email.update", scopes);
            Assert.Contains("user.multifactor_authentication.create", scopes);
            Assert.Contains("user.multifactor_authentication.delete", scopes);
            Assert.Contains("user.multifactor_authentication.read", scopes);
            Assert.Contains("user.multifactor_authentication.update", scopes);
            Assert.Contains("user.password.read", scopes);
            Assert.Contains("user.password.update", scopes);
            Assert.Contains("user.profile.read", scopes);
            Assert.Contains("user.profile.update", scopes);
            Assert.Contains("user.scheduled_sends.create", scopes);
            Assert.Contains("user.scheduled_sends.delete", scopes);
            Assert.Contains("user.scheduled_sends.read", scopes);
            Assert.Contains("user.scheduled_sends.update", scopes);
            Assert.Contains("user.settings.enforced_tls.read", scopes);
            Assert.Contains("user.settings.enforced_tls.update", scopes);
            Assert.Contains("user.timezone.read", scopes);
            Assert.Contains("user.username.read", scopes);
            Assert.Contains("user.username.update", scopes);
            Assert.Contains("user.webhooks.event.settings.read", scopes);
            Assert.Contains("user.webhooks.event.settings.update", scopes);
            Assert.Contains("user.webhooks.event.test.create", scopes);
            Assert.Contains("user.webhooks.event.test.read", scopes);
            Assert.Contains("user.webhooks.event.test.update", scopes);
            Assert.Contains("user.webhooks.parse.settings.create", scopes);
            Assert.Contains("user.webhooks.parse.settings.delete", scopes);
            Assert.Contains("user.webhooks.parse.settings.read", scopes);
            Assert.Contains("user.webhooks.parse.settings.update", scopes);
            Assert.Contains("user.webhooks.parse.stats.read", scopes);
            Assert.Contains("whitelabel.create", scopes);
            Assert.Contains("whitelabel.delete", scopes);
            Assert.Contains("whitelabel.read", scopes);
            Assert.Contains("whitelabel.update", scopes);
        }
    }
}
