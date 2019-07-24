using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using GrowthWheel_AutoTests.Configuration;
using GrowthWheel_AutoTests.Pages.CommonElements;
using System.Collections.Generic;
using System.Threading;

namespace GrowthWheel_AutoTests.Pages.Client.AdvisorRelation
{
    class ViewInteractionPage : SeleniumTestBase
    {
        public ViewInteractionPage(SettingUpFixture sb)
            :base(sb)
        { }

        protected string interactionTitleFieldSelector = "span.title";
        protected string interactionTypeFieldSelector = "#categoryheadline li div:last-child";
        protected string interactionDateFieldSelector = "span.date";
        protected string interactionStartTimeFieldSelector = "#durationTime .start_time";
        protected string interactionEndTimeFIeldSelector = "#durationTime .end_time";
        protected string interactionPreparationTimeFieldSelector = "#durationTime .preparation_time";
        protected string interactionTransportationTimeFieldSelector = "#durationTime .transportation_time";
        protected string interactionTotalDurationTimeFieldSelector = "#durationTime .duration-time";
        protected string interactionLocationFieldSelector = "#location span";
        protected string interactionConferenceCallInfoFieldSelector = "#conference-call-info span";
        protected string interactionPurposeFieldSelector = "#purpose span";

        public IWebElement GetTitle() => GetElement(interactionTitleFieldSelector);
        public IWebElement GetInteractionType() => GetElement(interactionTypeFieldSelector);
        public InteractionType GetInteractionTypeValue() => (InteractionType)Enum.Parse(typeof(InteractionType), GetElementAttribute(interactionTypeFieldSelector, "value"));
        public IWebElement GetDate() => GetElement(interactionDateFieldSelector);
        public IWebElement GetStartTime() => GetElement(interactionStartTimeFieldSelector);
        public IWebElement GetEndTime() => GetElement(interactionEndTimeFIeldSelector);
        public IWebElement GetPreparationTime() => GetElement(interactionPreparationTimeFieldSelector);
        public IWebElement GetTransportationTime() => GetElement(interactionTransportationTimeFieldSelector);
        public IWebElement GetTotalDurationTime() => GetElement(interactionTotalDurationTimeFieldSelector);
        public IWebElement GetLocation() => GetElement(interactionLocationFieldSelector);
        public IWebElement GetConferenceCallInfo() => GetElement(interactionConferenceCallInfoFieldSelector);
        public IWebElement GetPurpose() => GetElement(interactionPurposeFieldSelector);

    }
}
