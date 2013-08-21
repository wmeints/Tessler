using System;

namespace InfoSupport.Tessler.Core
{
    public class TakeScreenshotAttribute : Attribute
    {
        public TakeScreenshotAttribute(bool enabled = true, string text = "")
        {
            Enabled = enabled;
            Text = text;
        }

        public bool Enabled { get; set; }

        public bool HasText
        {
            get { return !string.IsNullOrEmpty(Text); }
        }

        public string Text { get; set; }
    }
}