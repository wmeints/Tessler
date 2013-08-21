using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoSupport.Tessler.Screenshots
{
    public interface IScreenshotManager
    {
        void AddScreenshot(Screenshot screenshot);

        void Initialize();

        void Finalize(bool keepScreenshots);
    }
}
