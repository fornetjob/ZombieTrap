using Assets.Scripts.Core.Networking;
using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Assets.Scripts.Features.Client.Networking
{
    [Event(EventTarget.Self)]
    public class ConnectionStateComponent:IComponent
    {
        public ConnectionState value;
        public int tryCount;
    }
}
