using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using GrowthWheel_AutoTests.Configuration;
using OpenQA.Selenium.Support.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace GrowthWheel_AutoTests.Configuration
{
    public class SettingUpFixture : IDisposable
    {
        public IWebDriver driver;
        public IJavaScriptExecutor js;
        protected IConfiguration config;

        //run before all tests in the one class
        public SettingUpFixture()
        {
            RemoteWebDriver driver = DriverFactory.Instance.Create();
            this.driver = driver;
            driver.Manage().Window.Maximize();

            config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional:true, reloadOnChange:true)
                .Build();

            js = (IJavaScriptExecutor)driver;
        }

        //run after all tests in the one class
        public void Dispose()
        {
            driver.Manage().Cookies.DeleteAllCookies();
            driver.Quit();
        } 
    }
}