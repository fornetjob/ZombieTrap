using Game.Core.Networking;
using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Assets.Scripts.Features.Client.Networking
{
    [Unique]
    [Event(EventTarget.Self)]
    public class ConnectionStateComponent:IComponent
    {
        public ConnectionState value;
        public int tryCount;
    }
}
