namespace Assets.Scripts.Core.Networking
{
    public interface IConnection
    {
        void Open();
        void Close();

        void Send(MessageContract msg);
        event MessageEventHandler OnReceive;
    }
}