using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using InfoSupport.Tessler.Configuration;
using InfoSupport.Tessler.Core;
using InfoSupport.Tessler.Util;

namespace InfoSupport.Tessler.Screenshots
{
    public class ScreenshotManager : IScreenshotManager
    {
        private List<Task> tasks;

        private int screenshotIndex;

        public ScreenshotManager()
        {

        }

        public void AddScreenshot(Screenshot screenshot)
        {
            tasks.Add(Task.Factory.StartNew(() => WriteScreenshot(screenshot, GetScreenshotDirectory(), ++screenshotIndex)));
        }

        public void Initialize()
        {
            tasks = new List<Task>();

            var directory = GetScreenshotDirectory();

            Log.InfoFormat("Initializing screenshots directory at '{0}'...", directory);

            if (Directory.Exists(directory))
            {
                foreach (var file in Directory.GetFiles(directory))
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch (Exception e)
                    {
                        Log.ErrorFormat("Could not delete file '{0}': {1}", file, e.Message);
                    }
                }
            }

            try
            {
                Directory.CreateDirectory(directory);
            }
            catch (Exception e)
            {
                Log.ErrorFormat("Could not delete screenshots directory: {0}", e.Message);
            }
        }

        public void Finalize(bool keepScreenshots)
        {
            screenshotIndex = 0;

            Task.WaitAll(tasks.ToArray());

            if (!keepScreenshots)
            {
                try
                {
                    Directory.Delete(GetScreenshotDirectory(), true);

                    Log.Info("Deleted screenshots");
                }
                catch (Exception e)
                {
                    Log.ErrorFormat("Could not delete screenshots: '{0}'", e.Message);
                }
            }

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            DirectoryCleanup.Clean(ConfigurationState.ScreenshotsPath);

            stopwatch.Stop();
            Log.InfoFormat("Cleaned up screenshots directory in {0}ms", stopwatch.ElapsedMilliseconds);
        }

        private static string GetScreenshotDirectory()
        {
            var rootNamespace = string.IsNullOrEmpty(ConfigurationState.StripNamespace) ? TesslerState.TestClass.Namespace : TesslerState.TestClass.Namespace.Replace(ConfigurationState.StripNamespace, "");

            var testClass = rootNamespace.Trim(new char[] { ' ', '.' }) + "." + TesslerState.TestClass.Name;
            var testMethod = TesslerState.TestMethod.Name;

            var directory = ConfigurationState.ScreenshotsPath;

            string[] subDirectory = (testClass + "." + testMethod).Split(new char[] { '.' });

            directory = Path.Combine(directory, Path.Combine(subDirectory));

            return directory;
        }

        private static void WriteScreenshot(Screenshot screenshot, string directory, int index)
        {
            try
            {
                var image = ScaleImage(screenshot.Image, 800, 8000);

                Graphics g = Graphics.FromImage(image);

                var font = new Font(FontFamily.GenericSansSerif, 14, FontStyle.Regular);
                var color = Color.FromArgb(255, 100, 255, 100);

                if (screenshot.TestResult == TestResult.Failed)
                {
                    color = Color.FromArgb(255, 255, 100, 100);
                }
                else if (screenshot.TestResult == TestResult.VerifyFailed)
                {
                    color = Color.FromArgb(255, 255, 125, 55);
                }

                var brush = new SolidBrush(Color.FromArgb(200, color));

                var textY = (int)image.Height - 30;

                g.FillRectangle(brush, new Rectangle(0, (int)image.Height - 40, (int)image.Width, 40));

                // Action
                g.DrawString(screenshot.Action, font, Brushes.Black, new PointF(20, textY));

                // Date
                var date = DateTime.Now.ToString(ConfigurationState.DateFormat + " HH:mm:ss");

                g.DrawString(date, font, Brushes.Black, new Point(image.Width - 210, textY));

                var fullPath = Path.Combine(directory, string.Format("{0:D4}{1}", index.ToString("0000"), ".png"));

                image.Save(fullPath);
            }
            catch (Exception e)
            {
                Log.ErrorFormat("Could not write screenshot: {0}. This might be caused by too long filenames, consider using stripNamespace in the configuration.", e.Message);
            }
        }

        private static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
            return newImage;
        }
    }
}