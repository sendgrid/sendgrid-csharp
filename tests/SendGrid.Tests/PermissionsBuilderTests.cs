namespace SendGrid.Tests
{
    using System;
    using System.Linq;
    using Permissions;
    using Permissions.Scopes;
    using Xunit;

    public class PermissionsBuilderTests
    {
        [Fact]
        public void AddPermissionWithReadOnlyScopeOptionIncludesOnlyReadScopes()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.AddPermissionsFor<Templates>(ScopeOptions.ReadOnly);

            var scopes = sb.Build().ToArray();

            Assert.Equal(3, scopes.Length);
            Assert.Contains("templates.read", scopes);
            Assert.Contains("templates.versions.activate.read", scopes);
            Assert.Contains("templates.versions.read", scopes);
        }

        [Fact]
        public void AddPermissionWithAllScopeOptionIncludesAllScopes()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.AddPermissionsFor<Templates>(ScopeOptions.All);

            var scopes = sb.Build().ToArray();

            Assert.Equal(12, scopes.Length);
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
        public void BillingPermissionIsMutuallyExclusiveWhenAddedToBuilderAlreadyContainingOtherPermissions()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.AddPermissionsFor<Alerts>();
            Assert.Throws<InvalidOperationException>(() => sb.AddPermissionsFor<Billing>());
        }

        [Fact]
        public void BillingPermissionIsMutuallyExclusiveWhenOtherPermissionsAreAddedToBuilderAlreadyContainingBilling()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.AddPermissionsFor<Billing>();
            Assert.Throws<InvalidOperationException>(() => sb.AddPermissionsFor<Alerts>());
        }

        [Fact]
        public void CanFilterByFunc()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.AddPermissionsFor<MailSettings>();
            sb.Exclude(scope => scope.Contains("spam"));
            var scopes = sb.Build().ToArray();
            Assert.DoesNotContain(scopes, x => x.Contains("spam"));
        }

        [Fact]
        public void CanFilterByList()
        {
            var apiScopes = new[] { "alerts.read" };
            var sb = new SendGridPermissionsBuilder();
            sb.AddPermissionsFor<Alerts>();
            sb.Exclude(scope => !apiScopes.Contains(scope));
            var scopes = sb.Build().ToArray();
            Assert.Single(scopes);
            Assert.Contains(scopes, x => x == "alerts.read");
        }

        [Fact]
        public void CanIncludeAdditionalScopes()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.Include("mail.send", "mail.batch.create");
            var scopes = sb.Build().ToArray();
            Assert.Contains("mail.send", scopes);
            Assert.Contains("mail.batch.create", scopes);
        }

        [Fact]
        public void CanIncludeAdditionalScopesList()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.Include(new[] { "alerts.create", "alerts.delete", "mail.send" });
            var scopes = sb.Build().ToArray();
            Assert.Contains("alerts.create", scopes);
            Assert.Contains("alerts.delete", scopes);
            Assert.Contains("mail.send", scopes);
        }
    }
}
