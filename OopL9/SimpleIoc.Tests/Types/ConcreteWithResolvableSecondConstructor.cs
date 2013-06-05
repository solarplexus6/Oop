namespace SimpleIoc.Tests.Types
{
    public class ConcreteWithResolvableSecondConstructor : ITypeToResolveWithConstructorParams
    {
        public ITypeToResolve Arg1 { get; set; }
        public ITypeToResolve Arg2 { get; set; }

        public ConcreteWithResolvableSecondConstructor(ITypeToResolve arg1, string param)
        {
        }

        public ConcreteWithResolvableSecondConstructor(ITypeToResolve arg1)
        {
            Arg1 = arg1;
        }
    }
}