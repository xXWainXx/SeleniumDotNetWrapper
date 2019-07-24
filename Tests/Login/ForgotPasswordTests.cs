using Xunit;
using GrowthWheel_AutoTests.Configuration;
using GrowthWheel_AutoTests.Pages.Guest;
using System;
using System.Linq;
using System.Threading;
using System.Text.RegularExpressions;
using System.Text;
using HtmlAgilityPack;
using System.Collections.Generic;

namespace GrowthWheel_AutoTests.Tests.Login
{
    public class ForgotPasswordTests : SeleniumTestBase
    {
        public ForgotPasswordTests(SettingUpFixture sb)
            : base(sb)
        {
            forgotPasswordPage = new ForgotPasswordPage(sb);
            loginPage = new LoginPage(sb);
            mailRepository = new MailRepository();

            driver.Navigate().GoToUrl(ForgotPasswordPage.URL);
        }

        ForgotPasswordPage forgotPasswordPage;
        LoginPage loginPage;
        MailRepository mailRepository;


        [Fact]
        public void IsEmailFieldPresent() => Check(() => Assert.True(forgotPasswordPage.GetEmailField().Displayed));

        [Fact]
        public void IsSubmitButtonPresent() => Check(() => Assert.True(forgotPasswordPage.GetSubmitButton().Displayed));

        [Fact]
        public void IsSupportButtonPresent() => Check(() => Assert.True(forgotPasswordPage.GetSupportButton().Displayed));

        [Fact]
        public void IsRememberedPasswordButtonPresent() => Check(() => Assert.True(forgotPasswordPage.GetRememberedPasswordButton().Displayed));

        [Fact]
        public void IsEmailFieldHasRequiredAttribute() => Check(() => Assert.NotNull(GetElementAttribute(forgotPasswordPage.GetEmailField(), "required")));

        [Fact]
        public void IsSuccessPopUpDisplayed()
        {
            Check(() =>
            {
                forgotPasswordPage.SetEmail(forgotPasswordPage.EmailAddress);
                forgotPasswordPage.SubmitForm();
                Assert.Equal(forgotPasswordPage.SuccessResetRequestPopUpText, forgotPasswordPage.GetFlashMessageInValidationModalWindow().Text);
            });
        }

        [Fact]
        public void IsUserRedirectedToLogin()
        {
            Check(() =>
            {
                forgotPasswordPage.SetEmail(forgotPasswordPage.EmailAddress);
                forgotPasswordPage.SubmitForm();
                forgotPasswordPage.ClickOkButtonInModalWindow();
                Assert.True(loginPage.GetPasswordField().Displayed);
            });
        }

        [Fact]
        public void IsEmailMessageSent()
        {
            Check(() =>
            {
                forgotPasswordPage.SetEmail(forgotPasswordPage.EmailAddress);
                forgotPasswordPage.SubmitForm();

                var unreadEmails = mailRepository.WaitAndGetUnreadEmails();

                foreach (var email in unreadEmails)
                {
                    Assert.Contains(forgotPasswordPage.RecoveryPasswordEmailText, email);
                }
            });
        }

        [Fact]
        public void IsRecoveryLinkInEmailMessageCorrect()
        {
            Check(() =>
            {
                driver.Navigate().GoToUrl(forgotPasswordPage.GetResetPasswordLinkFromEmail());

                Assert.True(forgotPasswordPage.GetNewPasswordField().Displayed);
                Assert.True(forgotPasswordPage.GetRetypeNewPasswordField().Displayed);
                Assert.True(forgotPasswordPage.GetSubmitButton().Displayed);
            });
        }

        [Fact]
        public void IsPopUpDisplayedAfterPasswordSuccessfulyChanged()
        {
            Check(() =>
            {
                driver.Navigate().GoToUrl(forgotPasswordPage.GetResetPasswordLinkFromEmail());
                forgotPasswordPage.SetNewPassword(forgotPasswordPage.NewPassword);
                forgotPasswordPage.SetRetypeNewPassword(forgotPasswordPage.NewPassword);
                forgotPasswordPage.SubmitForm();
                Assert.Contains(forgotPasswordPage.SuccessPasswordChangedPopUpText, forgotPasswordPage.GetFlashMessageInValidationModalWindow().Text);

                forgotPasswordPage.ResetPasswordToDefaultAfterTest();
            });
        }

        [Fact]
        public void CheckPossibilityToLoginWithChangedPassword()
        {
            Check(() =>
            {
                driver.Navigate().GoToUrl(forgotPasswordPage.GetResetPasswordLinkFromEmail());

                forgotPasswordPage.SetNewPassword(forgotPasswordPage.NewPassword);
                forgotPasswordPage.SetRetypeNewPassword(forgotPasswordPage.NewPassword);
                forgotPasswordPage.SubmitForm();
                forgotPasswordPage.ClickOkButtonInModalWindow();
                driver.Navigate().GoToUrl(LoginPage.URL);
                loginPage.SetEmail(forgotPasswordPage.EmailAddress);
                loginPage.SetPassword(forgotPasswordPage.NewPassword);
                loginPage.SubmitForm();
                Assert.True(loginPage.GetUserMenuAfterLogin().Displayed);

                loginPage.Logout();
                forgotPasswordPage.ResetPasswordToDefaultAfterTest();
            });
        }

        [Fact]
        public void UserShouldNotBeAbleToLoginWithOldPasswordAfterChange()
        {
            Check(() =>
            {
                driver.Navigate().GoToUrl(forgotPasswordPage.GetResetPasswordLinkFromEmail());
                forgotPasswordPage.SetNewPassword(forgotPasswordPage.NewPassword);
                forgotPasswordPage.SetRetypeNewPassword(forgotPasswordPage.NewPassword);
                forgotPasswordPage.SubmitForm();
                forgotPasswordPage.ClickOkButtonInModalWindow();
                driver.Navigate().GoToUrl(LoginPage.URL);
                loginPage.SetEmail(forgotPasswordPage.EmailAddress);
                loginPage.SetPassword(forgotPasswordPage.OldPassword);
                loginPage.SubmitForm();
                Assert.Contains(forgotPasswordPage.ValidationErrorMessageOnLoginPage, loginPage.GetFlashMessageInValidationModalWindow().Text);

                loginPage.Logout();
                forgotPasswordPage.ResetPasswordToDefaultAfterTest();
            });
        }

        [Fact]
        public void LinkToResetPasswordShouldNotBeValidAfterChangePassword()
        {
            Check(() => {               
                string link = forgotPasswordPage.GetResetPasswordLinkFromEmail();
                driver.Navigate().GoToUrl(link);
                
                forgotPasswordPage.SetNewPassword(forgotPasswordPage.NewPassword);
                forgotPasswordPage.SetRetypeNewPassword(forgotPasswordPage.NewPassword);
                forgotPasswordPage.SubmitForm();
                forgotPasswordPage.ClickOkButtonInModalWindow();
                driver.Navigate().GoToUrl(link);

                Assert.Contains(forgotPasswordPage.ValidationErrorMessageWheOpenResetLinkSecondTime, loginPage.GetFlashMessageInValidationModalWindow().Text);

                forgotPasswordPage.ResetPasswordToDefaultAfterTest();
            });           
        }

        [Fact]
        public void OkButtonShouldCloseErrorPopUpWhenOpenResetPasswordLinkSecondTime()
        {
            Check(() =>
            {
                string link = forgotPasswordPage.GetResetPasswordLinkFromEmail();
                driver.Navigate().GoToUrl(link);

                forgotPasswordPage.SetNewPassword(forgotPasswordPage.NewPassword);
                forgotPasswordPage.SetRetypeNewPassword(forgotPasswordPage.NewPassword);
                forgotPasswordPage.SubmitForm();
                forgotPasswordPage.ClickOkButtonInModalWindow();
                driver.Navigate().GoToUrl(link);

                Assert.True(forgotPasswordPage.GetFlashMessageInValidationModalWindow().Displayed);
                loginPage.ClickOkButtonInModalWindow();
                Assert.False(forgotPasswordPage.GetFlashMessageInValidationModalWindow().Displayed);

                forgotPasswordPage.ResetPasswordToDefaultAfterTest();
            });
        }

        [Fact]
        public void OkButtonShouldCloseSuccessPasswordChangedPopUp()
        {
            Check(() =>
            {
                driver.Navigate().GoToUrl(forgotPasswordPage.GetResetPasswordLinkFromEmail());

                forgotPasswordPage.SetNewPassword(forgotPasswordPage.NewPassword);
                forgotPasswordPage.SetRetypeNewPassword(forgotPasswordPage.NewPassword);
                forgotPasswordPage.SubmitForm();
                Assert.True(forgotPasswordPage.GetFlashMessageInValidationModalWindow().Displayed);
                forgotPasswordPage.ClickOkButtonInModalWindow();
                Assert.False(forgotPasswordPage.GetFlashMessageInValidationModalWindow().Displayed);

                forgotPasswordPage.ResetPasswordToDefaultAfterTest();
            });
        }

        [Fact]
        public void OkButtonShouldCloseErrorPopUpOnRequestPage()
        {
            Check(() =>
            {
                forgotPasswordPage.SetEmail(forgotPasswordPage.InvalidEmailAddress);
                forgotPasswordPage.SubmitForm();
                Assert.True(forgotPasswordPage.GetFlashMessageInValidationModalWindow().Displayed);
                forgotPasswordPage.ClickOkButtonInModalWindow();
                Assert.False(forgotPasswordPage.GetFlashMessageInValidationModalWindow().Displayed);
            });
        }

        [Fact]
        public void OkButtonShouldCloseSuccessRequestResetPasswordPopUp()
        {
            Check(() =>
            {
                forgotPasswordPage.SetEmail(forgotPasswordPage.EmailAddress);
                forgotPasswordPage.SubmitForm();
                Assert.True(forgotPasswordPage.GetFlashMessageInValidationModalWindow().Displayed);
                forgotPasswordPage.ClickOkButtonInModalWindow();
                Assert.False(forgotPasswordPage.GetFlashMessageInValidationModalWindow().Displayed);
            });
        }

        [Fact]
        public void OkButtonShouldCloseErrorPopUpOnResetPage()
        {
            Check(() =>
            {
                driver.Navigate().GoToUrl(forgotPasswordPage.GetResetPasswordLinkFromEmail());

                forgotPasswordPage.SetNewPassword(forgotPasswordPage.NewPassword);
                forgotPasswordPage.SetRetypeNewPassword(forgotPasswordPage.OldPassword);
                forgotPasswordPage.SubmitForm();
                Assert.True(forgotPasswordPage.GetFlashMessageInValidationModalWindow().Displayed);
                forgotPasswordPage.ClickOkButtonInModalWindow();
                Assert.False(forgotPasswordPage.GetFlashMessageInValidationModalWindow().Displayed);
            });
        }

        [Theory]
        [MemberData(nameof(ValidationErrorsTestDataRequestPage))]
        public void CheckValidationErrorsOnRequestPage(string email)
        {
            Check(() =>
            {
                forgotPasswordPage.RemoveRequiredAttributesFromFieldsOnRequestPage();
                forgotPasswordPage.SetEmail(email);
                forgotPasswordPage.SubmitForm();
                Assert.Contains(forgotPasswordPage.ValidationErrorMessageInInvalidEmailPopUp, forgotPasswordPage.GetFlashMessageInValidationModalWindow().Text);
            });
        }

        [Theory]
        [MemberData(nameof(ValidationErrorsTestDataResetPage))]
        public void CheckValidationErrorsOnResetPage(string newPassword, string retypeNewPassword)
        {
            Check(() =>
            {
                driver.Navigate().GoToUrl(forgotPasswordPage.GetResetPasswordLinkFromEmail());
                forgotPasswordPage.RemoveRequiredAttributesFromFieldsOnResetPage();
                forgotPasswordPage.SetNewPassword(newPassword);
                forgotPasswordPage.SetRetypeNewPassword(retypeNewPassword);
                forgotPasswordPage.SubmitForm();
                Assert.Contains(forgotPasswordPage.ValidationErrorMessageInInvalidPasswordPopUp, forgotPasswordPage.GetFlashMessageInValidationModalWindow().Text);
            });
        }

        public static TheoryData<string> ValidationErrorsTestDataRequestPage =>
            new TheoryData<string>
            {
                {""},
                {"test@example.com"},
            };

        public static TheoryData<string, string> ValidationErrorsTestDataResetPage =>
            new TheoryData<string, string>
            {
                {"", ""},
                {"123", ""},
                {"", "123"},
                {"123", "1234"}
            };

    }
}
