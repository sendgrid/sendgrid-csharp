namespace SendGrid.Tests
{
    using System.Linq;
    using Permissions;
    using SendGrid.Permissions.Scopes;
    using Xunit;

    public class PermissionsBuilderGroupsTests
    {
        [Fact]
        public void CreateReadOnlyMailSend()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.CreateReadOnlyMailSend();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 1, "read-only mail send should have only 1 scope");
            Assert.Contains(scopes, x => x == "mail.batch.read");
        }

        [Fact]
        public void CreateFullAccessMailSend()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.CreateFullAccessMailSend();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 5, "full access mail send should have 5 scopes");
            Assert.Contains(scopes, x => x == "mail.send");
            Assert.Contains(scopes, x => x == "mail.batch.create");
            Assert.Contains(scopes, x => x == "mail.batch.delete");
            Assert.Contains(scopes, x => x == "mail.batch.read");
            Assert.Contains(scopes, x => x == "mail.batch.update");
        }

        [Fact]
        public void CreateReadOnlyAlerts()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.CreateReadOnlyAlerts();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 1, "read-only alerts should have only 1 scope");
            Assert.Contains(scopes, x => x == "alerts.read");
        }

        [Fact]
        public void CreateFullAccessAlerts()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.CreateFullAccessAlerts();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 4, "full access alerts should have 4 scopes");
            Assert.Contains(scopes, x => x == "alerts.create");
            Assert.Contains(scopes, x => x == "alerts.delete");
            Assert.Contains(scopes, x => x == "alerts.read");
            Assert.Contains(scopes, x => x == "alerts.update");
        }

        [Fact]
        public void CreateStats()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.AddPermissionsFor<Stats>();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 12, "read-only stats should have 7 scopes");
            Assert.Contains(scopes, x => x == "email_activity.read");
            Assert.Contains(scopes, x => x == "stats.read");
            Assert.Contains(scopes, x => x == "stats.global.read");
            Assert.Contains(scopes, x => x == "browsers.stats.read");
            Assert.Contains(scopes, x => x == "devices.stats.read");
            Assert.Contains(scopes, x => x == "geo.stats.read");
            Assert.Contains(scopes, x => x == "mailbox_providers.stats.read");
            Assert.Contains(scopes, x => x == "clients.desktop.stats.read");
            Assert.Contains(scopes, x => x == "clients.phone.stats.read");
            Assert.Contains(scopes, x => x == "clients.stats.read");
            Assert.Contains(scopes, x => x == "clients.tablet.stats.read");
            Assert.Contains(scopes, x => x == "clients.webmail.stats.read");
        }

        [Fact]
        public void CreateReadOnlySupressions()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.CreateReadOnlySuppressions();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 6, "read-only suppressions should have 6 scope");
            Assert.Contains(scopes, x => x == "suppression.read");
            Assert.Contains(scopes, x => x == "suppression.bounces.read");
            Assert.Contains(scopes, x => x == "suppression.blocks.read");
            Assert.Contains(scopes, x => x == "suppression.invalid_emails.read");
            Assert.Contains(scopes, x => x == "suppression.spam_reports.read");
            Assert.Contains(scopes, x => x == "suppression.unsubscribes.read");
        }

        [Fact]
        public void CreateFullAccessSupressions()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.CreateFullAccessSuppressions();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 24, "full access suppressions should have 24 scopes");
            Assert.Contains(scopes, x => x == "suppression.create");
            Assert.Contains(scopes, x => x == "suppression.delete");
            Assert.Contains(scopes, x => x == "suppression.read");
            Assert.Contains(scopes, x => x == "suppression.update");
            Assert.Contains(scopes, x => x == "suppression.bounces.create");
            Assert.Contains(scopes, x => x == "suppression.bounces.read");
            Assert.Contains(scopes, x => x == "suppression.bounces.update");
            Assert.Contains(scopes, x => x == "suppression.bounces.delete");
            Assert.Contains(scopes, x => x == "suppression.blocks.create");
            Assert.Contains(scopes, x => x == "suppression.blocks.read");
            Assert.Contains(scopes, x => x == "suppression.blocks.update");
            Assert.Contains(scopes, x => x == "suppression.blocks.delete");
            Assert.Contains(scopes, x => x == "suppression.invalid_emails.create");
            Assert.Contains(scopes, x => x == "suppression.invalid_emails.read");
            Assert.Contains(scopes, x => x == "suppression.invalid_emails.update");
            Assert.Contains(scopes, x => x == "suppression.invalid_emails.delete");
            Assert.Contains(scopes, x => x == "suppression.spam_reports.create");
            Assert.Contains(scopes, x => x == "suppression.spam_reports.read");
            Assert.Contains(scopes, x => x == "suppression.spam_reports.update");
            Assert.Contains(scopes, x => x == "suppression.spam_reports.delete");
            Assert.Contains(scopes, x => x == "suppression.unsubscribes.create");
            Assert.Contains(scopes, x => x == "suppression.unsubscribes.read");
            Assert.Contains(scopes, x => x == "suppression.unsubscribes.update");
            Assert.Contains(scopes, x => x == "suppression.unsubscribes.delete");
        }

        [Fact]
        public void CreateReadOnlyWhitelabels()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.CreateReadOnlyDomainAuthentication();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 1, "read-only whitelabel should have 1 scope");
            Assert.Contains(scopes, x => x == "whitelabel.read");
        }

        [Fact]
        public void CreateFullAccessWhitelabels()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.CreateFullAccessDomainAuthentication();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 4, "full access whitelabel should have 4 scopes");
            Assert.Contains(scopes, x => x == "whitelabel.create");
            Assert.Contains(scopes, x => x == "whitelabel.delete");
            Assert.Contains(scopes, x => x == "whitelabel.read");
            Assert.Contains(scopes, x => x == "whitelabel.update");
        }

        [Fact]
        public void CreateReadOnlyIpManagement()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.CreateReadOnlyIpManagement();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 4, "read-only IP management should have 4 scopes");
            Assert.Contains(scopes, x => x == "ips.assigned.read");
            Assert.Contains(scopes, x => x == "ips.read");
            Assert.Contains(scopes, x => x == "ips.pools.read");
            Assert.Contains(scopes, x => x == "ips.warmup.read");
        }

        [Fact]
        public void CreateFullAccessIpManagement()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.CreateFullAccessIpManagement();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 11, "full access IP management should have 11 scopes");
            Assert.Contains(scopes, x => x == "ips.assigned.read");
            Assert.Contains(scopes, x => x == "ips.read");
            Assert.Contains(scopes, x => x == "ips.pools.create");
            Assert.Contains(scopes, x => x == "ips.pools.delete");
            Assert.Contains(scopes, x => x == "ips.pools.read");
            Assert.Contains(scopes, x => x == "ips.pools.update");
            Assert.Contains(scopes, x => x == "ips.pools.ips.create");
            Assert.Contains(scopes, x => x == "ips.pools.ips.delete");
            Assert.Contains(scopes, x => x == "ips.warmup.create");
            Assert.Contains(scopes, x => x == "ips.warmup.delete");
            Assert.Contains(scopes, x => x == "ips.warmup.read");
        }

        [Fact]
        public void CreateReadOnlyTemplates()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.CreateReadOnlyTemplates();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 3, "read-only templates should have 3 scopes");
            Assert.Contains(scopes, x => x == "templates.read");
            Assert.Contains(scopes, x => x == "templates.versions.activate.read");
            Assert.Contains(scopes, x => x == "templates.versions.read");
        }

        [Fact]
        public void CreateFullAccessOnlyTemplates()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.CreateFullAccessTemplates();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 12, "full access templates should have 12 scopes");
            Assert.Contains(scopes, x => x == "templates.create");
            Assert.Contains(scopes, x => x == "templates.delete");
            Assert.Contains(scopes, x => x == "templates.read");
            Assert.Contains(scopes, x => x == "templates.update");
            Assert.Contains(scopes, x => x == "templates.versions.activate.create");
            Assert.Contains(scopes, x => x == "templates.versions.activate.delete");
            Assert.Contains(scopes, x => x == "templates.versions.activate.read");
            Assert.Contains(scopes, x => x == "templates.versions.activate.update");
            Assert.Contains(scopes, x => x == "templates.versions.create");
            Assert.Contains(scopes, x => x == "templates.versions.delete");
            Assert.Contains(scopes, x => x == "templates.versions.read");
            Assert.Contains(scopes, x => x == "templates.versions.update");
        }

        [Fact]
        public void CreateReadOnlyInboundParse()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.CreateReadOnlyInboundParse();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 2, "read-only inbound parse should have 2 scopes");
            Assert.Contains(scopes, x => x == "user.webhooks.parse.settings.read");
            Assert.Contains(scopes, x => x == "user.webhooks.parse.stats.read");
        }

        [Fact]
        public void CreateFullAccessOnlyInboundParse()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.CreateFullAccessInboundParse();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 5, "full access inbound parse should have 5 scopes");
            Assert.Contains(scopes, x => x == "user.webhooks.parse.settings.create");
            Assert.Contains(scopes, x => x == "user.webhooks.parse.settings.delete");
            Assert.Contains(scopes, x => x == "user.webhooks.parse.settings.read");
            Assert.Contains(scopes, x => x == "user.webhooks.parse.settings.update");
            Assert.Contains(scopes, x => x == "user.webhooks.parse.stats.read");
        }

        [Fact]
        public void CreateReadOnlyMailSettings()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.CreateReadOnlyMailSettings();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 10, "read-only mail settings should have 10 scopes");
            Assert.Contains(scopes, x => x == "mail_settings.address_whitelist.read");
            Assert.Contains(scopes, x => x == "mail_settings.bcc.read");
            Assert.Contains(scopes, x => x == "mail_settings.bounce_purge.read");
            Assert.Contains(scopes, x => x == "mail_settings.footer.read");
            Assert.Contains(scopes, x => x == "mail_settings.forward_bounce.read");
            Assert.Contains(scopes, x => x == "mail_settings.forward_spam.read");
            Assert.Contains(scopes, x => x == "mail_settings.plain_content.read");
            Assert.Contains(scopes, x => x == "mail_settings.read");
            Assert.Contains(scopes, x => x == "mail_settings.spam_check.read");
            Assert.Contains(scopes, x => x == "mail_settings.template.read");
        }

        [Fact]
        public void CreateFullAccessOnlyMailSettings()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.CreateFullAccessMailSettings();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 19, "full access mail settings should have 19 scopes");
            Assert.Contains(scopes, x => x == "mail_settings.address_whitelist.read");
            Assert.Contains(scopes, x => x == "mail_settings.address_whitelist.update");
            Assert.Contains(scopes, x => x == "mail_settings.bcc.read");
            Assert.Contains(scopes, x => x == "mail_settings.bcc.update");
            Assert.Contains(scopes, x => x == "mail_settings.bounce_purge.read");
            Assert.Contains(scopes, x => x == "mail_settings.bounce_purge.update");
            Assert.Contains(scopes, x => x == "mail_settings.footer.read");
            Assert.Contains(scopes, x => x == "mail_settings.footer.update");
            Assert.Contains(scopes, x => x == "mail_settings.forward_bounce.read");
            Assert.Contains(scopes, x => x == "mail_settings.forward_bounce.update");
            Assert.Contains(scopes, x => x == "mail_settings.forward_spam.read");
            Assert.Contains(scopes, x => x == "mail_settings.forward_spam.update");
            Assert.Contains(scopes, x => x == "mail_settings.plain_content.read");
            Assert.Contains(scopes, x => x == "mail_settings.plain_content.update");
            Assert.Contains(scopes, x => x == "mail_settings.read");
            Assert.Contains(scopes, x => x == "mail_settings.spam_check.read");
            Assert.Contains(scopes, x => x == "mail_settings.spam_check.update");
            Assert.Contains(scopes, x => x == "mail_settings.template.read");
            Assert.Contains(scopes, x => x == "mail_settings.template.update");
        }

        [Fact]
        public void CreateReadOnlyMarketingCampaigns()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.CreateReadOnlyMarketingCampaigns();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 1, "read-only marketing campaigns should have 1 scopes");
            Assert.Contains(scopes, x => x == "marketing_campaigns.read");

        }

        [Fact]
        public void CreateFullAccessOnlyMarketingCampaigns()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.CreateFullAccessMarketingCampaigns();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 5, "full access marketing campaigns should have 5 scopes");
            Assert.Contains(scopes, x => x == "marketing_campaigns.create");
            Assert.Contains(scopes, x => x == "marketing_campaigns.delete");
            Assert.Contains(scopes, x => x == "marketing_campaigns.read");
            Assert.Contains(scopes, x => x == "marketing_campaigns.update");
            Assert.Contains(scopes, x => x == "partner_settings.new_relic.read");
        }

        [Fact]
        public void CreateAdminApiKeyScopes()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.CreateAdminPermissions();

            var scopes = sb.Build().ToArray();

            Assert.Equal(197, scopes.Length);

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
            Assert.Contains("credentials.create", scopes);
            Assert.Contains("credentials.delete", scopes);
            Assert.Contains("credentials.read", scopes);
            Assert.Contains("credentials.update", scopes);
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
            Assert.Contains("mail_settings.bcc.read", scopes);
            Assert.Contains("mail_settings.bcc.update", scopes);
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
            Assert.Contains("mail_settings.spam_check.read", scopes);
            Assert.Contains("mail_settings.spam_check.update", scopes);
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
            Assert.Contains("newsletter.create", scopes);
            Assert.Contains("newsletter.delete", scopes);
            Assert.Contains("newsletter.read", scopes);
            Assert.Contains("newsletter.update", scopes);
            Assert.Contains("partner_settings.new_relic.read", scopes);
            Assert.Contains("partner_settings.new_relic.update", scopes);
            Assert.Contains("partner_settings.read", scopes);
            Assert.Contains("partner_settings.sendwithus.read", scopes);
            Assert.Contains("partner_settings.sendwithus.update", scopes);
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
