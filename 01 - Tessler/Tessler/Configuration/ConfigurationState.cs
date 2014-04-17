using InfoSupport.Tessler.Adapters.Ajax;
using InfoSupport.Tessler.Core;
using InfoSupport.Tessler.Unity;
using InfoSupport.Tessler.Util;
using Microsoft.Practices.Unity;
using System;

namespace InfoSupport.Tessler.Configuration
{
    public class ConfigurationState : FluentObject
    {
        public static float AjaxWaitInterval { get { return _AjaxWaitInterval; } }
        internal static float _AjaxWaitInterval;
        internal static float _AjaxWaitIntervalShadow;

        public static float AjaxWaitTime { get { return _AjaxWaitTime; } }
        internal static float _AjaxWaitTime;
        internal static float _AjaxWaitTimeShadow;

        public static bool AutoLoadJQuery { get { return _AutoLoadJQuery; } }
        internal static bool _AutoLoadJQuery;
        internal static bool _AutoLoadJQueryShadow;

        public static Browser Browser { get { return _Browser; } }
        internal static Browser _Browser;
        internal static Browser _BrowserShadow;

        public static string BrowserProfile { get { return _BrowserProfile; } }
        internal static string _BrowserProfile;
        internal static string _BrowserProfileShadow;

        public static string DateFormat { get { return _DateFormat; } }
        internal static string _DateFormat;
        internal static string _DateFormatShadow;

        public static float FindElementTimeout { get { return _FindElementTimeout; } }
        internal static float _FindElementTimeout;
        internal static float _FindElementTimeoutShadow;

        public static string JQueryUrl { get { return _JQueryUrl; } }
        internal static string _JQueryUrl;
        internal static string _JQueryUrlShadow;

        public static TakeScreenshot TakeScreenshot { get { return _TakeScreenshot; } }
        internal static TakeScreenshot _TakeScreenshot;
        internal static TakeScreenshot _TakeScreenshotShadow;

        public static bool MaximizeBrowser { get { return _MaximizeBrowser; } }
        internal static bool _MaximizeBrowser;
        internal static bool _MaximizeBrowserShadow;

        public static float NotVisibleWaitTime { get { return _NotVisibleWaitTime; } }
        internal static float _NotVisibleWaitTime;
        internal static float _NotVisibleWaitTimeShadow;

        public static bool RecycleBrowser { get { return _RecycleBrowser; } }
        internal static bool _RecycleBrowser;
        internal static bool _RecycleBrowserShadow;

        public static bool ResetDatabase { get { return _ResetDatabase; } }
        internal static bool _ResetDatabase;
        internal static bool _ResetDatabaseShadow;

        public static string ScreenshotsPath { get { return _ScreenshotsPath; } }
        internal static string _ScreenshotsPath;
        internal static string _ScreenshotsPathShadow;

        public static string StripNamespace { get { return _StripNamespace; } }
        internal static string _StripNamespace;
        internal static string _StripNamespaceShadow;

        public static float WaitTime { get { return _WaitTime; } }
        internal static float _WaitTime;
        internal static float _WaitTimeShadow;

        public static string WebsiteUrl { get { return _WebsiteUrl; } }
        internal static string _WebsiteUrl;
        internal static string _WebsiteUrlShadow;

        static ConfigurationState()
        {
            TesslerState.Configure()
                .LoadFromAppConfig()
                .SaveState()
            ; // Save the state as read from the configuration file
        }

        public ConfigurationState LoadFromAppConfig()
        {
            SetAjaxWaitInterval(TesslerConfiguration.AjaxWaitInterval());
            SetAjaxWaitTime(TesslerConfiguration.AjaxWaitTime());
            SetAutoLoadJQuery(TesslerConfiguration.AutoLoadJQuery());
            SetBrowser(TesslerConfiguration.Browser());
            SetBrowserProfile(TesslerConfiguration.BrowserProfile());
            SetDateFormat(TesslerConfiguration.DateFormat());
            SetFindElementTimeout(TesslerConfiguration.FindElementTimeout());
            SetJQueryUrl(TesslerConfiguration.JQueryUrl());
            SetMaximizeBrowser(TesslerConfiguration.MaximizeBrowser());
            SetNotVisibleWaitTime(TesslerConfiguration.NotVisibleWaitTime());
            SetRecycleBrowser(TesslerConfiguration.RecycleBrowser());
            SetResetDatabase(TesslerConfiguration.ResetDatabase());
            SetScreenshotsPath(TesslerConfiguration.ScreenshotsPath());
            SetStripNamespace(TesslerConfiguration.StripNamespace());
            SetTakeScreenshot(TesslerConfiguration.TakeScreenshot());
            SetWaitTime(TesslerConfiguration.PauseWaitTime());
            SetWebsiteUrl(TesslerConfiguration.WebsiteUrl());

            return this;
        }

        /// <summary>
        /// Saves a shadow-copy of the current configuration state
        /// </summary>
        public ConfigurationState SaveState()
        {
            _AjaxWaitIntervalShadow = AjaxWaitInterval;
            _AjaxWaitTimeShadow = AjaxWaitTime;
            _AutoLoadJQueryShadow = AutoLoadJQuery;
            _BrowserShadow = Browser;
            _BrowserProfileShadow = BrowserProfile;
            _DateFormatShadow = DateFormat;
            _FindElementTimeoutShadow = FindElementTimeout;
            _JQueryUrlShadow = JQueryUrl;
            _TakeScreenshotShadow = TakeScreenshot;
            _MaximizeBrowserShadow = MaximizeBrowser;
            _NotVisibleWaitTimeShadow = NotVisibleWaitTime;
            _RecycleBrowserShadow = RecycleBrowser;
            _ResetDatabaseShadow = ResetDatabase;
            _ScreenshotsPathShadow = ScreenshotsPath;
            _StripNamespaceShadow = StripNamespace;
            _WaitTimeShadow = WaitTime;
            _WebsiteUrlShadow = WebsiteUrl;

            return this;
        }

        /// <summary>
        /// Restores the shadow-copy of a previously saved configuration state
        /// This automatically runs after each TesslerState.TestInitialize
        /// </summary>
        public ConfigurationState RestoreState()
        {
            _AjaxWaitInterval = _AjaxWaitIntervalShadow;
            _AjaxWaitTime = _AjaxWaitTimeShadow;
            _AutoLoadJQuery = _AutoLoadJQueryShadow;
            _Browser = _BrowserShadow;
            _BrowserProfile = _BrowserProfileShadow;
            _DateFormat = _DateFormatShadow;
            _FindElementTimeout = _FindElementTimeoutShadow;
            _JQueryUrl = _JQueryUrlShadow;
            _TakeScreenshot = _TakeScreenshotShadow;
            _MaximizeBrowser = _MaximizeBrowserShadow;
            _NotVisibleWaitTime = _NotVisibleWaitTimeShadow;
            _RecycleBrowser = _RecycleBrowserShadow;
            _ResetDatabase = _ResetDatabaseShadow;
            _ScreenshotsPath = _ScreenshotsPathShadow;
            _StripNamespace = _StripNamespaceShadow;
            _WaitTime = _WaitTimeShadow;
            _WebsiteUrl = _WebsiteUrlShadow;

            return this;
        }

        /// <summary>
        /// How long to wait before trying again while waiting for ajax operations to complete
        /// </summary>
        public ConfigurationState SetAjaxWaitInterval(float interval)
        {
            _AjaxWaitInterval = interval;

            return this;
        }

        /// <summary>
        /// How long to wait for ajax calls to complete
        /// </summary>
        public ConfigurationState SetAjaxWaitTime(float time)
        {
            _AjaxWaitTime = time;

            return this;
        }

        /// <summary>
        /// Whether jQuery should automatically be loaded if it is not available
        /// </summary>
        public ConfigurationState SetAutoLoadJQuery(bool autoLoad)
        {
            _AutoLoadJQuery = autoLoad;

            return this;
        }

        /// <summary>
        /// The browser to use
        /// </summary>
        public ConfigurationState SetBrowser(Browser browser)
        {
            _Browser = browser;

            return this;
        }

        /// <summary>
        /// The browser profile to use
        /// </summary>
        public ConfigurationState SetBrowserProfile(string browserProfileName)
        {
            _BrowserProfile = browserProfileName;

            return this;
        }

        /// <summary>
        /// Which date format to use
        /// </summary>
        public ConfigurationState SetDateFormat(string format)
        {
            _DateFormat = format;

            return this;
        }

        /// <summary>
        /// How long the webdriver will wait for an element before timing out
        /// </summary>
        public ConfigurationState SetFindElementTimeout(float timeout)
        {
            _FindElementTimeout = timeout;

            return this;
        }

        /// <summary>
        /// The url to the jQuery library, used when not available by de target website
        /// </summary>
        public ConfigurationState SetJQueryUrl(string url)
        {
            _JQueryUrl = url;

            return this;
        }

        /// <summary>
        /// The javascript adapter to use for polling ajax requests
        /// </summary>
        /// <returns></returns>
        public ConfigurationState SetJavascriptAdapter<TJavascriptAdapter>()
            where TJavascriptAdapter : IJavascriptAdapter
        {
            UnityInstance.Instance.RegisterType<IJavascriptAdapter, TJavascriptAdapter>();

            return this;
        }

        /// <summary>
        /// When to make a screenshot, can be one of the following: Always, Never, OnFail
        /// </summary>
        public ConfigurationState SetTakeScreenshot(TakeScreenshot takeScreenshot)
        {
            _TakeScreenshot = takeScreenshot;

            return this;
        }

        /// <summary>
        /// Whether to maximize the browser window
        /// </summary>
        public ConfigurationState SetMaximizeBrowser(bool maximize)
        {
            _MaximizeBrowser = maximize;

            return this;
        }

        /// <summary>
        /// How long a call to IsNotVisible should wait before returning
        /// </summary>
        public ConfigurationState SetNotVisibleWaitTime(float time)
        {
            _NotVisibleWaitTime = time;

            return this;
        }

        /// <summary>
        /// Whether to re-use the browser window from the last test, only deleting cookies
        /// </summary>
        public ConfigurationState SetRecycleBrowser(bool recycle)
        {
            _RecycleBrowser = recycle;

            return this;
        }

        /// <summary>
        /// Whether to reset the database(s) before each test
        /// </summary>
        public ConfigurationState SetResetDatabase(bool reset)
        {
            _ResetDatabase = reset;

            return this;
        }

        /// <summary>
        /// Where to save screenshots
        /// </summary>
        public ConfigurationState SetScreenshotsPath(string path)
        {
            _ScreenshotsPath = path;

            return this;
        }

        /// <summary>
        /// Which part of the namespace to strip when writing screenshots
        /// </summary>
        public ConfigurationState SetStripNamespace(string stripNamespace)
        {
            _StripNamespace = stripNamespace;

            return this;
        }

        /// <summary>
        /// How long a default Wait() takes
        /// </summary>
        public ConfigurationState SetWaitTime(float time)
        {
            _WaitTime = time;

            return this;
        }

        /// <summary>
        /// The url of the initial page to navigate to
        /// </summary>
        public ConfigurationState SetWebsiteUrl(string url)
        {
            _WebsiteUrl = url;

            return this;
        }
    }
}