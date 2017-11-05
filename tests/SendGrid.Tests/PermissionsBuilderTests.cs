using SendGrid.Permissions;
using SendGrid.Permissions.Scopes;
using System;
using System.Linq;
using Xunit;

namespace SendGrid.Tests
{

    public class PermissionsBuilderTests
    {
        [Fact]
        public void CanFilterFinalScopeList()
        {
            var sb = new SendGridPermissionsBuilder();
            var scopes = sb.AddPermissionsFor<Alerts>()
                .Exclude(s => s.EndsWith("delete") || s.EndsWith("read"))
                .Build()
                .ToArray();

            Assert.Contains(scopes, x => x == "alerts.create");
            Assert.Contains(scopes, x => x == "alerts.update");
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
