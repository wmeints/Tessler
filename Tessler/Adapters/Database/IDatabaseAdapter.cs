using InfoSupport.Tessler.Core;
using System.Data;

namespace InfoSupport.Tessler.Adapters.Database
{
    /// <summary>
    /// Interface for a database adapter, used to do resets and queries
    /// </summary>
    public interface IDatabaseAdapter
    {
        void ResetDatabase(DatabaseConnection databaseConnection);

        DataTable Query(DatabaseConnection databaseConnection, string query);
    }
}