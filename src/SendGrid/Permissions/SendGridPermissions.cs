using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using SendGrid.Permissions.Scopes;

namespace SendGrid
{
    public interface IScope
    {
        string Name { get; }

        IEnumerable<IScope> SubScopes { get; }

        string ToJson(ScopeOptions options, string prefix = null);
    }

    public abstract class Scope : IScope
    {
        protected Scope(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets the name of the scope
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets or sets gets the sub-scopes
        /// </summary>
        public IEnumerable<IScope> SubScopes { get; internal set; }

        public abstract string ToJson(ScopeOptions options, string prefix);
    }

    public class CrudScope : Scope
    {
        internal CrudScope(string name)
            : base(name)
        {
            this.AllowedOptions = ScopeOptions.AllCrud.ToArray();
        }

        internal CrudScope(string name, params string[] allowedOptions)
            : base(name)
        {
            this.AllowedOptions = allowedOptions.ToArray();
        }

        public string[] AllowedOptions { get; protected set; }

        public override string ToJson(ScopeOptions options, string prefix = null)
        {
            var setOptions = this.AllowedOptions.Join(options, allowed => allowed, set => set, (a, s) => a);
            var sb = new StringBuilder();
            var name = prefix == null ? this.Name : $"{prefix}.{this.Name}";
            foreach (var so in setOptions)
            {
                sb.Append($"\"{name}.{so}\",");
            }

            if (this.SubScopes != null && this.SubScopes.Any())
            {
                foreach (var subScope in this.SubScopes)
                {
                    sb.Append(subScope.ToJson(options, name));
                }
            }

            return sb.ToString();
        }
    }

    public class ScopeOptions : IEnumerable<string>
    {
        private List<string> options;

        public ScopeOptions(params string[] options)
        {
            this.options = options.ToList();
        }

        private ScopeOptions Plus(params string[] scope)
        {
            return new ScopeOptions(this.options.Concat(scope).ToArray());
        }

        public static ScopeOptions All => AllCrud.Plus("send");

        public static ScopeOptions AllCrud => new ScopeOptions("create", "delete", "read", "update");

        public static ScopeOptions ReadOnly => new ScopeOptions("read");

        public IEnumerator<string> GetEnumerator()
        {
            return this.options.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    public class SendGridPermissionsBuilder
    {
        private IDictionary<IScope, ScopeOptions> scopes = new Dictionary<IScope, ScopeOptions>();

        private IDictionary<Type, IScope> scopeMap = new Dictionary<Type, IScope>
        {
            { typeof(Alerts), new Alerts() },
            { typeof(Categories), new Categories() },
            { typeof(Mail), new Mail() }
        };

        public SendGridPermissionsBuilder AddPermissionsFor<TScope>(ScopeOptions options)
            where TScope : Scope
        {
            this.scopes.Add(this.scopeMap[typeof(TScope)], options);
            return this;
        }

        private SendGridPermissionsBuilder AddPermissionsFor<TScopeOptions>(Type scopeType, TScopeOptions options)
    where TScopeOptions : ScopeOptions
        {
            this.scopes.Add(this.scopeMap[scopeType], options);
            return this;
        }

        public SendGridPermissionsBuilder CreateAdminPermissions()
        {
            foreach (var scope in this.scopeMap.Keys)
            {
                this.AddPermissionsFor(scope, ScopeOptions.All);
            }

            return this;
        }

        public string ToJson()
        {
            var sb = new StringBuilder();
            sb.Append("[");

            foreach (var scope in this.scopes.Take(this.scopes.Count - 1))
            {
                sb.Append(scope.Key.ToJson(scope.Value));
            }

            sb.Append(this.scopes.Last().Key.ToJson(this.scopes.Last().Value));
            return sb.ToString().TrimEnd(',') + "]";
        }

        public string CreateFullAccessMailSend()
        {
            this.AddPermissionsFor<Mail>(ScopeOptions.All);
            return this.ToJson();
        }
    }
}
