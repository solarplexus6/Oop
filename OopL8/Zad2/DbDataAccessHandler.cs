using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlServerCe;
using System.Linq;

namespace Zad2
{
    public class DbDataAccessHandler : DataAccessHandler
    {
        #region Private fields

        private Database1Context _context;
        private List<int> _data;

        #endregion
        #region Properties

        public int Result { get; private set; }

        #endregion
        #region Overrides

        protected override void CloseConnection()
        {
            _context.Dispose();
        }

        protected override void GetData()
        {
            _data = _context.Numbers.Select(row => row.Number1).ToList();
        }

        protected override void OpenDataConnection()
        {
            var settings =
                ConfigurationManager.ConnectionStrings;
            var connString = settings["Zad2.Properties.Settings.Database1ConnectionString"].ConnectionString;
            var connection = new SqlCeConnection(connString);
            connection.Open();
            _context = new Database1Context(connection);
        }

        protected override void ProcessData()
        {
            Result = _data.Sum();
        }

        #endregion
    }
}