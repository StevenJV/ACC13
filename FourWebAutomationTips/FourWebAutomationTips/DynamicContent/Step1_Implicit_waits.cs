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
    class Step1_Implicit_waits
    {
        IWebDriver browser;
        WebDriverWait wait;

        private const string URL = "http://localhost/AjaxDemos/DropDown/DropDown.aspx";
   
        [TestFixtureSetUp]
        public void Run_once_before_any_tests()
        {
            browser = new FirefoxDriver();
            browser.Navigate().GoToUrl(URL);
            wait = new WebDriverWait(browser, TimeSpan.FromSeconds(1));
        }

        [TestFixtureTearDown]
        public void Run_once_after_all_tests_are_completed()
        {
            browser.Quit();
        }

        [Test]
        public void Working_with_drop_down_menu()
        {

            browser.FindElement(By.Id("ctl00_SampleContent_TextLabel")).Click();
            browser.FindElement(By.LinkText("Mocha Blast")).Click();

            browser.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(1));

            Assert.AreEqual("Mocha Blast",
                browser.FindElement(By.CssSelector("span[id='ctl00_SampleContent_lblSelection']>b")).Text);

        }


    }
}
