using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Microsoft.Extensions.Configuration;
using GrowthWheel_AutoTests.Pages.Initialization;
using OpenQA.Selenium.Interactions;

namespace GrowthWheel_AutoTests.Configuration
{
    public class SettingUpFixture : IDisposable
    {
        public IWebDriver driver;

        protected IConfiguration config;

        //run before all tests in the one class
        public SettingUpFixture()
        {
            RemoteWebDriver driver = DriverFactory.Instance.Create();
            this.driver = driver;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();

            config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional:true, reloadOnChange:true)
                .Build();

            driver.Manage().Cookies.DeleteAllCookies();
        }

        //run after all tests in the one class
        public void Dispose()
        {
            driver.Manage().Cookies.DeleteAllCookies();
            driver.Quit();
        } 
    }
}