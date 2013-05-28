using System;

namespace SimpleIoc
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