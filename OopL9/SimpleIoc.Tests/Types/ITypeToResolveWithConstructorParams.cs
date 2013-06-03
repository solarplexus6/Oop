namespace SimpleIoc.Tests.Types
{
    public interface ITypeToResolveWithConstructorParams
    {
        ITypeToResolve Arg1 { get; set; }
        ITypeToResolve Arg2 { get; set; }
    }
}