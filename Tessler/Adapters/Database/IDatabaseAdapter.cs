using System.Data;

namespace InfoSupport.Tessler.Adapters.Database
{
    /// <summary>
    /// Interface for a database adapter, used to do resets and queries
    /// </summary>
    public interface IDatabaseAdapter
    {
        void ResetDatabase(string connectionString, string databaseName, string snapshotName);

        DataTable Query(string connectionString, string query);
    }
}