using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using GrowthWheel_AutoTests.Configuration;
using GrowthWheel_AutoTests.Pages.CommonElements;
using GrowthWheel_AutoTests.Pages.Admin;
using GrowthWheel_AutoTests.Pages.Admin.Organizations;
using GrowthWheel_AutoTests.Pages.Admin.Users;
using GrowthWheel_AutoTests.Pages.Advisor.Banners;
using System.Collections.Generic;
using GrowthWheel_AutoTests.Pages.Advisor;

namespace GrowthWheel_AutoTests.Pages.Initialization
{
    class SampleData : SeleniumTestBase
    {
        private LoginPage lp;
        private OrganizationsPage op;
        private UsersPage up;
        private AddEditOrganizationPage addOrgPage;
        private AddEditUserPage addUserPage;
        private AdminHeader adminHeader;
        private AdvisorHeader advisorHeader;
        private TermsOfServicePage termsPopup;
        private PrivacyPolicyBanner policyBanner;

        public SampleData(SettingUpFixture sb)
            : base(sb)
        {
            lp = new LoginPage(sb);
            op = new OrganizationsPage(sb);
            up = new UsersPage(sb);
            addOrgPage = new AddEditOrganizationPage(sb);
            addUserPage = new AddEditUserPage(sb);
            adminHeader = new AdminHeader(sb);
            advisorHeader = new AdvisorHeader(sb);
            termsPopup = new TermsOfServicePage(sb);
            policyBanner = new PrivacyPolicyBanner(sb);
        }

        public void InitSampleData()
        {
            driver.Navigate().GoToUrl(LoginPage.URL);
            lp.LoginAsAdmin();

            op = addOrgPage.CreateNetworkOrganization(config["simple_network_org_name"], "France", "Paris", "Paris State", OrganizationStatus.Active);
            op = addOrgPage.CreateAdvisorOrganization(config["simple_advisor_org_name"], config["simple_network_org_name"], "France", "Paris", "Paris State", OrganizationStatus.Active);
            up = addUserPage.CreateUser(UserGroup.Advisor, config["simple_advisor_email"], "Alex 1", "AutoTest Advisor 1", new List<LanguagesList> { LanguagesList.English, LanguagesList.French }, config["simple_advisor_org_name"], "Paris", "Paris State", "Kyiv", UserStatus.Active, false);
            up.ChangeUserPassword(config["simple_advisor_email"], config["simple_advisor_password"]);
            up = addUserPage.CreateUser(UserGroup.Advisor, config["simple_advisor_2_email"], "Alex 2", "AutoTest Advisor 2", new List<LanguagesList> { LanguagesList.English, LanguagesList.French }, config["simple_advisor_org_name"], "Paris", "Paris State", "Kyiv", UserStatus.Active, false);
            up.ChangeUserPassword(config["simple_advisor_2_email"], config["simple_advisor_2_password"]);
            op = addOrgPage.CreateClientOrganization(config["simple_client_org_name"], config["simple_advisor_email"], LanguagesList.English, "France", "Paris", "Paris State", OrganizationStatus.Active);
            up = addUserPage.CreateUser(UserGroup.Client, config["simple_client_email"], "Alex 1", "AutoTest Client 1", new List<LanguagesList> { LanguagesList.English, LanguagesList.French }, config["simple_client_org_name"], "Paris", "Paris State", "Kyiv", UserStatus.Active, false);
            up.ChangeUserPassword(config["simple_client_email"], config["simple_client_password"]);
            up = addUserPage.CreateUser(UserGroup.Client, config["simple_client_2_email"], "Alex 2", "AutoTest Client 2", new List<LanguagesList> { LanguagesList.English, LanguagesList.French }, config["simple_client_org_name"], "Paris", "Paris State", "Kyiv", UserStatus.Active, false);
            up.ChangeUserPassword(config["simple_client_2_email"], config["simple_client_2_password"]);

            adminHeader.Logout();

            lp.LoginAsFirstAdvisor();
            termsPopup.AcceptTerms();
            policyBanner.AcceptPrivacyPolicy();
            advisorHeader.Logout();

            lp.LoginAsSecondAdvisor();
            termsPopup.AcceptTerms();
            policyBanner.AcceptPrivacyPolicy();
            advisorHeader.Logout();
        }

        public void DeleteSampleData()
        {
            driver.Navigate().GoToUrl(LoginPage.URL);
            lp.LoginAsAdmin();

            up.DeleteUser(config["simple_client_email"]);
            up.DeleteUser(config["simple_client_2_email"]);
            up.DeleteUser(config["simple_advisor_email"]);
            up.DeleteUser(config["simple_advisor_2_email"]);

            op.DeleteOrganization(config["simple_client_org_name"]);
            op.DeleteOrganization(config["simple_advisor_org_name"]);
            op.DeleteOrganization(config["simple_network_org_name"]);

            adminHeader.Logout();
        }
    }
}
