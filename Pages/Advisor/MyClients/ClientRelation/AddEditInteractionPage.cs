using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using GrowthWheel_AutoTests.Configuration;
using GrowthWheel_AutoTests.Pages.CommonElements;
using System.Collections.Generic;
using System.Threading;

namespace GrowthWheel_AutoTests.Pages.Advisor.MyClients.ClientRelation
{
    class AddEditInteractionPage : SeleniumTestBase
    {
        public AddEditInteractionPage(SettingUpFixture sb)
            :base(sb)
        { }

        protected string interactionTitleFieldSelector = "#title";
        protected string interactionDateFiledSelector = "#date";
        protected string interactionStartTimeFieldSelector = "#start_time";
        protected string interactionEndTimeFieldSelector = "#end_time";
        protected string interactionPreparationFieldSelector = "#preparation_time";
        protected string interactionTransportationFieldSelector = "#transportation_time";
        protected string interactionTotalDurationTimeFieldSelector = "#total-duration-time";
        protected string interactionLocationFieldSelector = "#location";
        protected string interactionConferenceCallInfoFieldSelector = "#conference-call-info";
        protected string interactionPurposeFieldSelector = "#purpose";
        protected string interactionSaveDraftButtonSelector = "button[name='save']";
        protected string interactionSaveAndShareButtonSelector = "button[name='save_share']";
        protected string interactionCancelButtonSelector = "#UserInteractionForm div.fr a";
        protected string interactionPopUpSaveAndShareButtonSelector = "#dialogForm button.send";

        public void SetTitle(string title) => InputValue(interactionTitleFieldSelector, title);
        public IWebElement GetTitle() => GetElement(interactionTitleFieldSelector);

        public void SetDate(string date) => InputValue(interactionDateFiledSelector, date);
        public IWebElement GetDate() => GetElement(interactionDateFiledSelector);

        public void SetStartTime(string startTime) => InputValue(interactionStartTimeFieldSelector, startTime);
        public IWebElement GetStartTime() => GetElement(interactionStartTimeFieldSelector);

        public void SetEndTime(string endTime) => InputValue(interactionEndTimeFieldSelector, endTime);
        public IWebElement GetEndTime() => GetElement(interactionEndTimeFieldSelector);

        public void SetPreparationTime(string prepTime) => InputValue(interactionPreparationFieldSelector, prepTime);
        public IWebElement GetPreparationTime() => GetElement(interactionPreparationFieldSelector);

        public void SetTransportationTime(string transportTime) => InputValue(interactionTransportationFieldSelector, transportTime);
        public IWebElement GetTransportationTime() => GetElement(interactionTransportationFieldSelector);

        public IWebElement GetTotalDurationTime() => GetElement(interactionTotalDurationTimeFieldSelector);

        public void SetLocation(string location) => InputValue(interactionLocationFieldSelector, location);
        public IWebElement GetLocation() => GetElement(interactionLocationFieldSelector);

        public void SetConferenceCallInfo(string ccinfo) => InputValue(interactionConferenceCallInfoFieldSelector, ccinfo);
        public IWebElement GetConferenceCallInfo() => GetElement(interactionConferenceCallInfoFieldSelector);

        public void SetPurpose(string purpose) => InputValue(interactionPurposeFieldSelector, purpose);
        public IWebElement GetPurpose() => GetElement(interactionPurposeFieldSelector);

        public void SetType(InteractionType type)
        {
            string selector = "a[data-value='" + (int)type + "']";
            GetElement(selector).Click();
        }

        public InteractionType GetTypeValue()
        {
            string code = "return document.getElementById('type_id').getAttribute('value')";
            string value = (string)js.ExecuteScript(code);
            return (InteractionType)int.Parse(value);
        }

        public InteractionsPage ClickSaveDraftButton()
        {
            GetElement(interactionSaveDraftButtonSelector).Click();
            return new InteractionsPage(sb);
        }

        public void ClickSaveAndShareButton() => GetElement(interactionSaveAndShareButtonSelector).Click();

        public InteractionsPage ClickCancelButton()
        {
            GetElement(interactionCancelButtonSelector).Click();
            return new InteractionsPage(sb);
        }

        public InteractionsPage ShareInteraction()
        {
            GetElement(interactionSaveAndShareButtonSelector).Click();
            GetElement(interactionPopUpSaveAndShareButtonSelector).Click();
            return new InteractionsPage(sb);
        }

        public InteractionsPage CreateAndShareInteraction(string title, InteractionType type, string date, string startTime, string endTime = "", string preparationTime = "", string transportationTime = "", string location = "", string confCallInfo = "", string purpose = "")
        {
            SetTitle(title);
            SetType(type);
            SetDate(date);
            SetStartTime(startTime);
            SetEndTime(endTime);
            SetPreparationTime(preparationTime);
            SetTransportationTime(transportationTime);
            SetLocation(location);
            SetConferenceCallInfo(confCallInfo);
            SetPurpose(purpose);
            return ShareInteraction();
        }

        public InteractionsPage CreateAndSaveAsDraftInteraction(string title, InteractionType type, string date, string startTime, string endTime = "", string preparationTime = "", string transportationTime = "", string location = "", string confCallInfo = "", string purpose = "")
        {
            SetTitle(title);
            SetType(type);
            SetDate(date);
            SetStartTime(startTime);
            SetEndTime(endTime);
            SetPreparationTime(preparationTime);
            SetTransportationTime(transportationTime);
            SetLocation(location);
            SetConferenceCallInfo(confCallInfo);
            SetPurpose(purpose);
            return ClickSaveDraftButton();
        }
    }


}
