using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using InfoSupport.Tessler.Util;
using InfoSupport.Tessler.Core;
using InfoSupport.Tessler.Unity;

namespace InfoSupport.Tessler.Adapters.Database
{
    public class SqlServerDatabaseAdapter : IDatabaseAdapter
    {
        private string databaseName;
        private string snapshotName;

        public SqlServerDatabaseAdapter(string databaseName, string snapshotName)
        {
            this.databaseName = databaseName;
            this.snapshotName = snapshotName;
        }

        public void ResetDatabase(DatabaseConnection databaseConnection)
        {
            if (snapshotName == null)
            {
                Log.Fatal("Cannot reset a database when no snapshot is configured");
                throw new InvalidOperationException("Cannot reset a database when no snapshot is configured");
            }

            Query(databaseConnection, string.Format(Resources.RestoreFromSnapshotQuery, databaseName, snapshotName));
        }

        [SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "MarcoO: We want to be able to freely query the database")]
        public DataTable Query(DatabaseConnection databaseConnection, string query)
        {
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionSettings.ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                DataTable table = new DataTable();
                table.Load(reader);

                return table;
            }
        }
    }

    public class SqlServerDatabaseConnection : DatabaseConnection
    {
        private string snapshotName;
        private string databaseName;

        public SqlServerDatabaseConnection(string connectionStringKey, string snapshotName)
            : base(connectionStringKey, true)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionSettings.ConnectionString;

            this.databaseName = connection.Database;
            this.snapshotName = snapshotName;
        }

        public SqlServerDatabaseConnection(string connectionStringKey)
            : base(connectionStringKey, false)
        {

        }

        protected override IDatabaseAdapter Adapter
        {
            get { return new SqlServerDatabaseAdapter(this.databaseName, this.snapshotName); }
        }
    }
}