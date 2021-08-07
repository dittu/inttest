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
    public class CreateNewEmployeeSteps : IDisposable
    {
        private IWebDriver Driver;
        private CreateNewEmployee createNewEmployee;
        private Settings configSettings;
        public CreateNewEmployeeSteps(WebDriverContext webDriverContext)
        {
            Driver = webDriverContext.Driver;
            configSettings = webDriverContext.configSettings;
            createNewEmployee = new CreateNewEmployee(Driver);
        }
        public void Dispose()
        {
            if(Driver != null)
            {
                Driver.Dispose();
                Driver = null;
            }
        }

        [Given(@"I'm in Codat main page")]
        public void GivenIMInCodatMainPage()
        {
            Driver.Navigate().GoToUrl(configSettings.Url);
            Driver.TitleContains("Index");
        }

        [When(@"I click on '(.*)'")]
        public void WhenIClickOn(string linkName)
        {
            createNewEmployee.ClickOn(linkName);
        }

        [When(@"I enter ""(.*)"" in ""(.*)""")]
        public void WhenIEnterIn(string value, string fieldName)
        {
            createNewEmployee.EnterValueInField(value, fieldName);
        }

        [Then(@"I should see validations for fields")]
        public void ThenIShouldSeeValidationsForFields(Table table)
        {
            var dictionary = table.ToDictionary();
            createNewEmployee.VerifyValidationMessage(dictionary);
        }

        [When(@"I enter valid details for new employee creation")]
        public void WhenIEnterValidDetailsForNewEmployeeCreation()
        {
            createNewEmployee.EnterValidValuesInFields();
        }

        [Then(@"I should be navigated to page with title containing ""(.*)""")]
        public void ThenIShouldBeNavigatedToPageWithTitleContaining(string title)
        {
            Driver.TitleContains(title);
        }

        [Then(@"created employee details should be same as entered")]
        public void ThenCreatedEmployeeDetailsShouldBeSameAsEntered()
        {
            createNewEmployee.ValidateValues();
        }

        [Then(@"new employee should be seen in the table")]
        public void ThenNewEmployeeShouldBeSeenInTheTable()
        {
            CreateNewEmployee.VerifyCreatedEmployeeInTable();
        }

    }
}
