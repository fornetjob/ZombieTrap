namespace Game.Core.Networking
{
    public static class MessageTypeHelper
    {
        public static bool IsStrongMessage(this MessageType type)
        {
            return type == MessageType.ServerSync
                || type == MessageType.Items;
        }
    }

    public enum MessageType
    {
        Connect,
        ServerSync,
        Items,
        Positions,
        Damage,
        Reply
    }
}
