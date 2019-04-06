﻿namespace Assets.Scripts.Core.Networking
{
    public static class MessageTypeHelper
    {
        public static bool IsStrongMessage(this MessageType type)
        {
            return type == MessageType.Zombies;
        }
    }

    public enum MessageType
    {
        Connect,
        Zombies,
        Positions,
        Reply
    }
}
