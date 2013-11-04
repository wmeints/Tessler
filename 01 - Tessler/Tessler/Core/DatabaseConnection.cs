using System.Collections.Generic;
using System.Configuration;
using System.Data;
using InfoSupport.Tessler.Adapters.Database;
using InfoSupport.Tessler.Util;

namespace InfoSupport.Tessler.Core
{
    public abstract class DatabaseConnection
    {
        internal static List<DatabaseConnection> ResetableConnections = new List<DatabaseConnection>();

        internal ConnectionStringSettings ConnectionSettings { get; private set; }

        public DatabaseConnection(string connectionStringKey, bool resetableConnection)
        {
            this.ConnectionSettings = ConfigurationManager.ConnectionStrings[connectionStringKey];

            if (resetableConnection)
            {
                ResetableConnections.Add(this);
            }

            Log.InfoFormat("Added new database connection for ConnectionString '{0}' {1} snapshot resets", connectionStringKey, (resetableConnection ? "with" : "without"));
        }

        public DataTable Query(string query)
        {
            return Adapter.Query(this, query);
        }

        public void ResetDatabase()
        {
            Adapter.ResetDatabase(this);
        }

        protected abstract IDatabaseAdapter Adapter { get; }
    }
}