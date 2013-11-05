using System;
using System.IO;
using InfoSupport.Tessler.Configuration;
using InfoSupport.Tessler.Core;
using InfoSupport.Tessler.Util;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

namespace InfoSupport.Tessler.Drivers
{
    public class WebDriverFactory : IWebDriverFactory
    {
        public IWebDriver BuildWebDriver()
        {
            // Browser selection
            switch (ConfigurationState.Browser)
            {
                case Browser.Chrome:
                    string chromeDriverFolder = UnpackDriver("chromedriver.exe", Resources.ChromeDriver);

                    return new ChromeDriver(chromeDriverFolder);

                case Browser.Firefox:
                    if (!string.IsNullOrEmpty(TesslerState.CurrentBrowserProfile))
                    {
                        FirefoxProfileManager profileManager = new FirefoxProfileManager();
                        FirefoxProfile profile = profileManager.GetProfile(TesslerState.CurrentBrowserProfile);
                        profile.AcceptUntrustedCertificates = true;
                        return new FirefoxDriver(profile);
                    }

                    var capabilities = DesiredCapabilities.Firefox();
                    capabilities.SetCapability(CapabilityType.AcceptSslCertificates, true);

                    return new FirefoxDriver(capabilities);
                case Browser.InternetExplorer:
                    string ieDriverFolder = UnpackDriver("IEDriverServer.exe", Resources.IEDriverServer);

                    return new InternetExplorerDriver(ieDriverFolder);
                default:
                    string message = string.Format("Error while building webdriver.");

                    Log.Fatal(message);
                    throw new Exception(message);
            }
        }

        private string UnpackDriver(string binaryName, byte[] driver)
        {
            // Define temp folder
            var tempFolder = Path.Combine(Path.GetTempPath(), "Tessler");

            // Create temp folder
            Directory.CreateDirectory(tempFolder);

            var driverPath = Path.Combine(tempFolder, binaryName);
            try
            {
                // Write driver to temp folder
                File.WriteAllBytes(driverPath, driver);
            }
            catch (Exception e)
            {
                throw new FileLoadException(string.Format("Could not deploy the driver '{0}', maybe the driver is still running?", binaryName), e);
            }

            return tempFolder;
        }
    }
}
