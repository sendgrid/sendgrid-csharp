namespace SendGrid.Permissions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SendGrid.Permissions.Scopes;

    /// <summary>
    /// A builder for constructing a list of API Key permissions scopes
    /// </summary>
    public class SendGridPermissionsBuilder
    {
        private IDictionary<ISendGridPermissionScope, ScopeOptions> scopes = new Dictionary<ISendGridPermissionScope, ScopeOptions>();

        private IDictionary<Type, ISendGridPermissionScope> scopeMap = new Dictionary<Type, ISendGridPermissionScope>
        {
            { typeof(Alerts), new Alerts() },
            { typeof(Categories), new Categories() },
            { typeof(Mail), new Mail() },
            { typeof(ApiKeys), new ApiKeys() },
            { typeof(Subusers), new Subusers() },
        };

        /// <summary>
        /// Builds the list of API Key scopes based on the permissions that have been added to this instance.
        /// </summary>
        /// <returns>A list of strings representing the scope names.</returns>
        public IEnumerable<string> Build()
        {
            var scopes = this.scopes.SelectMany(x => x.Key.Build(x.Value)).ToArray();
            return scopes;
        }

        /// <summary>
        /// Creates the full admin permissions.
        /// </summary>
        /// <returns>The builder instance with the permissions added.</returns>
        public SendGridPermissionsBuilder CreateAdminPermissions()
        {
            foreach (var scope in this.scopeMap.Keys)
            {
                this.AddPermissionsFor(scope, ScopeOptions.All);
            }

            return this;
        }

        /// <summary>
        /// Creates the full access mail send permission list.
        /// </summary>
        /// <returns>The builder instance with the permissions added.</returns>
        public SendGridPermissionsBuilder CreateFullAccessMailSend()
        {
            return this.AddPermissionsFor<Mail>(ScopeOptions.All);
        }

        /// <summary>
        /// Adds the permissions for the specified <typeparamref name="TScope"/>
        /// </summary>
        /// <typeparam name="TScope">The type of the scope.</typeparam>
        /// <param name="options">The options.</param>
        /// <returns>The builder instance with the permissions added.</returns>
        public SendGridPermissionsBuilder AddPermissionsFor<TScope>(ScopeOptions options)
            where TScope : SendGridPermissionScope
        {
            this.scopes.Add(this.scopeMap[typeof(TScope)], options);
            return this;
        }

        /// <summary>
        /// Adds the permissions for the specified <paramref name="scopeType"/>
        /// </summary>
        /// <typeparam name="TScopeOptions">The type of the scope options.</typeparam>
        /// <param name="scopeType">Type of the scope.</param>
        /// <param name="options">The options.</param>
        /// <returns>The builder instance with the permissions added</returns>
        private SendGridPermissionsBuilder AddPermissionsFor<TScopeOptions>(Type scopeType, TScopeOptions options)
            where TScopeOptions : ScopeOptions
        {
            this.scopes.Add(this.scopeMap[scopeType], options);
            return this;
        }
    }
}
