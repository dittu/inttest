using CodatLibrary.Extensions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CodatExam.Pages
{
    public class CreateNewEmployee
    {
        private IWebDriver Driver { get; set; }
        DetailsEmployee detailsEmployee;
        private string _employeeId;
        private string _dayOfTheWeek;
        private int _hours;
        private int _minutes;
        private decimal _hourlyRate;
        private string _timeSheetId;


        public CreateNewEmployee(IWebDriver driver)
        {
            Driver = driver;
            detailsEmployee = new DetailsEmployee(Driver);
        }

        public void ClickOn(string linkName)
        {
            switch (linkName)
            {
                case "Create New":
                    BtnCreateNew.Click();
                    Driver.TitleContains("Create");
                    break;
                case "Back To List":
                    LnkBackToList.Click();
                    Driver.TitleContains("Index");
                    break;
                case "Save":
                    BtnSave.Click();
                    break;
                case "Add New Row":
                    BtnAddRow.SendKeys(Keys.Return);
                    break;
                default:
                    throw new NotFoundException("Element not found.");

            }
        }

        public void EnterValueInField(string value,string fieldName)
        {
            switch (fieldName)
            {
                case "HourlyRate":
                    TxtHourlyRate.Clear();
                    TxtHourlyRate.SendKeys(value);
                    break;
                case "Day":
                    SelectElement selObj = new SelectElement(DdDay);
                    selObj.SelectByText(value);
                    break;
                case "Hours":
                    TxtHours.Clear();
                    TxtHours.SendKeys(value);
                    break;
                case "Minutes":
                    TxtMinutes.Clear();
                    TxtMinutes.SendKeys(value);
                    break;
                default: throw new NotFoundException("Element not found");
            }
        }

        public void VerifyValidationMessage(Dictionary<string,string> dictionary)
        {
            if(dictionary!=null)
            {
                foreach(var key in dictionary)
                {
                    switch (key.Key)
                    {
                        case "EmployeeID":
                            Debug.Write(ErrEmployeeId.Text);
                            Assert.True(ErrEmployeeId.Text.Contains(key.Value));
                            break;
                        case "HourlyRate":
                            Debug.Write(ErrHourlyRate.Text);
                            Assert.True(ErrHourlyRate.Text.Contains(key.Value));
                            break;
                        case "Day":
                            Debug.Write(ErrDay.Text);
                            Assert.True(ErrDay.Text.Contains(key.Value));

                            break;
                        case "Hours":
                            Debug.Write(ErrHours.Text);
                            Assert.True(ErrHours.Text.Contains(key.Value));
                            break;
                        case "Minutes":
                            Debug.Write(ErrMinutes.Text);
                            Assert.True(ErrMinutes.Text.Contains(key.Value));
                            break;
                        default:
                            Assert.Fail("Invalid field option sent through feature file", key.Value);
                            break;
                    }
                }
            }
        }

        public void EnterValidValuesInFields()
        {
            _employeeId = "CDT" + GenerateRandomNumber(1, 3);
            Driver.SendKeys(TxtEmployeeId, _employeeId);

            _hourlyRate = GenerateRandomNumber(0, 24);
            Driver.SendKeys(TxtHourlyRate,_hourlyRate.ToString());

            SelectElement selObj = new SelectElement(DdDay);
            int dayOfWeek = GenerateRandomNumber(0, 6);
            selObj.SelectByValue(dayOfWeek.ToString());
            _dayOfTheWeek = ((DayOfWeek)dayOfWeek).ToString();

            _hours = GenerateRandomNumber(0, 24);
            Driver.SendKeys(TxtHours, _hours.ToString());

            _minutes = GenerateRandomNumber(0, 60);
            Driver.SendKeys(TxtMinutes, _minutes.ToString());
            BtnAddRow.SendKeys(Keys.Return);
        }

        public void ValidateValues()
        {
            Assert.AreEqual(_employeeId, detailsEmployee.ConfirmedEmployeeId.Text);
            Assert.True(detailsEmployee.ConfirmedHourlyRate.Text.Contains(_hourlyRate.ToString()));
            Console.WriteLine(detailsEmployee.ConfirmedTimeSheets.Text);
            Assert.True(detailsEmployee.ConfirmedTimeSheets.Text.Contains(_dayOfTheWeek));
            Assert.True(detailsEmployee.ConfirmedTimeSheets.Text.Contains(_hours.ToString()));
            Assert.True(detailsEmployee.ConfirmedTimeSheets.Text.Contains(_minutes.ToString()));

            _timeSheetId = detailsEmployee.ConfirmedTimeSheetId.Text.Replace("Timesheet", string.Empty).Trim();
            Console.WriteLine("Steps successful");
        }

        public void VerifyCreatedEmployeeInTable()
        {

        }

        private int GenerateRandomNumber(int min,int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
      
        //Web element locators
        public IWebElement TxtEmployeeId => Driver.FindElement(By.Id("Timesheet_EmployeeId"));
        public IWebElement TxtHourlyRate => Driver.FindElement(By.Id("Timesheet_HourlyRate"));
        public IWebElement DdDay => Driver.FindElement(By.Id("newEntry_Day"));
        public IWebElement TxtHours => Driver.FindElement(By.Id("newEntry_Hours"));
        public IWebElement TxtMinutes => Driver.FindElement(By.Id("newEntry_Minutes"));
        public IWebElement BtnAddRow => Driver.FindElement(By.Id("add-row"));
        public IWebElement BtnSave => Driver.FindElement(By.XPath("//input[@type='submit']"));
        public IWebElement LnkBackToList => Driver.FindElement(By.LinkText("Back to List"));
        public IWebElement ErrEmployeeId => Driver.FindElement(By.Id("Timesheet_EmployeeId-error"));
        public IWebElement ErrHourlyRate => Driver.FindElement(By.Id("Timesheet_HourlyRate-error"));
        public IWebElement ErrDay => Driver.FindElement(By.Id("newEntry_Day-error"));
        public IWebElement ErrHours => Driver.FindElement(By.Id("newEntry_Hours-error"));
        public IWebElement ErrMinutes => Driver.FindElement(By.Id("newEntry_Minutes-error"));




    }
}
