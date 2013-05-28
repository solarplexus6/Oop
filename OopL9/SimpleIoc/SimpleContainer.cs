using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleIoc
{
    public class SimpleContainer : IContainer
    {
        #region Private fields

        private readonly IList<RegisteredObject> _registeredObjects = new List<RegisteredObject>();

        #endregion
        #region Private methods

        private object GetInstance(RegisteredObject registeredObject)
        {
            if (registeredObject.Instance == null ||
                registeredObject.LifeCycle == LifeCycle.Transient)
            {
                var parameters = ResolveConstructorParameters(registeredObject);
                registeredObject.CreateInstance(parameters.ToArray());
            }
            return registeredObject.Instance;
        }

        private IEnumerable<object> ResolveConstructorParameters(RegisteredObject registeredObject)
        {
            var constructorInfo = registeredObject.ConcreteType.GetConstructors().First();
            return constructorInfo.GetParameters()
                                  .Select(parameter => ResolveObject(parameter.ParameterType));
        }

        private object ResolveObject(Type typeToResolve)
        {
            var registeredObject = _registeredObjects.FirstOrDefault(o => o.TypeToResolve == typeToResolve);
            if (registeredObject == null)
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
            _registeredObjects.Add(new RegisteredObject(typeof (TFrom), typeof (TTo),
                                                        singleton ? LifeCycle.Singleton : LifeCycle.Transient));
        }

        public T Resolve<T>()            
        {
            return (T) ResolveObject(typeof (T));
        }

        #endregion
    }
}