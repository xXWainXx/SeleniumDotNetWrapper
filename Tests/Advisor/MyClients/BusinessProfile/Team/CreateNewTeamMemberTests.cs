using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using GrowthWheel_AutoTests.Configuration;
using GrowthWheel_AutoTests.Pages.Guest;
using GrowthWheel_AutoTests.Pages.Advisor.MyClients.BusinessProfile;
using System.Threading;
using GrowthWheel_AutoTests.Pages.Advisor;
using OpenQA.Selenium;

namespace GrowthWheel_AutoTests.Tests.Advisor.MyClients.BusinessProfile.Team
{
    public class CreateNewTeamMemberTests : SeleniumTestBase, IDisposable
    {
        public CreateNewTeamMemberTests(SettingUpFixture sb)
            : base(sb)
        {
            loginPage = new LoginPage(sb);
            teamPage = new TeamPage(sb);
            advisorHeader = new AdvisorHeader(sb);
            mailRepository = new MailRepository();
            random = new Random();
            fullClientNameForTests = config["client_first_name_for_advisor_tests"] + " " + config["client_last_name_for_advisor_tests"];

            driver.Navigate().GoToUrl(LoginPage.URL);
            loginPage.LoginAsFirstAdvisor();
            teamPage.GoToTeamPage(config["simple_client_org_name"]);
        }

        public new void Dispose()
        {
            driver.Manage().Cookies.DeleteAllCookies();
        }

        LoginPage loginPage;
        TeamPage teamPage;
        AdvisorHeader advisorHeader;
        MailRepository mailRepository;
        Random random;
        string fullClientNameForTests;
        (string email, string password, string linkToLogin) infoFromInviteEmail;

        //team page
        [Fact]
        public void IsTeamTabHasActiveState() => 
            Check(() => Assert.Contains("active", GetElementAttribute(advisorHeader.GetBusinessProfileTeamButton(), "class")));
        

        //add team member pop-up
        [Fact]
        public void IsAddTeamMemberButtonDisplayed() => 
            Check(() => Assert.True(teamPage.GetAddTeamMemberButtonInTable().Displayed));

        [Fact]
        public void IsAddTeamMemberPopupDisplayed()
        {
            Check(() =>
            {
                teamPage.ClickAddTeamMemberButtonInTable();
                Assert.True(teamPage.GetAddTeamMemberModal().Displayed);
            });
        }

        [Fact]
        public void IsFirstNameFieldDisplayedInAddTeamMemberPopup()
        {
            Check(() =>
            {
                teamPage.ClickAddTeamMemberButtonInTable();
                Assert.True(teamPage.GetFirstNameField().Displayed);
            });
        }

        [Fact]
        public void IsLastNameFieldDisplayedInAddTeamMemberPopup()
        {
            Check(() =>
            {
                teamPage.ClickAddTeamMemberButtonInTable();
                Assert.True(teamPage.GetLastNameField().Displayed);
            });
        }

        [Fact]
        public void IsCancelButtonDisplayedInAddTeamMemberPopup()
        {
            Check(() =>
            {
                teamPage.ClickAddTeamMemberButtonInTable();
                Assert.True(teamPage.GetCancelButtonInAddTeamMemberPopup().Displayed);
            });
        }

        [Fact]
        public void IsSubmitButtonDisplayedInAddTeamMemberPopup()
        {
            Check(() =>
            {
                teamPage.ClickAddTeamMemberButtonInTable();
                Assert.True(teamPage.GetSubmitButtonInAddTeamMemberPopup().Displayed);
            });
        }

        [Fact]
        public void IsCompanyNameDisplayedInTitleOfAddTeamMemberPopup()
        {
            Check(() =>
            {
                teamPage.ClickAddTeamMemberButtonInTable();
                Assert.Equal(config["simple_client_org_name"], teamPage.GetCompanyNameInAddTeamMemberModalTitle());
            });
        }

        [Fact]
        public void IsCancelButtonCloseAddTeamMemberPopup()
        {
            Check(() =>
            {
                teamPage.ClickAddTeamMemberButtonInTable();
                teamPage.ClickCancelButtonInAddTeamMemberForm();
                Thread.Sleep(1000);
                Assert.False(teamPage.GetAddTeamMemberModal().Displayed);
            });
        }

        [Fact]
        public void IsFirstNameFieldHasRequiredAttribute() => Check(() =>
            Assert.NotNull(GetElementAttribute(teamPage.GetFirstNameField(), "required")));

        [Fact]
        public void IsLastNameFieldHasRequiredAttribute() => Check(() =>
            Assert.NotNull(GetElementAttribute(teamPage.GetLastNameField(), "required")));

        [Theory]
        [MemberData(nameof(ValidationErrorsTestDataForAddTeamMemberPopup))]
        public void CheckValidationErrorsInAddTeamMemberPopup(string firstName, string lastName)
        {
            Check(() =>
            {
                teamPage.ClickAddTeamMemberButtonInTable();
                teamPage.RemoveRequiredAttributesFromFieldsInAddTeamMemberPopup();
                teamPage.SetFirstName(firstName);
                teamPage.SetLastName(lastName);
                teamPage.SubmitAddTeamMemberForm();
                Assert.True(teamPage.GetAddTeamMemberModal().Displayed);           
            });
        }

        //invite team member pop-up

        [Fact]
        public void IsInviteTeamMemberPopupDisplayedWhenUserSubmitAddTeamMemberPopupWithValidData()
        {
            Check(() =>
            {
                teamPage.ClickAddTeamMemberButtonInTable();
                teamPage.SetFirstName(config["client_2_first_name_for_advisor_tests"]);
                teamPage.SetLastName(config["client_2_last_name_for_advisor_tests"]);
                teamPage.SubmitAddTeamMemberForm();
                Assert.True(teamPage.GetEmailFieldInInvitePopup().Displayed);
                Assert.False(teamPage.GetAddTeamMemberModal().Displayed);
            });
        }

        [Fact]
        public void IsInviteTeamMemberPopupDisplayedWhenUserClickInviteButton()
        {
            Check(() =>
            {
                teamPage.ClickInviteButtonInTable(fullClientNameForTests);
                Assert.True(teamPage.GetEmailFieldInInvitePopup().Displayed);
            });
        }

        [Fact]
        public void IsNameFieldDisplayedInInvitePopup()
        {
            Check(() =>
            {
                teamPage.ClickInviteButtonInTable(fullClientNameForTests);
                Assert.True(teamPage.GetFullNameFieldInInvitePopup().Displayed);
            });
        }

        [Fact]
        public void IsEmailFieldDisplayedInInvitePopup()
        {
            Check(() =>
            {
                teamPage.ClickInviteButtonInTable(fullClientNameForTests);
                Assert.True(teamPage.GetEmailFieldInInvitePopup().Displayed);
            });
        }

        [Fact]
        public void IsSubjectFieldDisplayedInInvitePopup()
        {
            Check(() =>
            {
                teamPage.ClickInviteButtonInTable(fullClientNameForTests);
                Assert.True(teamPage.GetSubjectFieldInInvitePopup().Displayed);
            });
        }

        [Fact]
        public void IsDescriptionFieldDisplayedInInvitePopup()
        {
            Check(() =>
            {
                teamPage.ClickInviteButtonInTable(fullClientNameForTests);
                Assert.True(teamPage.GetDescriptionFieldInInvitePopup().Displayed);
            });
        }

        [Fact]
        public void IsSignatureFieldDisplayedInInvitePopup()
        {
            Check(() =>
            {
                teamPage.ClickInviteButtonInTable(fullClientNameForTests);
                Assert.True(teamPage.GetSignatureSectionInInvitePopup().Displayed);
            });
        }

        [Fact]
        public void IsSkipButtonDisplayedInInvitePopup()
        {
            Check(() =>
            {
                teamPage.ClickInviteButtonInTable(fullClientNameForTests);
                Assert.True(teamPage.GetSkipButtonInInvitePopup().Displayed);
            });
        }

        [Fact]
        public void IsInviteButtonDisplayedInInvitePopup()
        {
            Check(() =>
            {
                teamPage.ClickInviteButtonInTable(fullClientNameForTests);
                Assert.True(teamPage.GetInviteButtonInInvitePopup().Displayed);
            });
        }

        [Fact]
        public void IsCrossButtonDisplayedInInvitePopup()
        {
            Check(() =>
            {
                teamPage.ClickInviteButtonInTable(fullClientNameForTests);
                Assert.True(teamPage.GetCrossButtonInInvitePopup().Displayed);
            });
        }

        [Fact]
        public void IsNameFieldPrefilledInInvitePopup()
        {
            Check(() =>
            {
                teamPage.ClickInviteButtonInTable(fullClientNameForTests);
                Assert.Equal(fullClientNameForTests, teamPage.GetFullNameFieldInInvitePopup().Text);
            });
        }

        [Fact]
        public void IsSubjectFieldPrefilledInInvitePopup()
        {
            Check(() =>
            {
                teamPage.ClickInviteButtonInTable(fullClientNameForTests);
                Assert.False(String.IsNullOrWhiteSpace(GetElementAttribute(teamPage.GetSubjectFieldInInvitePopup(), "value").ToString()));

            });
        }

        [Fact]
        public void IsDescriptionFieldPrefilledInInvitePopup()
        {
            Check(() =>
            {
                teamPage.ClickInviteButtonInTable(fullClientNameForTests);
                Assert.NotEqual(0, teamPage.GetElementsInDescriptionFieldInInvitePopup().Count);
            });
        }

        [Theory]
        [MemberData(nameof(ValidationErrorsTestDataForInvitePopup))]
        public void CheckDisabledInviteButtonInInvitePopup(string email, string subject, string description, string errorMessage)
        {          
            Check(() =>
            {
                teamPage.ClickInviteButtonInTable(fullClientNameForTests);
                teamPage.SetEmailInInvitePopup(email);
                teamPage.SetSubjectInInvitePopup(subject);
                teamPage.SetDescriptionInInvitePopup(description);
                teamPage.GetFullNameFieldInInvitePopup().Click();
                Assert.False(teamPage.GetInviteButtonInInvitePopup().Enabled);
            });          
        }

        [Theory]
        [MemberData(nameof(ValidationErrorsTestDataForInvitePopup))]
        public void CheckValidationErrorsInInvitePopup(string email, string subject, string description, string errorMessage)
        {
            Check(() =>
            {
                teamPage.ClickInviteButtonInTable(fullClientNameForTests);

                teamPage.SetEmailInInvitePopup(email);
                teamPage.SetSubjectInInvitePopup(subject);
                teamPage.SetDescriptionInInvitePopup(description);
                teamPage.GetFullNameFieldInInvitePopup().Click();

                IWebElement errorElement = driver.FindElement(By.XPath("//div[@class='error-message' and contains(text(), '" + errorMessage + "' )]"));
                Assert.True(errorElement.Displayed);
            });
        }

        [Theory]
        [MemberData(nameof(InvalidEmailAddresses))]
        public void CheckValidationsInEmailField(string email)
        {
            Check(() =>
            {
                teamPage.ClickInviteButtonInTable(fullClientNameForTests);

                teamPage.SetEmailInInvitePopup(email);
                teamPage.GetFullNameFieldInInvitePopup().Click();               

                Assert.False(teamPage.GetInviteButtonInInvitePopup().Enabled);

                IWebElement errorElement = driver.FindElement(By.XPath("//div[@class='error-message' and contains(text(), '" + teamPage.ErrorMessageInInvalidEmail + "' )]"));
                Assert.True(errorElement.Displayed);
            });
        }

        [Fact]
        public void IsInviteButtonTurnsToActiveWhenUserInputValieEmail()
        {
            Check(() =>
            {
                teamPage.ClickInviteButtonInTable(fullClientNameForTests);

                teamPage.SetEmailInInvitePopup(config["client_email_for_advisor_tests"]);
                Assert.True(teamPage.GetInviteButtonInInvitePopup().Enabled);
            });
        }

        [Fact]
        public void IsClientCteatedPopupDisplayedWhenAdvisorInviteTeamMember()
        {
            Check(() =>
            {           
                string firstName = "Alex" + random.Next(1, 1000);
                string lastName = "Autotest" + random.Next(1, 1000);
                string email = config["basic_email_prefix"] + "+" + random.Next(1,10000) + config["basic_email_domain"];

                teamPage.CreateNotInviteTeamMember(firstName, lastName);
                teamPage.ClickInviteButtonInTable(firstName + " " + lastName);
                teamPage.SetEmailInInvitePopup(email);
                teamPage.SubmitInviteForm();

                Assert.Contains(teamPage.MessageInInvitationSendPopup, teamPage.GetTextInClientCreatedPopup());

                var unreadEmails = mailRepository.WaitAndGetUnreadEmails();
            });
        }

        [Fact]
        public void IsTeamMemberAppearsInTheTableAfterCreate()
        {
            Check(() =>
            {
                string firstName = "Alex" + random.Next(1, 1000);
                string lastName = "Autotest" + random.Next(1, 1000);

                teamPage.CreateNotInviteTeamMember(firstName, lastName);

                Assert.True(teamPage.GetTeamMemberNameFieldInTable(firstName + " " + lastName).Displayed);
            });
        }

        [Fact]
        public void IsInviteButtonNotDisplayedNearInvitedClient()
        {
            Check(() =>
            {
                string firstName = "Alex" + random.Next(1, 1000);
                string lastName = "Autotest" + random.Next(1, 1000);
                string email = config["basic_email_prefix"] + "+" + random.Next(1, 10000) + config["basic_email_domain"];

                teamPage.CreateAndInviteTeamMember(firstName, lastName, email);
                var unreadEmails = mailRepository.WaitAndGetUnreadEmails();

                Assert.Null(teamPage.GetInviteButtonInTable(firstName + " " + lastName));
            });
        }

        [Fact]
        public void IsEmailAddressDisplayedInInvitedUser()
        {
            Check(() =>
            {
                string firstName = "Alex" + random.Next(1, 1000);
                string lastName = "Autotest" + random.Next(1, 1000);
                string email = config["basic_email_prefix"] + "+" + random.Next(1, 10000) + config["basic_email_domain"];

                teamPage.CreateAndInviteTeamMember(firstName, lastName, email);
                var unreadEmails = mailRepository.WaitAndGetUnreadEmails();

                Assert.Contains(email, teamPage.GetTeamMemberEmailInTable(firstName + " " + lastName).Text);

            });
        }

        [Fact]
        public void IsClientCreatedPopupContainsOkButton()
        {
            Check(() =>
            {
                string firstName = "Alex" + random.Next(1, 1000);
                string lastName = "Autotest" + random.Next(1, 1000);

                teamPage.ClickAddTeamMemberButtonInTable();
                teamPage.SetFirstName(firstName);
                teamPage.SetLastName(lastName);
                teamPage.SubmitAddTeamMemberForm();
                teamPage.ClickSkipButtonInInvitePopup();

                Assert.True(teamPage.GetOkButtonInClientCreatedPopup().Displayed);
            });
        }

        [Fact]
        public void IsClientCreatedPopupContainsOpenClientButton()
        {
            Check(() =>
            {
                string firstName = "Alex" + random.Next(1, 1000);
                string lastName = "Autotest" + random.Next(1, 1000);

                teamPage.ClickAddTeamMemberButtonInTable();
                teamPage.SetFirstName(firstName);
                teamPage.SetLastName(lastName);
                teamPage.SubmitAddTeamMemberForm();
                teamPage.ClickSkipButtonInInvitePopup();

                Assert.True(teamPage.GetOpenClientButtonInClientCreatedPopup().Displayed);
            });          
        }

        [Fact]
        public void IsClientCreatedPopupContainsCrossButton()
        {
            Check(() =>
            {
                string firstName = "Alex" + random.Next(1, 1000);
                string lastName = "Autotest" + random.Next(1, 1000);

                teamPage.ClickAddTeamMemberButtonInTable();
                teamPage.SetFirstName(firstName);
                teamPage.SetLastName(lastName);
                teamPage.SubmitAddTeamMemberForm();
                teamPage.ClickSkipButtonInInvitePopup();

                Assert.True(teamPage.GetCrossButtonInClientCreatedPopup().Displayed);
            });
        }

        [Fact]
        public void IsCrossButtonCloseClientCreatedPopup()
        {
            Check(() =>
            {
                string firstName = "Alex" + random.Next(1, 1000);
                string lastName = "Autotest" + random.Next(1, 1000);

                teamPage.ClickAddTeamMemberButtonInTable();
                teamPage.SetFirstName(firstName);
                teamPage.SetLastName(lastName);
                teamPage.SubmitAddTeamMemberForm();
                teamPage.ClickSkipButtonInInvitePopup();
                teamPage.ClickCrossButtonInClientCreatedPopup();

                Assert.Null(teamPage.GetOpenClientButtonInClientCreatedPopup());
            });
        }

        [Fact]
        public void IsOkButtonCloseClientCreatedPopup()
        {
            Check(() =>
            {
                string firstName = "Alex" + random.Next(1, 1000);
                string lastName = "Autotest" + random.Next(1, 1000);

                teamPage.ClickAddTeamMemberButtonInTable();
                teamPage.SetFirstName(firstName);
                teamPage.SetLastName(lastName); 
                teamPage.SubmitAddTeamMemberForm();
                teamPage.ClickSkipButtonInInvitePopup();
                teamPage.ClickOkButtonInClientCreatedPopup();

                Assert.Null(teamPage.GetOpenClientButtonInClientCreatedPopup());
            });
        }

        [Fact]
        public void IsOpenClientButtonInClientCreatedPopupOpenTeamMemberPage()
        {
            Check(() =>
            {
                string firstName = "Alex" + random.Next(1, 1000);
                string lastName = "Autotest" + random.Next(1, 1000);

                teamPage.ClickAddTeamMemberButtonInTable();
                teamPage.SetFirstName(firstName);
                teamPage.SetLastName(lastName);
                teamPage.SubmitAddTeamMemberForm();
                teamPage.ClickSkipButtonInInvitePopup();
                teamPage.ClickOpenClientButtonInClientCreatedPopup();

                Assert.Contains("member", driver.Url);
            });
        }

        [Fact]
        public void IsInviteEmailSendToClientAfterInvite()
        {
            Check(() =>
            {
                string firstName = "Alex" + random.Next(1, 1000);
                string lastName = "Autotest" + random.Next(1, 1000);
                string email = config["basic_email_prefix"] + "+" + random.Next(1, 10000) + config["basic_email_domain"];

                teamPage.CreateAndInviteTeamMember(firstName, lastName, email);
                var unreadEmails = mailRepository.WaitAndGetUnreadEmails();

                foreach (var message in unreadEmails)
                {
                    Assert.Contains(teamPage.InviteEmailText, message);
                }
            });
        }

        [Fact]
        public void IsInviteEmailContainsUserEmailAddress()
        {
            Check(() =>
            {
                string firstName = "Alex" + random.Next(1, 1000);
                string lastName = "Autotest" + random.Next(1, 1000);
                string email = config["basic_email_prefix"] + "+" + random.Next(1, 10000) + config["basic_email_domain"];               
                teamPage.CreateAndInviteTeamMember(firstName, lastName, email);

                infoFromInviteEmail = teamPage.GetInfoFromInvitationEmail();

                Assert.Equal(email, infoFromInviteEmail.email);
            });
        }

        [Fact]
        public void IsInviteEmailContainsUserPassword()
        {
            Check(() =>
            {
                string firstName = "Alex" + random.Next(1, 1000);
                string lastName = "Autotest" + random.Next(1, 1000);
                string email = config["basic_email_prefix"] + "+" + random.Next(1, 10000) + config["basic_email_domain"];

                teamPage.CreateAndInviteTeamMember(firstName, lastName, email);

                infoFromInviteEmail = teamPage.GetInfoFromInvitationEmail();

                Assert.False(String.IsNullOrWhiteSpace(infoFromInviteEmail.password));
            });
        }

        [Fact]
        public void IsInviteEmailContainsLinkToLoginPage()
        {
            Check(() =>
            {
                string firstName = "Alex" + random.Next(1, 1000);
                string lastName = "Autotest" + random.Next(1, 1000);
                string email = config["basic_email_prefix"] + "+" + random.Next(1, 10000) + config["basic_email_domain"];

                teamPage.CreateAndInviteTeamMember(firstName, lastName, email);

                infoFromInviteEmail = teamPage.GetInfoFromInvitationEmail();

                Assert.False(String.IsNullOrWhiteSpace(infoFromInviteEmail.linkToLogin));
            });
        }

        [Fact]
        public void IsLoginPageOpenedWhenUserClickLinkToLoginFromInviteEmail()
        {
            Check(() =>
            {
                string firstName = "Alex" + random.Next(1, 1000);
                string lastName = "Autotest" + random.Next(1, 1000);
                string email = config["basic_email_prefix"] + "+" + random.Next(1, 10000) + config["basic_email_domain"];

                teamPage.CreateAndInviteTeamMember(firstName, lastName, email);
                advisorHeader.Logout();

                infoFromInviteEmail = teamPage.GetInfoFromInvitationEmail();
                driver.Navigate().GoToUrl(infoFromInviteEmail.linkToLogin);

                Assert.True(loginPage.GetLoginButton().Displayed);
            });
        }

        [Fact]
        public void IsTeamMemberAbleToLoginAfterInvite()
        {
            Check(() =>
            {
                string firstName = "Alex" + random.Next(1, 1000);
                string lastName = "Autotest" + random.Next(1, 1000);
                string email = config["basic_email_prefix"] + "+" + random.Next(1, 10000) + config["basic_email_domain"];

                teamPage.CreateAndInviteTeamMember(firstName, lastName, email);
                advisorHeader.Logout();

                while (true)
                {
                    infoFromInviteEmail = teamPage.GetInfoFromInvitationEmail();
                    if (infoFromInviteEmail.email == email)
                        break;
                }

                driver.Navigate().GoToUrl(infoFromInviteEmail.linkToLogin);
                loginPage.SetEmail(infoFromInviteEmail.email);
                loginPage.SetPassword(infoFromInviteEmail.password);
                loginPage.SubmitForm();

                Assert.True(loginPage.GetUserMenuAfterLogin().Displayed);
            });
        }


        public static TheoryData<string, string> ValidationErrorsTestDataForAddTeamMemberPopup =>
            new TheoryData<string, string>
            {
                { "", "" },
                { "FirstName", "" },
                { "", "LastName" },
            };

        public static TheoryData<string, string, string, string> ValidationErrorsTestDataForInvitePopup =>
            new TheoryData<string, string, string, string>
            {
                { "", "test sub", "test desc", "Add your client" },
                { "autotest@growthwheel.com", "", "test desc", "Add a subject line" },
                { "autotest@growthwheel.com", "test sub", "", "You need to add an email content" },
            };

        //data from https://blogs.msdn.microsoft.com/testing123/2009/02/06/email-address-test-cases/
        public static TheoryData<string> InvalidEmailAddresses =>
            new TheoryData<string>
            {
                { "plainaddress" },
                { "#@%^%#$@#$@#.com" },
                { "@domain.com" },
                { "Joe Smith <email@domain.com>" },
                { "email.domain.com" },
                { "email@domain@domain.com" },
                { ".email@domain.com" },
                { "email.@domain.com" },
                { "email..email@domain.com" },
                { "あいうえお@domain.com" },
                { "email@domain.com (Joe Smith)" },
                { "email@domain" },
                { "email@-domain.com" },
                { "email@domain.web" },
                { "email@111.222.333.44444" },
                { "email@domain..com" },
            };
    }
}



