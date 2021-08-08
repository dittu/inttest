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
    public class CreateNewEmployeeSteps
    {
        private IWebDriver _driver;
        private CreateNewEmployee _createNewEmployee;
        private IndexEmployee _indexEmployee;
        private Settings _configSettings;
        public CreateNewEmployeeSteps(WebDriverContext webDriverContext)
        {
                _driver = webDriverContext.Driver;
                _configSettings = webDriverContext.configSettings;
                _createNewEmployee = new CreateNewEmployee(_driver);
                _indexEmployee = new IndexEmployee(_driver, _createNewEmployee);
        }
      
        [Given(@"I am in Codat main page")]
        public void GivenIAmInCodatMainPage()
        {
            _createNewEmployee.NavigateToHomePage(_configSettings.Url);
        }
       
        [When(@"I click on '(.*)'")]
        public void WhenIClickOn(string linkName)
        {
            _createNewEmployee.ClickOn(linkName);
        }

        [When(@"I enter ""(.*)"" in ""(.*)""")]
        public void WhenIEnterIn(string value, string fieldName)
        {
            _createNewEmployee.EnterValueInField(value, fieldName);
        }

        [Then(@"I should see validations for fields")]
        public void ThenIShouldSeeValidationsForFields(Table table)
        {
            var dictionary = table.ToDictionary();
            _createNewEmployee.VerifyValidationMessage(dictionary);
        }

        [When(@"I enter ""(.*)"" details for new employee creation")]
        public void WhenIEnterDetailsForNewEmployeeCreation(string details)
        {
            _createNewEmployee.EmployeeCreate(details);
        }


        [Then(@"I should be navigated to page with title containing ""(.*)""")]
        public void ThenIShouldBeNavigatedToPageWithTitleContaining(string title)
        {
            _driver.TitleContains(title);
        }

        [Then(@"verify employee details same as entered")]
        public void ThenVerifyEmployeeDetailsSameAsEntered()
        {
            _createNewEmployee.ValidateValues();
        }

        [Then(@"new employee details displayed in the table")]
        public void ThenNewEmployeeDetailsDisplayedInTheTable()
        {
            _indexEmployee.VerifyCreatedEmployeeInTable();
        }

        [When(@"I use same employee Id to create another payer")]
        public void WhenIUseSameEmployeeIdToCreateAnotherPayer()
        {
            _createNewEmployee.NavigateToHomePage(_configSettings.Url);
            _createNewEmployee.ClickOn("CreateNew");
            _createNewEmployee.EnterSameEmployeeDetails();
        }

    }
}
