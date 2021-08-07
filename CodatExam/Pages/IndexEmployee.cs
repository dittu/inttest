using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodatExam.Pages
{
    class IndexEmployee
    {
        private IWebDriver Driver { get; set; }
        public IndexEmployee(IWebDriver driver)
        {
            Driver = driver;
        }


        public IWebElement BtnCreateNew => Driver.FindElement(By.LinkText("Create New"));

        
    }
}
