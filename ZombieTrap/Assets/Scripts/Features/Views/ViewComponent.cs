using Entitas;

namespace Assets.Scripts.Features.Core.Views
{
    public class ViewComponent:IComponent
    {
        public string name;
        public GameEntity attachedEntity;
    }
}
