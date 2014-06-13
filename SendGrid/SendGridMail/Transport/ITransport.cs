using System.Threading.Tasks;


namespace SendGrid
{
	/// <summary>
	///     Encapsulates the transport mechanism so that it can be used in a generic way,
	///     regardless of the transport type
	/// </summary>
	public interface ITransport
	{
		/// <summary>
		///     Delivers a message using the protocol of the derived class
		/// </summary>
		/// <param name="message">the message to be delivered</param>
		void Deliver(ISendGrid message);


        /// <summary>
        ///     Asynchronously delivers a message using the protocol of the derived class
        /// </summary>
        /// <param name="message">the message to be delivered</param>
        Task DeliverAsync(ISendGrid message);
	}
}