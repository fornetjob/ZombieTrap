using Entitas;

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

            _container = new DependencyContainer(context);
        }

        public override Systems Add(ISystem system)
        {
            _container.InjectTo(system);

            return base.Add(system);
        }
    }
}