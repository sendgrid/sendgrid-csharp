using SendGrid.Permissions;
using System.Linq;
using Xunit;

namespace SendGrid.Tests
{
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

            Assert.Contains(scopes, x => x == "access_settings.activity.read");
            Assert.Contains(scopes, x => x == "access_settings.whitelist.create");
            Assert.Contains(scopes, x => x == "access_settings.whitelist.delete");
            Assert.Contains(scopes, x => x == "access_settings.whitelist.read");
            Assert.Contains(scopes, x => x == "access_settings.whitelist.update");

            Assert.Contains(scopes, x => x == "alerts.create");
            Assert.Contains(scopes, x => x == "alerts.delete");
            Assert.Contains(scopes, x => x == "alerts.read");
            Assert.Contains(scopes, x => x == "alerts.update");

            Assert.Contains(scopes, x => x == "api_keys.create");
            Assert.Contains(scopes, x => x == "api_keys.delete");
            Assert.Contains(scopes, x => x == "api_keys.read");
            Assert.Contains(scopes, x => x == "api_keys.update");

            Assert.Contains(scopes, x => x == "asm.groups.create");
            Assert.Contains(scopes, x => x == "asm.groups.delete");
            Assert.Contains(scopes, x => x == "asm.groups.read");
            Assert.Contains(scopes, x => x == "asm.groups.update");

            Assert.Contains(scopes, x => x == "billing.create");
            Assert.Contains(scopes, x => x == "billing.delete");
            Assert.Contains(scopes, x => x == "billing.read");
            Assert.Contains(scopes, x => x == "billing.update");

            Assert.Contains(scopes, x => x == "browsers.stats.read");

            Assert.Contains(scopes, x => x == "categories.create");
            Assert.Contains(scopes, x => x == "categories.delete");
            Assert.Contains(scopes, x => x == "categories.read");
            Assert.Contains(scopes, x => x == "categories.stats.read");
            Assert.Contains(scopes, x => x == "categories.stats.sums.read");
            Assert.Contains(scopes, x => x == "categories.update");

            Assert.Contains(scopes, x => x == "clients.desktop.stats.read");
            Assert.Contains(scopes, x => x == "clients.phone.stats.read");
            Assert.Contains(scopes, x => x == "clients.stats.read");
            Assert.Contains(scopes, x => x == "clients.tablet.stats.read");
            Assert.Contains(scopes, x => x == "clients.webmail.stats.read");

            Assert.Contains(scopes, x => x == "credentials.create");
            Assert.Contains(scopes, x => x == "credentials.delete");
            Assert.Contains(scopes, x => x == "credentials.read");
            Assert.Contains(scopes, x => x == "credentials.update");

            Assert.Contains(scopes, x => x == "devices.stats.read");

            Assert.Contains(scopes, x => x == "email_activity.read");

            Assert.Contains(scopes, x => x == "geo.stats.read");

            Assert.Contains(scopes, x => x == "ips.assigned.read");
            Assert.Contains(scopes, x => x == "ips.pools.create");
            Assert.Contains(scopes, x => x == "ips.pools.delete");
            Assert.Contains(scopes, x => x == "ips.pools.ips.create");
            Assert.Contains(scopes, x => x == "ips.pools.ips.delete");
            Assert.Contains(scopes, x => x == "ips.pools.ips.read");
            Assert.Contains(scopes, x => x == "ips.pools.ips.update");
            Assert.Contains(scopes, x => x == "ips.pools.read");
            Assert.Contains(scopes, x => x == "ips.pools.update");
            Assert.Contains(scopes, x => x == "ips.read");
            Assert.Contains(scopes, x => x == "ips.warmup.create");
            Assert.Contains(scopes, x => x == "ips.warmup.delete");
            Assert.Contains(scopes, x => x == "ips.warmup.read");
            Assert.Contains(scopes, x => x == "ips.warmup.update");

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

            Assert.Contains(scopes, x => x == "mail.batch.create");
            Assert.Contains(scopes, x => x == "mail.batch.delete");
            Assert.Contains(scopes, x => x == "mail.batch.read");
            Assert.Contains(scopes, x => x == "mail.batch.update");
            Assert.Contains(scopes, x => x == "mail.send");

            Assert.Contains(scopes, x => x == "mailbox_providers.stats.read");

            Assert.Contains(scopes, x => x == "marketing_campaigns.create");
            Assert.Contains(scopes, x => x == "marketing_campaigns.delete");
            Assert.Contains(scopes, x => x == "marketing_campaigns.read");
            Assert.Contains(scopes, x => x == "marketing_campaigns.update");

            Assert.Contains(scopes, x => x == "partner_settings.new_relic.read");
            Assert.Contains(scopes, x => x == "partner_settings.new_relic.update");
            Assert.Contains(scopes, x => x == "partner_settings.read");
            Assert.Contains(scopes, x => x == "partner_settings.sendwithus.read");
            Assert.Contains(scopes, x => x == "partner_settings.sendwithus.update");

            Assert.Contains(scopes, x => x == "stats.global.read");
            Assert.Contains(scopes, x => x == "stats.read");

            Assert.Contains(scopes, x => x == "subusers.create");
            Assert.Contains(scopes, x => x == "subusers.delete");
            Assert.Contains(scopes, x => x == "subusers.read");
            Assert.Contains(scopes, x => x == "subusers.update");
            Assert.Contains(scopes, x => x == "subusers.credits.create");
            Assert.Contains(scopes, x => x == "subusers.credits.delete");
            Assert.Contains(scopes, x => x == "subusers.credits.read");
            Assert.Contains(scopes, x => x == "subusers.credits.update");
            Assert.Contains(scopes, x => x == "subusers.credits.remaining.create");
            Assert.Contains(scopes, x => x == "subusers.credits.remaining.delete");
            Assert.Contains(scopes, x => x == "subusers.credits.remaining.read");
            Assert.Contains(scopes, x => x == "subusers.credits.remaining.update");
            Assert.Contains(scopes, x => x == "subusers.monitor.create");
            Assert.Contains(scopes, x => x == "subusers.monitor.delete");
            Assert.Contains(scopes, x => x == "subusers.monitor.read");
            Assert.Contains(scopes, x => x == "subusers.monitor.update");
            Assert.Contains(scopes, x => x == "subusers.reputations.read");
            Assert.Contains(scopes, x => x == "subusers.stats.read");
            Assert.Contains(scopes, x => x == "subusers.stats.monthly.read");
            Assert.Contains(scopes, x => x == "subusers.stats.sums.read");
            Assert.Contains(scopes, x => x == "subusers.summary.read");

            Assert.Contains(scopes, x => x == "suppression.blocks.create");
            Assert.Contains(scopes, x => x == "suppression.blocks.delete");
            Assert.Contains(scopes, x => x == "suppression.blocks.read");
            Assert.Contains(scopes, x => x == "suppression.blocks.update");
            Assert.Contains(scopes, x => x == "suppression.bounces.create");
            Assert.Contains(scopes, x => x == "suppression.bounces.delete");
            Assert.Contains(scopes, x => x == "suppression.bounces.read");
            Assert.Contains(scopes, x => x == "suppression.bounces.update");
            Assert.Contains(scopes, x => x == "suppression.create");
            Assert.Contains(scopes, x => x == "suppression.delete");
            Assert.Contains(scopes, x => x == "suppression.invalid_emails.create");
            Assert.Contains(scopes, x => x == "suppression.invalid_emails.delete");
            Assert.Contains(scopes, x => x == "suppression.invalid_emails.read");
            Assert.Contains(scopes, x => x == "suppression.invalid_emails.update");
            Assert.Contains(scopes, x => x == "suppression.read");
            Assert.Contains(scopes, x => x == "suppression.spam_reports.create");
            Assert.Contains(scopes, x => x == "suppression.spam_reports.delete");
            Assert.Contains(scopes, x => x == "suppression.spam_reports.read");
            Assert.Contains(scopes, x => x == "suppression.spam_reports.update");
            Assert.Contains(scopes, x => x == "suppression.unsubscribes.create");
            Assert.Contains(scopes, x => x == "suppression.unsubscribes.delete");
            Assert.Contains(scopes, x => x == "suppression.unsubscribes.read");
            Assert.Contains(scopes, x => x == "suppression.unsubscribes.update");
            Assert.Contains(scopes, x => x == "suppression.update");

            Assert.Contains(scopes, x => x == "teammates.create");
            Assert.Contains(scopes, x => x == "teammates.read");
            Assert.Contains(scopes, x => x == "teammates.update");
            Assert.Contains(scopes, x => x == "teammates.delete");

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

            Assert.Contains(scopes, x => x == "tracking_settings.click.read");
            Assert.Contains(scopes, x => x == "tracking_settings.click.update");
            Assert.Contains(scopes, x => x == "tracking_settings.google_analytics.read");
            Assert.Contains(scopes, x => x == "tracking_settings.google_analytics.update");
            Assert.Contains(scopes, x => x == "tracking_settings.open.read");
            Assert.Contains(scopes, x => x == "tracking_settings.open.update");
            Assert.Contains(scopes, x => x == "tracking_settings.read");
            Assert.Contains(scopes, x => x == "tracking_settings.subscription.read");
            Assert.Contains(scopes, x => x == "tracking_settings.subscription.update");

            Assert.Contains(scopes, x => x == "user.account.read");
            Assert.Contains(scopes, x => x == "user.credits.read");
            Assert.Contains(scopes, x => x == "user.email.create");
            Assert.Contains(scopes, x => x == "user.email.delete");
            Assert.Contains(scopes, x => x == "user.email.read");
            Assert.Contains(scopes, x => x == "user.email.update");
            Assert.Contains(scopes, x => x == "user.multifactor_authentication.create");
            Assert.Contains(scopes, x => x == "user.multifactor_authentication.delete");
            Assert.Contains(scopes, x => x == "user.multifactor_authentication.read");
            Assert.Contains(scopes, x => x == "user.multifactor_authentication.update");
            Assert.Contains(scopes, x => x == "user.password.read");
            Assert.Contains(scopes, x => x == "user.password.update");
            Assert.Contains(scopes, x => x == "user.profile.read");
            Assert.Contains(scopes, x => x == "user.profile.update");
            Assert.Contains(scopes, x => x == "user.scheduled_sends.create");
            Assert.Contains(scopes, x => x == "user.scheduled_sends.delete");
            Assert.Contains(scopes, x => x == "user.scheduled_sends.read");
            Assert.Contains(scopes, x => x == "user.scheduled_sends.update");
            Assert.Contains(scopes, x => x == "user.settings.enforced_tls.read");
            Assert.Contains(scopes, x => x == "user.settings.enforced_tls.update");
            Assert.Contains(scopes, x => x == "user.timezone.read");
            Assert.Contains(scopes, x => x == "user.username.read");
            Assert.Contains(scopes, x => x == "user.username.update");

            Assert.Contains(scopes, x => x == "user.webhooks.event.settings.read");
            Assert.Contains(scopes, x => x == "user.webhooks.event.settings.update");
            Assert.Contains(scopes, x => x == "user.webhooks.event.test.create");
            Assert.Contains(scopes, x => x == "user.webhooks.event.test.read");
            Assert.Contains(scopes, x => x == "user.webhooks.event.test.update");
            Assert.Contains(scopes, x => x == "user.webhooks.parse.settings.create");
            Assert.Contains(scopes, x => x == "user.webhooks.parse.settings.delete");
            Assert.Contains(scopes, x => x == "user.webhooks.parse.settings.read");
            Assert.Contains(scopes, x => x == "user.webhooks.parse.settings.update");
            Assert.Contains(scopes, x => x == "user.webhooks.parse.stats.read");

            Assert.Contains(scopes, x => x == "whitelabel.create");
            Assert.Contains(scopes, x => x == "whitelabel.delete");
            Assert.Contains(scopes, x => x == "whitelabel.read");
            Assert.Contains(scopes, x => x == "whitelabel.update");
        }

         [Fact]
        public void CreateAdminCrudScopeContainsAllScopesInOrder()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.CreateAdminPermissions();

            var scopesListAsString = string.Join("," + System.Environment.NewLine, sb.Build().Select(scope => $"\"{scope}\""));

            Assert.Equal(@"""access_settings.activity.read"",
""access_settings.whitelist.create"",
""access_settings.whitelist.delete"",
""access_settings.whitelist.read"",
""access_settings.whitelist.update"",
""alerts.create"",
""alerts.delete"",
""alerts.read"",
""alerts.update"",
""api_keys.create"",
""api_keys.delete"",
""api_keys.read"",
""api_keys.update"",
""asm.groups.create"",
""asm.groups.delete"",
""asm.groups.read"",
""asm.groups.update"",
""billing.create"",
""billing.delete"",
""billing.read"",
""billing.update"",
""browsers.stats.read"",
""categories.create"",
""categories.delete"",
""categories.read"",
""categories.stats.read"",
""categories.stats.sums.read"",
""categories.update"",
""clients.desktop.stats.read"",
""clients.phone.stats.read"",
""clients.stats.read"",
""clients.tablet.stats.read"",
""clients.webmail.stats.read"",
""credentials.create"",
""credentials.delete"",
""credentials.read"",
""credentials.update"",
""devices.stats.read"",
""email_activity.read"",
""geo.stats.read"",
""ips.assigned.read"",
""ips.pools.create"",
""ips.pools.delete"",
""ips.pools.ips.create"",
""ips.pools.ips.delete"",
""ips.pools.ips.read"",
""ips.pools.ips.update"",
""ips.pools.read"",
""ips.pools.update"",
""ips.read"",
""ips.warmup.create"",
""ips.warmup.delete"",
""ips.warmup.read"",
""ips.warmup.update"",
""mail_settings.address_whitelist.read"",
""mail_settings.address_whitelist.update"",
""mail_settings.bcc.read"",
""mail_settings.bcc.update"",
""mail_settings.bounce_purge.read"",
""mail_settings.bounce_purge.update"",
""mail_settings.footer.read"",
""mail_settings.footer.update"",
""mail_settings.forward_bounce.read"",
""mail_settings.forward_bounce.update"",
""mail_settings.forward_spam.read"",
""mail_settings.forward_spam.update"",
""mail_settings.plain_content.read"",
""mail_settings.plain_content.update"",
""mail_settings.read"",
""mail_settings.spam_check.read"",
""mail_settings.spam_check.update"",
""mail_settings.template.read"",
""mail_settings.template.update"",
""mail.batch.create"",
""mail.batch.delete"",
""mail.batch.read"",
""mail.batch.update"",
""mail.send"",
""mailbox_providers.stats.read"",
""marketing_campaigns.create"",
""marketing_campaigns.delete"",
""marketing_campaigns.read"",
""marketing_campaigns.update"",
""newsletter.create"",
""newsletter.delete"",
""newsletter.read"",
""newsletter.update"",
""partner_settings.new_relic.read"",
""partner_settings.new_relic.update"",
""partner_settings.read"",
""partner_settings.sendwithus.read"",
""partner_settings.sendwithus.update"",
""stats.global.read"",
""stats.read"",
""subusers.create"",
""subusers.credits.create"",
""subusers.credits.delete"",
""subusers.credits.read"",
""subusers.credits.remaining.create"",
""subusers.credits.remaining.delete"",
""subusers.credits.remaining.read"",
""subusers.credits.remaining.update"",
""subusers.credits.update"",
""subusers.delete"",
""subusers.monitor.create"",
""subusers.monitor.delete"",
""subusers.monitor.read"",
""subusers.monitor.update"",
""subusers.read"",
""subusers.reputations.read"",
""subusers.stats.monthly.read"",
""subusers.stats.read"",
""subusers.stats.sums.read"",
""subusers.summary.read"",
""subusers.update"",
""suppression.blocks.create"",
""suppression.blocks.delete"",
""suppression.blocks.read"",
""suppression.blocks.update"",
""suppression.bounces.create"",
""suppression.bounces.delete"",
""suppression.bounces.read"",
""suppression.bounces.update"",
""suppression.create"",
""suppression.delete"",
""suppression.invalid_emails.create"",
""suppression.invalid_emails.delete"",
""suppression.invalid_emails.read"",
""suppression.invalid_emails.update"",
""suppression.read"",
""suppression.spam_reports.create"",
""suppression.spam_reports.delete"",
""suppression.spam_reports.read"",
""suppression.spam_reports.update"",
""suppression.unsubscribes.create"",
""suppression.unsubscribes.delete"",
""suppression.unsubscribes.read"",
""suppression.unsubscribes.update"",
""suppression.update"",
""teammates.create"",
""teammates.delete"",
""teammates.read"",
""teammates.update"",
""templates.create"",
""templates.delete"",
""templates.read"",
""templates.update"",
""templates.versions.activate.create"",
""templates.versions.activate.delete"",
""templates.versions.activate.read"",
""templates.versions.activate.update"",
""templates.versions.create"",
""templates.versions.delete"",
""templates.versions.read"",
""templates.versions.update"",
""tracking_settings.click.read"",
""tracking_settings.click.update"",
""tracking_settings.google_analytics.read"",
""tracking_settings.google_analytics.update"",
""tracking_settings.open.read"",
""tracking_settings.open.update"",
""tracking_settings.read"",
""tracking_settings.subscription.read"",
""tracking_settings.subscription.update"",
""user.account.read"",
""user.credits.read"",
""user.email.create"",
""user.email.delete"",
""user.email.read"",
""user.email.update"",
""user.multifactor_authentication.create"",
""user.multifactor_authentication.delete"",
""user.multifactor_authentication.read"",
""user.multifactor_authentication.update"",
""user.password.read"",
""user.password.update"",
""user.profile.read"",
""user.profile.update"",
""user.scheduled_sends.create"",
""user.scheduled_sends.delete"",
""user.scheduled_sends.read"",
""user.scheduled_sends.update"",
""user.settings.enforced_tls.read"",
""user.settings.enforced_tls.update"",
""user.timezone.read"",
""user.timezone.update"",
""user.username.read"",
""user.username.update"",
""user.webhooks.event.settings.read"",
""user.webhooks.event.settings.update"",
""user.webhooks.event.test.create"",
""user.webhooks.event.test.read"",
""user.webhooks.event.test.update"",
""user.webhooks.parse.settings.create"",
""user.webhooks.parse.settings.delete"",
""user.webhooks.parse.settings.read"",
""user.webhooks.parse.settings.update"",
""user.webhooks.parse.stats.read"",
""whitelabel.create"",
""whitelabel.delete"",
""whitelabel.read"",
""whitelabel.update""", scopesListAsString);
        }
    }
}
