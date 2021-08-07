using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace CodatExam.Pages
{
    public class DetailsEmployee
    {
         private IWebDriver Driver { get; set; }
        public DetailsEmployee(IWebDriver driver)
        {
            Driver = driver;
        }
        public IWebElement ConfirmedEmployeeId => Driver.FindElement(By.XPath("//dd[@class='col-sm-10'][1]"));
        public IWebElement ConfirmedHourlyRate => Driver.FindElement(By.XPath("//dd[@class='col-sm-10'][2]"));
        public IWebElement ConfirmedTimeSheets => Driver.FindElement(By.XPath("//*[@class='table']/tbody/tr"));
        public IWebElement ConfirmedTimeSheetId => Driver.FindElement(By.XPath("//main/h1"));
      
    }
}
