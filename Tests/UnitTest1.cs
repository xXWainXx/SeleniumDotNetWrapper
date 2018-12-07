using System;
using System.Threading;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using GrowthWheel_AutoTests.Configuration;

namespace GrowthWheel_AutoTests.Tests
{
    public class UnitTest1 : SeleniumTestBase
    {    
        public UnitTest1(SettingUpFixture sb)
            :base(sb)
        {            
        }

        [Fact]
        public void Test1()
        {
            driver.Navigate().GoToUrl("https://google.com");
            Thread.Sleep(5000);
        }

        [Fact]
        public void Test2()
        {
            driver.Navigate().GoToUrl("https://google.com");
            Thread.Sleep(5000);
        }
    }
}
