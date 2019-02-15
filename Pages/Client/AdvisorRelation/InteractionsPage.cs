using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using GrowthWheel_AutoTests.Configuration;
using GrowthWheel_AutoTests.Pages.CommonElements;
using System.Collections.Generic;
using System.Threading;

namespace GrowthWheel_AutoTests.Pages.Client.AdvisorRelation
{
    class InteractionsPage : SeleniumTestBase
    {
        public InteractionsPage(SettingUpFixture sb)
            : base(sb)
        { }

        protected string interactionTypeListSelector = "tbody span.type";
        protected string interactionTitleListSelector = "tbody .title a";
        protected string interactionStatusListSelector = "tbody .status";
        protected string interactionDateListSelector = "tbody .date";
        protected string interactionDurationListSelector = "tbody .duration";
        protected string interactionCreatedByListSelector = "tbody .created_by *";
        protected string interactionActionsListSelector = "tbody .actions";
        protected string downloadInteractionButtonListSelector = "tbody .actions a.download";
        protected string nextButtonSelector = "a[rel='next']";
        protected string prevButtonSelector = "a[rel='prev']";

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

        public ViewInteractionPage ClickOnInteractionTitle(string interactionTitle)
        {
            ClickOnItemInTable(interactionTitleListSelector, interactionTitle, nextButtonSelector);
            return new ViewInteractionPage(sb);
        }

        public void DownloadInteraction(string interactionTitle)
        {
            var elementIndex = GetElementIndexInListByText(interactionTitleListSelector, interactionTitle);
            GetElementInListByIndex(downloadInteractionButtonListSelector, elementIndex).Click();
        }

        public InteractionsPage GoToInteractionsPage()
        {
            ClientHeader header = new ClientHeader(sb);
            header.ClickAdvisorRelationButton();
            header.ClickAdvisorRelationInteractionsButton();
            return new InteractionsPage(sb);
        }
    }
}
