using System;
using OpenQA.Selenium;
using GrowthWheel_AutoTests.Configuration;

namespace GrowthWheel_AutoTests.Pages.Admin.Organizations
{
    public class OrganizationsPage : SeleniumTestBase
    {
        public OrganizationsPage(SettingUpFixture sb)
            :base(sb)
        { }

        public static string URL = config["basic_url"] + "/organizations";

        protected string addOrganizationButtonSelector = "a[href='/organizations/add']";
        protected string organizationNameSearchFieldSelector = "#OrganizationName";
        protected string submitSearchButtonSelector = "#filterTableMenuSubmit";
        protected string deleteButtonSelector = "table tbody tr:nth-child(2) .deleteOrg";
        protected string deleteConfirmationButtonSelector = "#organizationModal input[type='submit']";

        public AddEditOrganizationPage GoToAddOrganizationPage(SettingUpFixture sb)
        {
            GetElement(addOrganizationButtonSelector).Click();
            return new AddEditOrganizationPage(sb);
        }

        public void DeleteOrganization(string name)
        {
            driver.Navigate().GoToUrl(URL);

            InputValue(organizationNameSearchFieldSelector, name);
            GetElement(submitSearchButtonSelector).Click();
            GetElement(deleteButtonSelector).Click();
            GetElement(deleteConfirmationButtonSelector).Click();
        }
    }
}
