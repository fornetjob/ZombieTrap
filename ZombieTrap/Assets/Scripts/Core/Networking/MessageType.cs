namespace Game.Core.Networking
{
    public static class MessageTypeHelper
    {
        public static bool IsStrongMessage(this MessageType type)
        {
            return type == MessageType.Room
                || type == MessageType.Items;
        }
    }

    public enum MessageType
    {
        Connect,
        Room,
        Items,
        Positions,
        Reply
    }
}
