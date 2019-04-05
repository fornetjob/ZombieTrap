namespace Assets.Scripts.Core.Networking
{
    public interface IListener: IConnection
    {
        event MessageEventHandler OnReceive;
    }
}
