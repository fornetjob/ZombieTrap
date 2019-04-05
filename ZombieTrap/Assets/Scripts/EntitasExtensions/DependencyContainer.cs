using Assets.Scripts.EntitasExtensions;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;

public partial class Contexts
{
    public DependencyContainer dependencies;

    [PostConstructor]
    public void OnPostContructor()
    {
        dependencies = new DependencyContainer(this);
    }
}

namespace Assets.Scripts.EntitasExtensions
{
    public class DependencyContainer
    {
        private readonly Contexts
            _context;

        private readonly Type
            _interfaceType;

        private readonly Type
            _groupInterfaceType;

        private readonly Dictionary<string, IDependency>
            _dict = new Dictionary<string, IDependency>();

        public DependencyContainer(Contexts context)
        {
            _interfaceType = typeof(IDependency);
            _groupInterfaceType = typeof(IGroup);

            _context = context;
        }

        public void Registrate(Type dependencyType, Type interfaceType)
        {
            AddDependency(GetTypeKey(interfaceType), dependencyType);
        }

        public void InjectTo(object obj)
        {
            var fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

            for (int i = 0; i < fields.Length; i++)
            {
                var field = fields[i];

                if (_interfaceType.IsAssignableFrom(field.FieldType))
                {
                    if (field.IsPublic)
                    {
                        throw new System.NotSupportedException("Public not supported");
                    }

                    var key = GetTypeKey(field.FieldType);

                    if (_dict.ContainsKey(key) == false)
                    {
                        AddDependency(key, field.FieldType);
                    }

                    var dependency = _dict[key];

                    field.SetValue(obj, dependency);
                }
                else if (_groupInterfaceType.IsAssignableFrom(field.FieldType))
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

            CheckInterfaces(obj);
        }

        private void CheckInterfaces(object dependency)
        {
            if (dependency is IDependencyInitialize)
            {
                ((IDependencyInitialize)dependency).Initialize();
            }

            if (dependency is IContextInitialize)
            {
                ((IContextInitialize)dependency).Initialize(_context);
            }
        }

        private IDependency AddDependency(string key, Type dependencyType)
        {
            var attrs = dependencyType.GetCustomAttributes(typeof(DependencyAttribute), false);

            if (attrs.Length > 0)
            {
                dependencyType = ((DependencyAttribute)attrs[0]).DependencyType;
            }

            var dependency = (IDependency)Activator.CreateInstance(dependencyType);

            _dict.Add(key, dependency);

            InjectTo(dependency);

            return dependency;
        }

        private string GetTypeKey(Type type)
        {
            return type.FullName;
        }
    }
}