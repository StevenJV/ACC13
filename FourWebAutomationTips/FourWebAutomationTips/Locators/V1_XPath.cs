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
    public class V1_XPath
    {
        const string LOCAL = "http://localhost:3000";
        // this is a demo rails app, available from https://github.com/PillarTechnology/Telerik-Demo 
        // running on C:\Telerik-Demo-master
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
                        By.XPath("id('top-menu')/a[3]"))
                   .Click();
            //  fully-qualified XPath to find form input fields
            browser.FindElement(
                        By.XPath("/html/body/div[3]/div[2]/form/div[2]/input"))
                   .SendKeys("testuser");
            browser.FindElement(
                        By.XPath("/html/body/div[3]/div[2]/form/div[3]/input"))
                   .SendKeys("abc123");
            browser.FindElement(
                        By.XPath("/html/body/div[3]/div[2]/form/div[4]/input"))
                   .Click();

            // this works fine, until the page design changes
            // C:\Telerik-Demo-master\app\views\sessions\_form.html.erb
            // 
            
            DontAskAboutThisYet();

            Assert.IsTrue(browser.FindElement(By.XPath("id('top-menu')/a[3]"))
                                 .Text
                                 .Equals("Logout"));

            Assert.IsTrue(browser.FindElement(By.XPath("id('top-menu')/a[3]"))
                                 .GetAttribute("href")
                                 .Equals("http://localhost:3000/logout"));
        }

        private void DontAskAboutThisYet()
        {
            WebDriverWait wait = new WebDriverWait(browser, TimeSpan.FromSeconds(10));
            wait.Until<bool>((d) =>
            {
                return d.FindElement(By.XPath("id('top-menu')/a[3]")).Text.Equals("Logout");
            });
        }
    }
}