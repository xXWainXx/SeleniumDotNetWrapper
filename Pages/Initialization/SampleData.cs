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
using GrowthWheel_AutoTests.Pages.Guest;

namespace GrowthWheel_AutoTests.Pages.Initialization
{
    public class SampleData : SeleniumTestBase
    {
        private LoginPage loginPage;
        private OrganizationsPage organizationsPage;
        private UsersPage usersPage;
        private AddEditOrganizationPage addEditOrganizationPage;
        private AddEditUserPage addEditUserPage;
        private AdminHeader adminHeader;
        private AdvisorHeader advisorHeader;
        private TermsOfServicePage termsPopup;
        private PrivacyPolicyBanner policyBanner;

        public SampleData(SettingUpFixture sb)
            : base(sb)
        {
            loginPage = new LoginPage(sb);
            organizationsPage = new OrganizationsPage(sb);
            usersPage = new UsersPage(sb);
            addEditOrganizationPage = new AddEditOrganizationPage(sb);
            addEditUserPage = new AddEditUserPage(sb);
            adminHeader = new AdminHeader(sb);
            advisorHeader = new AdvisorHeader(sb);
            termsPopup = new TermsOfServicePage(sb);
            policyBanner = new PrivacyPolicyBanner(sb);
        }

        public void InitSampleData()
        {
            driver.Navigate().GoToUrl(LoginPage.URL);
            loginPage.LoginAsAdmin();

            addEditOrganizationPage.CreateNetworkOrganization(config["simple_network_org_name"], "France", "Paris", "Paris State", OrganizationStatus.Active);

            addEditOrganizationPage.CreateAdvisorOrganization(config["simple_advisor_org_name"], config["simple_network_org_name"], "France", "Paris", "Paris State", OrganizationStatus.Active);
            organizationsPage.IncreaseMentorLicenses(config["simple_advisor_org_name"], "5");

            addEditUserPage.CreateUser(UserGroup.Advisor, config["simple_advisor_email"], "Alex 1", "AutoTest Advisor 1", new List<LanguagesList> { LanguagesList.English, LanguagesList.French }, config["simple_advisor_org_name"], "Paris", "Paris State", "Kyiv", UserStatus.Active, false);
            usersPage.ChangeUserPassword(config["simple_advisor_email"], config["simple_advisor_password"]);

            addEditUserPage.CreateUser(UserGroup.Advisor, config["simple_advisor_2_email"], "Alex 2", "AutoTest Advisor 2", new List<LanguagesList> { LanguagesList.English, LanguagesList.French }, config["simple_advisor_org_name"], "Paris", "Paris State", "Kyiv", UserStatus.Active, false);
            usersPage.ChangeUserPassword(config["simple_advisor_2_email"], config["simple_advisor_2_password"]);

            addEditOrganizationPage.CreateClientOrganization(config["simple_client_org_name"], config["simple_advisor_email"], LanguagesList.English, "France", "Paris", "Paris State", OrganizationStatus.Active);

            addEditUserPage.CreateUser(UserGroup.Client, config["simple_client_email"], "Alex 1", "AutoTest Client 1", new List<LanguagesList> { LanguagesList.English, LanguagesList.French }, config["simple_client_org_name"], "Paris", "Paris State", "Kyiv", UserStatus.Active, false);
            usersPage.ChangeUserPassword(config["simple_client_email"], config["simple_client_password"]);

            addEditUserPage.CreateUser(UserGroup.Client, config["simple_client_2_email"], "Alex 2", "AutoTest Client 2", new List<LanguagesList> { LanguagesList.English, LanguagesList.French }, config["simple_client_org_name"], "Paris", "Paris State", "Kyiv", UserStatus.Active, false);
            usersPage.ChangeUserPassword(config["simple_client_2_email"], config["simple_client_2_password"]);

            addEditUserPage.CreateUser(UserGroup.Client, "", config["client_first_name_for_advisor_tests"], config["client_last_name_for_advisor_tests"], new List<LanguagesList> { LanguagesList.English, LanguagesList.French }, config["simple_client_org_name"], "Paris", "Paris State", "Kyiv", UserStatus.Inactive, false);

            addEditUserPage.CreateUser(UserGroup.Mentor, config["simple_mentor_email"], "Alex 1", "AutoTest Mentor 1", new List<LanguagesList> { LanguagesList.English, LanguagesList.French }, config["simple_advisor_org_name"], "Paris", "Paris State", "Kyiv", UserStatus.Active, false);
            usersPage.ChangeUserPassword(config["simple_mentor_email"], config["simple_mentor_password"]);

            addEditUserPage.CreateUser(UserGroup.RestrictedUser, config["simple_restricted_user_email"], "Alex 1", "AutoTest Restricted 1", new List<LanguagesList> { LanguagesList.English, LanguagesList.French }, config["simple_advisor_org_name"], "Paris", "Paris State", "Kyiv", UserStatus.Active, false);
            usersPage.ChangeUserPassword(config["simple_restricted_user_email"], config["simple_restricted_user_password"]);

            adminHeader.Logout();
        }

        public void AcceptTermsPopUpForAllSampleUsers()
        {
            driver.Navigate().GoToUrl(LoginPage.URL);

            loginPage.LoginAsFirstAdvisor();
            termsPopup.AcceptTerms();
            policyBanner.AcceptPrivacyPolicy();
            advisorHeader.Logout();

            loginPage.LoginAsSecondAdvisor();
            termsPopup.AcceptTerms();
            policyBanner.AcceptPrivacyPolicy();
            advisorHeader.Logout();

            loginPage.LoginAsRestrictedUser();
            termsPopup.AcceptTerms();
            policyBanner.AcceptPrivacyPolicy();
            advisorHeader.Logout();

            //need to add for mentors
            
        }

        public void DeleteSampleData()
        {
            driver.Navigate().GoToUrl(LoginPage.URL);
            loginPage.LoginAsAdmin();

            usersPage.DeleteUsersByOrganizationName(config["simple_client_org_name"]);
            usersPage.DeleteUser(config["simple_advisor_email"]);
            usersPage.DeleteUser(config["simple_advisor_2_email"]);
            usersPage.DeleteUser(config["simple_mentor_email"]);
            usersPage.DeleteUser(config["simple_restricted_user_email"]);

            organizationsPage.DeleteOrganization(config["simple_client_org_name"]);
            organizationsPage.DeleteOrganization(config["simple_advisor_org_name"]);
            organizationsPage.DeleteOrganization(config["simple_network_org_name"]);

            adminHeader.Logout();
        }
    }
}
