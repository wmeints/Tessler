using System.Data;
using System.Linq;

namespace InfoSupport.Tessler.DataDriven
{
    public static class DataRowMapper
    {
        public static TData MapTo<TData>(this DataRow dataRow) where TData : new()
        {
            var type = typeof(TData);
            var instance = new TData();

            foreach (var property in type.GetProperties())
            {
                var columnName = property
                    .GetCustomAttributes(typeof(DataColumnAttribute), false)
                    .Cast<DataColumnAttribute>()
                    .Select(d => d.ColumnName).FirstOrDefault();

                // If DataColumnAttribute is not defined, use the property name
                columnName = columnName ?? property.Name;

                try
                {
                    property.SetValue(instance, dataRow[columnName], null);
                }
                catch {} // Possible ArgumentNullException
            }

            return instance;
        }
    }
}
