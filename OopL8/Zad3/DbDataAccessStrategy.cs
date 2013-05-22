using System.Configuration;
using System.Data.SqlServerCe;
using System.Linq;

namespace Zad3
{
    public class DbDataAccessStrategy : IDataAccessStrategy
    {
        public int GetResultFromData()
        {
            var settings =
                ConfigurationManager.ConnectionStrings;
            var connString = settings["Zad2.Properties.Settings.Database1ConnectionString"].ConnectionString;
            var connection = new SqlCeConnection(connString);
            connection.Open();
            using (var context = new Database1Context(connection))
            {
                return context.Numbers.Select(row => row.Number1).Sum();
            }
            
        }
    }
}