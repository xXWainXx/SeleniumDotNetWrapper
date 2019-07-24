using System;
using System.Threading;
using Xunit;
using GrowthWheel_AutoTests.Configuration;
using GrowthWheel_AutoTests.Pages;
using GrowthWheel_AutoTests.Pages.Advisor;
using GrowthWheel_AutoTests.Pages.Advisor.Banners;
using OpenQA.Selenium.Support.PageObjects;
using GrowthWheel_AutoTests.Pages.Admin;
using GrowthWheel_AutoTests.Pages.Admin.Organizations;
using GrowthWheel_AutoTests.Pages.Admin.Users;
using GrowthWheel_AutoTests.Pages.CommonElements;
using GrowthWheel_AutoTests.Pages.Initialization;
using GrowthWheel_AutoTests.Pages.Advisor.MyClients;
using GrowthWheel_AutoTests.Pages.Advisor.MyClients.BusinessProfile;
using GrowthWheel_AutoTests.Pages.Advisor.MyClients.ClientRelation;
using System.Collections.Generic;
using GrowthWheel_AutoTests.Pages.Guest;

namespace GrowthWheel_AutoTests.Tests
{
    public class UnitTest1 : SeleniumTestBase
    {
        public UnitTest1(SettingUpFixture sb)
            : base(sb)
        {
            loginPage = new LoginPage(sb);
            interactionsPage = new InteractionsPage(sb);
            addEditInteractionPage = new AddEditInteractionPage(sb);
            clientsListPage = new ClientsListPage(sb);
            advisorHeader = new AdvisorHeader(sb);
        }

        LoginPage loginPage;
        InteractionsPage interactionsPage;
        AddEditInteractionPage addEditInteractionPage;
        ClientsListPage clientsListPage;
        AdvisorHeader advisorHeader;

        [Fact]
        public void Test1()
        {
           
        }
    }
}
