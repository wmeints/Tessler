using System;

namespace InfoSupport.Tessler.Screenshots
{
    public interface IScreenshotManager
    {
        void AddScreenshot(Screenshot screenshot);

        void Initialize();

        void Finalize(bool keepScreenshots);
    }
}
