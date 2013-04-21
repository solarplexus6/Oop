using System.Collections.Generic;
using System.Threading;

namespace Zad1
{
    public class ThreadSingleton
    {
        #region Private fields

        private static Dictionary<int, ThreadSingleton> _instances;


        #endregion
        #region Properties

        public static ThreadSingleton Instance
        {
            get 
            { 
                _instances = _instances ?? new Dictionary<int, ThreadSingleton>();
                var threadKey = Thread.CurrentThread.GetHashCode();
                ThreadSingleton ts;
                if (_instances.TryGetValue(threadKey, out ts))
                {
                    return ts;
                }
                
                _instances[threadKey] = ts = new ThreadSingleton();
                return ts;
            }
        }

        #endregion
        #region Ctors

        private ThreadSingleton()
        {
        }

        #endregion
    }
}