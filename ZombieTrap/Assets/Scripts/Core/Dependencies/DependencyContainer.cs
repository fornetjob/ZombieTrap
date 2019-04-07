using System;
using System.Collections.Generic;
using System.Reflection;

namespace Assets.Scripts.Core.Dependencies
{
    public class DependencyContainer : IDependencyContainer
    {
        private static readonly Type
            _interfaceType = typeof(IDependency);

        private readonly Dictionary<string, IDependency>
            _dict = new Dictionary<string, IDependency>();

        public void Registrate<TInterface, TDependency>()
        {
            Registrate(typeof(TInterface), typeof(TDependency));
        }

        public T Provide<T>()
            where T : IDependency
        {
            var type = typeof(T);

            var typeKey = GetTypeKey(type);

            if (_dict.ContainsKey(typeKey) == false)
            {
                AddDependency(typeKey, type);
            }

            return (T)_dict[typeKey];
        }

        public void InjectTo(object obj)
        {
            var fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

            for (int i = 0; i < fields.Length; i++)
            {
                CheckField(obj, fields[i]);
            }

            CheckInterfaces(obj);
        }

        private void Registrate(Type interfaceType, Type dependencyType)
        {
            AddDependency(GetTypeKey(interfaceType), dependencyType);
        }

        protected virtual void CheckField(object obj, FieldInfo field)
        {
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
        }

        protected virtual void CheckInterfaces(object dependency)
        {
            if (dependency is IDependencyInitialize)
            {
                ((IDependencyInitialize)dependency).Initialize();
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
