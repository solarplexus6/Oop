using System;

namespace SimpleIoc
{
    [AttributeUsage(AttributeTargets.Constructor)]
    public class DependencyConstructorAttribute : Attribute
    {
    }
}