using SendGrid.Permissions.Scopes;
using Xunit;

namespace SendGrid.Tests.Permissions
{
    public class PermissionsFixture
    {
        [Fact]
        public void CreateReadOnlyCrudScope()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.AddPermissionsFor<Alerts>(ScopeOptions.ReadOnly);

           var json =  sb.ToJson();

            Assert.Equal("[\"alerts.read\"]", json);
        }

        [Fact]
        public void CreateAllCrudScope()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.AddPermissionsFor<Alerts>(ScopeOptions.All);

            var json = sb.ToJson();

            Assert.Equal("[\"alerts.create\",\"alerts.delete\",\"alerts.read\",\"alerts.update\"]", json);
        }

        [Fact]
        public void BuildAllCrudScope()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.AddPermissionsFor<Alerts>(ScopeOptions.All);

            var scopes = sb.Build();
            Assert.Contains(scopes, x => x == "alerts.create");
            Assert.Contains(scopes, x => x == "alerts.delete");
            Assert.Contains(scopes, x => x == "alerts.read");
            Assert.Contains(scopes, x => x == "alerts.update");
        }

        [Fact]
        public void CreateAdminCrudScope()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.CreateAdminPermissions();
            var json = sb.ToJson();

            Assert.Equal("[\"alerts.create\",\"alerts.delete\",\"alerts.read\",\"alerts.update\",\"categories.create\",\"categories.delete\",\"categories.read\",\"categories.update\",\"categories.stats.read\",\"categories.stats.sums.read\",\"mail.send\",\"mail.batch.create\",\"mail.batch.delete\",\"mail.batch.read\",\"mail.batch.update\"]", json);
        }

        [Fact]
        public void CreateScopeWithSubScopes()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.AddPermissionsFor<Categories>(ScopeOptions.All);
            var json = sb.ToJson();

            Assert.Equal("[\"categories.create\",\"categories.delete\",\"categories.read\",\"categories.update\",\"categories.stats.read\",\"categories.stats.sums.read\"]", json);
        }

        [Fact]
        public void CreateFullAccessMailGroupScope()
        {
            var sb = new SendGridPermissionsBuilder();
            sb.CreateFullAccessMailSend();
            var json = sb.ToJson();

            Assert.Equal("[\"mail.send\",\"mail.batch.create\",\"mail.batch.delete\",\"mail.batch.read\",\"mail.batch.update\"]", json);
        }
    }
}
