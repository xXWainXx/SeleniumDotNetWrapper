using System;
using OpenQA.Selenium;
using GrowthWheel_AutoTests.Configuration;

namespace GrowthWheel_AutoTests.Pages.Advisor.MyClients.ClientRelation
{
    class InteractionsPage : SeleniumTestBase
    {
        public InteractionsPage(SettingUpFixture sb)
            :base(sb)
        { }

        protected string addPastInteractionButtonSelector = "#create_new_past_interaction";
        protected string addFutureInteractionButtonSelector = "create_new_future_interaction";
        protected string interactionTypeListSelector = "tbody span.type";
        protected string interactionTitleListSelector = "tbody .title a";
        protected string interactionStatusListSelector = "tbody .status";
        protected string interactionDateListSelector = "tbody .date";
        protected string interactionDurationListSelector = "tbody .duration";
        protected string interactionCreatedByListSelector = "tbody .created_by *";
        protected string interactionActionsListSelector = "tbody .actions";
        protected string downloadInteractionButtonListSelector = "tbody .actions a.download";
        protected string deleteInteractionButtonListSelector = "tbody .actions a.delete";
        protected string confirmDeleteButtonSelector = "button.submit.yes";
        protected string nextButtonSelector = "a[rel='next']";
        protected string prevButtonSelector = "a[rel='prev']";
        protected string deleteModalWindowSelector = ".confirmDialog.modal.fade";

        public IWebElement GetType(string interactionTitle)
        {
            var elementIndex = GetElementIndexInListByText(interactionTitleListSelector, interactionTitle);
            return GetElementInListByIndex(interactionTypeListSelector, elementIndex);
        }

        public IWebElement GetTitle(string interactionTitle)
        {
            return GetElementInListByText(interactionTitleListSelector, interactionTitle);
        }

        public IWebElement GetStatus(string interactionTitle)
        {
            var elementIndex = GetElementIndexInListByText(interactionTitleListSelector, interactionTitle);
            return GetElementInListByIndex(interactionStatusListSelector, elementIndex);
        }

        public IWebElement GetDate(string interactionTitle)
        {
            var elementIndex = GetElementIndexInListByText(interactionTitleListSelector, interactionTitle);
            return GetElementInListByIndex(interactionDateListSelector, elementIndex);
        }

        public IWebElement GetDuration(string interactionTitle)
        {
            var elementIndex = GetElementIndexInListByText(interactionTitleListSelector, interactionTitle);
            return GetElementInListByIndex(interactionDurationListSelector, elementIndex);
        }

        public IWebElement GetCreatedBy(string interactionTitle)
        {
            var elementIndex = GetElementIndexInListByText(interactionTitleListSelector, interactionTitle);
            return GetElementInListByIndex(interactionCreatedByListSelector, elementIndex);
        }

        public AddEditInteractionPage ClickOnInteractionTitle(string interactionTitle)
        {
            ClickOnItemInTable(interactionTitleListSelector, interactionTitle, nextButtonSelector);
            return new AddEditInteractionPage(sb);
        }

        public AddEditInteractionPage ClickCreatePastInteractionButton()
        {
            GetElement(addPastInteractionButtonSelector).Click();
            return new AddEditInteractionPage(sb);
        }

        public AddEditInteractionPage ClickCreateFutureInteractionButton()
        {
            GetElement(addPastInteractionButtonSelector).Click();
            return new AddEditInteractionPage(sb);
        }

        public void DownloadInteraction(string interactionTitle)
        {
            var elementIndex = GetElementIndexInListByText(interactionTitleListSelector, interactionTitle);
            GetElementInListByIndex(downloadInteractionButtonListSelector, elementIndex).Click();
        }

        public bool DeleteInteraction(string interactionTitle)
        {
            var elementIndex = GetElementIndexInListByText(interactionTitleListSelector, interactionTitle);
            string css = $"tbody tr:nth-child({elementIndex}) a.delete";
            var element = GetElement(css);

            if (element.Displayed)
            {
                MoveToElement(css);
                element.Click();
                GetElement(deleteModalWindowSelector);
                GetElement(confirmDeleteButtonSelector).Click();
                return true;
            }
            else
            {
                return false;
            }
           
        }

        public InteractionsPage GoToInteractionsPage(string clientCompanyTitle)
        {
            AdvisorHeader header = new AdvisorHeader(sb);
            ClientsListPage clientsListPage = new ClientsListPage(sb);
            header.ClickMyClientsButton();
            clientsListPage.GoToClientCompanyPage(clientCompanyTitle);
            header.ClickClientRelationButton();
            header.ClickClientRelationInteractionsButton();
            return new InteractionsPage(sb);
        }
    }
}
