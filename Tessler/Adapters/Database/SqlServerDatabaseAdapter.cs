using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using InfoSupport.Tessler.Util;

namespace InfoSupport.Tessler.Adapters.Database
{
    public class SqlServerDatabaseAdapter : IDatabaseAdapter
    {
        public void ResetDatabase(string connectionString, string databaseName, string snapshotName)
        {
            if (snapshotName == null)
            {
                Log.Fatal("Cannot reset a database when no snapshot is configured");
                throw new InvalidOperationException("Cannot reset a database when no snapshot is configured");
            }

            Query(connectionString, string.Format(Resources.RestoreFromSnapshotQuery, databaseName, snapshotName));
        }

        [SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "MarcoO: We want to be able to freely query the database")]
        public DataTable Query(string connectionString, string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
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
}