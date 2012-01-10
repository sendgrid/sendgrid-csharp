using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
