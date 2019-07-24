using System;
using System.Collections.Generic;
using System.Text;
using GrowthWheel_AutoTests.Configuration;
using GrowthWheel_AutoTests.Pages.Advisor.MyClients.BusinessProfile;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace GrowthWheel_AutoTests.Pages.Advisor.MyClients
{
    class ClientsListPage : SeleniumTestBase
    {
        public ClientsListPage(SettingUpFixture sb)
            : base(sb)
        { }

        public static string URL = config["basic_global_url"] + "/clients";

        protected string listButtonSelector = "#list";
        protected string analyticsButtonSelector = "a[href='/analytics/clients']";
        protected string addClientCompanyButtonSelector = ".list-btn.add-item";
        protected string companyTitleListSelector = ".company_name";
        protected string demoCompanyTitleSelector = "a[href='/clients/787']";
        protected string nextButtonSelector = "a[rel='next']";
        protected string prevButtonSelector = "a[rel='prev']";

        public ContactPage GoToClientCompanyPage(string companyName)
        {
            ClickOnItemInTable(companyTitleListSelector, companyName, nextButtonSelector);
            return new ContactPage(sb);
        }
    }
}
