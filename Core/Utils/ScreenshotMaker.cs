using System.Drawing.Imaging;
using System.Runtime.Versioning;
using OpenQA.Selenium;

namespace Core.Utils
{
    [SupportedOSPlatform("windows")]
    [SupportedOSPlatform("linux")]
    public static class ScreenshotMaker
    {
        private static string NewScreenshotName
        {
            get { return "_" + DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss-fff") + "." + ScreenshotImageFormat; }
        }
        private static ImageFormat ScreenshotImageFormat
        {
            get { return ImageFormat.Jpeg; }
        }
        public static string TakeBrowserScreenshot(IWebDriver driver)
        {
            if (driver is not ITakesScreenshot takesScreenshot)
            {
                throw new ArgumentException("The provided WebDriver does not support taking screenshots.");
            }

            var screenshotPath = Path.Combine(Environment.CurrentDirectory, "Display" + NewScreenshotName);
            var screenshot = takesScreenshot.GetScreenshot();
            screenshot.SaveAsFile(screenshotPath);

            return screenshotPath;
        }
    }
}
