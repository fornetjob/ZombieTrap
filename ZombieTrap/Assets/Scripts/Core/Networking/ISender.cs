﻿namespace Game.Core.Networking
{
    public interface ISender:IConnection
    {
        void Send(MessageContract msg);
    }
}
