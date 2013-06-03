using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            var foo = c.Resolve<ITypeToResolveWithConstructorParams>();

            Assert.IsNotNull(foo.Arg1);
            Assert.IsNull(foo.Arg2);
        }
    }
}
