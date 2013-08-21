using InfoSupport.Tessler.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace InfoSupport.Tessler.Core
{
    public static class SystemExtensions
    {
        /// <summary>
        /// Returns the specified datetime as a string, formatted using the format as set in the configuration
        /// </summary>
        public static string ToDateString(this DateTime datetime)
        {
            return datetime.ToString(ConfigurationState.DateFormat, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Parses the string into a datetime, using the format as set in the configuration
        /// </summary>
        public static DateTime ToDateTime(this string str)
        {
            return DateTime.ParseExact(str, ConfigurationState.DateFormat, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Executes the specified action for each item in the specified list
        /// </summary>
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
            }
        }

        /// <summary>
        /// Concatenates the specified list using the specified optional seperator
        /// </summary>
        public static string Concat<T>(this List<T> list, string seperator = ", ")
        {
            var concat = new StringBuilder();

            for (int i = 0; i < list.Count; i++)
            {
                if (i > 0) concat.Append(seperator);
                concat.Append(list[i]);
            }

            return concat.ToString();
        }
    }
}