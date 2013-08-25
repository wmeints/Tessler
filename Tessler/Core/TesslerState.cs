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
using OpenQA.Selenium;

namespace InfoSupport.Tessler.Core
{
    public static class TesslerState
    {
        private static TestContext testContext;
        private static Type testClass;
        private static MethodInfo testMethod;

        private static ITesslerWebDriver driverInstance;

        internal static Type TestClass { get { return testClass; } }

        internal static MethodInfo TestMethod { get { return testMethod; } }

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

            var testClassAttribute = testClass.GetCustomAttributes(typeof(TestClassAttribute), false).FirstOrDefault();

            if (testClassAttribute == null)
            {
                Log.Fatal("Calling class has no [TestClass] attribute");
                throw new InvalidOperationException("Calling class has no [TestClass] attribute");
            }

            // Get method info
            testMethod = testClass.GetMethod(context.TestName);

            var testMethodAttribute = testMethod.GetCustomAttributes(typeof(TestMethodAttribute), false);

            if (testMethodAttribute == null)
            {
                Log.Fatal("Test method has no [TestMethod] attribute");
                throw new InvalidOperationException("Test method has no [TestMethod] attribute");
            }

            // Clear VerifyFails
            Verify.Fails.Clear();
            
            // Setup webdriver and reset databases
            Task.WaitAll(
                Task.Factory.StartNew(new Action(() => SetupWebDriver())),
                Task.Factory.StartNew(new Action(() => ResetDatabases()))
            );

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

                bool keepScreenshots = ConfigurationState.MakeScreenshot == TakeScreenshot.Always ||
                    (ConfigurationState.MakeScreenshot == TakeScreenshot.OnFail && !GetTestSuccessful());

                screenshotManager.Finalize(keepScreenshots);
            }
        }

        public static void AssemblyInitialize(TestContext context)
        {
            Log.Info("- Configuration -");
            Log.InfoFormat("Website url: '{0}'", ConfigurationState.WebsiteUrl);
            Log.InfoFormat("Find element timeout: '{0}'", ConfigurationState.FindElementTimeout);
            Log.InfoFormat("Wait time: '{0}'", ConfigurationState.WaitTime);
            Log.InfoFormat("Not visible wait time: '{0}'", ConfigurationState.NotVisibleWaitTime);
            Log.InfoFormat("Browser: '{0}'", ConfigurationState.Browser);
            Log.InfoFormat("Screenshots path: '{0}'", ConfigurationState.ScreenshotsPath);
            Log.InfoFormat("Make screenshot: '{0}'", ConfigurationState.MakeScreenshot);
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

            if ((reset == null && ConfigurationState.ResetDatabase) || reset.Reset)
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
            var driver = GetWebDriver();

            driver.Navigate().GoToUrl(ConfigurationState.WebsiteUrl);

            if (ConfigurationState.MaximizeBrowser)
                driver.Manage().Window.Maximize();
        }
    }
}