using InfoSupport.Tessler.Configuration;
using InfoSupport.Tessler.Util;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.PhantomJS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Remote;
using InfoSupport.Tessler.Core;

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
                    else
                    {
                        var capabilities = DesiredCapabilities.Firefox();
                        capabilities.SetCapability(CapabilityType.AcceptSslCertificates, true);

                        return new FirefoxDriver(capabilities);
                    }

                case Browser.InternetExplorer:
                    string ieDriverFolder = UnpackDriver("IEDriverServer.exe", Resources.IEDriverServer);
                    
                    return new InternetExplorerDriver(ieDriverFolder);

                case Browser.PhantomJs:
                    string pjsDriverFolder = UnpackDriver("phantomjs.exe", Resources.PhantomJs);

                    return new PhantomJSDriver(pjsDriverFolder);

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
            if (!File.Exists(driverPath))
            {
                // Write driver to temp folder
                File.WriteAllBytes(driverPath, driver);
            }

            return tempFolder;
        }
    }
}
