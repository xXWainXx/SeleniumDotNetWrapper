using System;
using Xunit;
using GrowthWheel_AutoTests.Configuration;
using GrowthWheel_AutoTests.Pages.Guest;
using GrowthWheel_AutoTests.Pages.Advisor.MyToolbox;
using System.Threading;
using GrowthWheel_AutoTests.Pages.Advisor;
using OpenQA.Selenium;

namespace GrowthWheel_AutoTests.Tests.Advisor.MyToolbox.PersonalTools
{
    class UploadToolsTests : SeleniumTestBase, IDisposable
    {
        public UploadToolsTests(SettingUpFixture sb)
            : base(sb)
        {
            loginPage = new LoginPage(sb);
            advisorHeader = new AdvisorHeader(sb);
            personalToolsPage = new PersonalToolsPage(sb);
            random = new Random();
        }

        public new void Dispose()
        {
            driver.Manage().Cookies.DeleteAllCookies();
        }

        LoginPage loginPage;
        AdvisorHeader advisorHeader;
        PersonalToolsPage personalToolsPage;
        Random random;

        [Fact]
        
    }
}
