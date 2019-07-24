using System;
using OpenQA.Selenium;
using GrowthWheel_AutoTests.Configuration;
using OpenQA.Selenium.Support.UI;
using Xunit;
using HtmlAgilityPack;
using System.Threading;

namespace GrowthWheel_AutoTests.Pages.Guest
{
    public class ForgotPasswordPage : LoginPage
    {
        public ForgotPasswordPage(SettingUpFixture sb)
            : base(sb)
        {
            EmailAddress = config["simple_advisor_email"];
            InvalidEmailAddress = config["invalid_email"];
            OldPassword = config["simple_advisor_password"];
        }

        public static new string URL = config["basic_global_url"] + "/users/resetpassword";
        public static string PreviousFindedLink = "";

        public string EmailAddress { get; } = "";
        public string InvalidEmailAddress { get; } = "";
        public string OldPassword { get; } = "";
        public string NewPassword { get; } = "Test123456";
        public string RecoveryPasswordEmailText { get; } = "You requested a new password for GrowthWheel Online";
        public string SuccessResetRequestPopUpText { get; } = "An email has been sent to your email address with a password retrieval link. Please log in to your email and follow the link.";
        public string SuccessPasswordChangedPopUpText { get; } = "Your password has been changed";
        public string RecoveryPasswordLinkXpath { get; } = "//a";
        public string RecoveryPasswordLinkDetectionText { get; } = "email.growthwheel.net";
        public string ValidationErrorMessageInInvalidEmailPopUp { get; } = "Email that you entered is not associated with any email in our database";
        public string ValidationErrorMessageInInvalidPasswordPopUp { get; } = "Your passwords didn't match, please try again";
        public string ValidationErrorMessageOnLoginPage { get; } = "You have entered wrong login information";
        public string ValidationErrorMessageWheOpenResetLinkSecondTime { get; } = "The link you followed is no longer valid";

        protected new string emailFieldSelector = "#UserEmail";
        protected string submitButtonSelector = ".submit-btn";
        protected string rememberedPasswordButtonSelector = "#loginform a:last-child";

        protected string newPasswordFieldSelector = "#UserPassword1";
        protected string retypeNewPasswordFieldSelector = "#UserPassword2";

        public override IWebElement GetEmailField() => GetElement(emailFieldSelector);
        public override void SetEmail(string email)
        {
            GetElement(emailFieldSelector).Clear();
            InputValue(emailFieldSelector, email);
        }     

        public IWebElement GetSubmitButton() => GetElement(submitButtonSelector);
        public IWebElement GetRememberedPasswordButton() => GetElement(rememberedPasswordButtonSelector);

        public IWebElement GetNewPasswordField() => GetElement(newPasswordFieldSelector);
        public void SetNewPassword(string password)
        {
            GetElement(newPasswordFieldSelector).Clear();
            InputValue(newPasswordFieldSelector, password);
        }

        public IWebElement GetRetypeNewPasswordField() => GetElement(retypeNewPasswordFieldSelector);
        public void SetRetypeNewPassword(string password)
        {
            GetElement(retypeNewPasswordFieldSelector).Clear();
            InputValue(retypeNewPasswordFieldSelector, password);
        }

        public void ClickRememberedPasswordButton() => GetElement(rememberedPasswordButtonSelector).Click();

        public void RemoveRequiredAttributesFromFieldsOnRequestPage()
        {
            string script = "document.getElementById('UserEmail').removeAttribute('required');";
            js.ExecuteScript(script);
        }

        public void RemoveRequiredAttributesFromFieldsOnResetPage()
        {
            string script = "document.getElementById('UserPassword1').removeAttribute('required'); " +
                            "document.getElementById('UserPassword2').removeAttribute('required');";
            js.ExecuteScript(script);
        }

        public string GetResetPasswordLinkFromEmail()
        {
            SetEmail(EmailAddress);
            SubmitForm();

            var mailRepository = new MailRepository();
            var unreadEmails = mailRepository.WaitAndGetUnreadEmails();
            string href = null;

            foreach (var email in unreadEmails)
            {
                if (email.Contains(RecoveryPasswordEmailText))
                {
                    var doc = new HtmlDocument();
                    doc.LoadHtml(email);
                    var links = doc.DocumentNode.SelectNodes(RecoveryPasswordLinkXpath);
                    foreach (var link in links)
                    {
                        if (link.OuterHtml.Contains(RecoveryPasswordLinkDetectionText))
                        {
                            href = link.GetAttributeValue("href", null);
                            if (href != PreviousFindedLink)
                                break;                                                    
                        }
                    }
                }

                if (!String.IsNullOrWhiteSpace(href))
                    break;
            }

            PreviousFindedLink = href;
            return href;
        }

        public void ResetPasswordToDefaultAfterTest()
        {
            driver.Navigate().GoToUrl(URL);
            driver.Navigate().GoToUrl(GetResetPasswordLinkFromEmail());
            SetNewPassword(OldPassword);
            SetRetypeNewPassword(OldPassword);
            SubmitForm();
            Check(() => Assert.Contains(SuccessPasswordChangedPopUpText, GetFlashMessageInValidationModalWindow().Text));
        }

    }
}
