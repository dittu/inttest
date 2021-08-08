using CodatLibrary.Drivers;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using TechTalk.SpecFlow;
using System.Linq;
using CodatExam.Helper;
using System.Reflection;
using TechTalk.SpecRun;

namespace CodatExam.Context
{
    public class WebDriverContext:IDisposable
    {
        public IWebDriver Driver;

        public Settings configSettings;

        public WebDriverContext(TestRunContext testRunContext, ScenarioContext scenariocontext)
        {
            if (Driver == null)
            {                
                var builder = new ConfigurationBuilder().SetBasePath(testRunContext.TestDirectory)
                                                        .AddJsonFile("appSettings.json", false, true).Build();
                configSettings = builder.GetSection("AutomationSettings").Get<Settings>();
                
                Driver = WebDrivers.InitBrowser(configSettings.Browser);
                Driver.Manage().Window.Maximize();
                Driver.Manage().Cookies.DeleteAllCookies();
            }
        }

        public void Dispose()
        {
            if (Driver != null)
            {
                Driver.Quit();
                Driver.Dispose();
                Driver = null;
            }
        }
    }
}
