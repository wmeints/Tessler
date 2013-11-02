using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using InfoSupport.Tessler.Configuration;
using InfoSupport.Tessler.Core;
using InfoSupport.Tessler.Drivers;
using InfoSupport.Tessler.Screenshots;
using InfoSupport.Tessler.Selenium;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace InfoSupport.Tessler.Unity
{
    public class CallHandler : ICallHandler
    {
        private List<string> interceptionIgnores;

        public CallHandler()
        {
            interceptionIgnores = new List<string>();

            typeof(object).GetMethods().ForEach(m => interceptionIgnores.Add(m.Name));
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            if (interceptionIgnores.Contains(input.MethodBase.Name))
            {
                return getNext()(input, getNext);
            }

            TesslerObject po = input.Target as TesslerObject;

            JQueryScriptExtensions.LoadExtensions();

            if (!TesslerWebDriver.InhibitExecution)
            {
                po.FireOnCalling();
            }

            var result = getNext()(input, getNext);

            if (!TesslerWebDriver.InhibitExecution)
            {
                po.FireOnCalled();
            }

            HandleScreenshot(input, result);

            return result;
        }

        public int Order
        {
            get;
            set;
        }

        private void HandleScreenshot(IMethodInvocation input, IMethodReturn result)
        {
            if (ConfigurationState.MakeScreenshot == Configuration.TakeScreenshot.Always || ConfigurationState.MakeScreenshot == Configuration.TakeScreenshot.OnFail)
            {
                TestResult testResult = TestResult.Passed;

                if (result.Exception != null)
                {
                    testResult = TestResult.Failed;
                }
                else if (Verify.Failed)
                {
                    testResult = TestResult.VerifyFailed;
                }

                TakeScreenshot(input, testResult);

                Verify.Failed = false;
            }
        }

        private void TakeScreenshot(IMethodInvocation input, TestResult testResult)
        {
            // First check the method
            var screenshotAttribute = input.MethodBase.GetCustomAttributes(typeof(TakeScreenshotAttribute), true).FirstOrDefault() as TakeScreenshotAttribute;

            // If no attribute was found, check the class
            if (screenshotAttribute == null) screenshotAttribute = input.Target.GetType().GetCustomAttributes(typeof(TakeScreenshotAttribute), true).FirstOrDefault() as TakeScreenshotAttribute;

            if (testResult != TestResult.Passed || screenshotAttribute == null || screenshotAttribute.Enabled)
            {
                var driver = TesslerState.GetWebDriver();

                var image = Image.FromStream(new MemoryStream(driver.GetScreenshot().AsByteArray));

                var screenshotText = (screenshotAttribute != null && screenshotAttribute.HasText) ? screenshotAttribute.Text : input.MethodBase.Name;
                var screenshot = new Screenshots.Screenshot(image, screenshotText, testResult);

                var screenshotManager = UnityInstance.Resolve<ScreenshotManager>();
                screenshotManager.AddScreenshot(screenshot);
            }
        }
    }
}