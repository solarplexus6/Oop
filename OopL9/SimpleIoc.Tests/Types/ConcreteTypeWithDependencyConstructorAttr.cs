namespace SimpleIoc.Tests.Types
{
    public class ConcreteTypeWithDependencyConstructorAttr : ITypeToResolveWithConstructorParams
    {
        #region Ctors

        public ConcreteTypeWithDependencyConstructorAttr(ITypeToResolve arg1, ITypeToResolve arg2)
        {
            Arg1 = arg1;
            Arg2 = arg2;
        }

        [DependencyConstructor]
        public ConcreteTypeWithDependencyConstructorAttr(ITypeToResolve arg1)
        {
            Arg1 = arg1;
        }

        #endregion
        #region ITypeToResolveWithConstructorParams Members

        public ITypeToResolve Arg1 { get; set; }
        public ITypeToResolve Arg2 { get; set; }

        #endregion
    }
}