namespace SendGrid.Permissions.Scopes
{
    using System.Linq;

    public class Mail : SendGridPermissionScope
    {
        public Mail()
            : base("mail")
        {
            this.AllowedOptions = new[] { "send" };
            this.SubScopes = new[]
            {
                new SendGridPermissionScope("batch")
            };
        }
    }

    public class Alerts : SendGridPermissionScope
    {
        public Alerts()
            : base("alerts")
        {
        }
    }

    public class ApiKeys : SendGridPermissionScope
    {
        public ApiKeys()
            : base("api_keys")
        {
        }
    }

    public class Categories : SendGridPermissionScope
    {
        public Categories()
            : base("categories")
        {
            this.SubScopes = new[]
            {
                new SendGridPermissionScope("stats", ScopeOptions.ReadOnly.ToArray())
                {
                    SubScopes = new[] { new SendGridPermissionScope("sums", ScopeOptions.ReadOnly.ToArray()) }
                }
            };
        }
    }

    public class Subusers : SendGridPermissionScope
    {
        public Subusers()
            : base("subusers")
        {
            this.SubScopes = new[]
            {
                new SendGridPermissionScope("credits")
                {
                    SubScopes = new[] { new SendGridPermissionScope("remaining") }
                },
                new SendGridPermissionScope("monitor"),
                new SendGridPermissionScope("reputations", ScopeOptions.ReadOnly.ToArray()),
                new SendGridPermissionScope("stats", ScopeOptions.ReadOnly.ToArray())
                {
                    SubScopes = new[]
                    {
                        new SendGridPermissionScope("monthly", ScopeOptions.ReadOnly.ToArray()),
                         new SendGridPermissionScope("sums", ScopeOptions.ReadOnly.ToArray())
                    }
                },
                new SendGridPermissionScope("summary", ScopeOptions.ReadOnly.ToArray()),
            };
        }
    }
}
