namespace Zad1
{
    public class Singleton
    {
        #region Private fields

        private static Singleton _instance;

        #endregion
        #region Properties

        public static Singleton Instance
        {
            get { return _instance ?? (_instance = new Singleton()); }
        }

        #endregion
        #region Ctors

        private Singleton()
        {
        }

        #endregion
    }
}