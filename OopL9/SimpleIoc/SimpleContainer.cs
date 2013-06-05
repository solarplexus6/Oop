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

        private object GetInstance(RegisteredObject registeredObject, HashSet<Type> resolvingTypes = null)
        {
            if (registeredObject.Instance == null ||
                registeredObject.LifeCycle == LifeCycle.Transient)
            {
                foreach (var constructorInfo in ResolveConstructors(registeredObject))
                {
                    try
                    {
                        if (resolvingTypes == null)
                        {
                            resolvingTypes = new HashSet<Type> {registeredObject.TypeToResolve};
                        }
                        else
                        {
                            resolvingTypes.Add(registeredObject.TypeToResolve);
                        }

                        var parameters = constructorInfo.GetParameters().Select(pi => pi.ParameterType).ToArray();
                        if (parameters.Any(resolvingTypes.Contains))
                        {
                            throw new CyclicDependencyException();
                        }

                        registeredObject.CreateInstance(parameters.Select(pi => ResolveObject(pi, resolvingTypes)).ToArray());
                    }
                    catch (MissingMethodException)
                    {
                        continue;
                    }
                    break;
                }
                if (registeredObject.Instance == null)
                {
                    throw new MissingMethodException();
                }
            }
            if (resolvingTypes != null)
            {
                resolvingTypes.Remove(registeredObject.TypeToResolve);
            }
            return registeredObject.Instance;
        }

        private IEnumerable<ConstructorInfo> ResolveConstructors(RegisteredObject registeredObject)
        {
            var constructors = registeredObject.ConcreteType.GetConstructors();
            //with DependencyConstructor attribute
            var dependencyConstructor =
                constructors.FirstOrDefault(
                    ci => ci.GetCustomAttribute<DependencyConstructorAttribute>() != null);
            if (dependencyConstructor != null)
            {
                yield return dependencyConstructor;
            }            
            var defaultConstructors = constructors.OrderBy(ci => ci.GetParameters().Count());
            if (!defaultConstructors.Any())
            {
                throw new MissingMethodException();
            }
            foreach (var constructorInfo in defaultConstructors)
            {
                yield return constructorInfo;
            }
        }


        private object ResolveObject(Type typeToResolve, HashSet<Type> resolvingTypes = null)
        {
            RegisteredObject registeredObject;

            if (!_registeredObjects.TryGetValue(typeToResolve, out registeredObject))
            {
                if (typeToResolve.IsInterface || typeToResolve.IsAbstract)
                {
                    throw new TypeNotRegisteredException(string.Format(
                        "The type {0} has not been registered.", typeToResolve.Name));
                }

                registeredObject = new RegisteredObject(typeToResolve, typeToResolve, LifeCycle.Transient);
            }
            return GetInstance(registeredObject, resolvingTypes);
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