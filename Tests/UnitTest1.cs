using System;
using System.Threading;
using Xunit;
using GrowthWheel_AutoTests.Configuration;
using GrowthWheel_AutoTests.Pages;
using GrowthWheel_AutoTests.Pages.Advisor;
using GrowthWheel_AutoTests.Pages.Advisor.Banners;
using OpenQA.Selenium.Support.PageObjects;
using GrowthWheel_AutoTests.Pages.Admin;
using GrowthWheel_AutoTests.Pages.Admin.Organizations;
using GrowthWheel_AutoTests.Pages.Admin.Users;
using GrowthWheel_AutoTests.Pages.CommonElements;
using GrowthWheel_AutoTests.Pages.Initialization;
using GrowthWheel_AutoTests.Pages.Advisor.MyClients;
using GrowthWheel_AutoTests.Pages.Advisor.MyClients.BusinessProfile;
using GrowthWheel_AutoTests.Pages.Advisor.MyClients.ClientRelation;
using System.Collections.Generic;

namespace GrowthWheel_AutoTests.Tests
{
    public class UnitTest1 : SeleniumTestBase
    {
        public UnitTest1(SettingUpFixture sb)
            : base(sb)
        {
        }

        [Fact]
        public void Test1()
        {
            LoginPage lp = new LoginPage(sb);
            UsersPage up = new UsersPage(sb);
            AddEditOrganizationPage addOrganizationPage = new AddEditOrganizationPage(sb);
            AddEditUserPage addUserPage = new AddEditUserPage(sb);
            OrganizationsPage op = new OrganizationsPage(sb);
            SampleData sd = new SampleData(sb);
            AdvisorHeader header = new AdvisorHeader(sb);
            ClientsListPage clp = new ClientsListPage(sb);
            ContactPage cp = new ContactPage(sb);
            InteractionsPage ip = new InteractionsPage(sb);
            AddEditInteractionPage createInteractionPage = new AddEditInteractionPage(sb);
            TermsOfServicePage termsOfServicePage = new TermsOfServicePage(sb);
            PrivacyPolicyBanner privacyPolicyBanner = new PrivacyPolicyBanner(sb);

            driver.Navigate().GoToUrl(LoginPage.URL);
            lp.LoginAsFirstAdvisor();
            header.Logout();

        }

    }
}
