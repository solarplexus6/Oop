using System;

namespace SimpleIoc
{
    public class RegisteredObject
    {
        #region Properties

        public Type ConcreteType { get; private set; }

        public object Instance { get; private set; }

        public LifeCycle LifeCycle { get; private set; }
        public Type TypeToResolve { get; private set; }

        #endregion
        #region Ctors

        public RegisteredObject(Type typeToResolve, Type concreteType, LifeCycle lifeCycle)
        {
            TypeToResolve = typeToResolve;
            ConcreteType = concreteType;
            LifeCycle = lifeCycle;
        }

        #endregion
        #region Public methods

        public void CreateInstance(params object[] args)
        {
            Instance = Activator.CreateInstance(ConcreteType, args);
        }

        #endregion
    }
}