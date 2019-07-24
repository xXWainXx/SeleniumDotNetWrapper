using System;
using OpenQA.Selenium;
using GrowthWheel_AutoTests.Configuration;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace GrowthWheel_AutoTests.Pages.Advisor.MyClients.BusinessProfile
{
    class TeamPage : SeleniumTestBase
    {
        public TeamPage(SettingUpFixture sb)
            : base(sb)
        { }

        public string InviteEmailText { get; } = "Your login information";
        public string ErrorMessageInInvalidEmail { get; } = "You have entered an invalid email";
        public string MessageInInvitationSendPopup { get; } = "An email invitation was sent to your client";
        public static string PreviousFindedPassword = null;
        public static string PreviousFindedEmail = null;

        //team members table
        protected string addTeamMemberButtonSelector = "#ClientGrid .head .btn-add";
        protected string teamMemberNameListSelector = "#ClientGridForm tbody .full_name a";
        protected string teamMemberTitleListSelector = "#ClientGridForm tbody .title a";
        protected string teamMemberRoleListSelector = "#ClientGridForm tbody .role a";
        protected string teamMemberEmailListSelector = "#ClientGridForm tbody .username a";
        protected string teamMemberPhoneListSelector = "#ClientGridForm tbody .phone a";
        protected string downloadButtonListSelector = "#ClientGridForm tbody .actions a.download";

        //add team member pop-up
        protected string addTeamMemberModalSelector = "#advisorAddClientModal";
        protected string firstNameFieldSelector = "#CreateNewTeamMember #first_name";
        protected string lastNameFiledSelector = "#CreateNewTeamMember #last_name";
        protected string submitAddTeamMemberFormButtomSelector = "#CreateNewTeamMember button.submit";
        protected string cancelAddTeamMemberButtonSelector = "#CreateNewTeamMember button.cancel";
        protected string companyNameInTitleSelector = "#addClientOrganizationName";

        //invite and give access pop-up
        protected string nameFieldSelector = "div.invite-user .client-name";
        protected string emailFieldSelector = "div.invite-user #username";
        protected string subjectFieldSelector = "div.invite-user #subject";
        protected string descriptionFieldSelector = "div.invite-user #email-message";
        protected string signatureSectionSelector = ".additional-content";
        protected string submitInviteFormButtomSelector = "div.invite-user button[type='submit']";
        protected string skipInviteFormButtomSelector = "div.invite-user button.cancel";
        protected string childElementsInDescriptionFieldSelector = "#email-message *";
        protected string errorMessageInInvitePopupSelector = ".error-message";

        //client created pop-up
        protected string okButtonSelector = "div.cancel-invite-user button.cancel";
        protected string openClientButtonSelector = "div.cancel-invite-user.in button.submit";
        protected string crossButtonSelector = "div.modal-cross.no";
        protected string textInClientCreatedPopupSelector = "div.modal-body .success";

        //InviteEmailMessage
        protected string passwordInInviteEmailId = "pswrd";
        protected string emailInInviteEmailId = "usrnm";
        protected string linkToLoginPageInInviteEmailId = "goto";


        //team members table
        public IWebElement GetAddTeamMemberButtonInTable() => GetElement(addTeamMemberButtonSelector);

        public IWebElement GetTeamMemberNameFieldInTable(string fullName) => GetElementInListByText(teamMemberNameListSelector, fullName);

        public IWebElement GetTeamMemberTitleInTable(string fullName)
        {
            var elementIndex = GetElementIndexInListByText(teamMemberNameListSelector, fullName);
            return GetElementInListByIndex(teamMemberTitleListSelector, elementIndex);
        }

        public IWebElement GetTeamMemberRoleInTable(string fullName)
        {
            var elementIndex = GetElementIndexInListByText(teamMemberNameListSelector, fullName);
            return GetElementInListByIndex(teamMemberRoleListSelector, elementIndex);
        }

        public IWebElement GetTeamMemberEmailInTable(string fullName)
        {
            var elementIndex = GetElementIndexInListByText(teamMemberNameListSelector, fullName);
            string selector = "#ClientGridForm tbody  tr:nth-child(" + elementIndex + ") td.username  a";
            try
            {
                return GetElement(selector);
            }
            catch
            {
                throw new Exception("Email Not Found");
            }
        }

        public IWebElement GetTeamMemberPhoneInTable(string fullName)
        {
            var elementIndex = GetElementIndexInListByText(teamMemberNameListSelector, fullName);
            return GetElementInListByIndex(teamMemberPhoneListSelector, elementIndex);
        }

        public void ClickAddTeamMemberButtonInTable()
        {
            GetElement(addTeamMemberButtonSelector).Click();
            webDriverWait.Until(x => GetElement(addTeamMemberModalSelector).Displayed);
        }

        public void ClickDownloadButtonInTable(string fullName)
        {
            var elementIndex = GetElementIndexInListByText(teamMemberNameListSelector, fullName);
            GetElementInListByIndex(downloadButtonListSelector, elementIndex).Click();
        }

        public IWebElement GetInviteButtonInTable(string fullName)
        {
            var elementIndex = GetElementIndexInListByText(teamMemberNameListSelector, fullName);
            string inviteButtonSelector = "#ClientGridForm tbody tr:nth-child(" + elementIndex + ") .actions a[data-target='#advisorInviteClientModal']";
            try
            {
                return GetElement(inviteButtonSelector);
            }
            catch
            {
                throw new Exception("Invite Button Not Found");
            }
        }

        public void ClickInviteButtonInTable(string fullName)
        {
            var elementIndex = GetElementIndexInListByText(teamMemberNameListSelector, fullName);
            string inviteButtonSelector = "#ClientGridForm tbody tr:nth-child(" + elementIndex + ") .actions a[data-target='#advisorInviteClientModal']";
            try
            {
                GetElement(inviteButtonSelector).Click();
            }
            catch
            {
               throw new Exception("Invite Button Not Found");
            }
        }

        //add team member pop-up
        public IWebElement GetAddTeamMemberModal() => GetElement(addTeamMemberModalSelector);
        public string GetCompanyNameInAddTeamMemberModalTitle() => GetElement(companyNameInTitleSelector).Text.ToString();       

        public void SetFirstName(string firstNname) => InputValue(firstNameFieldSelector, firstNname);
        public IWebElement GetFirstNameField() => GetElement(firstNameFieldSelector);

        public void SetLastName(string lastName) => InputValue(lastNameFiledSelector, lastName);
        public IWebElement GetLastNameField() => GetElement(lastNameFiledSelector);

        public IWebElement GetCancelButtonInAddTeamMemberPopup() => GetElement(cancelAddTeamMemberButtonSelector);
        public IWebElement GetSubmitButtonInAddTeamMemberPopup() => GetElement(submitAddTeamMemberFormButtomSelector);

        public TeamPage SubmitAddTeamMemberForm()
        {            
            GetElement(submitAddTeamMemberFormButtomSelector).Click();
            return new TeamPage(sb);
        }      

        public void ClickCancelButtonInAddTeamMemberForm() => GetElement(cancelAddTeamMemberButtonSelector).Click();    

        //invite and give access pop-up
        public IWebElement GetFullNameFieldInInvitePopup() => GetElement(nameFieldSelector);

        public void SetEmailInInvitePopup(string email) => InputValue(emailFieldSelector, email);
        public IWebElement GetEmailFieldInInvitePopup() => GetElement(emailFieldSelector);

        public void SetSubjectInInvitePopup(string subject) => InputValue(subjectFieldSelector, subject);
        public IWebElement GetSubjectFieldInInvitePopup() => GetElement(subjectFieldSelector);

        public void SetDescriptionInInvitePopup(string description) => InputValue(descriptionFieldSelector, description);
        public IWebElement GetDescriptionFieldInInvitePopup() => GetElement(descriptionFieldSelector);

        public IWebElement GetSignatureSectionInInvitePopup() => GetElement(signatureSectionSelector);

        public IWebElement GetInviteButtonInInvitePopup() => GetElement(submitInviteFormButtomSelector);

        public IWebElement GetSkipButtonInInvitePopup() => GetElement(skipInviteFormButtomSelector);
        public void ClickSkipButtonInInvitePopup() => GetElement(skipInviteFormButtomSelector).Click();

        public IWebElement GetCrossButtonInInvitePopup() => GetElement(crossButtonSelector);
        public void ClickCrossButtonInInvitePopup() => GetElement(crossButtonSelector).Click();

        public IWebElement GetErrorMessageInInvitePopup() => GetElement(errorMessageInInvitePopupSelector);

        public IList<IWebElement> GetElementsInDescriptionFieldInInvitePopup() => driver.FindElements(By.CssSelector(childElementsInDescriptionFieldSelector));

        //client created pop-up
        public IWebElement GetOkButtonInClientCreatedPopup() => GetElement(okButtonSelector);
        public void ClickOkButtonInClientCreatedPopup() => GetElement(okButtonSelector).Click();

        public IWebElement GetCrossButtonInClientCreatedPopup() => GetElement(crossButtonSelector);
        public void ClickCrossButtonInClientCreatedPopup() => GetElement(crossButtonSelector).Click();

        public IWebElement GetOpenClientButtonInClientCreatedPopup() => GetElement(openClientButtonSelector);
        public void ClickOpenClientButtonInClientCreatedPopup() => GetOpenClientButtonInClientCreatedPopup().Click();

        public string GetTextInClientCreatedPopup() => GetElement(textInClientCreatedPopupSelector).Text;

        public TeamPage SubmitInviteForm()
        {
            GetFullNameFieldInInvitePopup().Click();
            GetElement(submitInviteFormButtomSelector).Click();
            return new TeamPage(sb);
        }

        //general functions
        public TeamPage GoToTeamPage(string clientCompanyTitle)
        {
            AdvisorHeader header = new AdvisorHeader(sb);
            ClientsListPage clientsListPage = new ClientsListPage(sb);
            header.ClickMyClientsButton();
            clientsListPage.GoToClientCompanyPage(clientCompanyTitle);
            header.ClickBusinessProfileButton();
            header.ClickBusinessProfileTeamButton();
            return new TeamPage(sb);
        }

        public TeamPage CreateNotInviteTeamMember(string firstName, string lastName)
        {
            ClickAddTeamMemberButtonInTable();
            SetFirstName(firstName);
            SetLastName(lastName);
            SubmitAddTeamMemberForm();
            ClickSkipButtonInInvitePopup();
            ClickOkButtonInClientCreatedPopup();
            return new TeamPage(sb);
        }

        public TeamPage CreateAndInviteTeamMember(string firstName, string lastName, string email)
        {
            ClickAddTeamMemberButtonInTable();
            SetFirstName(firstName);
            SetLastName(lastName);
            SubmitAddTeamMemberForm();
            SetEmailInInvitePopup(email);
            SubmitInviteForm();
            ClickOkButtonInClientCreatedPopup();
            return new TeamPage(sb);
        }

        public void RemoveRequiredAttributesFromFieldsInAddTeamMemberPopup()
        {
            string script = "document.getElementById('first_name').removeAttribute('required'); " +
                            "document.getElementById('last_name').removeAttribute('required');";
            js.ExecuteScript(script);
        }

        public (string email, string password, string linkToLogin) GetInfoFromInvitationEmail()
        {
            var mailRepository = new MailRepository();
            var unreadEmails = mailRepository.WaitAndGetUnreadEmails();
            string emailAddress = null;
            string password = null;
            string linkToLogin = null;

            foreach (var email in unreadEmails)
            {
                if (email.Contains(InviteEmailText))
                {
                    var doc = new HtmlDocument();
                    doc.LoadHtml(email);
                    emailAddress = doc.GetElementbyId(emailInInviteEmailId).InnerText;
                    password = doc.GetElementbyId(passwordInInviteEmailId).InnerText;
                    linkToLogin = doc.GetElementbyId(linkToLoginPageInInviteEmailId).GetAttributeValue("href", null);

                    if (emailAddress != PreviousFindedPassword && password != PreviousFindedPassword && !String.IsNullOrWhiteSpace(emailAddress) && !String.IsNullOrWhiteSpace(password))
                        break;                
                }
            }

            PreviousFindedPassword = password;
            PreviousFindedEmail = emailAddress;

            return (email: emailAddress, password: password, linkToLogin: linkToLogin);
        }
    }
}
