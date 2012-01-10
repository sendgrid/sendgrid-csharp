namespace SendGrid.Transport
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITransport
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void Deliver(ISendGrid message);
    }
}
