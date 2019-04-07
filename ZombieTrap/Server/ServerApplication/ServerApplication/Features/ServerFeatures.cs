using Assets.Scripts.Core.Dependencies;
using System.Collections.Generic;

namespace ServerApplication.Features
{
    public class ServerFeatures : IFixedExecuteSystem
    {
        private DependencyContainer
            _dependencyContainer = new DependencyContainer();

        private List<IFixedExecuteSystem>
            _fixedSystems = new List<IFixedExecuteSystem>();

        public IDependencyContainer Dependencies {  get { return _dependencyContainer; } }

        public void Add(IFixedExecuteSystem system)
        {
            _dependencyContainer.InjectTo(system);

            _fixedSystems.Add(system);
        }

        public void FixedExecute()
        {
            for (int i = 0; i < _fixedSystems.Count; i++)
            {
                _fixedSystems[i].FixedExecute();
            }
        }
    }
}
