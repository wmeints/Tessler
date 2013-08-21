using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoSupport.Tessler.Util
{
    public static class DirectoryCleanup
    {
        /// <summary>
        /// Recursively cleans any empty folders from the specified folder down
        /// </summary>
        /// <param name="folder">The top folder</param>
        /// <returns>Whether the specified folder is empty and thus can be removed</returns>
        public static bool Clean(string folder)
        {
            try
            {
                if (!Directory.Exists(folder)) return false;

                bool clearedAll = true;

                Directory.GetDirectories(folder).ToList().ForEach(d =>
                {
                    if (Clean(d))
                    {
                        try
                        {
                            Directory.Delete(d);
                        }
                        catch (Exception e)
                        {
                            Log.WarnFormat("Could not remove empty screenshots directory '{0}': '{1}'", d, e.Message);
                        }
                    }
                    else
                    {
                        clearedAll = false;
                    }
                });

                if (Directory.GetFiles(folder).Length > 0) return false;

                return clearedAll;
            }
            catch (Exception e)
            {
                Log.WarnFormat("Error while cleaning directory '{0}': '{1}'", folder, e.Message);
            }

            return false;
        }
    }
}
