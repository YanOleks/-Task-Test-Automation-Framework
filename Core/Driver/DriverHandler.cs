using OpenQA.Selenium;

namespace Core.Driver
{
    public static class DriverHandler
    {
        private static readonly ThreadLocal<IWebDriver?> webDriver = new();

        public static IWebDriver GetDriver()
        {
            ArgumentNullException.ThrowIfNull(webDriver.Value);
            return webDriver.Value;
        }

        public static void InitDriver(BrowserType browser = BrowserType.Chrome, bool headless = false, bool maximize = true)
        {
            if (webDriver.Value == null)
            {
                IWebDriver driver = WebDriverFactory.CreateDriver(browser, headless);
                webDriver.Value = driver;

                if (maximize)
                {
                    driver.Manage().Window.Maximize();
                }
            }
        }

        public static void QuitDriver()
        {
            webDriver.Value?.Quit();
            webDriver.Value?.Dispose();
            webDriver.Value = null;
        }
    }
}
