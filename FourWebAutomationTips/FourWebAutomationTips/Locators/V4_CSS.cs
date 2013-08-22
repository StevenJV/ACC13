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
    public class V4_CSS
    {
        const string LOCAL = "http://localhost:3000";
        IWebDriver browser;

        private string LOGINLINK = "a[id^='login']";
        private string USERNAME = "label[for='username']+input";
        private string PASSWORD = "label[for='password']+input";
        private string LOGINBUTTON = "input[value='Log in']";
        private string LOGOUTLINK = "a[href='/logout']";

        

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
                By.CssSelector(this.LOGINLINK))
                .Click();
            browser.FindElement(
                By.CssSelector(this.USERNAME))
                .SendKeys("testuser");
            browser.FindElement(
                By.CssSelector(this.PASSWORD))
                .SendKeys("abc123");
            browser.FindElement(
                By.CssSelector(this.LOGINBUTTON))
                .Click();

            Dont_ask_about_this_yet();

            Assert.IsTrue(browser.FindElement(By.CssSelector(this.LOGOUTLINK))
                                 .Text
                                 .Equals("Logout"));

            Assert.IsTrue(browser.FindElement(By.CssSelector(LOGOUTLINK))
                                 .GetAttribute("href")
                                 .Equals("http://localhost:3000/logout"));
        }

        private void Dont_ask_about_this_yet()
       {
           WebDriverWait wait = new WebDriverWait(browser, TimeSpan.FromSeconds(10));
           wait.Until<bool>((d) =>
           {
               return d.FindElement(By.XPath("id('top-menu')/a[3]")).Text.Equals("Logout");
           });
       }


        

        //private string USERNAME = "//label[text()='Username']/../input";
        //private string PASSWORD = "//label[text()='Password']/../input";
    }
}
