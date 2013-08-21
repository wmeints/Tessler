using System;

namespace InfoSupport.Tessler.Core
{
    public class ResetDatabaseAttribute : Attribute
    {
        public ResetDatabaseAttribute(bool resetDatabase = true)
        {
            Reset = resetDatabase;
        }

        public bool Reset { get; set; }
    }
}