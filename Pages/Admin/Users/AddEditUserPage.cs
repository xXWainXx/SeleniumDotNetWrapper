using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using GrowthWheel_AutoTests.Configuration;
using GrowthWheel_AutoTests.Pages.CommonElements;
using System.Collections.Generic;
using System.Threading;

namespace GrowthWheel_AutoTests.Pages.Admin.Users
{
    public class AddEditUserPage : SeleniumTestBase
    {
        public AddEditUserPage(SettingUpFixture sb)
            : base(sb)
        { }

        public static string URL = config["basic_url"] + "/users/add";

        private IWebElement _userLanguageDanishCheckbox;
        private IWebElement _userLanguageEnglishCheckbox;
        private IWebElement _userLanguageSpanishCheckbox;
        private IWebElement _userLanguageGermanCheckbox;
        private IWebElement _userLanguageFrenchCheckbox;
        private IWebElement _userLanguageBulgarianCheckbox;
        private IWebElement _userLanguageDutchCheckbox;
        private IWebElement _userLanguageCzechCheckbox;
        private IWebElement _userLanguageNorwegianCheckbox;
        private IWebElement _userLanguagePortugueseCheckbox;
        private IWebElement _userLanguageArabicCheckbox;
        private IWebElement _userLanguageHungarianCheckbox;
        private IWebElement _userLanguageSwedishCheckbox;

        protected string userEmailFieldSelector = "#UserUsername";
        protected string userGroupSelectFieldSelector = "#UserGroupId";
        protected string userStatusSelectFieldSelector = "#UserCurrentState";
        protected string userFirstNameFieldSelector = "#UserFirstName";
        protected string userLastNameFieldSelector = "#UserLastName";
        protected string userLanguageDanishCheckboxSelector = "#UserLanguagesDen";
        protected string userLanguageEnglishCheckboxSelector = "#UserLanguagesEng";
        protected string userLanguageSpanishCheckboxSelector = "#UserLanguagesEsp";
        protected string userLanguageGermanCheckboxSelector = "#UserLanguagesDeu";
        protected string userLanguageFrenchCheckboxSelector = "#UserLanguagesFra";
        protected string userLanguageBulgarianCheckboxSelector = "#UserLanguagesBul";
        protected string userLanguageDutchCheckboxSelector = "#UserLanguagesDut";
        protected string userLanguageCzechCheckboxSelector = "#UserLanguagesCze";
        protected string userLanguageNorwegianCheckboxSelector = "#UserLanguagesNor";
        protected string userLanguagePortugueseCheckboxSelector = "#UserLanguagesPor";
        protected string userLanguageArabicCheckboxSelector = "#UserLanguagesAra";
        protected string userLanguageHungarianCheckboxSelector = "#UserLanguagesHun";
        protected string userLanguageSwedishCheckboxSelector = "#UserLanguagesSwe";
        protected string userOrganizationDropDownFieldSelector = "#UserOrganizationLabel";
        protected string userCountryFieldSelector = "#UserCountry";
        protected string userCityFieldSelector = "#UserCity";
        protected string userStateGlobalFieldSelector = "#UserState";
        protected string userStateUSSelectFieldSelector = "#UserStateUSA";
        protected string userStateCanadaSelectFieldSelector = "#UserStateCA";
        protected string userCertificationCityFieldSelector = "#UserCityOfCertification";
        protected string userWelcomeEmailCheckboxSelector = "#UserEnableWelEmail";
        protected string userSubmitButtonSelector = "input[type='submit']";

        public void SetEmail(string email) => InputValue(userEmailFieldSelector, email);

        public IWebElement GetEmail() => GetElement(userEmailFieldSelector);

        public void SetFirstName(string name) => InputValue(userFirstNameFieldSelector, name);

        public IWebElement GetFirstName() => GetElement(userFirstNameFieldSelector);

        public void SetLastName(string name) => InputValue(userLastNameFieldSelector, name);

        public IWebElement GetLastName() => GetElement(userLastNameFieldSelector);

        public void SetGroup(UserGroup userGroup) => SelectElementByValue(userGroupSelectFieldSelector, ((int)userGroup).ToString());

        public IWebElement GetGroup() => GetElement(userGroupSelectFieldSelector);

        public UserGroup GetGroupValue() => (UserGroup)int.Parse(GetElementAttribute(userGroupSelectFieldSelector, "value"));

        public void SetStatus(UserStatus userStatus) => SelectElementByValue(userStatusSelectFieldSelector, ((int)userStatus).ToString());

        public IWebElement GetStatus() => GetElement(userStatusSelectFieldSelector);

        public UserStatus GetStatusValue() => (UserStatus)int.Parse(GetElementAttribute(userStatusSelectFieldSelector, "value"));

        public void SetCertificationCity(string city) => InputValue(userCertificationCityFieldSelector, city);

        public IWebElement GetCertificationCity() => GetElement(userCertificationCityFieldSelector);

        public IWebElement GetCountry() => GetElement(userCountryFieldSelector);

        public void SetCity(string city) => InputValue(userCityFieldSelector, city);

        public IWebElement GetCity() => GetElement(userCityFieldSelector);

        public void SetLanguage(IList<LanguagesList> languagesLists)
        {
            InitLanguages();

            foreach (var language in languagesLists)
            {
                if (language == LanguagesList.Danish)
                    _userLanguageDanishCheckbox.Click();
                if (language == LanguagesList.English)
                    _userLanguageEnglishCheckbox.Click();
                if (language == LanguagesList.Spanish)
                    _userLanguageSpanishCheckbox.Click();
                if (language == LanguagesList.German)
                    _userLanguageGermanCheckbox.Click();
                if (language == LanguagesList.French)
                    _userLanguageFrenchCheckbox.Click();
                if (language == LanguagesList.Bulgarian)
                    _userLanguageBulgarianCheckbox.Click();
                if (language == LanguagesList.Dutch)
                    _userLanguageDutchCheckbox.Click();
                if (language == LanguagesList.Czech)
                    _userLanguageCzechCheckbox.Click();
                if (language == LanguagesList.Norwegian)
                    _userLanguageNorwegianCheckbox.Click();
                if (language == LanguagesList.Portuguese)
                    _userLanguagePortugueseCheckbox.Click();
                if (language == LanguagesList.Arabic)
                    _userLanguageArabicCheckbox.Click();
                if (language == LanguagesList.Hungarian)
                    _userLanguageHungarianCheckbox.Click();
                if (language == LanguagesList.Swedish)
                    _userLanguageSwedishCheckbox.Click();
            }
        }

        public IList<LanguagesList> GetLanguageValue()
        {
            InitLanguages();
            IList<LanguagesList> SelectedLanguages = new List<LanguagesList>();

            if (_userLanguageEnglishCheckbox.Selected)
                SelectedLanguages.Add(LanguagesList.English);
            if (_userLanguageDanishCheckbox.Selected)
                SelectedLanguages.Add(LanguagesList.Danish);
            if (_userLanguageSpanishCheckbox.Selected)
                SelectedLanguages.Add(LanguagesList.Spanish);
            if (_userLanguageGermanCheckbox.Selected)
                SelectedLanguages.Add(LanguagesList.German);
            if (_userLanguageFrenchCheckbox.Selected)
                SelectedLanguages.Add(LanguagesList.French);
            if (_userLanguageBulgarianCheckbox.Selected)
                SelectedLanguages.Add(LanguagesList.Bulgarian);
            if (_userLanguageDutchCheckbox.Selected)
                SelectedLanguages.Add(LanguagesList.Dutch);
            if (_userLanguageCzechCheckbox.Selected)
                SelectedLanguages.Add(LanguagesList.Czech);
            if (_userLanguageNorwegianCheckbox.Selected)
                SelectedLanguages.Add(LanguagesList.Norwegian);
            if (_userLanguagePortugueseCheckbox.Selected)
                SelectedLanguages.Add(LanguagesList.Portuguese);
            if (_userLanguageArabicCheckbox.Selected)
                SelectedLanguages.Add(LanguagesList.Arabic);
            if (_userLanguageHungarianCheckbox.Selected)
                SelectedLanguages.Add(LanguagesList.Hungarian);
            if (_userLanguageSwedishCheckbox.Selected)
                SelectedLanguages.Add(LanguagesList.Swedish);

            return SelectedLanguages;
        }

        public void SetOrganization(string orgName)
        {
            try
            {
                InputValue(userOrganizationDropDownFieldSelector, orgName);
                GetElement("#ui-id-1 > li:first-child > a").Click();
            }
            catch
            {
                throw new Exception("Organization not found");
            }
        }

        public IWebElement GetOrganization() => GetElement(userOrganizationDropDownFieldSelector);

        public void SetState(string state)
        {
            switch (GetElementValueWhenAppear(userCountryFieldSelector))
            {
                case "United States":
                    SelectElementByValue(userStateUSSelectFieldSelector, state);
                    break;
                case "Canada":
                    SelectElementByValue(userStateCanadaSelectFieldSelector, state);
                    break;
                default:
                    InputValue(userStateGlobalFieldSelector, state);
                    break;
            }
        }

        public IWebElement GetState()
        {
            switch (GetElementAttribute(userCountryFieldSelector, "value"))
            {
                case "United States":
                    return GetElement(userStateUSSelectFieldSelector);
                case "Canada":
                    return GetElement(userStateCanadaSelectFieldSelector);
                default:
                    return GetElement(userStateGlobalFieldSelector);
            }
        }

        public void SetWelcomeEmail(bool value)
        {
            if (!value)
            {
                if (GetElement(userWelcomeEmailCheckboxSelector).Selected)
                {
                    GetElement(userWelcomeEmailCheckboxSelector).Click();
                }
            }
            else if (value)
            {
                if (!GetElement(userWelcomeEmailCheckboxSelector).Selected)
                {
                    GetElement(userWelcomeEmailCheckboxSelector).Click();
                }
            }
        }

        public IWebElement GetWelcomeEmail() => GetElement(userWelcomeEmailCheckboxSelector);

        public UsersPage SubmitForm()
        {
            GetElement(userSubmitButtonSelector).Click();
            return new UsersPage(sb);
        }

        public UsersPage CreateUser(UserGroup userGroup, string email, string firstName, string lastName, IList<LanguagesList> userLanguages, string organization, string city, string state, string certificationCity, UserStatus userStatus = UserStatus.Archived, bool welcomeEmail = false)
        {
            driver.Navigate().GoToUrl(URL);

            if ((userGroup == UserGroup.Advisor || userGroup == UserGroup.Admin || userGroup == UserGroup.RestrictedUser) && userStatus == UserStatus.Inactive)
                userStatus = UserStatus.Archived;

            SetGroup(userGroup);
            SetStatus(userStatus);
            SetEmail(email);
            SetFirstName(firstName);
            SetLastName(lastName);
            SetLanguage(userLanguages);
            SetOrganization(organization);
            SetCity(city);
            SetState(state);
            SetCertificationCity(certificationCity);

            if (userGroup != UserGroup.Admin)
                SetWelcomeEmail(welcomeEmail);

            return SubmitForm();
        }

        public void InitLanguages()
        {
            wait = webDriverWait.Until(x => x.FindElement(By.CssSelector(userLanguageDanishCheckboxSelector)));
            wait = webDriverWait.Until(x => x.FindElement(By.CssSelector(userLanguageEnglishCheckboxSelector)));
            wait = webDriverWait.Until(x => x.FindElement(By.CssSelector(userLanguageSpanishCheckboxSelector)));
            wait = webDriverWait.Until(x => x.FindElement(By.CssSelector(userLanguageGermanCheckboxSelector)));
            wait = webDriverWait.Until(x => x.FindElement(By.CssSelector(userLanguageFrenchCheckboxSelector)));
            wait = webDriverWait.Until(x => x.FindElement(By.CssSelector(userLanguageBulgarianCheckboxSelector)));
            wait = webDriverWait.Until(x => x.FindElement(By.CssSelector(userLanguageDutchCheckboxSelector)));
            wait = webDriverWait.Until(x => x.FindElement(By.CssSelector(userLanguageCzechCheckboxSelector)));
            wait = webDriverWait.Until(x => x.FindElement(By.CssSelector(userLanguageNorwegianCheckboxSelector)));
            wait = webDriverWait.Until(x => x.FindElement(By.CssSelector(userLanguagePortugueseCheckboxSelector)));
            wait = webDriverWait.Until(x => x.FindElement(By.CssSelector(userLanguageArabicCheckboxSelector)));
            wait = webDriverWait.Until(x => x.FindElement(By.CssSelector(userLanguageHungarianCheckboxSelector)));
            wait = webDriverWait.Until(x => x.FindElement(By.CssSelector(userLanguageSwedishCheckboxSelector)));

            _userLanguageDanishCheckbox = driver.FindElement(By.CssSelector(userLanguageDanishCheckboxSelector));
            _userLanguageEnglishCheckbox = driver.FindElement(By.CssSelector(userLanguageEnglishCheckboxSelector));
            _userLanguageSpanishCheckbox = driver.FindElement(By.CssSelector(userLanguageSpanishCheckboxSelector));
            _userLanguageGermanCheckbox = driver.FindElement(By.CssSelector(userLanguageGermanCheckboxSelector));
            _userLanguageFrenchCheckbox = driver.FindElement(By.CssSelector(userLanguageFrenchCheckboxSelector));
            _userLanguageBulgarianCheckbox = driver.FindElement(By.CssSelector(userLanguageBulgarianCheckboxSelector));
            _userLanguageDutchCheckbox = driver.FindElement(By.CssSelector(userLanguageDutchCheckboxSelector));
            _userLanguageCzechCheckbox = driver.FindElement(By.CssSelector(userLanguageCzechCheckboxSelector));
            _userLanguageNorwegianCheckbox = driver.FindElement(By.CssSelector(userLanguageNorwegianCheckboxSelector));
            _userLanguagePortugueseCheckbox = driver.FindElement(By.CssSelector(userLanguagePortugueseCheckboxSelector));
            _userLanguageArabicCheckbox = driver.FindElement(By.CssSelector(userLanguageArabicCheckboxSelector));
            _userLanguageHungarianCheckbox = driver.FindElement(By.CssSelector(userLanguageHungarianCheckboxSelector));
            _userLanguageSwedishCheckbox = driver.FindElement(By.CssSelector(userLanguageSwedishCheckboxSelector));
        }

    }

    public enum UserGroup
    {
        Admin = 3,
        Advisor = 4,
        RestrictedUser = 6,
        Mentor = 9,
        Client = 15,
        Null
    }

    public enum UserStatus
    {
        Active = 1,
        Archived = 0,
        Inactive = 3,
        Null
    }

    

}
