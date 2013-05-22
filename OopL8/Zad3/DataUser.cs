namespace Zad3
{
    public class DataUser
    {
        private readonly IDataAccessStrategy _dataAccessStrategy;

        public DataUser(IDataAccessStrategy dataAccessStrategy)
        {
            _dataAccessStrategy = dataAccessStrategy;
        }

        public int UseData()
        {
            return _dataAccessStrategy.GetResultFromData();
        }
    }
}