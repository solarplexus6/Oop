using System;
using System.Collections.Generic;

namespace Zad5
{
    public abstract class PersonRegistryNotify
    {
        #region Private fields

        protected List<Person> _persons = new List<Person>();
        private readonly ILoader _loader;

        #endregion
        #region Ctors

        protected PersonRegistryNotify(ILoader loader)
        {
            _loader = loader;
        }

        #endregion
        #region Public methods

        public void LoadPersons()
        {
            if (_loader == null)
            {
                throw new InvalidOperationException();
            }
            _persons = _loader.LoadPersons();
        }

        public abstract void NotifyPersons();

        #endregion
    }
}