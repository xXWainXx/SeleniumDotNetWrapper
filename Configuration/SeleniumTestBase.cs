using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using GrowthWheel_AutoTests.Configuration;
using OpenQA.Selenium.Support.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace GrowthWheel_AutoTests.Tests
{
    public class SeleniumTestBase<TFixture> : IDisposable, IClassFixture<TFixture>
        where TFixture: SettingUpFixture
    {
        protected IWebDriver driver;
        protected IJavaScriptExecutor js;
        protected IConfiguration config; 
        protected TFixture sb;

        //running before each test
        public SeleniumTestBase(TFixture sb)
        {
            this.driver = sb.driver;
            this.sb = sb;
        }

        public void TakeScreenshot()
        {
            driver.TakeScreenshot()
                .SaveAsFile(config["screenshots_path"].ToString() + "Screenshot" + "_" + DateTime.Now.ToString("dd_MMMM_hh_mm_ss_tt"), ScreenshotImageFormat.Png);
        }

        //running after each test
        public void Dispose()
        {
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