using SendGrid.Permissions;
using SendGrid.Permissions.Scopes;
using System;
using System.Linq;
using Xunit;

namespace SendGrid.Tests
{

    public class PermissionsBuilderTests
    {

        [Fact] public void CanAddRawScope()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.AddScope("alerts.read");
            var scopes = sb.Build().ToArray();

            Assert.Equal("alerts.read", scopes[0]);
        }

        [Fact] public void ThrowsIfRawScopeIsNotValid()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.AddScope("invalid.scope");
            Assert.Throws<InvalidCastException>(() => sb.Build());
        }


        [Fact]
        public void CreateReadOnlyCrudScope()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.AddPermissionsFor<Alerts>(ScopeOptions.ReadOnly);

            var scopes = sb.Build();

            Assert.Contains(scopes, x => x == "alerts.read");
        }

        [Fact]
        public void BuildAllCrudScope()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.AddPermissionsFor<Alerts>(ScopeOptions.All);

            var scopes = sb.Build().ToArray();

            Assert.Contains(scopes, x => x == "alerts.create");
            Assert.Contains(scopes, x => x == "alerts.delete");
            Assert.Contains(scopes, x => x == "alerts.read");
            Assert.Contains(scopes, x => x == "alerts.update");
        }

        [Fact]
        public void BuildAllSubusersScopes()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.AddPermissionsFor<Subusers>(ScopeOptions.All);

            var scopes = sb.Build().ToArray();

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
        }

        [Fact(Skip="I'm not sure order matters")]
        public void CreateAdminCrudScopeContainsAllScopesInOrder()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.CreateAdminPermissions();

            var scopesListAsString = string.Join(","+Environment.NewLine, sb.Build().Select(scope => $"\"{scope}\""));

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

        [Fact]
        public void CreateScopeWithSubScopes()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.AddPermissionsFor<Categories>(ScopeOptions.All);

            var scopes = sb.Build().ToArray();

            Assert.Contains(scopes, x => x == "categories.create");
            Assert.Contains(scopes, x => x == "categories.delete");
            Assert.Contains(scopes, x => x == "categories.read");
            Assert.Contains(scopes, x => x == "categories.update");
            Assert.Contains(scopes, x => x == "categories.stats.read");
            Assert.Contains(scopes, x => x == "categories.stats.sums.read");
        }

        [Fact]
        public void BillingPermissionIsMutuallyExclusiveWhenAddedToBuilderAlreadyContainingOtherPermissions()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.AddPermissionsFor<Alerts>();
            Assert.Throws<InvalidOperationException>(() => sb.AddPermissionsFor<Billing>());
        }

        [Fact]
        public void BillingPermissionIsMutuallyExclusiveWhenOtherPermissionsAreAddedToBuilderAlreadyBilling()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.AddPermissionsFor<Billing>();
            Assert.Throws<InvalidOperationException>(() => sb.AddPermissionsFor<Alerts>());
        }
    }
}
