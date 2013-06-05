using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleIoc.Exceptions;
using SimpleIoc.Tests.Types;

namespace SimpleIoc.Tests
{
    [TestClass]
    public class L10Tests
    {
        [TestMethod]
        public void RegisterInstance()
        {
            var c = new SimpleContainer();

            var foo1 = new ConcreteType();
            c.RegisterInstance<ITypeToResolve>(foo1);
            var foo2 = c.Resolve<ITypeToResolve>();
            Assert.AreSame(foo1, foo2);
        }

        [TestMethod]
        public void DependencyContructor()
        {
            var c = new SimpleContainer();
            c.RegisterType<ITypeToResolveWithConstructorParams, ConcreteTypeWithDependencyConstructorAttr>();
            c.RegisterType<ITypeToResolve, ConcreteType>();
            var foo = c.Resolve<ITypeToResolveWithConstructorParams>();

            Assert.IsNotNull(foo.Arg1);
            Assert.IsNull(foo.Arg2);
        }

        [TestMethod]
        [ExpectedException(typeof (CyclicDependencyException))]
        public void NoResolvableConstructor()
        {
            var c = new SimpleContainer();

            // wyjątek, string nie ma konstruktora bezparametrowego i nie da się rozwikłać żadnego z konstruktorów
            c.Resolve<ConcreteWithStringParam>();
        }

        [TestMethod]
        public void ResolvableWithInstance()
        {
            var c = new SimpleContainer();

            c.RegisterInstance("ala ma kota"); // rejestruje instancję string
            // jest ok, zarejestrowano instancję string więc rozwikłanie konstruktora X jest możliwe
            c.Resolve<ConcreteWithStringParam>();
        }

        [TestMethod]
        public void ResolvableSecondConstructor()
        {
            var c = new SimpleContainer();

            c.RegisterType<ITypeToResolve, ConcreteType>();
            var instance = c.Resolve<ConcreteWithResolvableSecondConstructor>();
            Assert.IsNotNull(instance.Arg1);
        }

        [TestMethod]
        [ExpectedException(typeof(CyclicDependencyException))]
        [Timeout(1000)]
        public void CyclicDependencySimple()
        {
            var c = new SimpleContainer();

            c.RegisterType<ITypeToResolve, ConcreteWithSimpleCyclicDependency>();
            c.Resolve<ITypeToResolve>();
        }

        [TestMethod]
        [ExpectedException(typeof(CyclicDependencyException))]
        //[Timeout(1000)]
        public void CyclicDependencyNested()
        {
            var c = new SimpleContainer();

            c.RegisterType<ITypeToResolve, ConcreteWithCyclicDependency>();
            c.Resolve<ITypeToResolve>();
        }
    }
}