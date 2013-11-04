using System.Drawing;
using System.Text;
using InfoSupport.Tessler.Drivers;
using InfoSupport.Tessler.Util;
using OpenQA.Selenium;

namespace InfoSupport.Tessler.Selenium
{
    public class JQueryElement : FluentObject
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
        }

        public void ClearAndSendKeys(string keys)
        {
            Clear();
            SendKeys(keys);
        }

        public void Clear()
        {
            _element.Clear();
        }

        public void Click()
        {
            _element.Click();
        }

        public bool Displayed
        {
            get { return _element.Displayed; }
        }

        public bool Enabled
        {
            get { return _element.Enabled; }
        }

        public string GetAttribute(string attributeName)
        {
            return _element.GetAttribute(attributeName);
        }

        public string GetCssValue(string propertyName)
        {
            return _element.GetCssValue(propertyName);
        }

        public Point Location
        {
            get { return _element.Location; }
        }

        public bool Selected
        {
            get { return _element.Selected; }
        }

        public void SendKeys(string text)
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
        }

        public void SetChecked(bool isChecked)
        {
            if (Selected != isChecked)
            {
                Click();
            }
        }

        public Size Size
        {
            get { return _element.Size; }
        }

        public void Submit()
        {
            _element.Submit();
        }

        public string TagName
        {
            get { return _element.TagName; }
        }

        public string Text
        {
            get { return _element.Text; }
        }

        public string Value
        {
            get { return _element.GetAttribute("value"); }
        }
    }
}