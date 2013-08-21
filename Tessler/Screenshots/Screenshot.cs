using System.Drawing;

namespace InfoSupport.Tessler.Screenshots
{
    public class Screenshot
    {
        public Image Image { get; private set; }

        public string Action { get; private set; }

        public TestResult TestResult { get; private set; }

        public Screenshot(Image image, string action, TestResult testResult)
        {
            Image = image;
            Action = action;
            TestResult = testResult;
        }
    }
}