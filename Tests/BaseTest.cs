using NUnit.Framework.Interfaces;
using Serilog;
using Serilog.Context;
using Core.Utils;
using Core.Driver;
using System.Runtime.InteropServices;
using _Task__Page_Object_Pattern.Pages;

namespace Tests
{    
    public class BaseTest
    {
        protected IndexPage indexPage;

        [SetUp]
        public void Setup()
        {
            LogContext.PushProperty("TestName", TestContext.CurrentContext.Test.Name);
            Log.Information("Starting test: {TestName}", TestContext.CurrentContext.Test.Name);

            DriverHandler.InitDriver(browser: BrowserType.Chrome, maximize: true);
            indexPage = new IndexPage(DriverHandler.GetDriver());
        }

        [TearDown]
        public void TearDown()
        {
            var testStatus = TestContext.CurrentContext.Result.Outcome.Status;

            if (testStatus == TestStatus.Failed)
            {
                Log.Error("Test failed. Capturing a screenshot.");
                CaptureScreenshotIfSupported();
            }

            Log.Information("Test finished with status: {TestStatus}", testStatus);
            LogContext.Reset();

            DriverHandler.QuitDriver();
        }

        private static void CaptureScreenshotIfSupported()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) || RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                ScreenshotMaker.TakeBrowserScreenshot(DriverHandler.GetDriver());
            }
        }
    }
}
