using CodatLibrary.Extensions;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodatExam.Pages
{
    public class DeleteEmployee
    {
        private IWebDriver _driver;
        private IndexEmployee _indexEmployee;
        private CreateNewEmployee _createNew;
        private string _employeeTimeSheetId;
        public DeleteEmployee(IWebDriver driver)
        {
            _driver = driver;
            _indexEmployee = new IndexEmployee(driver);
            _createNew = new CreateNewEmployee(driver);
        }

        public IWebElement BtnConfirmDelete => _driver.FindElement(By.XPath("//input[@type='submit']"));

        public IWebElement TimesheetId=> _driver.FindElement(By.XPath("//main/div/h4"));

        public void GetEmployeeTimeSheetId(int rowToDelete)
        {
            int totalRows = GetRows();
            if (rowToDelete > totalRows)
                Assert.Fail("There are no details for the row {0} requested in the table", rowToDelete);
            else
            {
                _employeeTimeSheetId = _indexEmployee.TimeSheetId(rowToDelete).Text;
            }
        }

        public void DeleteEmployeeTimeSheetId(int rowToDelete)
        {
            _driver.Click(_indexEmployee.DeleteEmployeeRow(rowToDelete));
            Assert.AreEqual(_employeeTimeSheetId, TimesheetId.Text.Replace("TimeSheet", "").Trim(), "Time sheet of the employee does not match in delete confirmation");
            _driver.Click(BtnConfirmDelete);
            _driver.TitleContains("Index");
        }

        public void VerifyEmployeeTimeSheetInTable()
        {
            int exists = _createNew.VerifyEmployeeInTable(_employeeTimeSheetId);
            if (exists ==0)
                Assert.True(1 == 1, "Deleted employee {0} does not exists in table", _employeeTimeSheetId);
            else
                Assert.Fail("Deleted employee {0} still exist in table", _employeeTimeSheetId);
        }

        private int GetRows()
        {
            return _indexEmployee.EmployeeTimeSheetColumn.Count;
        }
    }
}
