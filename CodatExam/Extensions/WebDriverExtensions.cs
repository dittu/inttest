using CodatExam.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodatLibrary.Extensions
{
    public static class WebDriverExtensionMethod
    {
        public static void Navigate(this IWebDriver driver, string URL)
        {
            driver.Navigate().GoToUrl(URL);
        }

        public static bool TitleContains(this IWebDriver driver, string title)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleContains(title));
            return true;
        }

        public static void SendKeys(this IWebDriver driver,IWebElement element,string text)
        {
            try
            {
                if (!element.Displayed)
                {
                    driver.WaitUntilElementClickable(element);
                    element.Clear();
                    element.SendKeys(text);
                }
                if (element.Displayed)
                {
                    element.Clear();
                    element.SendKeys(text);
                }
            }catch(NoSuchElementException e)
            {
                Console.WriteLine("Element {0} is not available to enter the text {1}", element, text);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
