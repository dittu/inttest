using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace CodatExam.Pages
{
    public class DetailsEmployee
    {
        private IWebDriver _driver;
        public DetailsEmployee(IWebDriver driver)
        {
            _driver = driver;
        }
        public IWebElement ConfirmedEmployeeId => _driver.FindElement(By.XPath("//dd[@class='col-sm-10'][1]"));
        public IWebElement ConfirmedHourlyRate => _driver.FindElement(By.XPath("//dd[@class='col-sm-10'][2]"));
        public IWebElement ConfirmedTimeSheets => _driver.FindElement(By.XPath("//*[@class='table']/tbody/tr"));
        public IWebElement ConfirmedTimeSheetId => _driver.FindElement(By.XPath("//main/h1"));
      
    }
}
