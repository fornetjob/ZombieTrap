namespace Game.Core.Networking
{
    public interface IConnection
    {
        void Open();
        void Close();
    }
}