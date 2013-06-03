using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SimpleIoc.Exceptions;

namespace SimpleIoc
{
    public class SimpleContainer : IContainer
    {
        #region Private fields

        private readonly IDictionary<Type, RegisteredObject> _registeredObjects =
            new Dictionary<Type, RegisteredObject>();

        #endregion
        #region Private methods

        private object GetInstance(RegisteredObject registeredObject)
        {
            if (registeredObject.Instance == null ||
                registeredObject.LifeCycle == LifeCycle.Transient)
            {
                var constructorInfo = ResolveConstructor(registeredObject);
                var parameters = ResolveConstructorParameters(constructorInfo);
                registeredObject.CreateInstance(parameters.ToArray());
            }
            return registeredObject.Instance;
        }

        private ConstructorInfo ResolveConstructor(RegisteredObject registeredObject)
        {
            var constructors = registeredObject.GetType().GetConstructors();
            //with DependencyConstructor attribute
            var dependencyConstructor =
                constructors.FirstOrDefault(
                    ci => ci.GetCustomAttribute<DependencyConstructorAttribute>() != null);
            if (dependencyConstructor != null)
            {
                return dependencyConstructor;
            }

            var defaultContructor = constructors.OrderBy(ci => ci.GetParameters().Count()).FirstOrDefault();
            if (defaultContructor == null)
            {
                throw new MissingMethodException();
            }
            return defaultContructor;
        }

        private IEnumerable<object> ResolveConstructorParameters(ConstructorInfo constructorInfo)
        {            
            return constructorInfo.GetParameters()
                                  .Select(parameter => ResolveObject(parameter.ParameterType));
        }

        private object ResolveObject(Type typeToResolve)
        {
            RegisteredObject registeredObject = null;

            if (!_registeredObjects.TryGetValue(typeToResolve, out registeredObject))
            {
                throw new TypeNotRegisteredException(string.Format(
                    "The type {0} has not been registered.", typeToResolve.Name));
            }
            return GetInstance(registeredObject);
        }

        #endregion
        #region IContainer Members

        public void RegisterType<T>(bool singleton = true) where T : class
        {
            RegisterType<T, T>(singleton);
        }

        public void RegisterType<TFrom, TTo>(bool singleton = true) where TTo : TFrom
        {
            _registeredObjects[typeof (TFrom)] = new RegisteredObject(typeof (TFrom), typeof (TTo),
                                                                      singleton
                                                                          ? LifeCycle.Singleton
                                                                          : LifeCycle.Transient);            
        }

        public T Resolve<T>()            
        {
            return (T) ResolveObject(typeof (T));
        }

        public void RegisterInstance<T>(T instance)
        {
            _registeredObjects[typeof (T)] = new RegisteredObject(typeof (T), instance.GetType(), LifeCycle.Singleton)
                {
                    Instance = instance
                };
        }

        #endregion
    }
}