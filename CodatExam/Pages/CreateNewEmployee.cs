using CodatLibrary.Extensions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace CodatExam.Pages
{
    public class CreateNewEmployee
    {
        private IWebDriver _driver;
        DetailsEmployee _detailsEmployee;
        IndexEmployee _indexEmployee;
        private string _employeeId;
        private string _dayOfTheWeek;
        private int _hours;
        private int _minutes;
        private decimal _hourlyRate;
        public string _employeeTimeSheetId;


        public CreateNewEmployee(IWebDriver driver)
        {
            _driver = driver;
            _detailsEmployee = new DetailsEmployee(_driver);
            _indexEmployee = new IndexEmployee(driver);
        }

        public void NavigateToHomePage(string Url)
        {
            _driver.Navigate(Url);
        }

        public void ClickOn(string linkName)
        {
            switch (linkName)
            {
                case "CreateNew":
                    _driver.Click(BtnCreateNew);
                    _driver.TitleContains("Create");
                    break;
                case "BackToList":
                    _driver.Click(LnkBackToList);
                    _driver.TitleContains("Index");
                    break;
                case "Save":
                    _driver.Click(BtnSave);
                    break;
                case "AddNewRow":
                    //_driver.Click(BtnAddRow);
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
                case "EmployeeId":
                    _driver.SendKeys(TxtEmployeeId, value);
                    break;
                case "HourlyRate":
                    _driver.SendKeys(TxtHourlyRate, value);
                    break;
                case "Day":
                    SelectElement selObj = new SelectElement(DdDay);
                    selObj.SelectByText(value);
                    break;
                case "Hours":
                    _driver.SendKeys(TxtHours, value);
                    break;
                case "Minutes":
                    _driver.SendKeys(TxtMinutes, value);
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
                            try
                            {
                                Assert.True(ErrEmployeeId.Text.Contains(key.Value), "Employee ID validation message not shown");
                            }
                            catch (NoSuchElementException)
                            {
                                Assert.Fail("Validation message for Employee Id is not shown");
                            }
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

        public void EmployeeCreate(string detailstype)
        {
            if (detailstype == "valid")
            {
                _employeeId = "CDT" + GenerateRandomNumber(1, 3);
                _driver.SendKeys(TxtEmployeeId, _employeeId);

                _hourlyRate = GenerateRandomNumber(0, 24);
                _driver.SendKeys(TxtHourlyRate, _hourlyRate.ToString());

                SelectElement selObj = new SelectElement(DdDay);
                int dayOfWeek = GenerateRandomNumber(0, 6);
                selObj.SelectByValue(dayOfWeek.ToString());
                _dayOfTheWeek = ((DayOfWeek)dayOfWeek).ToString();

                _hours = GenerateRandomNumber(0, 24);
                _driver.SendKeys(TxtHours, _hours.ToString());

                _minutes = GenerateRandomNumber(0, 60);
                _driver.SendKeys(TxtMinutes, _minutes.ToString());
               
            }
            if (detailstype == "invalid")
            {
                _employeeId = "TTD" + GenerateRandomNumber(0, 0);
                _driver.SendKeys(TxtEmployeeId, _employeeId);

                _hourlyRate = GenerateRandomNumber(-50, 0);
                _driver.SendKeys(TxtHourlyRate, _hourlyRate.ToString());

                SelectElement selObj = new SelectElement(DdDay);
                int dayOfWeek = GenerateRandomNumber(0, 6);
                selObj.SelectByValue(dayOfWeek.ToString());
                _dayOfTheWeek = ((DayOfWeek)dayOfWeek).ToString();

                _hours = GenerateRandomNumber(-50, 0);
                _driver.SendKeys(TxtHours, _hours.ToString());

                _minutes = GenerateRandomNumber(-50, 0);
                _driver.SendKeys(TxtMinutes, _minutes.ToString());
            }
             _driver.Click(BtnAddRow);
        }

        public void EnterSameEmployeeDetails()
        {
            _driver.SendKeys(TxtEmployeeId, _employeeId);

            _driver.SendKeys(TxtHourlyRate, _hourlyRate.ToString());

            SelectElement selObj = new SelectElement(DdDay);
            int dayOfWeek = GenerateRandomNumber(0, 6);
            selObj.SelectByValue(dayOfWeek.ToString());
            _dayOfTheWeek = ((DayOfWeek)dayOfWeek).ToString();

            _driver.SendKeys(TxtHours, _hours.ToString());

            _driver.SendKeys(TxtMinutes, _minutes.ToString());
            ClickOn("Save");
            Thread.Sleep(1000);
        }

        public void ValidateValues()
        {
            Assert.AreEqual(_employeeId, _detailsEmployee.ConfirmedEmployeeId.Text);
            Assert.True(_detailsEmployee.ConfirmedHourlyRate.Text.Contains(_hourlyRate.ToString()));
            Console.WriteLine(_detailsEmployee.ConfirmedTimeSheets.Text);
            Assert.True(_detailsEmployee.ConfirmedTimeSheets.Text.Contains(_dayOfTheWeek));
            Assert.True(_detailsEmployee.ConfirmedTimeSheets.Text.Contains(_hours.ToString()));
            Assert.True(_detailsEmployee.ConfirmedTimeSheets.Text.Contains(_minutes.ToString()));

            _employeeTimeSheetId = _detailsEmployee.ConfirmedTimeSheetId.Text.Replace("Timesheet", string.Empty).Trim();
            Console.WriteLine("Steps successful");
        }

        public void VerifyNewEmployeeInTable()
        {
            int exists = VerifyEmployeeInTable(_employeeTimeSheetId);
            if (exists == 1)
                Assert.True(1 == 1, "Created new employee {0} exists in table", _employeeTimeSheetId);
            else
                Assert.Fail("Created new employee {0} does not exist in table", _employeeTimeSheetId);
        }

        public int VerifyEmployeeInTable(string timesheetId)
        {
            int timeIdFound = 0;
            string TimesheetId = timesheetId;
            foreach (IWebElement timesheetrow in _indexEmployee.EmployeeTimeSheetColumn)
            {
                if (timesheetrow.Text.Contains(TimesheetId))
                {
                    timeIdFound += 1;
                    break;
                }
            }
            //Console.WriteLine("New employee details can be seen in Index page.");
            return timeIdFound;
        }

        private int GenerateRandomNumber(int min,int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        //Web element locators
        public IWebElement BtnCreateNew => _driver.FindElement(By.LinkText("Create New"));
        public IWebElement TxtEmployeeId => _driver.FindElement(By.Id("Timesheet_EmployeeId"));
        public IWebElement TxtHourlyRate => _driver.FindElement(By.Id("Timesheet_HourlyRate"));
        public IWebElement DdDay => _driver.FindElement(By.Id("newEntry_Day"));
        public IWebElement TxtHours => _driver.FindElement(By.Id("newEntry_Hours"));
        public IWebElement TxtMinutes => _driver.FindElement(By.Id("newEntry_Minutes"));
        public IWebElement BtnAddRow => _driver.FindElement(By.Id("add-row"));
        public IWebElement BtnSave => _driver.FindElement(By.XPath("//input[@type='submit']"));
        public IWebElement LnkBackToList => _driver.FindElement(By.LinkText("Back to List"));
        public IWebElement ErrEmployeeId => _driver.FindElement(By.Id("Timesheet_EmployeeId-error"));
        public IWebElement ErrHourlyRate => _driver.FindElement(By.Id("Timesheet_HourlyRate-error"));
        public IWebElement ErrDay => _driver.FindElement(By.Id("newEntry_Day-error"));
        public IWebElement ErrHours => _driver.FindElement(By.Id("newEntry_Hours-error"));
        public IWebElement ErrMinutes => _driver.FindElement(By.Id("newEntry_Minutes-error"));




    }
}
