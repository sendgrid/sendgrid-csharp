namespace SendGrid.Permissions
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a set of possible scope options
    /// </summary>
    /// <seealso cref="string" />
    public class ScopeOptions : IEnumerable<string>
    {
        /// <summary>
        /// All scopes reference
        /// </summary>
        private static readonly ScopeOptions AllScopesReference = new ScopeOptions("all");

        private readonly List<string> options;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScopeOptions"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public ScopeOptions(params string[] options)
        {
            this.options = options.ToList();
        }

        /// <summary>
        /// Gets a scope option instance representing no scopes. This is useful for building nested permissions when the top
        /// level permission has no options itself.
        /// </summary>
        /// <value>
        /// The none.
        /// </value>
        public static ScopeOptions None => new ScopeOptions();

        /// <summary>
        /// Gets a scope option instance representing all scopes.
        /// </summary>
        /// <value>
        /// All.
        /// </value>
        public static ScopeOptions All => AllScopesReference;

        /// <summary>
        /// Gets the CRUD scope options: create, delete, read and update.
        /// </summary>
        /// <value>
        /// All crud.
        /// </value>
        public static ScopeOptions Crud => new ScopeOptions("create", "delete", "read", "update");

        /// <summary>
        /// Gets the read only scope.
        /// </summary>
        /// <value>
        /// The read only.
        /// </value>
        public static ScopeOptions ReadOnly => new ScopeOptions("read");

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<string> GetEnumerator()
        {
            return this.options.GetEnumerator();
        }

        /// <summary>
        /// Filter the specified options.
        /// </summary>
        /// <param name="requestedOptions">The options.</param>
        /// <returns>A new ScopeOptions object with the distinct values from this instance and <paramref name="requestedOptions"/></returns>
        public ScopeOptions Filter(ScopeOptions requestedOptions)
        {
            return requestedOptions == ScopeOptions.All
                ? this
                : new ScopeOptions(this.Join(requestedOptions, allowed => allowed, set => set, (a, s) => a).ToArray());
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
