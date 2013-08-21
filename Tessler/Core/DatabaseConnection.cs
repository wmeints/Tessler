using System.Collections.Generic;
using System.Configuration;
using System.Data;
using InfoSupport.Tessler.Adapters.Database;
using InfoSupport.Tessler.Unity;
using InfoSupport.Tessler.Util;

namespace InfoSupport.Tessler.Core
{
    public class DatabaseConnection
    {
        internal static List<DatabaseConnection> ResetableConnections = new List<DatabaseConnection>();

        private ConnectionStringSettings connectionSettings;
        private string databaseName;
        private string snapshotName;

        public DatabaseConnection(string connectionStringKey, string databaseName, string snapshotName)
        {
            this.connectionSettings = ConfigurationManager.ConnectionStrings[connectionStringKey];
            this.databaseName = databaseName;

            this.snapshotName = snapshotName;

            if (this.snapshotName != null)
            {
                ResetableConnections.Add(this);
            }

            Log.InfoFormat("Added new database connection for database '{0}' {1} snapshot resets", databaseName, ((snapshotName != null) ? "with" : "without"));
        }

        public DatabaseConnection(string connectionStringKey, string databaseName)
            : this(connectionStringKey, databaseName, null)
        {
        }

        public DataTable Query(string query)
        {
            return Adapter.Query(connectionSettings.ConnectionString, query);
        }

        public void RestoreFromSnapshot()
        {
            Adapter.ResetDatabase(connectionSettings.ConnectionString, databaseName, snapshotName);
        }

        private IDatabaseAdapter Adapter
        {
            get { return UnityInstance.Resolve<IDatabaseAdapter>(); }
        }
    }
}