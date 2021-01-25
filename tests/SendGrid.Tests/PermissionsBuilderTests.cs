namespace SendGrid.Tests
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Moq;
    using Permissions;
    using Permissions.Scopes;
    using Xunit;
    using Xunit.Abstractions;

    public class PermissionsBuilderTests
    {
        private readonly ITestOutputHelper _output;

        public PermissionsBuilderTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task CanFilterFinalScopeList()
        {
            var content = "{\"scopes\": [\"alerts.read\"]}";
            var client = new Mock<ISendGridClient>();
            client.Setup(x => x.RequestAsync(SendGridClient.Method.GET, null, null, "scopes", CancellationToken.None))
                .ReturnsAsync(new Response(HttpStatusCode.OK, new StringContent(content), null));

            var sb = new SendGridPermissionsBuilder();
            sb.AddPermissionsFor<Alerts>();
            await sb.FilterByCurrentApiKeyAsync(client.Object);
            var scopes = sb.Build().ToArray();

            Assert.Single(scopes);
            Assert.Contains(scopes, x => x == "alerts.read");
        }

        [Fact]
        public void BuildReadOnlyScopeContainsOnlyReadScope()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.AddPermissionsFor<Alerts>(ScopeOptions.ReadOnly);

            var scopes = sb.Build();

            Assert.Single(scopes);
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
    }
}
