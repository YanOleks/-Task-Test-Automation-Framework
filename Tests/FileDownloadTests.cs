using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using _Task__Page_Object_Pattern.Pages;

namespace _Task__Page_Object_Pattern
{
    [TestFixture]
    public class FileDownloadTests
    {
        private IWebDriver driver;
        private string downloadFolderPath;
        private readonly string expectedFileName = "EPAM_Corporate_Overview_Q4FY-2024.pdf";
        protected IndexPage indexPage;

        [SetUp]
        public void Setup()
        {
            downloadFolderPath = Path.Combine(Path.GetTempPath(), "SeleniumDownloads", Guid.NewGuid().ToString());
            if (!Directory.Exists(downloadFolderPath))
            {
                Directory.CreateDirectory(downloadFolderPath);
            }
            else
            {
                Directory.EnumerateFiles(downloadFolderPath).ToList().ForEach(File.Delete);
            }

            ChromeOptions options = new ChromeOptions();
            options.AddUserProfilePreference("download.default_directory", downloadFolderPath);
            options.AddUserProfilePreference("download.prompt_for_download", false);
            options.AddUserProfilePreference("download.directory_upgrade", true);
            options.AddUserProfilePreference("plugins.always_open_pdf_externally", true);

            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();

            Console.WriteLine($"Configuration: Download directory: {downloadFolderPath}");

            indexPage = new IndexPage(driver);
        }

        [Test]
        public void VerifyPdfFileDownload()
        {
            indexPage.Open()
                .ClickAboutLink()
                .ScrollToGlanceSection()
                .ClickDesktopDownloadButton();

            string expectedFilePath = Path.Combine(downloadFolderPath, expectedFileName);
            Console.WriteLine($"Waiting for download to finish: {expectedFilePath}");

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            try
            {
                wait.Until(d => {
                    if (File.Exists(expectedFilePath))
                    {
                        FileInfo fileInfo = new FileInfo(expectedFilePath);
                        return fileInfo.Length > 0 && !fileInfo.Extension.EndsWith("crdownload", StringComparison.OrdinalIgnoreCase);
                    }
                    return false;
                });
                Console.WriteLine($"File '{expectedFileName}' is downloaded.");
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Fail($"Timeout: File '{expectedFileName}' was not downloaded during expected time.");
            }

            using (Assert.EnterMultipleScope())
            {
                Assert.That(File.Exists(expectedFilePath), Is.True, $"File '{expectedFileName}' has to exist in '{downloadFolderPath}'.");
                Assert.That(Path.GetExtension(expectedFilePath).ToLower(), Is.EqualTo(".pdf"), $".pdf extension was expected '{expectedFileName}'.");
            }

            Console.WriteLine($"Validated: file '{expectedFileName}' was found and checked.");
        }

        [TearDown]
        public void Teardown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }

            if (Directory.Exists(downloadFolderPath))
            {
                try
                {
                    Directory.Delete(downloadFolderPath, true);
                    Console.WriteLine($"Temporal download directory is removed: {downloadFolderPath}");
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Error during directory removal: {ex.Message}");
                }
            }
        }
    }
}
