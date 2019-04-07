using Game.Core.Dependencies;
using Assets.Scripts.EntitasExtensions;

using Entitas;
using Entitas.CodeGeneration.Attributes;

using System;
using System.Reflection;

public partial class Contexts
{
    public IDependencyContainer dependencies;

    [PostConstructor]
    public void OnPostContructor()
    {
        dependencies = new EntitasDependencyContainer(this);
    }
}

namespace Assets.Scripts.EntitasExtensions
{
    public class EntitasDependencyContainer: DependencyContainer
    {
        private readonly Contexts
            _context;

        private readonly Type
            _groupInterfaceType;

        public EntitasDependencyContainer(Contexts context)
        {
            _groupInterfaceType = typeof(IGroup);

            _context = context;
        }

        protected override void CheckField(object obj, FieldInfo field)
        {
            base.CheckField(obj, field);

            if (_groupInterfaceType.IsAssignableFrom(field.FieldType))
            {
                var groupAttrs = field.GetCustomAttributes(typeof(GroupAttribute), false);

                if (groupAttrs.Length > 0)
                {
                    var grAttr = (GroupAttribute)groupAttrs[0];

                    if (field.FieldType == typeof(IGroup<GameEntity>))
                    {
                        field.SetValue(obj, _context.game.GetGroup(GameMatcher.AllOf(grAttr.ComponentIndexes)));
                    }
                    else
                    {
                        throw new NotSupportedException(field.FieldType.Name);
                    }
                }
            }
        }

        protected override void CheckInterfaces(object dependency)
        {
            base.CheckInterfaces(dependency);

            if (dependency is IContextInitialize)
            {
                ((IContextInitialize)dependency).Initialize(_context);
            }
        }
    }
}