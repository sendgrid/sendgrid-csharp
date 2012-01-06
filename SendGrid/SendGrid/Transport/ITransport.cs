using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SendGrid.Transport
{
    public interface ITransport
    {
        void Deliver(ISendGrid message);
    }
}
