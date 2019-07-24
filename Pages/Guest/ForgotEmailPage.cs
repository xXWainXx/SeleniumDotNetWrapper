using GrowthWheel_AutoTests.Configuration;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrowthWheel_AutoTests.Pages.Guest
{
    public class ForgotEmailPage : SeleniumTestBase
    {
        public ForgotEmailPage(SettingUpFixture sb)
            : base(sb)
        {

        }

        public static string URL = config["basic_global_url"] + "/users/forgotusername";

        public string SupportText { get; } = "Your username to GrowthWheel Online is by default your email address";

        protected string supportTextSelector = "#loginform span:first-child";

        public IWebElement GetSupportText() => GetElement(supportTextSelector);
    }
}
