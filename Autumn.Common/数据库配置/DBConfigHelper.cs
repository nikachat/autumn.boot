using System.IO;

namespace Autumn.Common
{
    public class DBConfigHelper
    {
        private static string sqliteConnection = Appsettings.Get(new string[] { "AppSettings", "Sqlite", "SqliteConnection" });
        private static bool isSqliteEnabled = (Appsettings.Get(new string[] { "AppSettings", "Sqlite", "Enabled" })).ToBool();

        private static string sqlServerConnection = Appsettings.Get(new string[] { "AppSettings", "SqlServer", "SqlServerConnection" });
        private static bool isSqlServerEnabled = (Appsettings.Get(new string[] { "AppSettings", "SqlServer", "Enabled" })).ToBool();

        private static string mySqlConnection = Appsettings.Get(new string[] { "AppSettings", "MySql", "MySqlConnection" });
        private static bool isMySqlEnabled = (Appsettings.Get(new string[] { "AppSettings", "MySql", "Enabled" })).ToBool();

        private static string oracleConnection = Appsettings.Get(new string[] { "AppSettings", "Oracle", "OracleConnection" });
        private static bool IsOracleEnabled = (Appsettings.Get(new string[] { "AppSettings", "Oracle", "Enabled" })).ToBool();


        public static string ConnectionString => InitConn();
        public static DataBaseType DbType = DataBaseType.SqlServer;


        private static string InitConn()
        {
            if (isSqliteEnabled)
            {
                DbType = DataBaseType.Sqlite;
                return sqliteConnection;
            }
           else if (isSqlServerEnabled)
            {
                DbType = DataBaseType.SqlServer;
                return sqlServerConnection;
            }
            else if (isMySqlEnabled)
            {
                DbType = DataBaseType.MySql;
                return mySqlConnection;
            }
            else if (IsOracleEnabled)
            {
                DbType = DataBaseType.Oracle;
                return oracleConnection;
            }
            else
            {
                return "server=.;uid=sa;pwd=sa;database=db";
            }
        }
    }

    public enum DataBaseType
    {
        MySql = 0,
        SqlServer = 1,
        Sqlite = 2,
        Oracle = 3,
        PostgreSQL = 4
    }
}
