namespace SendGrid.Transport
{
    public interface ITransport
    {
        void Deliver(ISendGrid message);
    }
}
