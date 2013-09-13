using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using InfoSupport.Tessler.Configuration;
using InfoSupport.Tessler.Core;
using InfoSupport.Tessler.Drivers;
using InfoSupport.Tessler.Screenshots;
using InfoSupport.Tessler.Selenium;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

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

            PrepareDialogs(input);

            var result = getNext()(input, getNext);

            if (!TesslerWebDriver.InhibitExecution)
            {
                po.FireOnCalled();
            }

            HandleDialogs(input);

            HandleScreenshot(input, result);

            return result;
        }

        public int Order
        {
            get;
            set;
        }

        private void PrepareDialogs(IMethodInvocation input)
        {
            var driver = TesslerState.GetWebDriver();

            driver.ClearDialogMessages();

            // Alert Dialog
            var alertDialogAttribute = input.MethodBase.GetCustomAttributes(typeof(AlertDialogAttribute), true).Cast<AlertDialogAttribute>().FirstOrDefault();
            driver.SetDialogAlert(alertDialogAttribute != null);

            // Confirm Dialog
            var confirmDialogAttribute = input.MethodBase.GetCustomAttributes(typeof(ConfirmDialogAttribute), true).Cast<ConfirmDialogAttribute>().FirstOrDefault();
            if(confirmDialogAttribute != null) 
            {
                driver.SetDialogConfirm(confirmDialogAttribute.Confirmation);
            }
            else
            {
                driver.SetDialogConfirm();
            }

            // Leave Page Dialog
            var leavePageDialogAttribute = input.MethodBase.GetCustomAttributes(typeof(LeavePageDialogAttribute), true).Cast<LeavePageDialogAttribute>().FirstOrDefault();
            driver.SetDialogLeavePage(leavePageDialogAttribute != null);
        }

        private void HandleDialogs(IMethodInvocation input)
        {
            var driver = TesslerState.GetWebDriver();

            // Alert Dialog
            var alertDialogAttribute = input.MethodBase.GetCustomAttributes(typeof(AlertDialogAttribute), true).Cast<AlertDialogAttribute>().FirstOrDefault();
            var alertDialogMessage = driver.GetDialogAlertMessage();
            HandleDialogMessage("Alert", alertDialogAttribute, alertDialogMessage);

            // Confirm Dialog
            var confirmDialogAttribute = input.MethodBase.GetCustomAttributes(typeof(ConfirmDialogAttribute), true).Cast<ConfirmDialogAttribute>().FirstOrDefault();
            if (confirmDialogAttribute != null)
            {
                driver.SetDialogConfirm(confirmDialogAttribute.Confirmation);
            }
            else
            {
                driver.SetDialogConfirm();
            }

            // Leave Page Dialog
            var leavePageDialogAttribute = input.MethodBase.GetCustomAttributes(typeof(LeavePageDialogAttribute), true).Cast<LeavePageDialogAttribute>().FirstOrDefault();
            driver.SetDialogLeavePage(leavePageDialogAttribute != null);
        }

        private void HandleDialogMessage(string dialogType, LeavePageDialogAttribute attribute, string dialogMessage)
        {
            if (attribute == null && dialogMessage != null)
            {
                Assert.Fail("Unexpected '{0}' Dialog was found", dialogType);
            }
            else if (attribute != null)
            {
                if (attribute.ResultAction == DialogResultAction.DoNothing) return;

                if (dialogMessage == null)
                {
                    CreateAssertOrVerifyMessage(attribute.ResultAction,
                        string.Format("Expected '{0}' Dialog, but none was found", dialogType));
                }
                else if (attribute.ExpectedMessage != null && dialogMessage != attribute.ExpectedMessage)
                {
                    CreateAssertOrVerifyMessage(attribute.ResultAction,
                        string.Format("Expected '{0}' Dialog was found, but actual message <{1}> does not equal expected message <{2}>", dialogType, dialogMessage, attribute.ExpectedMessage));
                }
            }
        }

        private void CreateAssertOrVerifyMessage(DialogResultAction resultAction, string message)
        {
            if (resultAction == DialogResultAction.Assert)
            {
                Assert.Fail(message);
            }
            else if (resultAction == DialogResultAction.Verify)
            {
                Verify.Fail(message);
            }
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