﻿using System;
using System.IO;
using InfoSupport.Tessler.Configuration;
using InfoSupport.Tessler.Core;
using InfoSupport.Tessler.Util;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.PhantomJS;

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
                {
                    string chromeDriverFolder = UnpackDriver("chromedriver.exe", Resources.chromedriver);

                    var options = new ChromeOptions();
                    
                    options.AddArgument("test-type");

                    // Add the disable-extensions options to prevent the "Disable developer mode" popup from rendering.
                    // This popup causes Tessler to not be able to detect the presence of jQuery in the browser.
                    options.AddArgument("--disable-extensions");
                    
                    return new ChromeDriver(chromeDriverFolder, options);
                }
                case Browser.Firefox:
                {
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
                }
                case Browser.InternetExplorer:
                {
                    string ieDriverFolder = UnpackDriver("IEDriverServer.exe", Resources.IEDriverServer);
                    
                    return new InternetExplorerDriver(ieDriverFolder);
                }
                case Browser.PhantomJS:
                {
                    var driverFolder = UnpackDriver("phantomjs.exe", Resources.phantomjs);
                    var options = new PhantomJSOptions();
                    var driver = new PhantomJSDriver(driverFolder, options);
                    driver.Manage().Window.Size = new System.Drawing.Size(1280, 1024);
                    return driver;
                }
                default:
                {
                    string message = string.Format("Error while building webdriver.");

                    Log.Fatal(message);
                    throw new Exception(message);
                }
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
                // Throw new FileLoadException(string.Format("Could not deploy the driver '{0}', maybe the driver is still running?", binaryName), e);
                Log.WarnFormat("Could not deploy the driver '{0}', maybe the driver is still running? Using the existing driver.", binaryName);
            }

            return tempFolder;
        }
    }
}
