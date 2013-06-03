using System;

namespace SimpleIoc.Exceptions
{
    public class TypeNotRegisteredException : Exception
    {
        #region Ctors

        public TypeNotRegisteredException(string message)
            : base(message)
        {
        }

        #endregion
    }
}