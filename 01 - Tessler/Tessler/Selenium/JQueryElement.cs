using System.Drawing;
using System.Text;
using InfoSupport.Tessler.Drivers;
using InfoSupport.Tessler.Util;
using OpenQA.Selenium;
using InfoSupport.Tessler.Configuration;
using System;

namespace InfoSupport.Tessler.Selenium
{
    public class JQueryElement : FluentObject, IJQueryElement
    {
        private ITesslerWebDriver _driver;
        private IWebElement _element;

        private JQuery _selector;

        public JQueryElement(ITesslerWebDriver driver, IWebElement element, JQuery selector)
        {
            _driver = driver;
            _element = element;

            _selector = selector;
        }

        public void SetText(string keys)
        {
            Retry.Create("SetText(" + keys + ")", () =>
            {
                var sb = new StringBuilder();

                sb.Append("jQuery{0}.val('{1}');");
                sb.Append("jQuery{0}.keydown();");
                sb.Append("jQuery{0}.keypress();");
                sb.Append("jQuery{0}.keyup();");
                sb.Append("jQuery{0}.blur();");
                sb.Append("jQuery{0}.focus();");
                sb.Append("jQuery{0}.change();");

                var js = string.Format(sb.ToString(), _selector.Selector, keys);

                _driver.Js(js);

                return true;
            })
            .AcceptAnyException()
            .SetInterval(TimeSpan.FromSeconds(TesslerConfiguration.AjaxWaitInterval()))
            .SetTimeout(TimeSpan.FromSeconds(TesslerConfiguration.FindElementTimeout()))
            .Start();
        }

        public void ClearAndSendKeys(string keys)
        {
            Retry.Create("ClearAndSendKeys(" + keys + ")", () =>
            {
                Clear();
                SendKeys(keys);

                return true;
            })
            .AcceptAnyException()
            .SetInterval(TimeSpan.FromSeconds(TesslerConfiguration.AjaxWaitInterval()))
            .SetTimeout(TimeSpan.FromSeconds(TesslerConfiguration.FindElementTimeout()))
            .Start();
        }

        public void Clear()
        {
            Retry.Create("Clear()", () =>
            {
                _element.Clear();

                return true;
            })
            .AcceptAnyException()
            .SetInterval(TimeSpan.FromSeconds(TesslerConfiguration.AjaxWaitInterval()))
            .SetTimeout(TimeSpan.FromSeconds(TesslerConfiguration.FindElementTimeout()))
            .Start();
        }

        public void Click()
        {
            Retry.Create("Click()", () =>
            {
                _element.Click();

                return true;
            })
            .AcceptAnyException()
            .SetInterval(TimeSpan.FromSeconds(TesslerConfiguration.AjaxWaitInterval()))
            .SetTimeout(TimeSpan.FromSeconds(TesslerConfiguration.FindElementTimeout()))
            .Start();
        }

        public bool Displayed
        {
            get
            {
                bool result = false;
                Retry.Create("Displayed", () =>
                {
                    result = _element.Displayed;

                    return true;
                })
                .AcceptAnyException()
                .SetInterval(TimeSpan.FromSeconds(TesslerConfiguration.AjaxWaitInterval()))
                .SetTimeout(TimeSpan.FromSeconds(TesslerConfiguration.FindElementTimeout()))
                .Start();

                return result;
            }
        }

        public bool Enabled
        {
            get
            {
                bool result = false;
                Retry.Create("Enabled", () =>
                {
                    result = _element.Enabled;

                    return true;
                })
                .AcceptAnyException()
                .SetInterval(TimeSpan.FromSeconds(TesslerConfiguration.AjaxWaitInterval()))
                .SetTimeout(TimeSpan.FromSeconds(TesslerConfiguration.FindElementTimeout()))
                .Start();

                return result;
            }
        }

        public string GetAttribute(string attributeName)
        {
            string result = null;

            Retry.Create("GetAttribute(" + attributeName + ")", () =>
            {
                result = _element.GetAttribute(attributeName);

                return true;
            })
            .AcceptAnyException()
            .SetInterval(TimeSpan.FromSeconds(TesslerConfiguration.AjaxWaitInterval()))
            .SetTimeout(TimeSpan.FromSeconds(TesslerConfiguration.FindElementTimeout()))
            .Start();

            return result;
        }

        public string GetCssValue(string propertyName)
        {
            string result = null;

            Retry.Create("GetCssValue(" + propertyName + ")", () =>
            {
                result = _element.GetCssValue(propertyName);

                return true;
            })
            .AcceptAnyException()
            .SetInterval(TimeSpan.FromSeconds(TesslerConfiguration.AjaxWaitInterval()))
            .SetTimeout(TimeSpan.FromSeconds(TesslerConfiguration.FindElementTimeout()))
            .Start();

            return result;
        }

        public Point Location
        {
            get
            {
                Point result = Point.Empty;

                Retry.Create("Location", () =>
                {
                    result = _element.Location;

                    return true;
                })
                .AcceptAnyException()
                .SetInterval(TimeSpan.FromSeconds(TesslerConfiguration.AjaxWaitInterval()))
                .SetTimeout(TimeSpan.FromSeconds(TesslerConfiguration.FindElementTimeout()))
                .Start();

                return result;
            }
        }

        public bool Selected
        {
            get
            {
                bool result = false;

                Retry.Create("Selected", () =>
                {
                    result = _element.Selected;

                    return true;
                })
                .AcceptAnyException()
                .SetInterval(TimeSpan.FromSeconds(TesslerConfiguration.AjaxWaitInterval()))
                .SetTimeout(TimeSpan.FromSeconds(TesslerConfiguration.FindElementTimeout()))
                .Start();

                return result;
            }
        }

        public void SendKeys(string text)
        {
            Retry.Create("SendKeys(" + text + ")", () =>
            {
                _element.SendKeys(text);

                var sb = new StringBuilder();

                sb.Append("jQuery{0}.keydown();");
                sb.Append("jQuery{0}.keypress();");
                sb.Append("jQuery{0}.keyup();");
                sb.Append("jQuery{0}.blur();");
                sb.Append("jQuery{0}.focus();");
                sb.Append("jQuery{0}.change();");

                var js = string.Format(sb.ToString(), _selector.Selector);

                _driver.Js(js);

                return true;
            })
            .AcceptAnyException()
            .SetInterval(TimeSpan.FromSeconds(TesslerConfiguration.AjaxWaitInterval()))
            .SetTimeout(TimeSpan.FromSeconds(TesslerConfiguration.FindElementTimeout()))
            .Start();
        }

        public void SetChecked(bool isChecked)
        {
            Retry.Create("SetChecked(" + isChecked + ")", () =>
            {
                if (Selected != isChecked)
                {
                    Click();
                }

                return true;
            })
            .AcceptAnyException()
            .SetInterval(TimeSpan.FromSeconds(TesslerConfiguration.AjaxWaitInterval()))
            .SetTimeout(TimeSpan.FromSeconds(TesslerConfiguration.FindElementTimeout()))
            .Start();
        }

        public Size Size
        {
            get
            {
                Size result = Size.Empty;

                Retry.Create("Size", () =>
                {
                    result = _element.Size;

                    return true;
                })
                .AcceptAnyException()
                .SetInterval(TimeSpan.FromSeconds(TesslerConfiguration.AjaxWaitInterval()))
                .SetTimeout(TimeSpan.FromSeconds(TesslerConfiguration.FindElementTimeout()))
                .Start();

                return result;
            }
        }

        public void Submit()
        {
            Retry.Create("Submit()", () =>
            {
                _element.Submit();

                return true;
            })
            .AcceptAnyException()
            .SetInterval(TimeSpan.FromSeconds(TesslerConfiguration.AjaxWaitInterval()))
            .SetTimeout(TimeSpan.FromSeconds(TesslerConfiguration.FindElementTimeout()))
            .Start();
        }

        public string TagName
        {
            get
            {
                string result = null;

                Retry.Create("TagName", () =>
                {
                    result = _element.TagName;

                    return true;
                })
                .AcceptAnyException()
                .SetInterval(TimeSpan.FromSeconds(TesslerConfiguration.AjaxWaitInterval()))
                .SetTimeout(TimeSpan.FromSeconds(TesslerConfiguration.FindElementTimeout()))
                .Start();

                return result;
            }
        }

        public string Text
        {
            get
            {
                string result = null;

                Retry.Create("Text", () =>
                {
                    result = _element.Text;

                    return true;
                })
                .AcceptAnyException()
                .SetInterval(TimeSpan.FromSeconds(TesslerConfiguration.AjaxWaitInterval()))
                .SetTimeout(TimeSpan.FromSeconds(TesslerConfiguration.FindElementTimeout()))
                .Start();

                return result;
            }
        }

        public string Value
        {
            get
            {
                string result = null;

                Retry.Create("Text", () =>
                {
                    result = _element.GetAttribute("value");

                    return true;
                })
                .AcceptAnyException()
                .SetInterval(TimeSpan.FromSeconds(TesslerConfiguration.AjaxWaitInterval()))
                .SetTimeout(TimeSpan.FromSeconds(TesslerConfiguration.FindElementTimeout()))
                .Start();

                return result;
            }
        }
    }
}