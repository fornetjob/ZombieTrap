namespace Assets.Scripts.Core.Networking
{
    public interface IMessageListener
    {
        void Open();
        void Close();

        //void Send(MessageContract msg);
        event MessageEventHandler OnReceive;
    }
}