using System.Collections.Generic;
using System.Drawing;
using OpenQA.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InfoSupport.Tessler.Drivers;

namespace InfoSupport.Tessler.Selenium
{
    public class SelectElement
    {
        private ITesslerWebDriver _driver;

        private JQueryElement _element;
        private JQuery _selector;

        internal SelectElement(ITesslerWebDriver driver, JQueryElement element, JQuery selector)
        {
            _driver = driver;
            _element = element;

            _selector = selector;
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

        public JQueryElement SelectedOption
        {
            get { return _selector.Children("option:selected").Element(); }
        }

        public IEnumerable<JQueryElement> Options
        {
            get { return _selector.Children("option").Elements(); }
        }

        public Size Size
        {
            get { return _element.Size; }
        }

        public string TagName
        {
            get { return _element.TagName; }
        }

        public void SelectByText(string text)
        {
            if (!_element.Displayed)
                Assert.Fail("SelectElement with selector '{0}' is not visible", _selector.Selector);

            _selector.Children(string.Format("option:contains('{0}')", text)).Element().Click();
        }
    }
}