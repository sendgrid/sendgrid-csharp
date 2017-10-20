using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SendGrid.Permissions.Scopes
{
    public class Mail : CrudScope
    {
        public Mail()
            : base("mail")
        {
            this.AllowedOptions = new[] { "send" };
            this.SubScopes = new[]
            {
                new CrudScope("batch")
            };
        }
    }

    public class Alerts : CrudScope
    {
        public Alerts()
            : base("alerts")
        {
        }
    }

    public class Categories : CrudScope
    {
        public Categories()
            : base("categories")
        {
            this.SubScopes = new[]
            {
                new CrudScope("stats", "read")
                {
                    SubScopes = new[] { new CrudScope("sums", "read") }
                }
            };
        }
    }
}
