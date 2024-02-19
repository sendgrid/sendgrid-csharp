namespace SendGrid.Helpers.Errors.Model
{
    /// <summary>
    /// Each SMTP call you make returns a response.
    /// 200 responses are usually success responses, and 400 responses are usually deferrals.
    /// SendGrid continues to retry resending 400 messages for up to 72 hours.
    /// 500 responses are hard failures that are not retried by our servers.
    /// </summary>
    /// <remarks>
    /// <see href="https://docs.sendgrid.com/for-developers/sending-email/smtp-errors-and-troubleshooting"/>.
    /// </remarks>
    public enum SendGridStatusCode
    {
        /// <summary>
        /// Your mail has been successfully queued!
        /// This response indicates that the recipient server has accepted the message.
        /// </summary>
        QueuedForDelivery = 250,

        /// <summary>
        /// This means the "from" address does not match a verified Sender Identity.
        /// Mail cannot be sent until this error is resolved.
        /// </summary>
        /// <remarks>
        /// To learn how to resolve this error, see our <see href="https://docs.sendgrid.com/for-developers/sending-email/sender-identity">Sender Identity requirements</see>.
        /// </remarks>
        InvalidFromAddress = 403,

        /// <summary>
        /// Messages are temporarily deferred because of recipient server policy - often it's because of too many messages or connections in too short of a timeframe.
        /// </summary>
        /// <remarks>
        /// We continue to retry deferred messages for up to 72 hours.
        /// Consider temporarily sending less messages to a domain that is returning this code because this could further delay your messages currently being tried.
        /// </remarks>
        MessagesDeferred = 421,

        /// <summary>
        /// The message failed because the recipient's mailbox was unavailable, perhaps because it was locked or was not routable at the time.
        /// </summary>
        /// <remarks>
        /// We continue to retry messages for up to 72 hours.
        /// Consider temporarily sending less messages to a domain that is returning this code because this could further delay your messages currently being tried.
        /// </remarks>
        MailboxUnavailable = 450,

        /// <summary>
        /// There is a credit limit of emails per day enforced in error.
        /// </summary>
        /// <remarks>
        /// <see href="https://support.sendgrid.com/hc">Contact support</see> to remove that limit.
        /// </remarks>
        MaximumCreditsExceeded = 451,

        /// <summary>
        /// The message has been deferred due to insufficient system storage.
        /// </summary>
        /// <remarks>
        /// We continue to retry messages for up to 72 hours.
        /// </remarks>
        TooManyRecipientsThisHour = 452,

        /// <summary>
        /// The user’s mailbox was unavailable.
        /// Usually because it could not be found, or because of incoming policy reasons.
        /// </summary>
        /// <remarks>
        /// Remove these address from your list - it is likely a fake, or it was mistyped.
        /// </remarks>
        InvalidMailbox = 550,

        /// <summary>
        /// The intended mailbox does not exist on this recipient server.
        /// </summary>
        /// <remarks>
        /// Remove these addresses from your list.
        /// </remarks>
        UserDoesNotExist = 551,

        /// <summary>
        /// The recipients mailbox has exceeded its storage limits.
        /// </summary>
        /// <remarks>
        /// We don't resend messages with this error code because this is usually a sign this is an abandoned email.
        /// </remarks>
        MailboxIsFull = 552,

        /// <summary>
        /// The message was refused because the mailbox name is either malformed or does not exist.
        /// </summary>
        /// <remarks>
        /// Remove these addresses from your list.
        /// </remarks>
        InvalidUser = 553,

        /// <summary>
        /// This is a default response that can be caused by a lot of issues.
        /// </summary>
        /// <remarks>
        /// There is often a human readable portion of this error that gives more detailed information, but if not,
        /// remove these addresses from your list.
        /// </remarks>
        MailRefused = 554,
    }
}
