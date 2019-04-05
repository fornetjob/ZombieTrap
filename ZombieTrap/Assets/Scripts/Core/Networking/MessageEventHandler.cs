using System.Net;

namespace Assets.Scripts.Core.Networking
{
    public delegate void MessageEventHandler(IPEndPoint endPoint, MessageContract message);
}
