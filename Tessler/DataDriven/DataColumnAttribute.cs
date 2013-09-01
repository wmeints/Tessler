using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
