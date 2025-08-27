using NUnit.Framework.Interfaces;
using Serilog;
using Serilog.Context;
using Core.Utils;
using Core.Driver;
using System.Runtime.InteropServices;
using _Task__Page_Object_Pattern.Pages;
using OpenQA.Selenium;

namespace Tests
{    
    public abstract class BaseTest
    {
        protected IndexPage indexPage;
        protected IWebElement? element;

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
                Log.Error($"Test failed. {TestContext.CurrentContext.Result.Message}");
                var path = CaptureScreenshotIfSupported();
                Log.Information($"Screenshot is saved at {path}");
            }

            Log.Information("Test finished with status: {TestStatus}", testStatus);
            LogContext.Reset();

            DriverHandler.QuitDriver();
        }

        private static string? CaptureScreenshotIfSupported()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) || RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return ScreenshotMaker.TakeBrowserScreenshot(DriverHandler.GetDriver());
            }
            else return null;
        }
    }
}
