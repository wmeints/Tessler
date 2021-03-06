﻿using System.Collections.Generic;
using System.Drawing;
using InfoSupport.Tessler.Drivers;
using InfoSupport.Tessler.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InfoSupport.Tessler.Selenium
{
    public class SelectElement : FluentObject
    {
        private ITesslerWebDriver _driver;

        private IJQueryElement _element;
        private JQuery _selector;

        internal SelectElement(ITesslerWebDriver driver, IJQueryElement element, JQuery selector)
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

        public IJQueryElement SelectedOption
        {
            get { return _selector.Children("option:selected").Element(); }
        }

        public IEnumerable<IJQueryElement> Options
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