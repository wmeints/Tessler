using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using InfoSupport.Tessler.Configuration;
using InfoSupport.Tessler.Drivers;
using InfoSupport.Tessler.Screenshots;
using InfoSupport.Tessler.Unity;
using InfoSupport.Tessler.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InfoSupport.Tessler.Core
{
    public static class TesslerState
    {
        private static TestContext testContext;
        private static Type testClass;
        private static MethodInfo testMethod;
        private static string currentBrowserProfile;

        private static ITesslerWebDriver driverInstance;

        internal static Type TestClass { get { return testClass; } }

        internal static MethodInfo TestMethod { get { return testMethod; } }

        internal static int? TestIterationIndex
        {
            get
            {
                var datasource = testMethod.GetCustomAttributes(typeof(DataSourceAttribute), true).FirstOrDefault() as DataSourceAttribute;
                if (datasource == null)
                {
                    return null;
                }
                return testContext.DataRow.Table.Rows.IndexOf(testContext.DataRow);
            }
        }

        internal static string CurrentBrowserProfile { get { return currentBrowserProfile; } }

        public static ConfigurationState Configure()
        {
            return new ConfigurationState();
        }

        public static string GetWebsiteUrl()
        {
            return ConfigurationState.WebsiteUrl;
        }

        public static IScreenshotManager GetScreenshotManager()
        {
            return UnityInstance.Resolve<IScreenshotManager>();
        }

        public static ITesslerWebDriver GetWebDriver()
        {
            if (driverInstance != null && driverInstance.IsActive)
            {
                return driverInstance;
            }

            driverInstance = UnityInstance.Resolve<ITesslerWebDriver>();

            return driverInstance;
        }

        internal static bool GetTestSuccessful()
        {
            return testContext.CurrentTestOutcome == UnitTestOutcome.Passed && Verify.Fails.Count == 0;
        }

        public static void TestInitialize(TestContext context)
        {
            if (context == null)
            {
                Log.Fatal("ArgumentNullException: 'context'");
                throw new ArgumentNullException("context");
            }

            testContext = context;

            var assembly = Assembly.GetCallingAssembly();

            // Get class type
            testClass = assembly.GetType(context.FullyQualifiedTestClassName);

            // Get method info
            testMethod = testClass.GetMethod(context.TestName);

            // Clear VerifyFails
            Verify.Fails.Clear();

            // Setup webdriver and reset databases
            Task.WaitAll(
                Task.Factory.StartNew(new Action(() => SetupWebDriver())),
                Task.Factory.StartNew(new Action(() => ResetDatabases()))
            );

            Configure().RestoreState();

            GetScreenshotManager().Initialize();
        }

        public static void TestCleanup()
        {
            try
            {
                // Check for VerifyFails
                if (Verify.Fails.Count > 0 && testContext.CurrentTestOutcome == UnitTestOutcome.Passed)
                    throw new AssertFailedException(string.Format("Verify failed:{0}{1}", Environment.NewLine, Verify.GetFailMessage()));
            }
            finally
            {
                if (ConfigurationState.RecycleBrowser)
                {
                    GetWebDriver().ClosePopups();
                    GetWebDriver().Manage().Cookies.DeleteAllCookies();
                }
                else
                {
                    GetWebDriver().Close();
                }

                var screenshotManager = GetScreenshotManager();

                bool keepScreenshots = ConfigurationState.TakeScreenshot == TakeScreenshot.Always ||
                    (ConfigurationState.TakeScreenshot == TakeScreenshot.OnFail && !GetTestSuccessful());

                screenshotManager.Finalize(keepScreenshots);
            }
        }

        private static bool isInitialized;
        public static void AssemblyInitialize()
        {
            if (!isInitialized)
            {
                UnityConfiguration.InitializeStandAlone();

                isInitialized = true;
            }

            log4net.Config.XmlConfigurator.Configure();

            Log.Info("- Configuration -");
            Log.InfoFormat("Website url: '{0}'", ConfigurationState.WebsiteUrl);
            Log.InfoFormat("Find element timeout: '{0}'", ConfigurationState.FindElementTimeout);
            Log.InfoFormat("Wait time: '{0}'", ConfigurationState.WaitTime);
            Log.InfoFormat("Not visible wait time: '{0}'", ConfigurationState.NotVisibleWaitTime);
            Log.InfoFormat("Browser: '{0}'", ConfigurationState.Browser);
            Log.InfoFormat("Screenshots path: '{0}'", ConfigurationState.ScreenshotsPath);
            Log.InfoFormat("Make screenshot: '{0}'", ConfigurationState.TakeScreenshot);
            Log.InfoFormat("Maximize browser: '{0}'", ConfigurationState.MaximizeBrowser);
            Log.InfoFormat("Recycle browser: '{0}'", ConfigurationState.RecycleBrowser);
            Log.InfoFormat("Ajax wait interval: '{0}'", ConfigurationState.AjaxWaitInterval);
            Log.InfoFormat("Ajax wait time: '{0}'", ConfigurationState.AjaxWaitTime);
            Log.InfoFormat("JQuery url: '{0}'", ConfigurationState.JQueryUrl);
            Log.InfoFormat("Date format: '{0}'", ConfigurationState.DateFormat);
            Log.InfoFormat("Strip namespace: '{0}'", ConfigurationState.StripNamespace);
            Log.Info("-----------------");
        }

        public static void AssemblyCleanup()
        {
            if (driverInstance.IsActive)
            {
                GetWebDriver().Close();
            }
        }

        private static void ResetDatabases()
        {
            var reset = testMethod.GetCustomAttributes(typeof(ResetDatabaseAttribute), true).FirstOrDefault() as ResetDatabaseAttribute;

            if ((reset == null && ConfigurationState.ResetDatabase) || (reset != null && reset.Reset))
            {
                Log.InfoFormat("Resetting {0} databases...", DatabaseConnection.ResetableConnections.Count);

                var tasks = new List<Task>();

                DatabaseConnection.ResetableConnections.ForEach(db => tasks.Add(Task.Factory.StartNew(db.ResetDatabase)));

                Task.WaitAll(tasks.ToArray());

                Log.Info("Database reset complete");
            }
        }

        private static void SetupWebDriver()
        {
            SwitchBrowserProfile();

            var driver = GetWebDriver();

            driver.Navigate().GoToUrl(ConfigurationState.WebsiteUrl);

            if (ConfigurationState.MaximizeBrowser)
                driver.Manage().Window.Maximize();
        }

        private static void SwitchBrowserProfile()
        {
            var profile = testMethod.GetCustomAttributes(typeof(BrowserProfileAttribute), true).FirstOrDefault() as BrowserProfileAttribute;

            string browserProfile = ConfigurationState.BrowserProfile;
            if (profile != null)
            {
                browserProfile = profile.Profile;
            }

            if (currentBrowserProfile != browserProfile)
            {
                if (driverInstance != null && !driverInstance.IsActive) // Avoid creating WebDriver
                {
                    GetWebDriver().Close();
                }
                currentBrowserProfile = browserProfile;
            }
        }
    }
}