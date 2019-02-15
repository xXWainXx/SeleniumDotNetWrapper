using System;
using OpenQA.Selenium;
using GrowthWheel_AutoTests.Configuration;

namespace GrowthWheel_AutoTests.Pages.Advisor.Banners
{
    class TermsOfServicePage : SeleniumTestBase
    {
        public TermsOfServicePage(SettingUpFixture sb)
            :base(sb)
        { }

        protected string modalWindow = "#dialogForm";
        protected string acceptButton = "#dialogForm button[type='submit']";
        protected string declineButton = "#dialogForm button[type='button']";

        public void AcceptTerms()
        {
            if (GetElement(acceptButton) != null)
                GetElement(acceptButton).Click();
        }

        public void DeclineTerms()
        {
            if (GetElement(declineButton) != null)
                GetElement(declineButton).Click();
        }
    }
}
