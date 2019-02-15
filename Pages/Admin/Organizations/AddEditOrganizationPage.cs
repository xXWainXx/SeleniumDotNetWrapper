using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using GrowthWheel_AutoTests.Configuration;
using GrowthWheel_AutoTests.Pages.CommonElements;

namespace GrowthWheel_AutoTests.Pages.Admin.Organizations
{
    public class AddEditOrganizationPage : SeleniumTestBase
    {
        public AddEditOrganizationPage(SettingUpFixture sb)
            : base(sb)
        { }

        public static string URL = config["basic_url"] + "/organizations/add";

        protected string organizationNameFieldSelector = "#OrganizationName";
        protected string organizationTypeSelectFieldSelector = "#OrganizationTypeId";
        protected string organizationNetworkOrganizationSelectFieldSelector = "#OrganizationParentOrganizationId";
        protected string organizationAdvisorOrganizationDropDownFieldSelector = "#OrganizationParentAdvisorName";
        protected string organizationStatusSelectFieldSelector = "#OrganizationCurrentState";
        protected string organizationLogoUploadFieldSelector = "#OrganizationBrand";
        protected string organizationLanguageFieldSelector = "#OrganizationLanguage";
        protected string organizationCountryFieldSelector = "#OrganizationCountry";
        protected string organizationStateFieldGlobalSelector = "#OrganizationState";
        protected string organizationStateFieldUSSelector = "#OrganizationStateUSA";
        protected string organizationStateFieldCanadaSelector = "#OrganizationStateCA";
        protected string organizationCityFieldSelector = "#OrganizationCity";
        protected string organizationTypeValueSelector = "#organization-type-switcher";
        protected string organizationSubmitButtonSelector = "input[type='submit']";

        public void SetName(string name) => InputValue(organizationNameFieldSelector, name);

        public IWebElement GetName() => GetElement(organizationNameFieldSelector);

        public void SetOrganizationType(OrganizationType type) => SelectElementByValue(organizationTypeSelectFieldSelector, ((int)type).ToString());

        public IWebElement GetOrganizationType() => GetElement(organizationTypeSelectFieldSelector);

        public OrganizationType GetOrganizationTypeValue() => (OrganizationType)int.Parse(GetElementAttribute(organizationTypeSelectFieldSelector, "value"));        

        public void SetNetworkOrganization(string networkOrg) => SelectElementByText(organizationNetworkOrganizationSelectFieldSelector, networkOrg);

        public IWebElement GetNetworkOrganization() => GetElement(organizationNetworkOrganizationSelectFieldSelector);

        public void SetAdvisorOrganization(string advOrg)
        {
            InputValue(organizationAdvisorOrganizationDropDownFieldSelector, advOrg);
            GetElement("#ui-id-1 > li:first-child > a").Click();
        }

        public IWebElement GetAdvisorOrganization() => GetElement(organizationAdvisorOrganizationDropDownFieldSelector);

        public void SetLanguage(LanguagesList language) => SelectElementByText(organizationLanguageFieldSelector, language.ToString());

        public IWebElement GetLanguage() => GetElement(organizationLanguageFieldSelector);

        public LanguagesList GetLanguageValue() => (LanguagesList)int.Parse(GetElementAttribute(organizationLanguageFieldSelector, "value"));

        public void SetCountry(string country)
        {
            InputValue(organizationCountryFieldSelector, country);
            GetElement("#ui-id-2 > li:first-child > a").Click();
        }

        public IWebElement GetCountry() => GetElement(organizationCountryFieldSelector);

        public void SetCity(string city) => InputValue(organizationCityFieldSelector, city);

        public IWebElement GetCity() => GetElement(organizationCityFieldSelector);

        public void SetState(string state)
        {
            switch (GetElementValueWhenAppear(organizationCountryFieldSelector))
            {
                case "United States":
                    SelectElementByValue(organizationStateFieldUSSelector, state);
                    break;
                case "Canada":
                    SelectElementByValue(organizationStateFieldCanadaSelector, state);
                    break;
                default:
                    InputValue(organizationStateFieldGlobalSelector, state);
                    break;
            }
        }

        public IWebElement GetState()
        {
            switch (GetElementAttribute(organizationCountryFieldSelector, "value"))
            {
                case "United States":
                    return GetElement(organizationStateFieldUSSelector);
                case "Canada":
                    return GetElement(organizationStateFieldCanadaSelector);
                default:
                    return GetElement(organizationStateFieldGlobalSelector);
            }
        }

        public void SetStatus(OrganizationStatus status) => SelectElementByValue(organizationStatusSelectFieldSelector, ((int)status).ToString());

        public IWebElement GetStatus() => GetElement(organizationStatusSelectFieldSelector);

        public OrganizationStatus GetStatusValue() => (OrganizationStatus)int.Parse(GetElementAttribute(organizationStatusSelectFieldSelector, "value"));

        public OrganizationsPage SubmitForm()
        {
            GetElement(organizationSubmitButtonSelector).Click();
            return new OrganizationsPage(sb);
        }

        public OrganizationsPage CreateNetworkOrganization (string name, string country, string city, string state, OrganizationStatus organizationStatus = OrganizationStatus.Inactive)
        {
            driver.Navigate().GoToUrl(URL);
            SetName(name);
            SetOrganizationType(OrganizationType.Network);
            SetCountry(country);
            SetCity(city);
            SetState(state);
            SetStatus(organizationStatus);
            return SubmitForm();
        }

        public OrganizationsPage CreateAdvisorOrganization(string name, string networkOrganization, string country, string city, string state, OrganizationStatus organizationStatus = OrganizationStatus.Inactive)
        {
            driver.Navigate().GoToUrl(URL);
            SetName(name);
            SetOrganizationType(OrganizationType.Advisor);
            SetNetworkOrganization(networkOrganization);
            SetCountry(country);
            SetCity(city);
            SetState(state);
            SetStatus(organizationStatus);
            return SubmitForm();
        }

        public OrganizationsPage CreateClientOrganization(string name, string advisorEmail, LanguagesList language, string country, string city, string state, OrganizationStatus organizationStatus = OrganizationStatus.Inactive)
        {
            driver.Navigate().GoToUrl(URL);
            SetName(name);
            SetOrganizationType(OrganizationType.Client);
            SetAdvisorOrganization(advisorEmail);
            SetLanguage(language);
            SetCountry(country);
            SetCity(city);
            SetState(state);
            SetStatus(organizationStatus);
            return SubmitForm();
        }

    }

    public enum OrganizationType
    {
        Network = 1,
        Advisor = 2,
        Client = 3,
        Empty
    }

    public enum OrganizationStatus
    {
        Active = 1,
        Inactive = 3,
        Archived = 0,
        Null
    }
}
