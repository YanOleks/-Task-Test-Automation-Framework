using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;

namespace Core.Driver
{
    public enum BrowserType
    {
        Chrome,
        Firefox,
        Edge,
    }

    public static class WebDriverFactory
    {
        public static IWebDriver CreateDriver(BrowserType browser, bool headless)
        {
            return browser switch
            {
                BrowserType.Chrome => CreateChromeDriver(headless),
                BrowserType.Firefox => CreateFirefoxDriver(headless),
                BrowserType.Edge => CreateEdgeDriver(headless),
                _ => throw new ArgumentException("Unsupported browser type"),
            };
        }

        private static ChromeDriver CreateChromeDriver(bool headless)
        {
            var options = new ChromeOptions();
            if (headless) options.AddArgument("--headless");
            return new ChromeDriver(options);
        }

        private static FirefoxDriver CreateFirefoxDriver(bool headless)
        {
            var options = new FirefoxOptions();
            if (headless) options.AddArgument("--headless");
            return new FirefoxDriver(options);
        }

        private static EdgeDriver CreateEdgeDriver(bool headless)
        {
            var options = new EdgeOptions();
            if (headless) options.AddArgument("--headless");
            return new EdgeDriver(options);
        }
    }
}
