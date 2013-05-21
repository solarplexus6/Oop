namespace Zad2
{
    public abstract class DataAccessHandler
    {
        #region Public methods

        public void Execute()
        {                        
            OpenDataConnection();
            GetData();
            ProcessData();
            CloseConnection();
        }

        #endregion
        #region Protected methods

        protected abstract void CloseConnection();

        protected abstract void GetData();

        protected abstract void OpenDataConnection();
        protected abstract void ProcessData();

        #endregion
    }
}