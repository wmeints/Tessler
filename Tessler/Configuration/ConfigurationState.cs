using InfoSupport.Tessler.Adapters.Ajax;
using InfoSupport.Tessler.Adapters.Database;
using InfoSupport.Tessler.Unity;
using InfoSupport.Tessler.Util;
using Microsoft.Practices.Unity;

namespace InfoSupport.Tessler.Configuration
{
    public class ConfigurationState : FluentObject
    {
        internal static float AjaxWaitInterval = TesslerConfiguration.AjaxWaitInterval();

        internal static float AjaxWaitTime = TesslerConfiguration.AjaxWaitTime();

        internal static Browser Browser = TesslerConfiguration.Browser();

        internal static string BrowserProfile = TesslerConfiguration.BrowserProfile();

        internal static string DateFormat = TesslerConfiguration.DateFormat();

        internal static float FindElementTimeout = TesslerConfiguration.FindElementTimeout();

        internal static string JQueryUrl = TesslerConfiguration.JQueryUrl();

        internal static TakeScreenshot MakeScreenshot = TesslerConfiguration.TakeScreenshot();

        internal static bool MaximizeBrowser = TesslerConfiguration.MaximizeBrowser();

        internal static float NotVisibleWaitTime = TesslerConfiguration.NotVisibleWaitTime();

        internal static bool RecycleBrowser = TesslerConfiguration.RecycleBrowser();

        internal static bool ResetDatabase = TesslerConfiguration.ResetDatabase();

        internal static string ScreenshotsPath = TesslerConfiguration.ScreenshotsPath();

        internal static string StripNamespace = TesslerConfiguration.StripNamespace();

        internal static float WaitTime = TesslerConfiguration.PauseWaitTime();

        internal static string WebsiteUrl = TesslerConfiguration.WebsiteUrl();

        /// <summary>
        /// How long to wait before trying again while waiting for ajax operations to complete
        /// </summary>
        public ConfigurationState SetAjaxWaitInterval(float interval)
        {
            AjaxWaitInterval = interval;

            return this;
        }

        /// <summary>
        /// How long to wait for ajax calls to complete
        /// </summary>
        public ConfigurationState SetAjaxWaitTime(float time)
        {
            AjaxWaitTime = time;

            return this;
        }

        /// <summary>
        /// The browser to use
        /// </summary>
        public ConfigurationState SetBrowser(Browser browser)
        {
            Browser = browser;

            return this;
        }

        /// <summary>
        /// The browser profile to use
        /// </summary>
        public ConfigurationState SetBrowserProfile(string browserProfileName)
        {
            BrowserProfile = browserProfileName;

            return this;
        }

        /// <summary>
        /// Which date format to use
        /// </summary>
        public ConfigurationState SetDateFormat(string format)
        {
            DateFormat = format;

            return this;
        }

        /// <summary>
        /// How long the webdriver will wait for an element before timing out
        /// </summary>
        public ConfigurationState SetFindElementTimeout(float timeout)
        {
            FindElementTimeout = timeout;

            return this;
        }

        /// <summary>
        /// The url to the jQuery library, used when not available by de target website
        /// </summary>
        public ConfigurationState SetJQueryUrl(string url)
        {
            JQueryUrl = url;

            return this;
        }

        /// <summary>
        /// The javascript adapter to use for polling ajax requests
        /// </summary>
        /// <returns></returns>
        public ConfigurationState SetJavascriptAdapter<TStatusAdapter>()
            where TStatusAdapter : IJavascriptAdapter
        {
            UnityInstance.Instance.RegisterType<IJavascriptAdapter, TStatusAdapter>();

            return this;
        }

        /// <summary>
        /// When to make a screenshot, can be one of the following: Always, Never, OnFail
        /// </summary>
        public ConfigurationState SetMakeScreenshot(TakeScreenshot makeScreenshot)
        {
            MakeScreenshot = makeScreenshot;

            return this;
        }

        /// <summary>
        /// Whether to maximize the browser window
        /// </summary>
        public ConfigurationState SetMaximizeBrowser(bool maximize)
        {
            MaximizeBrowser = maximize;

            return this;
        }

        /// <summary>
        /// How long a call to IsNotVisible should wait before returning
        /// </summary>
        public ConfigurationState SetNotVisibleWaitTime(float time)
        {
            NotVisibleWaitTime = time;

            return this;
        }

        /// <summary>
        /// Whether to re-use the browser window from the last test, only deleting cookies
        /// </summary>
        public ConfigurationState SetRecycleBrowser(bool recycle)
        {
            RecycleBrowser = recycle;

            return this;
        }

        /// <summary>
        /// Whether to reset the database(s) before each test
        /// </summary>
        public ConfigurationState SetResetDatabase(bool reset)
        {
            RecycleBrowser = reset;

            return this;
        }

        /// <summary>
        /// Where to save screenshots
        /// </summary>
        public ConfigurationState SetScreenshotsPath(string path)
        {
            ScreenshotsPath = path;

            return this;
        }

        /// <summary>
        /// Which part of the namespace to strip when writing screenshots
        /// </summary>
        public ConfigurationState SetStripNamespace(string stripNamespace)
        {
            StripNamespace = stripNamespace;

            return this;
        }

        /// <summary>
        /// How long a default Wait() takes
        /// </summary>
        public ConfigurationState SetWaitTime(float time)
        {
            WaitTime = time;

            return this;
        }

        /// <summary>
        /// The url of the initial page to navigate to
        /// </summary>
        public ConfigurationState SetWebsiteUrl(string url)
        {
            WebsiteUrl = url;

            return this;
        }

        
    }
}