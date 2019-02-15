using System;
using System.Collections.Generic;
using System.Text;
using GrowthWheel_AutoTests.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;

namespace GrowthWheel_AutoTests.Pages.Admin
{
    public class AdminHeader : SeleniumTestBase
    {
        public AdminHeader(SettingUpFixture sb)
            : base(sb)
        { }

        protected string logOutButtonSelector = "a[href='/users/logout']";

        public void Logout()
        {
            GetElement(logOutButtonSelector).Click();
        }
    }
}
