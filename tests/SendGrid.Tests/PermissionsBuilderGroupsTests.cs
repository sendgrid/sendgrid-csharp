using System;

namespace SendGrid.Tests
{
    using Permissions;
    using System.Linq;
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
        public void CreateReadOnlyStats()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.CreateReadOnlyStats();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 7, "read-only stats should have 7 scopes");
            Assert.Contains(scopes, x => x == "email_activity.read");
            Assert.Contains(scopes, x => x == "stats.read");
            Assert.Contains(scopes, x => x == "stats.global.read");
            Assert.Contains(scopes, x => x == "browsers.stats.read");
            Assert.Contains(scopes, x => x == "devices.stats.read");
            Assert.Contains(scopes, x => x == "geo.stats.read");
            Assert.Contains(scopes, x => x == "mailbox_providers.stats.read");
        }

        [Fact]
        public void CreateReadOnlySupressions()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.CreateReadOnlySuppressions();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 1, "read-only suppressions should have 1 scope");
            Assert.Contains(scopes, x => x == "suppressions.read");
        }

        [Fact]
        public void CreateFullAccessSupressions()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.CreateFullAccessSuppressions();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 4, "full access suppressions should have 4 scopes");
            Assert.Contains(scopes, x => x == "suppressions.create");
            Assert.Contains(scopes, x => x == "suppressions.delete");
            Assert.Contains(scopes, x => x == "suppressions.read");
            Assert.Contains(scopes, x => x == "suppressions.update");
        }

        [Fact]
        public void CreateReadOnlyWhitelabels()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.CreateReadOnlyWhitelabels();

            var scopes = sb.Build().ToArray();

            Assert.True(scopes.Length == 1, "read-only whitelabel should have 1 scope");
            Assert.Contains(scopes, x => x == "whitelabel.read");
        }

        [Fact]
        public void CreateFullAccessWhitelabels()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.CreateFullAccessWhitelabels();

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

            Assert.Equal(110, scopes.Length);

            Assert.Contains(scopes, x => x == "alerts.create");
            Assert.Contains(scopes, x => x == "alerts.read");
            Assert.Contains(scopes, x => x == "alerts.update");
            Assert.Contains(scopes, x => x == "alerts.delete");
            Assert.Contains(scopes, x => x == "asm.groups.create");
            Assert.Contains(scopes, x => x == "asm.groups.read");
            Assert.Contains(scopes, x => x == "asm.groups.update");
            Assert.Contains(scopes, x => x == "asm.groups.delete");
            Assert.Contains(scopes, x => x == "ips.pools.ips.read");
            Assert.Contains(scopes, x => x == "mail.send");
            Assert.Contains(scopes, x => x == "mail_settings.bcc.read");
            Assert.Contains(scopes, x => x == "mail_settings.bcc.update");
            Assert.Contains(scopes, x => x == "mail_settings.address_whitelist.read");
            Assert.Contains(scopes, x => x == "mail_settings.address_whitelist.update");
            Assert.Contains(scopes, x => x == "mail_settings.footer.read");
            Assert.Contains(scopes, x => x == "mail_settings.footer.update");
            Assert.Contains(scopes, x => x == "mail_settings.forward_spam.read");
            Assert.Contains(scopes, x => x == "mail_settings.forward_spam.update");
            Assert.Contains(scopes, x => x == "mail_settings.plain_content.read");
            Assert.Contains(scopes, x => x == "mail_settings.plain_content.update");
            Assert.Contains(scopes, x => x == "mail_settings.spam_check.read");
            Assert.Contains(scopes, x => x == "mail_settings.spam_check.update");
            Assert.Contains(scopes, x => x == "mail_settings.bounce_purge.read");
            Assert.Contains(scopes, x => x == "mail_settings.bounce_purge.update");
            Assert.Contains(scopes, x => x == "mail_settings.forward_bounce.read");
            Assert.Contains(scopes, x => x == "mail_settings.forward_bounce.update");
            Assert.Contains(scopes, x => x == "partner_settings.new_relic.read");
            Assert.Contains(scopes, x => x == "partner_settings.new_relic.update");
            Assert.Contains(scopes, x => x == "partner_settings.sendwithus.read");
            Assert.Contains(scopes, x => x == "partner_settings.sendwithus.update");
            Assert.Contains(scopes, x => x == "tracking_settings.click.read");
            Assert.Contains(scopes, x => x == "tracking_settings.click.update");
            Assert.Contains(scopes, x => x == "tracking_settings.subscription.read");
            Assert.Contains(scopes, x => x == "tracking_settings.subscription.update");
            Assert.Contains(scopes, x => x == "tracking_settings.open.read");
            Assert.Contains(scopes, x => x == "tracking_settings.open.update");
            Assert.Contains(scopes, x => x == "tracking_settings.google_analytics.read");
            Assert.Contains(scopes, x => x == "tracking_settings.google_analytics.update");
            Assert.Contains(scopes, x => x == "user.webhooks.event.settings.read");
            Assert.Contains(scopes, x => x == "user.webhooks.event.settings.update");
            Assert.Contains(scopes, x => x == "user.webhooks.event.test.create");
            Assert.Contains(scopes, x => x == "user.webhooks.event.test.read");
            Assert.Contains(scopes, x => x == "user.webhooks.event.test.update");
            Assert.Contains(scopes, x => x == "user.webhooks.parse.settings.create");
            Assert.Contains(scopes, x => x == "user.webhooks.parse.settings.read");
            Assert.Contains(scopes, x => x == "user.webhooks.parse.settings.update");
            Assert.Contains(scopes, x => x == "user.webhooks.parse.settings.delete");
            Assert.Contains(scopes, x => x == "stats.read");
            Assert.Contains(scopes, x => x == "stats.global.read");
            Assert.Contains(scopes, x => x == "categories.stats.read");
            Assert.Contains(scopes, x => x == "categories.stats.sums.read");
            Assert.Contains(scopes, x => x == "devices.stats.read");
            Assert.Contains(scopes, x => x == "clients.stats.read");
            Assert.Contains(scopes, x => x == "clients.phone.stats.read");
            Assert.Contains(scopes, x => x == "clients.tablet.stats.read");
            Assert.Contains(scopes, x => x == "clients.webmail.stats.read");
            Assert.Contains(scopes, x => x == "clients.desktop.stats.read");
            Assert.Contains(scopes, x => x == "geo.stats.read");
            Assert.Contains(scopes, x => x == "mailbox_providers.stats.read");
            Assert.Contains(scopes, x => x == "browsers.stats.read");
            Assert.Contains(scopes, x => x == "user.webhooks.parse.stats.read");
            Assert.Contains(scopes, x => x == "templates.create");
            Assert.Contains(scopes, x => x == "templates.read");
            Assert.Contains(scopes, x => x == "templates.update");
            Assert.Contains(scopes, x => x == "templates.delete");
            Assert.Contains(scopes, x => x == "templates.versions.create");
            Assert.Contains(scopes, x => x == "templates.versions.read");
            Assert.Contains(scopes, x => x == "templates.versions.update");
            Assert.Contains(scopes, x => x == "templates.versions.delete");
            Assert.Contains(scopes, x => x == "templates.versions.activate.create");
            Assert.Contains(scopes, x => x == "user.timezone.read");
            Assert.Contains(scopes, x => x == "user.timezone.update");
            Assert.Contains(scopes, x => x == "user.settings.enforced_tls.read");
            Assert.Contains(scopes, x => x == "user.settings.enforced_tls.update");
            Assert.Contains(scopes, x => x == "api_keys.create");
            Assert.Contains(scopes, x => x == "api_keys.read");
            Assert.Contains(scopes, x => x == "api_keys.update");
            Assert.Contains(scopes, x => x == "api_keys.delete");
            Assert.Contains(scopes, x => x == "email_activity.read");
            Assert.Contains(scopes, x => x == "categories.create");
            Assert.Contains(scopes, x => x == "categories.read");
            Assert.Contains(scopes, x => x == "categories.update");
            Assert.Contains(scopes, x => x == "categories.delete");
            Assert.Contains(scopes, x => x == "mail_settings.template.read");
            Assert.Contains(scopes, x => x == "mail_settings.template.update");
            Assert.Contains(scopes, x => x == "marketing_campaigns.create");
            Assert.Contains(scopes, x => x == "marketing_campaigns.read");
            Assert.Contains(scopes, x => x == "marketing_campaigns.update");
            Assert.Contains(scopes, x => x == "marketing_campaigns.delete");
            Assert.Contains(scopes, x => x == "mail.batch.create");
            Assert.Contains(scopes, x => x == "mail.batch.read");
            Assert.Contains(scopes, x => x == "mail.batch.update");
            Assert.Contains(scopes, x => x == "mail.batch.delete");
            Assert.Contains(scopes, x => x == "user.scheduled_sends.create");
            Assert.Contains(scopes, x => x == "user.scheduled_sends.read");
            Assert.Contains(scopes, x => x == "user.scheduled_sends.update");
            Assert.Contains(scopes, x => x == "user.scheduled_sends.delete");
            Assert.Contains(scopes, x => x == "access_settings.whitelist.create");
            Assert.Contains(scopes, x => x == "access_settings.whitelist.read");
            Assert.Contains(scopes, x => x == "access_settings.whitelist.update");
            Assert.Contains(scopes, x => x == "access_settings.whitelist.delete");
            Assert.Contains(scopes, x => x == "access_settings.activity.read");
            Assert.Contains(scopes, x => x == "whitelabel.create");
            Assert.Contains(scopes, x => x == "whitelabel.read");
            Assert.Contains(scopes, x => x == "whitelabel.update");
            Assert.Contains(scopes, x => x == "whitelabel.delete");
            Assert.Contains(scopes, x => x == "suppression.create");
            Assert.Contains(scopes, x => x == "suppression.read");
            Assert.Contains(scopes, x => x == "suppression.update");
            Assert.Contains(scopes, x => x == "suppression.delete");
        }
    }
}
