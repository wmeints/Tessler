using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfoSupport.Tessler.Core;
using InfoSupport.Tessler.Drivers;
using InfoSupport.Tessler.Util;

namespace InfoSupport.Tessler.Selenium
{
    public static class JQueryScriptExtensions
    {
        private static List<string> extensions = new List<string>();

        internal static List<string> Extensions { get { return extensions; } }

        public static void Add(string selector)
        {
            extensions.Add(selector);
        }

        internal static void LoadExtensions()
        {
            var driver = TesslerState.GetWebDriver();

            foreach(var extension in extensions)
            {
                try
                {
                    driver.Js(extension);
                }
                catch (Exception e)
                {
                    Log.WarnFormat("Could not load extension '{0}': '{1}'", extension, e.Message);
                }
            }
        }
    }
}
