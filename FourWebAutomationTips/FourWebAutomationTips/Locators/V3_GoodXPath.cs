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
    public class V3_GoodXPath
    {
        const string LOCAL = "http://localhost:3000";
        IWebDriver browser;

        private string LOGINLINK = "id('top-menu')/a[3]";
        //private string USERNAME = "/html/body/div[3]/div[2]/form/div[2]/input";
        //private string PASSWORD = "/html/body/div[3]/div[2]/form/div[3]/input";
        private string LOGINBUTTON = "/html/body/div[3]/div[2]/form/div[4]/input";
        private string LOGOUTLINK = "id('top-menu')/a[3]";

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
                By.XPath(this.LOGINLINK)).Click();
            browser.FindElement(
                By.XPath(this.USERNAME)).SendKeys("testuser");
            browser.FindElement(
                By.XPath(this.PASSWORD)).SendKeys("abc123");
            browser.FindElement(
                By.XPath(this.LOGINBUTTON)).Click();

            Please_stop_asking_about_this();

            Assert.IsTrue(browser.FindElement(By.XPath(this.LOGOUTLINK))
                                 .Text
                                 .Equals("Logout"));

            Assert.IsTrue(browser.FindElement(By.XPath(LOGOUTLINK))
                                 .GetAttribute("href")
                                 .Equals("http://localhost:3000/logout"));
        }

        private void Please_stop_asking_about_this()
        {
            WebDriverWait wait = new WebDriverWait(browser, TimeSpan.FromSeconds(10));
            wait.Until<bool>((d) =>
            {
                return d.FindElement(By.XPath("id('top-menu')/a[3]")).Text.Equals("Logout");
            });
        }


        private string USERNAME = "//label[text()='Username']/../input";
        private string PASSWORD = "//label[text()='Password']/../input";
    }
}
