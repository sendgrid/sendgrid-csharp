

namespace SendGrid.Tests
{
    using Permissions;
    using Permissions.Scopes;
    using System;
    using System.Linq;
    using Xunit;

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
        public void BuildReadOnlyScopeContainsOnlyReadScope()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.AddPermissionsFor<Alerts>(ScopeOptions.ReadOnly);

            var scopes = sb.Build();

            Assert.Contains(scopes, x => x == "alerts.read");
        }

        [Fact]
        public void BuildFullAccessScopeContainsAllCrudScopes()
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
        public void BuildFullAccessScopeContainsExtraScopes()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.AddPermissionsFor<Mail>(ScopeOptions.All);

            var scopes = sb.Build().ToArray();

            Assert.Contains(scopes, x => x == "mail.send");
        }

        [Fact]
        public void BuildScopeWithSubScopesContainsSubScopesWithPrefixOfParentScope()
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
        public void BillingPermissionIsMutuallyExclusiveWhenOtherPermissionsAreAddedToBuilderAlreadyContainingBilling()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.AddPermissionsFor<Billing>();
            Assert.Throws<InvalidOperationException>(() => sb.AddPermissionsFor<Alerts>());
        }
    }
}
