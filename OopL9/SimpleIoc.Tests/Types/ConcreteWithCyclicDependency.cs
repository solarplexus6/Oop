namespace SimpleIoc.Tests.Types
{
    public class ConcreteWithCyclicDependency :  ITypeToResolve
    {
        public ConcreteWithCyclicDependency(ConcreteWithSimpleCyclicDependency param)
        {            
        }        
    }
}