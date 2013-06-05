namespace SimpleIoc.Tests.Types
{
    public class ConcreteWithStringParam : ITypeToResolveWithConstructorParams
    {
        public ITypeToResolve Arg1 { get; set; }
        public ITypeToResolve Arg2 { get; set; }

        public ConcreteWithStringParam(string param)
        {
            
        }
    }
}