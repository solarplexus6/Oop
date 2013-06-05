namespace SimpleIoc.Tests.Types
{
    public class ConcreteWithSimpleCyclicDependency : ITypeToResolve
    {
        public ConcreteWithSimpleCyclicDependency(ITypeToResolve param)
        {            
        }
    }
}