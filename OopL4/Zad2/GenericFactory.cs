using System;
using System.Collections.Generic;
using System.Linq;
using KellermanSoftware.CompareNetObjects;

namespace Zad2
{
    public class GenericFactory
    {
        #region Private fields

        /// <summary>
        ///     Slownik instancji obiektow, kluczem jest typ, a wartoscia lista par instancji i odpowiadajacym im parametrom konstruktora
        ///     potrzebnym do sprawdzenia istnienia instancji
        /// </summary>
        private readonly Dictionary<Type, List<KeyValuePair<object[], object>>> _instances;

        #endregion
        #region Ctors

        public GenericFactory()
        {
            _instances = new Dictionary<Type, List<KeyValuePair<object[], object>>>();
        }

        #endregion
        #region Public methods

        public object CreateObject(string typeName, bool isSingleton = false, params object[] parameters)
        {
            var type = Type.GetType(typeName, true);
            if (isSingleton)
            {
                List<KeyValuePair<object[], object>> existingInsts;
                if (_instances.TryGetValue(type, out existingInsts) && existingInsts != null)
                {
                    var compareObjects = new CompareObjects();
                    var instance = existingInsts.FirstOrDefault(ei => compareObjects.Compare(ei.Key, parameters));

                    if (!instance.Equals(default(KeyValuePair<object[], object>)))
                    {
                        return instance.Value;
                    }
                }
                else
                {
                    existingInsts = new List<KeyValuePair<object[], object>>();
                    _instances.Add(type, existingInsts);
                }

                var newInst = Activator.CreateInstance(type, parameters);
                existingInsts.Add(new KeyValuePair<object[], object>(parameters, newInst));
                return newInst;
            }
            return Activator.CreateInstance(type, parameters);
        }

        #endregion
    }
}