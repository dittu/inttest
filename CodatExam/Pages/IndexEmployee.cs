using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodatExam.Pages
{
    public class IndexEmployee
    {
        private IWebDriver _driver;
       // private CreateNewEmployee _createNewEmployee;
        public IndexEmployee(IWebDriver driver)//, CreateNewEmployee employee=null)
        {
            _driver = driver;
         //   _createNewEmployee = employee;
        }
      
        public IList<IWebElement> EmployeeTimeSheetColumn => _driver.FindElements(By.XPath("//*[@class='table']/tbody/tr"));

        public IWebElement DeleteEmployeeRow(int rowNum)
        {
            return _driver.FindElement(By.XPath("//*[@class='table']/tbody/tr[" + rowNum + "]/td[5]/a[3]"));
        }
        public IWebElement DetailsEmployeeRow(int rowNum)
        {
            return _driver.FindElement(By.XPath("//*[@class='table']/tbody/tr[" + rowNum + "]/td[5]/a[2]"));
        }

        public IWebElement EditEmployeeRow(int rowNum)
        {
            return _driver.FindElement(By.XPath("//*[@class='table']/tbody/tr[" + rowNum + "]/td[5]/a[1]"));
        }

        public IWebElement TimeSheetId(int rowNum)
        {
            return _driver.FindElement(By.XPath("//*[@class='table']/tbody/tr[" + rowNum + "]/td[2]"));
        }

        //public int VerifyEmployeeInTable()
        //{
        //    int timeIdFound = 0;
        //    foreach(IWebElement timesheetrow in EmployeeTimeSheetColumn)
        //    {
        //        if (timesheetrow.Text.Contains(_createNewEmployee._timeSheetId))
        //        {
        //            timeIdFound += 1;
        //            break;
        //        }
        //    }
        //    if (timeIdFound != 1)
        //        Assert.Fail("Created employee with timesheet id {0} is not found in index/home page", _createNewEmployee._timeSheetId);
        //    Console.WriteLine("New employee details can be seen in Index page.");
        //    return timeIdFound;
        //}
    }
}
