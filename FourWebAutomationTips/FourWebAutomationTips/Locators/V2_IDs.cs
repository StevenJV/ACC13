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
    public class V2_IDs
    {
        const string LOCAL = "http://localhost:3000";
        IWebDriver browser;

        [TestFixtureSetUp]
        public void Run_once_before_any_other_tests()
        {
            browser = new FirefoxDriver();
        }

        [TestFixtureTearDown]
        public void Run_after_all_other_tests_are_done()
        {
            browser.Quit();
        }

        [Test]
        public void Can_log_on_to_system()
        {
            browser.Navigate().GoToUrl(LOCAL);
            browser.FindElement(
                By.Id("login_link"))
                .Click();
            browser.FindElement(
                By.Name("username"))
                .SendKeys("testuser");
            browser.FindElement(
                By.Id("password"))
                .SendKeys("abc123");
            browser.FindElement(
                By.Id("login_button"))
                .Click();

            Still_dont_ask_about_this_yet();

            Assert.IsTrue(browser.FindElement(By.Id("logout_link"))
                                 .Text.Equals("Logout"));

            Assert.IsTrue(browser.FindElement(By.Id("logout_link"))
                                 .GetAttribute("href")
                                 .Equals("http://localhost:3000/logout"));

        }


        private void Still_dont_ask_about_this_yet()
        {
            WebDriverWait wait = new WebDriverWait(browser, TimeSpan.FromSeconds(10));
            wait.Until<bool>((d) =>
            {
                // this uses a relative XPath to find the logout item - 'a' tag #3 in the top-menu 
                return d.FindElement(By.XPath("id('top-menu')/a[3]")).Text.Equals("Logout");
            });
        }
    }
}
