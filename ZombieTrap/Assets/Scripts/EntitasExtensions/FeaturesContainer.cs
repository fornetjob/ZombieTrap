using Assets.Scripts.EntitasExtensions;
using Assets.Scripts.Features.Core.GameTime;
using Assets.Scripts.Features.Core.Move;
using Assets.Scripts.Features.Core.Views;
using Entitas;
using Entitas.CodeGeneration.Attributes;

public partial class Contexts
{
    public FeaturesContainer feautures;

    [PostConstructor]
    public void OnFeaturesPostContructor()
    {
        feautures = new FeaturesContainer(this);
    }
}

namespace Assets.Scripts.EntitasExtensions
{
    public class FeaturesContainer : Feature
    {
        private readonly Contexts
            _context;

        private readonly DependencyContainer
            _container;

        public FeaturesContainer(Contexts context)
        {
            _context = context;

            _container = (DependencyContainer)_context.dependencies;

            Add(new GameEventSystems(context));
            Add(new GameTimeSystem());
            Add(new ViewSystem());
            Add(new MoveSystem());
        }

        public override Systems Add(ISystem system)
        {
            _container.InjectTo(system);

            return base.Add(system);
        }
    }
}