using System;

namespace Zad1
{
    public class LifespanSingleton
    {
        #region Constants

        private const int SINGLETON_LIFESPAN_SEC = 5;

        #endregion
        #region Private fields

        private static LifespanSingleton _instance;
        private static DateTime _timestamp;

        #endregion
        #region Properties

        public static LifespanSingleton Instance
        {
            get
            {
                
                if (_instance == null || DateTime.Now - _timestamp > TimeSpan.FromSeconds(SINGLETON_LIFESPAN_SEC))
                {
                    _instance = new LifespanSingleton();
                    _timestamp = DateTime.Now;
                }
                return _instance;
            }
        }

        #endregion
        #region Ctors

        private LifespanSingleton()
        {
        }

        #endregion
    }
}