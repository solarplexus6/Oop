using System;
using System.Collections.Generic;

namespace Zad5
{
    public abstract class PersonRegistryLoad
    {
        #region Private fields

        protected List<Person> _persons = new List<Person>();
        private readonly INotifier _notifier;

        #endregion
        #region Ctors

        protected PersonRegistryLoad(INotifier notifier)
        {
            _notifier = notifier;
        }

        #endregion
        #region Public methods

        public abstract void LoadPersons();

        public void NotifyPersons()
        {
            if (_notifier == null)
            {
                throw new InvalidOperationException();
            }

            foreach (var person in _persons)
            {
                _notifier.Notify(person);
            }
        }

        #endregion
    }
}