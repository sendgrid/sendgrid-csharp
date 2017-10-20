namespace SendGrid.Permissions
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class ScopeOptions : IEnumerable<string>
    {
        private readonly List<string> options;

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
}
