using System;
namespace InfoSupport.Tessler.Selenium
{
    public interface IJQueryElement
    {
        void Clear();
        void ClearAndSendKeys(string keys);
        void Click();
        bool Displayed { get; }
        bool Enabled { get; }
        string GetAttribute(string attributeName);
        string GetCssValue(string propertyName);
        System.Drawing.Point Location { get; }
        bool Selected { get; }
        void SendKeys(string text);
        void SetChecked(bool isChecked);
        void SetText(string keys);
        System.Drawing.Size Size { get; }
        void Submit();
        string TagName { get; }
        string Text { get; }
        string Value { get; }
    }
}
