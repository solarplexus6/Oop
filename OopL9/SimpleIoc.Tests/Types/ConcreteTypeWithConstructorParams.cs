namespace SimpleIoc.Tests.Types
{
    public class ConcreteTypeWithConstructorParams : ITypeToResolveWithConstructorParams
    {
        #region Ctors

        public ConcreteTypeWithConstructorParams(ITypeToResolve arg1)
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