using System;
using System.Collections.Generic;
using System.Text;
using GrowthWheel_AutoTests.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using GrowthWheel_AutoTests.Pages.Guest;

namespace GrowthWheel_AutoTests.Pages.Advisor
{
    public class AdvisorHeader : SeleniumTestBase
    {
        public AdvisorHeader(SettingUpFixture sb)
            : base(sb)
        {
        }

        //main header
        protected string myProfileSettingsButtonSelector = ".usermenu-popup a[href='/users/myprofile']";
        protected string adminDashboardButtonSelector = "a[href='/user_new_statistics']";
        protected string logoutButtonSelector = "a[href='/users/logout']";
        protected string goToHomepageButtonSelector = "a[href = '/categories/showall']"; 
        protected string myToolboxButtonSelector = "#my_toolbox";
        protected string myClientsButtonSelector = "#my_clients";
        protected string myNetworkButtonSelector = "a[id^='my_network']";
        protected string searchFieldSelector = "input[name=q]";
        protected string searchButtonSelector = ".searchSubmit";
        protected string toolsBasketMenuSelector = "#toolbasket";
        protected string userMenuSelector = ".usermenu";

        //header under client company
        protected string listButtonSelector = "#list";
        protected string businessProfileButtonSelector = "div.user-profile-tab-bar a:nth-child(2)";
        protected string decisionsAndActionsButtonSelector = ".user-profile-tab-bar a[href*='day-plan']";
        protected string scoreboardButtonSelector = ".user-profile-tab-bar a[href*='scoreboard']";
        protected string clientRelationButtonSelector = ".user-profile-tab-bar a[href*='relationship']";

        //business profile header
        protected string businessContactButtonSelector = "#submenu a:first-child";
        protected string businessBusinessButtonSelector = "#submenu a:nth-child(2)";
        protected string businessCustomerReferencesButtonSelector = "#submenu a[href*='markets-and-customers']";
        protected string businessTeamButtonSelector = "#submenu a[href*='team']";
        protected string businessPartnersButtonSelector = "#submenu a[href*='partners']";
        protected string businessLegalButtonSelector = "#submenu a[href*='legal']";
        protected string businessFinancialsButtonSelector = "#submenu a[href*='financials']";

        //decisions and actions header
        protected string decisionsAndActionsDaysPlanButtonSelector = "#submenu a[href*='day-plan']";
        protected string decisionsAndActionsScreeningsButtonSelector = "#submenu a[href*='screenings']";
        protected string decisionsAndActionsAdvisorFilesButtonSelector = "#submenu a[href*='shared-tools']";
        protected string decisionsAndActionsClientFilesButtonSelector = "#submenu a[href*='files']";
        protected string decisionsAndActionsAnalyticsButtonSelector = "#submenu a[href*='dashboard']";


        //scoreboard header
        protected string scoreboardAmbitionsButtonSelector = "#submenu a[href*='ambitions']";
        protected string scoreboardOutcomesButtonSelector = "#submenu a:nth-child(2)";
        protected string scoreboardAnalyticsButtonSelector = "#submenu a:nth-child(3)";

        //client relation header
        protected string clientRelationEngagementButtonSelector = "#submenu a[href*='relationship']";
        protected string clientRelationDemographicsButtonSelector = "#submenu a[href*='demographics']";
        protected string clientRelationInteractionsButtonSelector = "#submenu a:nth-child(3)";
        protected string clientRelationMailboxButtonSelector = "#submenu a[href*='postbox']";
        protected string clientRelationAnalyticsButtonSelector = "#submenu a:nth-child(5)";


        //main header
        public void ClickMyToolboxButton() => GetElement(myToolboxButtonSelector).Click();

        public void ClickMyClientsButton() => GetElement(myClientsButtonSelector).Click();

        public void ClickMyNetworkButton() => GetElement(myNetworkButtonSelector).Click();

        public void ClickHomepageButton() => GetElement(goToHomepageButtonSelector).Click();

        public void ClickAdminDashboardButton()
        {
            MoveToElement(userMenuSelector);
            GetElement(adminDashboardButtonSelector).Click();
        }

        public void ClickMyProfileSettingsButton()
        {
            MoveToElement(userMenuSelector);
            GetElement(myProfileSettingsButtonSelector).Click();
        }

        public LoginPage Logout()
        {
            MoveToElement(userMenuSelector);
            GetElement(logoutButtonSelector).Click();
            return new LoginPage(sb);
        }

        //header under client company
        public void ClickListButton() => GetElement(listButtonSelector).Click();

        public void ClickBusinessProfileButton() => GetElement(businessProfileButtonSelector).Click();

        public void ClickDecisionsAndActionsButton() => GetElement(decisionsAndActionsButtonSelector).Click();

        public void ClickScoreboardButton() => GetElement(scoreboardButtonSelector).Click();

        public void ClickClientRelationButton() => GetElement(clientRelationButtonSelector).Click();

        //business profile header
        public void ClickBusinessProfileContactButton() => GetElement(businessContactButtonSelector).Click();

        public void ClickBusinessProfileBusinessButton() => GetElement(businessBusinessButtonSelector).Click();

        public void ClickBusinessProfileCustomerReferencesButton() => GetElement(businessCustomerReferencesButtonSelector).Click();

        public IWebElement GetBusinessProfileTeamButton() => GetElement(businessTeamButtonSelector);
        public void ClickBusinessProfileTeamButton() => GetElement(businessTeamButtonSelector).Click();

        public void ClickBusinessProfilePartnersButton() => GetElement(businessPartnersButtonSelector).Click();

        public void ClickBusinessProfileLegalButton() => GetElement(businessLegalButtonSelector).Click();

        public void ClickBusinessProfileFinancialsButton() => GetElement(businessFinancialsButtonSelector).Click();

        //decisions and actions header
        public void ClickDecisionsAndActionsDaysPlanButton() => GetElement(decisionsAndActionsDaysPlanButtonSelector).Click();

        public void ClickDecisionsAndActionsScreeningsButton() => GetElement(decisionsAndActionsScreeningsButtonSelector).Click();

        public void ClickDecisionsAndActionsAdvisorFilesButton() => GetElement(decisionsAndActionsAdvisorFilesButtonSelector).Click();

        public void ClickDecisionsAndActionsClientFilesButton() => GetElement(decisionsAndActionsClientFilesButtonSelector).Click();

        public void ClickDecisionsAndActionsAnalyticsButton() => GetElement(decisionsAndActionsAnalyticsButtonSelector).Click();

        //scoreboard header
        public void ClickScoreboardAmbitionsButton() => GetElement(scoreboardAmbitionsButtonSelector).Click();

        public void ClickScoreboardOutcomesButton() => GetElement(scoreboardOutcomesButtonSelector).Click();

        public void ClickScoreboardAnalyticsButton() => GetElement(scoreboardAnalyticsButtonSelector).Click();

        //client relation header
        public void ClickClientRelationEngagementButton() => GetElement(clientRelationEngagementButtonSelector).Click();

        public void ClickClientRelationDemographicsButton() => GetElement(clientRelationDemographicsButtonSelector).Click();

        public void ClickClientRelationInteractionsButton() => GetElement(clientRelationInteractionsButtonSelector).Click();

        public void ClickClientRelationMailboxButton() => GetElement(clientRelationMailboxButtonSelector).Click();

        public void ClickClientRelationAnalyticsButton() => GetElement(clientRelationAnalyticsButtonSelector).Click();
    }
}
