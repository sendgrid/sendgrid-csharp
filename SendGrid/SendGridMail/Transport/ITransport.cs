namespace SendGridMail.Transport
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITransport
    {
        /// <summary>
        /// Delivers a message using the protocol of the derived class
        /// </summary>
        /// <param name="message">the message to be delivered</param>
        void Deliver(ISendGrid message);
    }
}
