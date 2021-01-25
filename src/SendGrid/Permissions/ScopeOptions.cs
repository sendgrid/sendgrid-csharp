namespace SendGrid.Permissions
{
    /// <summary>
    /// Represents a set of possible scope options
    /// </summary>
    /// <seealso cref="string" />
    public enum ScopeOptions
    {
        /// <summary>
        /// All scopes. When filtering scopes no scopes will be exluded
        /// </summary>
        All,

        /// <summary>
        /// Read-only scopes. When fitlering scopes this will include only those that end with ".read"
        /// </summary>
        ReadOnly       
    }
}
