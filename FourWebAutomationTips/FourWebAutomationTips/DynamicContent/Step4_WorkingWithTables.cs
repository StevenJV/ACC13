using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace FourWebAutomationTips.DynamicContent
{
    [TestFixture]
    class Step4_WorkingWithTables
    {
        IWebDriver browser;
        WebDriverWait wait;

        private const string URL = "http://localhost/RadControlExamples/default.aspx";
        //"http://localhost/WorkingWithLocators/";
        private const string GRIDID = "ctl00_MainContent_PeopleGrid";

        const string EDITTABLEID = "div.rgEditForm>table";

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
        public void v1_edit_cobb_row_shows_proper_values()
        {
            IWebElement aRow = FindCobbRow();
            IWebElement editLink = aRow.FindElement(By.LinkText("Edit"));

            editLink.Click();
            string temp = aRow.FindElement(
                By.CssSelector(REGION)).GetAttribute("Value");

            Assert.AreEqual("New Earth",
                aRow.FindElement(
                By.CssSelector(REGION)).GetAttribute("Value"));
            Assert.AreEqual("Blue Sun",
                aRow.FindElement(
                By.CssSelector(COMPANY)).GetAttribute("Value"));
            Assert.AreEqual("Cobb",
                aRow.FindElement(
                By.CssSelector(LNAME)).GetAttribute("Value"));
            Assert.AreEqual("Jayne",
                aRow.FindElement(By.CssSelector(FNAME)).GetAttribute("Value"));
            Assert.AreEqual("12",
                aRow.FindElement(By.CssSelector(ID)).GetAttribute("Value"));
        }

        
        [Test]
        public void v2_edit_cobb_row_shows_proper_values()
        {
            IWebElement aRow = FindCobbRow();
            IWebElement editLink = aRow.FindElement(By.LinkText("Edit"));

            editLink.Click();

            IWebElement editGrid = browser.FindElement(By.CssSelector(EDITTABLEID));
            editGrid = FindCobbRow();
            Assert.IsTrue(
                editGrid.Text.Contains("New Earth"));
            Assert.IsTrue( 
                editGrid.Text.Contains("Blue Sun"));
            Assert.IsTrue( 
                editGrid.Text.Contains("Cobb"));
            Assert.IsTrue(
                editGrid.Text.Contains("Jayne"));
            Assert.IsTrue( 
                editGrid.Text.Contains("12"));
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


        [Test]
        public void other_locator()
        {
            var row = browser.FindElement(By.XPath("id('ctl00_MainContent_PeopleGrid')//tr[contains(.,'Cobb')]"));
        }
    }
}
