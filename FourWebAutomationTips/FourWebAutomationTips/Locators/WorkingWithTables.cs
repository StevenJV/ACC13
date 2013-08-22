using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace FourWebAutomationTips.Locators
{
    [TestFixture]
    class WorkingWithTables
    {
        IWebDriver browser;
        WebDriverWait wait;

        private const string URL = "http://localhost/RadControlExamples/default.aspx";
        private const string GRIDID = "ctl00_MainContent_PeopleGrid";

        const string EDITTABLEID = "//*[@id='ctl00_MainContent_PeopleGrid_ctl00']//tr[contains(.,'Cobb')]";

        const string REGION = "td[id$='__Region']>input";
        const string COMPANY = "td[id$='__Company']>input";
        const string LNAME = "td[id$='__LastName']>input";
        const string FNAME = "td[id$='__FirstName']>input";
        const string ID = "td[id$='__Id']>input";

        [TestFixtureSetUp]
        public void Run_once_before_any_tests()
        {
            browser = new FirefoxDriver();
            browser.Navigate().GoToUrl(URL);
            wait = new WebDriverWait(browser, TimeSpan.FromSeconds(10));
        }

        [TestFixtureTearDown]
        public void Run_once_after_all_tests_are_completed()
        {
            browser.Quit();
        }

        [Test]
        public void Table_contains_two_from_New_Earth_region()
        {
            int numHits = 0;

            wait.Until(ExpectedConditions.ElementExists(By.Id(GRIDID)));
            IWebElement table = browser.FindElement(By.Id(GRIDID));
            IList<IWebElement> rows =
                table.FindElements(By.TagName("tr"));
            foreach (var row in rows)
            {
                if (row.Text.Contains("New Earth"))
                {
                    numHits++;
                }
            }

            Assert.AreEqual(2, numHits);
        }


        [Test]
        public void edit_cobb_row_shows_proper_values()
        {
            IWebElement aRow = FindCobbRow();
            IWebElement editLink = aRow.FindElement(By.LinkText("Edit"));

            editLink.Click();

            Assert.AreEqual("New Earth", 
                browser.FindElement(
                By.CssSelector(REGION)).GetAttribute("Value"));
            Assert.AreEqual("Blue Sun", 
                browser.FindElement(
                By.CssSelector(COMPANY)).GetAttribute("Value"));
            Assert.AreEqual("Cobb", 
                browser.FindElement(
                By.CssSelector(LNAME)).GetAttribute("Value"));
            Assert.AreEqual("Jayne", 
                browser.FindElement(By.CssSelector(FNAME)).GetAttribute("Value"));
            Assert.AreEqual("12", 
                browser.FindElement(By.CssSelector(ID)).GetAttribute("Value"));
        }

        private IWebElement FindCobbRow()
        {
            wait.Until(ExpectedConditions.ElementExists(By.Id(GRIDID)));
            IWebElement table = browser.FindElement(By.Id(GRIDID));

            IWebElement targetRow = null;
            IList<IWebElement> rows = table.FindElements(By.TagName("tr"));
            foreach (var row in rows)
            {
                if (row.Text.Contains("Cobb"))
                {
                    targetRow = row;
                }
            }
            return targetRow;
        }
    }
}
