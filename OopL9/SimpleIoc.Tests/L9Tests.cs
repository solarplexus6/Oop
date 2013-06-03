using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleIoc.Exceptions;
using SimpleIoc.Tests.Types;

namespace SimpleIoc.Tests
{
    [TestClass]
    public class L9Tests
    {
        [TestMethod]
        public void ResolveObjectWithInterface()
        {
            var container = new SimpleContainer();

            container.RegisterType<ITypeToResolve, ConcreteType>();

            var instance = container.Resolve<ITypeToResolve>();

            Assert.IsInstanceOfType(instance, typeof(ConcreteType));
        }

        [TestMethod]
        public void ResolveConcreteTypeSingleton()
        {
            var container = new SimpleContainer();
            container.RegisterType<ConcreteType>();

            var instance = container.Resolve<ConcreteType>();

            Assert.AreSame(container.Resolve<ConcreteType>(), instance);
        }

        [TestMethod]
        public void ResolveConcreteTypeTransient()
        {
            var container = new SimpleContainer();
            container.RegisterType<ConcreteType>(false);

            var instance = container.Resolve<ConcreteType>();

            Assert.AreNotSame(container.Resolve<ConcreteType>(), instance);
        }

        [TestMethod]
        [ExpectedException(typeof(TypeNotRegisteredException))]
        public void ThrowExceptionIfTypeNotRegistered()
        {
            var container = new SimpleContainer();

            container.Resolve<ITypeToResolve>();            
        }


        [TestMethod]
        public void DefaultSingletonCreation()
        {
            var container = new SimpleContainer();

            container.RegisterType<ITypeToResolve, ConcreteType>();

            var instance = container.Resolve<ITypeToResolve>();
            Assert.AreSame(container.Resolve<ITypeToResolve>(), instance);            
        }

        [TestMethod]
        public void TransientInstance()
        {
            var container = new SimpleContainer();

            container.RegisterType<ITypeToResolve, ConcreteType>(false);

            var instance = container.Resolve<ITypeToResolve>();

            Assert.AreNotSame(container.Resolve<ITypeToResolve>(), instance);            
        }

        [TestMethod]
        public void ResolveObjectWithConstructorParams()
        {
            var container = new SimpleContainer();

            container.RegisterType<ITypeToResolve, ConcreteType>();
            container.RegisterType<ITypeToResolveWithConstructorParams, ConcreteTypeWithConstructorParams>();

            var instance = container.Resolve<ITypeToResolveWithConstructorParams>();
            Assert.IsInstanceOfType(instance, typeof(ConcreteTypeWithConstructorParams));
        }

        
    }
}
