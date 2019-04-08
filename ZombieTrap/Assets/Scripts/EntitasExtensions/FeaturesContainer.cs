using Assets.Scripts.EntitasExtensions;

using Entitas;
using Entitas.CodeGeneration.Attributes;

using System.Collections.Generic;

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
    public class FeaturesContainer : Feature, IFixedExecuteSystem
    {
        private readonly Contexts
            _context;

        private readonly EntitasDependencyContainer
            _container;

        private List<IFixedExecuteSystem>
            _fixedExecuteSystems = new List<IFixedExecuteSystem>();

        public FeaturesContainer(Contexts context)
        {
            _context = context;

            _container = (EntitasDependencyContainer)_context.dependencies;
        }

        public void FixedExecute()
        {
            for (int i = 0; i < _fixedExecuteSystems.Count; i++)
            {
                _fixedExecuteSystems[i].FixedExecute();
            }
        }

        public override Systems Add(ISystem system)
        {
            if (system is IFixedExecuteSystem)
            {
                _fixedExecuteSystems.Add((IFixedExecuteSystem)system);
            }

            _container.InjectTo(system);

            return base.Add(system);
        }
    }
}