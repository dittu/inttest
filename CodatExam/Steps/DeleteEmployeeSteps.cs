using CodatExam.Context;
using CodatExam.Helper;
using CodatExam.Pages;
using CodatLibrary.Extensions;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace CodatExam.Steps
{
    [Binding]
    public class DeleteEmployeeSteps
    {
        private IWebDriver _driver;
        private DeleteEmployee _deleteEmployee;
        private IndexEmployee _indexEmployee;
        private Settings _configSettings;
        public DeleteEmployeeSteps(WebDriverContext webDriverContext)
        {
                _driver = webDriverContext.Driver;
                _configSettings = webDriverContext.configSettings;
                _deleteEmployee = new DeleteEmployee(_driver);
        }
        
        [When(@"I delete the row ""(.*)"" from the table")]
        public void WhenIDeleteTheRowFromTheTable(int rowToDelete)
        {
            _deleteEmployee.GetEmployeeTimeSheetId(rowToDelete);
            _deleteEmployee.DeleteEmployeeTimeSheetId(rowToDelete);
        }

        [Then(@"the employee details are not in the table")]
        public void ThenTheEmployeeDetailsAreNotInTheTable()
        {
            _deleteEmployee.VerifyEmployeeTimeSheetInTable();
        }

    }
}
