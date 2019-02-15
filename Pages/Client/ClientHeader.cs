using System;
using System.Collections.Generic;
using System.Text;
using GrowthWheel_AutoTests.Configuration;

namespace GrowthWheel_AutoTests.Pages.Client
{
    class ClientHeader : SeleniumTestBase
    {
        public ClientHeader(SettingUpFixture sb)
            :base(sb)
        { }

        //main header
        protected string userMenuSelector = ".usermenu";
        protected string myProfileSettingsButtonSelector = ".usermenu-popup a[href='/users/myprofile']";
        protected string logoutButtonSelector = "a[href='/users/logout']";
        protected string businessProfileButtonSelector = "a[href='/business-profile/contact']";
        protected string decisionsAndActionsButtonSelector = "a[href='/day-plan']";
        protected string scoreboardButtonSelector = "a[href='/scoreboard/ambitions']";
        protected string advisorRelationButtonSelector = "a[href='/n/interactions']";
        protected string videoButtonSelector = "a[href='/video']";

        //business profile header
        protected string businessContactButtonSelector = "#submenu a[href$='/contact']";
        protected string businessBusinessButtonSelector = "#submenu a[href$='/business']";
        protected string businessCustomerReferencesButtonSelector = "#submenu a[href$='markets-and-customers']";
        protected string businessTeamButtonSelector = "#submenu a[href$='team']";
        protected string businessPartnersButtonSelector = "#submenu a[href$='partners']";
        protected string businessLegalButtonSelector = "#submenu a[href$='legal']";
        protected string businessFinancialsButtonSelector = "#submenu a[href$='financials']";

        //decisions and actions header
        protected string decisionsAndActionsDaysPlanButtonSelector = "#submenu a[href$='day-plan']";
        protected string decisionsAndActionsScreeningsButtonSelector = "#submenu a[href$='screenings']";
        protected string decisionsAndActionsAdvisorFilesButtonSelector = "#submenu a[href$='shared-tools']";
        protected string decisionsAndActionsCompanyFilesButtonSelector = "#submenu a[href$='files']";

        //scoreboard header
        protected string scoreboardAmbitionsButtonSelector = "#submenu a[href$='ambitions']";
        protected string scoreboardOutcomesButtonSelector = "#submenu a[href$='outcomes']";

        //advisor relation header
        protected string advisorRelationInteractionsButtonSelector = "#submenu a[href$='interactions']";

        //main header
        public void ClickBusinessProfileButton() => GetElement(businessProfileButtonSelector).Click();

        public void ClickDecisionsAndActionsButton() => GetElement(decisionsAndActionsButtonSelector).Click();

        public void ClickScoreboardButton() => GetElement(scoreboardButtonSelector).Click();

        public void ClickAdvisorRelationButton() => GetElement(advisorRelationButtonSelector).Click();

        public void ClickVideoButton() => GetElement(videoButtonSelector).Click();

        public LoginPage Logout()
        {
            MoveToElement(userMenuSelector);
            GetElement(logoutButtonSelector).Click();
            return new LoginPage(sb);
        }

        public void ClickMyProfileSettingsButton()
        {
            MoveToElement(userMenuSelector);
            GetElement(logoutButtonSelector).Click();
        }

        //business profile header
        public void ClickBusinessProfileContactButton() => GetElement(businessContactButtonSelector).Click();

        public void ClickBusinessProfileBusinessButton() => GetElement(businessBusinessButtonSelector).Click();

        public void ClickBusinessProfileCustomerReferencesButton() => GetElement(businessCustomerReferencesButtonSelector).Click();

        public void ClickBusinessProfileTeamButton() => GetElement(businessTeamButtonSelector).Click();

        public void ClickBusinessProfilePartnersButton() => GetElement(businessPartnersButtonSelector).Click();

        public void ClickBusinessProfileLegalButton() => GetElement(businessLegalButtonSelector).Click();

        public void ClickBusinessProfileFinancialsButton() => GetElement(businessFinancialsButtonSelector).Click();

        //decisions and actions header
        public void ClickDecisionsAndActionsDaysPlanButton() => GetElement(decisionsAndActionsDaysPlanButtonSelector).Click();

        public void ClickDecisionsAndActionsScreeningsButton() => GetElement(decisionsAndActionsScreeningsButtonSelector).Click();

        public void ClickDecisionsAndActionsAdvisorFilesButton() => GetElement(decisionsAndActionsAdvisorFilesButtonSelector).Click();

        public void ClickDecisionsAndActionsCompanyFilesButton() => GetElement(decisionsAndActionsCompanyFilesButtonSelector).Click();

        //scoreboard header
        public void ClickScoreboardAmbitionsButton() => GetElement(scoreboardAmbitionsButtonSelector).Click();

        public void ClickScoreboardOutcomesButton() => GetElement(scoreboardOutcomesButtonSelector).Click();

        //advisor relation header
        public void ClickAdvisorRelationInteractionsButton() => GetElement(advisorRelationInteractionsButtonSelector).Click();
    }
}
