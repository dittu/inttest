using CodatExam.Context;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Tracing;

namespace CodatExam.Hooks
{
    public sealed class Hooks
    {
        private readonly IWebDriver _webDriver;
        private ScenarioContext _scenarioContext;
        private FeatureContext _featureContext;
        public Hooks(WebDriverContext webDriverContext,ScenarioContext scenarioContext,FeatureContext featureContext)
        {
            _webDriver = webDriverContext.Driver;
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
        }

        private static IConfiguration config;

        [BeforeScenario]
        
        [AfterScenario]
        public void TearDown()
        {
            if (_scenarioContext.TestError != null)
                TakeScreenshot();
        }

        private void TakeScreenshot()
        {
            try
            {
                string fileNameBase = string.Format("error_{0}_{1}_{2}",
                    _featureContext.FeatureInfo.Title.ToIdentifier(),
                    _scenarioContext.ScenarioInfo.Title.ToIdentifier(),
                    DateTime.Now.ToString("yyyyMMdd_HHmmss"));

                var artifactDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Screenshots");
                if (!Directory.Exists(artifactDirectory))
                    Directory.CreateDirectory(artifactDirectory);

                string pageSource = _webDriver.PageSource;
                string sourceFilePath = Path.Combine(artifactDirectory, fileNameBase + "_source.html");
                File.WriteAllText(sourceFilePath, pageSource, Encoding.UTF8);
                Console.WriteLine("Page Source {0}", new Uri(sourceFilePath));

                ITakesScreenshot takesScreenshot = _webDriver as ITakesScreenshot;

                if (takesScreenshot != null)
                {
                    var screenShot = takesScreenshot.GetScreenshot();
                    string screenShotFilePath = Path.Combine(artifactDirectory, fileNameBase + "_screenshot.png");
                    screenShot.SaveAsFile(screenShotFilePath, ScreenshotImageFormat.Png);
                    Console.WriteLine("Screenshot {0}", new Uri(screenShotFilePath));
                }
            }catch(Exception ex)
            {
                Console.WriteLine("Error while taking a screenshot {0}", ex);
            }
        }
    }
}
