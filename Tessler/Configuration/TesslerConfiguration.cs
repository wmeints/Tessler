using InfoSupport.Tessler.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoSupport.Tessler.Configuration
{
    internal static class TesslerConfiguration
    {
        private static TesslerSection TesslerSectionInstance = (TesslerSection)ConfigurationManager.GetSection("tessler");

        internal static float AjaxWaitInterval()
        {
            return TesslerSectionInstance.AjaxWaitIntervalConfig.Value;
        }

        internal static float AjaxWaitTime()
        {
            return TesslerSectionInstance.AjaxWaitTimeConfig.Value;
        }

        internal static Browser Browser()
        {
            return TesslerSectionInstance.BrowserConfig.Value;
        }

        internal static string BrowserProfile()
        {
            return TesslerSectionInstance.BrowserProfileConfig.Value;
        }

        internal static string DateFormat()
        {
            return TesslerSectionInstance.DateFormatConfig.Value;
        }

        internal static float FindElementTimeout()
        {
            return TesslerSectionInstance.FindElementTimeoutConfig.Value;
        }

        internal static string JQueryUrl()
        {
            return TesslerSectionInstance.JQueryUrlConfig.Value;
        }

        internal static bool MaximizeBrowser()
        {
            return TesslerSectionInstance.MaximizeBrowserConfig.Value;
        }

        internal static float NotVisibleWaitTime()
        {
            return TesslerSectionInstance.NotVisibleWaitTimeConfig.Value;
        }

        internal static float PauseWaitTime()
        {
            return TesslerSectionInstance.PauseWaitTimeConfig.Value;
        }

        internal static bool RecycleBrowser()
        {
            return TesslerSectionInstance.RecycleBrowserConfig.Value;
        }

        internal static bool ResetDatabase()
        {
            return TesslerSectionInstance.ResetDatabaseConfig.Value;
        }

        internal static string ScreenshotsPath()
        {
            return TesslerSectionInstance.ScreenshotsPathConfig.Value;
        }

        internal static string StripNamespace()
        {
            return TesslerSectionInstance.StripNamespaceConfig.Value;
        }

        internal static TakeScreenshot TakeScreenshot()
        {
            return TesslerSectionInstance.TakeScreenshotConfig.Value;
        }

        internal static string WebsiteUrl()
        {
            return TesslerSectionInstance.WebsiteUrlConfig.Value;
        }
    }

    internal class TesslerSection : ConfigurationSection
    {
        #region ElementNames

        private const string ELEMENTNAME_AJAXWAITINTERVAL = "ajaxWaitInterval";
        private const string ELEMENTNAME_AJAXWAITTIME = "ajaxWaitTime";
        private const string ELEMENTNAME_BROWSER = "browser";
        private const string ELEMENTNAME_BROWSERPROFILE = "browserProfile";
        private const string ELEMENTNAME_DATEFORMAT = "dateFormat";
        private const string ELEMENTNAME_FINDELEMENT = "findElementTimeout";
        private const string ELEMENTNAME_JQUERYURL = "jQueryUrl";
        private const string ELEMENTNAME_MAXIMIZEBROWSER = "maximizeBrowser";
        private const string ELEMENTNAME_NOTVISIBLEWAIT = "notVisibleWaitTime";
        private const string ELEMENTNAME_PAUSEWAIT = "pauseWaitTime";
        private const string ELEMENTNAME_RECYCLEBROWSER = "recycleBrowser";
        private const string ELEMENTNAME_RESETDATABASE = "resetDatabase";
        private const string ELEMENTNAME_SCREENSHOTSPATH = "screenshotsPath";
        private const string ELEMENTNAME_STRIPNAMESPACE = "stripNamespace";
        private const string ELEMENTNAME_TAKESCREENSHOT = "takeScreenshot";
        private const string ELEMENTNAME_WEBSITEURL = "websiteUrl";
        
        #endregion

        #region PropertyNames

        private const string PROPERTYNAME_DEFAULT = "value";

        #endregion

        #region DefaultValues

        private const float DEFAULTVALUE_AJAXWAITINTERVAL = 0.2f;
        private const float DEFAULTVALUE_AJAXWAITTIME = 10f;
        private const Browser DEFAULTVALUE_BROWSER = Browser.Chrome;
        private const string DEFAULTVALUE_BROWSERPROFILE = "";
        private const string DEFAULTVALUE_DATEFORMAT = "dd-MM-yyyy";
        private const float DEFAULTVALUE_FINDELEMENT = 10f;
        private const string DEFAULTVALUE_JQUERYURL = "http://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js";
        private const bool DEFAULTVALUE_MAXIMIZEBROWSER = true;
        private const float DEFAULTVALUE_NOTVISIBLEWAIT = 1f;
        private const float DEFAULTVALUE_PAUSEWAIT = 1f;
        private const bool DEFAULTVALUE_RECYCLEBROWSER = false;
        private const bool DEFAULTVALUE_RESETDATABASE = true;
        private const string DEFAULTVALUE_SCREENSHOTSPATH = "Screenshots";
        private const string DEFAULTVALUE_STRIPNAMESPACE = "";
        private const TakeScreenshot DEFAULTVALUE_TAKESCREENSHOT = TakeScreenshot.OnFail;
        private const string DEFAULTVALUE_WEBSITEURL = "";
        
        #endregion

        #region ConfigurationSections

        [ConfigurationProperty(ELEMENTNAME_AJAXWAITINTERVAL, IsRequired = false)]
        internal AjaxWaitIntervalElement AjaxWaitIntervalConfig
        {
            get
            {
                return (AjaxWaitIntervalElement)this[ELEMENTNAME_AJAXWAITINTERVAL];
            }
        }

        [ConfigurationProperty(ELEMENTNAME_AJAXWAITTIME, IsRequired = false)]
        internal AjaxWaitTimeElement AjaxWaitTimeConfig
        {
            get
            {
                return (AjaxWaitTimeElement)this[ELEMENTNAME_AJAXWAITTIME];
            }
        }

        [ConfigurationProperty(ELEMENTNAME_BROWSER, IsRequired = false)]
        internal BrowserElement BrowserConfig
        {
            get
            {
                return (BrowserElement)this[ELEMENTNAME_BROWSER];
            }
        }

        [ConfigurationProperty(ELEMENTNAME_BROWSERPROFILE, IsRequired = false)]
        internal BrowserProfileElement BrowserProfileConfig
        {
            get
            {
                return (BrowserProfileElement)this[ELEMENTNAME_BROWSERPROFILE];
            }
        }

        [ConfigurationProperty(ELEMENTNAME_DATEFORMAT, IsRequired = false)]
        internal DateFormatElement DateFormatConfig
        {
            get
            {
                return (DateFormatElement)this[ELEMENTNAME_DATEFORMAT];
            }
        }

        [ConfigurationProperty(ELEMENTNAME_FINDELEMENT, IsRequired = false)]
        internal FindElementTimeoutElement FindElementTimeoutConfig
        {
            get
            {
                return (FindElementTimeoutElement)this[ELEMENTNAME_FINDELEMENT];
            }
        }

        [ConfigurationProperty(ELEMENTNAME_JQUERYURL, IsRequired = false)]
        internal JQueryUrlElement JQueryUrlConfig
        {
            get
            {
                return (JQueryUrlElement)this[ELEMENTNAME_JQUERYURL];
            }
        }

        [ConfigurationProperty(ELEMENTNAME_MAXIMIZEBROWSER, IsRequired = false)]
        internal MaximizeBrowserElement MaximizeBrowserConfig
        {
            get
            {
                return (MaximizeBrowserElement)this[ELEMENTNAME_MAXIMIZEBROWSER];
            }
        }

        [ConfigurationProperty(ELEMENTNAME_NOTVISIBLEWAIT, IsRequired = false)]
        internal NotVisibleWaitTimeElement NotVisibleWaitTimeConfig
        {
            get
            {
                return (NotVisibleWaitTimeElement)this[ELEMENTNAME_NOTVISIBLEWAIT];
            }
        }

        [ConfigurationProperty(ELEMENTNAME_PAUSEWAIT, IsRequired = false)]
        internal PauseWaitTimeElement PauseWaitTimeConfig
        {
            get
            {
                return (PauseWaitTimeElement)this[ELEMENTNAME_PAUSEWAIT];
            }
        }

        [ConfigurationProperty(ELEMENTNAME_RECYCLEBROWSER, IsRequired = false)]
        internal RecycleBrowserElement RecycleBrowserConfig
        {
            get
            {
                return (RecycleBrowserElement)this[ELEMENTNAME_RECYCLEBROWSER];
            }
        }

        [ConfigurationProperty(ELEMENTNAME_RESETDATABASE, IsRequired = false)]
        internal ResetDatabaseElement ResetDatabaseConfig
        {
            get
            {
                return (ResetDatabaseElement)this[ELEMENTNAME_RESETDATABASE];
            }
        }

        [ConfigurationProperty(ELEMENTNAME_SCREENSHOTSPATH, IsRequired = false)]
        internal ScreenshotsPathElement ScreenshotsPathConfig
        {
            get
            {
                return (ScreenshotsPathElement)this[ELEMENTNAME_SCREENSHOTSPATH];
            }
        }

        [ConfigurationProperty(ELEMENTNAME_TAKESCREENSHOT, IsRequired = false)]
        internal TakeScreenshotElement TakeScreenshotConfig
        {
            get
            {
                return (TakeScreenshotElement)this[ELEMENTNAME_TAKESCREENSHOT];
            }
        }

        [ConfigurationProperty(ELEMENTNAME_WEBSITEURL, IsRequired = true)]
        internal WebsiteUrlElement WebsiteUrlConfig
        {
            get
            {
                return (WebsiteUrlElement)this[ELEMENTNAME_WEBSITEURL];
            }
        }

        [ConfigurationProperty(ELEMENTNAME_STRIPNAMESPACE, IsRequired = false)]
        internal StripNamespaceElement StripNamespaceConfig
        {
            get
            {
                return (StripNamespaceElement)this[ELEMENTNAME_STRIPNAMESPACE];
            }
        }

        #endregion

        #region ConfigurationElements

        internal class AjaxWaitIntervalElement : ConfigurationElement
        {
            [ConfigurationProperty(PROPERTYNAME_DEFAULT, DefaultValue = DEFAULTVALUE_AJAXWAITINTERVAL, IsRequired = true)]
            public float Value
            {
                get
                {
                    if (this[PROPERTYNAME_DEFAULT] is float)
                        return (float)this[PROPERTYNAME_DEFAULT];

                    float result;
                    if (float.TryParse((string)this[PROPERTYNAME_DEFAULT], out result))
                        return result;
                    else
                    {
                        string message = string.Format("Value '{0}' of element '{1}' is invalid, value must be a floating number.",
                            this[PROPERTYNAME_DEFAULT], ELEMENTNAME_AJAXWAITINTERVAL);

                        Log.Fatal(message);
                        throw new ConfigurationErrorsException(message);
                    }
                }
            }
        }

        internal class AjaxWaitTimeElement : ConfigurationElement
        {
            [ConfigurationProperty(PROPERTYNAME_DEFAULT, DefaultValue = DEFAULTVALUE_AJAXWAITTIME, IsRequired = true)]
            public float Value
            {
                get
                {
                    if (this[PROPERTYNAME_DEFAULT] is float)
                        return (float)this[PROPERTYNAME_DEFAULT];

                    float result;
                    if (float.TryParse((string)this[PROPERTYNAME_DEFAULT], out result))
                        return result;
                    else
                    {
                        string message = string.Format("Value '{0}' of element '{1}' is invalid, value must be a floating number.",
                            this[PROPERTYNAME_DEFAULT], ELEMENTNAME_AJAXWAITTIME);

                        Log.Fatal(message);
                        throw new ConfigurationErrorsException(message);
                    }
                }
            }
        }

        internal class BrowserElement : ConfigurationElement
        {
            [ConfigurationProperty(PROPERTYNAME_DEFAULT, DefaultValue = DEFAULTVALUE_BROWSER, IsRequired = true)]
            public Browser Value
            {
                get
                {
                    if (this[PROPERTYNAME_DEFAULT] is Browser)
                        return (Browser)this[PROPERTYNAME_DEFAULT];

                    Browser result;
                    if (Browser.TryParse((string)this[PROPERTYNAME_DEFAULT], out result))
                        return result;
                    else
                    {
                        string message = string.Format("Value '{0}' of element '{1}' is invalid, value must be 'Chrome', 'Firefox' or 'InternetExplorer'.",
                            this[PROPERTYNAME_DEFAULT], ELEMENTNAME_BROWSER);

                        Log.Fatal(message);
                        throw new ConfigurationErrorsException(message);
                    }
                }
            }
        }

        internal class BrowserProfileElement : ConfigurationElement
        {
            [ConfigurationProperty(PROPERTYNAME_DEFAULT, DefaultValue = DEFAULTVALUE_BROWSERPROFILE, IsRequired = true)]
            public string Value
            {
                get
                {
                    return (string)this[PROPERTYNAME_DEFAULT];
                }
            }
        }

        internal class DateFormatElement : ConfigurationElement
        {
            [ConfigurationProperty(PROPERTYNAME_DEFAULT, DefaultValue = DEFAULTVALUE_DATEFORMAT, IsRequired = true)]
            public string Value
            {
                get
                {
                    return (string)this[PROPERTYNAME_DEFAULT];
                }
            }
        }

        internal class FindElementTimeoutElement : ConfigurationElement
        {
            [ConfigurationProperty(PROPERTYNAME_DEFAULT, DefaultValue = DEFAULTVALUE_FINDELEMENT, IsRequired = true)]
            public float Value
            {
                get
                {
                    if (this[PROPERTYNAME_DEFAULT] is float)
                        return (float)this[PROPERTYNAME_DEFAULT];

                    float result;
                    if (float.TryParse((string)this[PROPERTYNAME_DEFAULT], out result))
                        return result;
                    else
                    {
                        string message = string.Format("Value '{0}' of element '{1}' is invalid, value must be a floating number.",
                            this[PROPERTYNAME_DEFAULT], ELEMENTNAME_FINDELEMENT);

                        Log.Fatal(message);
                        throw new ConfigurationErrorsException(message);
                    }
                }
            }
        }

        internal class JQueryUrlElement : ConfigurationElement
        {
            [ConfigurationProperty(PROPERTYNAME_DEFAULT, DefaultValue = DEFAULTVALUE_JQUERYURL, IsRequired = true)]
            public string Value
            {
                get
                {
                    return (string)this[PROPERTYNAME_DEFAULT];
                }
            }
        }

        internal class MaximizeBrowserElement : ConfigurationElement
        {
            [ConfigurationProperty(PROPERTYNAME_DEFAULT, DefaultValue = DEFAULTVALUE_MAXIMIZEBROWSER, IsRequired = true)]
            public bool Value
            {
                get
                {
                    if (this[PROPERTYNAME_DEFAULT] is bool)
                        return (bool)this[PROPERTYNAME_DEFAULT];

                    bool result = true;
                    if (bool.TryParse((string)this[PROPERTYNAME_DEFAULT], out result))
                        return result;
                    else
                    {
                        string message = string.Format("Value '{0}' of element '{1}' is invalid, value must be a boolean value.",
                            this[PROPERTYNAME_DEFAULT], ELEMENTNAME_MAXIMIZEBROWSER);

                        Log.Fatal(message);
                        throw new ConfigurationErrorsException(message);
                    }
                }
            }
        }

        internal class NotVisibleWaitTimeElement : ConfigurationElement
        {
            [ConfigurationProperty(PROPERTYNAME_DEFAULT, DefaultValue = DEFAULTVALUE_NOTVISIBLEWAIT, IsRequired = true)]
            public float Value
            {
                get
                {
                    if (this[PROPERTYNAME_DEFAULT] is float)
                        return (float)this[PROPERTYNAME_DEFAULT];

                    float result;
                    if (float.TryParse((string)this[PROPERTYNAME_DEFAULT], out result))
                        return result;
                    else
                    {
                        string message = string.Format("Value '{0}' of element '{1}' is invalid, value must be a floating number.",
                            this[PROPERTYNAME_DEFAULT], ELEMENTNAME_NOTVISIBLEWAIT);

                        Log.Fatal(message);
                        throw new ConfigurationErrorsException(message);
                    }
                }
            }
        }

        internal class PauseWaitTimeElement : ConfigurationElement
        {
            [ConfigurationProperty(PROPERTYNAME_DEFAULT, DefaultValue = DEFAULTVALUE_PAUSEWAIT, IsRequired = true)]
            public float Value
            {
                get
                {
                    if (this[PROPERTYNAME_DEFAULT] is float)
                        return (float)this[PROPERTYNAME_DEFAULT];

                    float result;
                    if (float.TryParse((string)this[PROPERTYNAME_DEFAULT], out result))
                        return result;
                    else
                    {
                        string message = string.Format("Value '{0}' of element '{1}' is invalid, value must be a floating number.",
                            this[PROPERTYNAME_DEFAULT], ELEMENTNAME_PAUSEWAIT);

                        Log.Fatal(message);
                        throw new ConfigurationErrorsException(message);
                    }
                }
            }
        }

        internal class RecycleBrowserElement : ConfigurationElement
        {
            [ConfigurationProperty(PROPERTYNAME_DEFAULT, DefaultValue = DEFAULTVALUE_RECYCLEBROWSER, IsRequired = true)]
            public bool Value
            {
                get
                {
                    if (this[PROPERTYNAME_DEFAULT] is bool)
                        return (bool)this[PROPERTYNAME_DEFAULT];

                    bool result = true;
                    if (bool.TryParse((string)this[PROPERTYNAME_DEFAULT], out result))
                        return result;
                    else
                    {
                        string message = string.Format("Value '{0}' of element '{1}' is invalid, value must be a boolean value.",
                            this[PROPERTYNAME_DEFAULT], ELEMENTNAME_RECYCLEBROWSER);

                        Log.Fatal(message);
                        throw new ConfigurationErrorsException(message);
                    }
                }
            }
        }

        internal class ResetDatabaseElement : ConfigurationElement
        {
            [ConfigurationProperty(PROPERTYNAME_DEFAULT, DefaultValue = DEFAULTVALUE_RESETDATABASE, IsRequired = true)]
            public bool Value
            {
                get
                {
                    if (this[PROPERTYNAME_DEFAULT] is bool)
                        return (bool)this[PROPERTYNAME_DEFAULT];

                    bool result = true;
                    if (bool.TryParse((string)this[PROPERTYNAME_DEFAULT], out result))
                        return result;
                    else
                    {
                        string message = string.Format("Value '{0}' of element '{1}' is invalid, value must be a boolean value.",
                            this[PROPERTYNAME_DEFAULT], ELEMENTNAME_RESETDATABASE);

                        Log.Fatal(message);
                        throw new ConfigurationErrorsException(message);
                    }
                }
            }
        }

        internal class ScreenshotsPathElement : ConfigurationElement
        {
            [ConfigurationProperty(PROPERTYNAME_DEFAULT, DefaultValue = DEFAULTVALUE_SCREENSHOTSPATH, IsRequired = true)]
            public string Value
            {
                get
                {
                    return (string)this[PROPERTYNAME_DEFAULT];
                }
            }
        }

        internal class StripNamespaceElement : ConfigurationElement
        {
            [ConfigurationProperty(PROPERTYNAME_DEFAULT, DefaultValue = DEFAULTVALUE_STRIPNAMESPACE, IsRequired = true)]
            public string Value
            {
                get
                {
                    return (string)this[PROPERTYNAME_DEFAULT];
                }
            }
        }

        internal class TakeScreenshotElement : ConfigurationElement
        {
            [ConfigurationProperty(PROPERTYNAME_DEFAULT, DefaultValue = DEFAULTVALUE_TAKESCREENSHOT, IsRequired = true)]
            public TakeScreenshot Value
            {
                get
                {
                    if (this[PROPERTYNAME_DEFAULT] is TakeScreenshot)
                        return (TakeScreenshot)this[PROPERTYNAME_DEFAULT];

                    TakeScreenshot result;
                    if (TakeScreenshot.TryParse((string)this[PROPERTYNAME_DEFAULT], out result))
                        return result;
                    else
                    {
                        string message = string.Format("Value '{0}' of element '{1}' is invalid, value must be 'Always', 'Never' or 'OnFail'.",
                            this[PROPERTYNAME_DEFAULT], ELEMENTNAME_TAKESCREENSHOT);

                        Log.Fatal(message);
                        throw new ConfigurationErrorsException(message);
                    }
                }
            }
        }

        internal class WebsiteUrlElement : ConfigurationElement
        {
            [ConfigurationProperty(PROPERTYNAME_DEFAULT, IsRequired = true)]
            public string Value
            {
                get
                {
                    return (string)this[PROPERTYNAME_DEFAULT];
                }
            }
        }

        #endregion
    }
}
