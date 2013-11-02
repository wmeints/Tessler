using System;

namespace InfoSupport.Tessler.DataDriven
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class DataColumnAttribute : Attribute
    {
        public string ColumnName { get; set; }

        public DataColumnAttribute(string columnName)
        {
            ColumnName = columnName;
        }
    }
}
