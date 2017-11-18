namespace SendGrid.Permissions.Scopes
{
    /// <summary>
    /// Scopes for devices.stats
    /// </summary>
    /// <seealso cref="SendGridPermissionScope" />
    internal class Devices : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Devices"/> class.
        /// </summary>
        public Devices()
            : base("devices.stats", "read")
        {
        }
    }
}
