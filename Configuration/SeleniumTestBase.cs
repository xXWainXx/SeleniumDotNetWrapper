using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;

namespace GrowthWheel_AutoTests.Configuration
{
    public class SeleniumTestBase<TFixture> : IDisposable, IClassFixture<TFixture>
        where TFixture: SettingUpFixture
    {
        protected static IConfiguration config;
        protected IWebDriver driver;
        protected WebDriverWait webDriverWait;
        protected IJavaScriptExecutor js;
        protected TFixture sb;
        protected IWebElement wait;
        protected Actions actions;

        //running before each test
        public SeleniumTestBase(TFixture sb)
        {
            this.driver = sb.driver;
            this.sb = sb;
            this.webDriverWait = new WebDriverWait(sb.driver, TimeSpan.FromSeconds(10));           
            this.actions = new Actions(sb.driver);
            this.js = (IJavaScriptExecutor)sb.driver;

            config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
        }

        //running after each test
        public void Dispose()
        {
            
        }

        public void TakeScreenshot(string methodName)
        {
            driver.TakeScreenshot()
                .SaveAsFile(config["screenshots_path"].ToString() + methodName + "_" + DateTime.Now.ToString("dd_MMMM_hh_mm_ss_tt") + ".png", ScreenshotImageFormat.Png);
        }

        protected IWebElement GetElement(string css)
        {
            try
            {
                return webDriverWait.Until(x => x.FindElement(By.CssSelector(css)));
            }
            catch (Exception e)
            {
                return null;
            }
        }

        protected string GetElementAttribute(string css, string attributeName)
        {
            return GetElement(css).GetAttribute(attributeName);
        }

        
        protected string GetElementAttribute(IWebElement element, string attributeName)
        {
            return element.GetAttribute(attributeName);
        }

        protected void InputValue(string css, string value)
        {
            IWebElement element = GetElement(css);
            element.Clear();
            element.SendKeys(value);
        }

        protected void SelectElementByValue(string css, string value)
        {
            var selectElement = new SelectElement(GetElement(css));
            selectElement.SelectByValue(value);
        }

        protected void SelectElementByText(string css, string value)
        {
            var selectElement = new SelectElement(GetElement(css));
            selectElement.SelectByText(value);
        }

        protected string GetElementValueWhenAppear(string css)
        {
            return webDriverWait.Until(x =>
            {
                var el = x.FindElement(By.CssSelector(css));
                var value = el.GetAttribute("value");
                if (String.IsNullOrWhiteSpace(value))
                {
                    return null;
                }

                return value;                
            });
        }

        protected IWebElement GetElementInListByText(string css, string textInElement, out int count)
        {
            wait = webDriverWait.Until(x => x.FindElement(By.CssSelector(css)));
            IList<IWebElement> ItemsList = driver.FindElements(By.CssSelector(css));
            count = 1;
            foreach (var c in ItemsList)
            {
                if (c.Text.Equals(textInElement))
                    return c;
                count++;
            }
            return null;
        }

        protected IWebElement GetElementInListByText(string css, string textInElement, string cssNextButton)
        {
            while (true)
            {
                var elem = GetElementInListByText(css, textInElement, out var count);

                if (elem != null)
                    return elem;

                GetElement(cssNextButton).Click();
            }
        }

        protected IWebElement GetElementInListByText(string css, string textInElement)
        {            
            return GetElementInListByText(css, textInElement, out var count);
        }

        protected int GetElementIndexInListByText(string css, string textInElement)
        {
            GetElementInListByText(css, textInElement, out var count);
            return count;
        }

        protected IWebElement GetElementInListByIndex(string css, int index)
        {
            wait = webDriverWait.Until(x => x.FindElement(By.CssSelector(css)));
            IList<IWebElement> elements = driver.FindElements(By.CssSelector(css));
            return elements[index];
        }

        protected void MoveToElement(string css)
        {
            actions = new Actions(sb.driver);
            actions.MoveToElement(GetElement(css)).Build().Perform();          
        }

        protected void ClickOnItemInTable(string cssItemsList, string title, string cssNextButton)
        {
            while (true)
            {
                var elem = GetElementInListByText(cssItemsList, title);

                if (elem != null)
                {
                    elem.Click();
                    break;
                }

                GetElement(cssNextButton).Click();
            }
        }

        protected IWebElement GetItemInTableWithPagination(string cssItemsList, string title, string cssNextButton)
        {
            while (true)
            {
                var elem = GetElementInListByText(cssItemsList, title);

                if (elem != null)
                    return elem;

                GetElement(cssNextButton).Click();
            }
        }

        protected void Check(Action action)
        {
            var frame = new StackFrame(1);
            var methodName = frame.GetMethod().Name.ToString();

            try
            {               
                action();
            }
            catch (Exception ex)
            {     
                TakeScreenshot(methodName);
                throw;
            }
        }
    }

    public class SeleniumTestBase : SeleniumTestBase<SettingUpFixture>
    {
        public SeleniumTestBase(SettingUpFixture sb)
            :base(sb)
        {
        }
    }
}