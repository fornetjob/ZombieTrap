using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Assets.Scripts.Features.Room
{
    [Unique]
    [Event(EventTarget.Self)]
    public class RoomComponent:IComponent
    {
        public uint number;
    }
}
