using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfoSupport.Tessler.Exceptions
{
    public class TesslerWebDriverException : Exception
    {
        public TesslerWebDriverException(string message)
            : base(message)
        {
        }

        public TesslerWebDriverException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
