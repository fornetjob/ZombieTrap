using System.Net;

namespace Game.Core.Networking
{
    public delegate void MessageEventHandler(IPEndPoint endPoint, MessageContract message);
}
