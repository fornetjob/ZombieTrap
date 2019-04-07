namespace Game.Core.Networking
{
    public interface IListener: IConnection
    {
        event MessageEventHandler OnReceive;
    }
}
