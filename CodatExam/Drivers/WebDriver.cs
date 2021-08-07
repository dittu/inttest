using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace CodatLibrary.Drivers
{
    public class WebDrivers
    {
        public static IWebDriver InitBrowser(string browserName)
        {
            try
            {
                switch (browserName)
                {
                    case "Chrome":
                        {
                            ChromeOptions options = new ChromeOptions();
                            options.AcceptInsecureCertificates = true;
                            var driverService = ChromeDriverService.CreateDefaultService(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                            return new ChromeDriver(driverService, options);
                        }
                    default:
                        {
                            throw new NotImplementedException();
                        }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($@"Error occurred: { e.Message} StackTrace: {e.StackTrace}");
                throw;
            }
        }
    }
}

