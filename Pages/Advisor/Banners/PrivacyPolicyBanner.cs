using System;
using OpenQA.Selenium;
using GrowthWheel_AutoTests.Configuration;

namespace GrowthWheel_AutoTests.Pages.Advisor.Banners
{
    class PrivacyPolicyBanner : SeleniumTestBase
    {
        public PrivacyPolicyBanner(SettingUpFixture sb)
            :base(sb)
        { }

        protected string acceptButton = ".cookie-bar a[actn='hidePrivacyBar']";

        public void AcceptPrivacyPolicy()
        {
            if (GetElement(acceptButton) != null)
                GetElement(acceptButton).Click();
        }
    }
}
