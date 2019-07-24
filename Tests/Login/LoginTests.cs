using Xunit;
using GrowthWheel_AutoTests.Configuration;
using GrowthWheel_AutoTests.Pages.Guest;
using System;

namespace GrowthWheel_AutoTests.Tests.Login
{
    public class LoginTests : SeleniumTestBase, IDisposable
    {
        public LoginTests(SettingUpFixture sb)
            : base(sb)
        {
            loginPage = new LoginPage(sb);
            forgotPasswordPage = new ForgotPasswordPage(sb);
            forgotEmailPage = new ForgotEmailPage(sb);
            driver.Navigate().GoToUrl(LoginPage.URL);
        }

        public new void Dispose()
        {
            driver.Manage().Cookies.DeleteAllCookies();
        }

        LoginPage loginPage;
        ForgotPasswordPage forgotPasswordPage;
        ForgotEmailPage forgotEmailPage;

        [Fact]
        public void IsEnglishSiteOpened() => Check(() => Assert.Equal(loginPage.PageTitle, driver.Title));

        [Fact]
        public void IsEmailFieldPresent() => Check(() => Assert.True(loginPage.GetEmailField().Displayed));

        [Fact]
        public void IsPasswordFieldPresent() => Check(() => Assert.True(loginPage.GetPasswordField().Displayed));

        [Fact]
        public void IsLoginButtonPresent() => Check(() => Assert.True(loginPage.GetLoginButton().Displayed));

        [Fact]
        public void IsSupportButtonPresent() => Check(() => Assert.True(loginPage.GetSupportButton().Displayed));

        [Fact]
        public void IsForgotPasswordButtonPresent() => Check(() => Assert.True(loginPage.GetForgotPasswordButton().Displayed));

        [Fact]
        public void IsForgotEmailButtonPresent() => Check(() => Assert.True(loginPage.GetForgotEmailButton().Displayed));

        [Fact]
        public void IsPasswordFieldHasRequiredAttribute() => Check(() => Assert.NotNull(GetElementAttribute(loginPage.GetPasswordField(), "required")));

        [Fact]
        public void IsEmailFieldHasRequiredAttribute() => Check(() => Assert.NotNull(GetElementAttribute(loginPage.GetEmailField(), "required")));

        [Theory]
        [MemberData(nameof(ValidationErrorsTestData))]
        public void CheckValidationErrors(string email, string password, string validationMessage)
        {
            Check(() =>
            {
                loginPage.RemoveRequiredAttributesFromFields();
                loginPage.SetEmail(email);
                loginPage.SetPassword(password);
                loginPage.SubmitForm();
                Assert.Contains(validationMessage, loginPage.GetFlashMessageInValidationModalWindow().Text);
            });
        }

        [Fact]
        public void OkButtonShouldClosePopUp()
        {
            Check(() =>
            {
                loginPage.RemoveRequiredAttributesFromFields();
                loginPage.SubmitForm();
                Assert.True(loginPage.GetFlashMessageInValidationModalWindow().Displayed);
                loginPage.ClickOkButtonInModalWindow();
                Assert.False(loginPage.GetFlashMessageInValidationModalWindow().Displayed);
            });
        }

        [Fact]
        public void ForgotPasswordButtonShouldOpenFollowingPage()
        {
            Check(() =>
            {
                loginPage.ClickForgotPasswordButton();
                Assert.True(forgotPasswordPage.GetEmailField().Displayed);
            });
        }

        [Fact]
        public void ForgotEmailButtonShouldOpenFollowingPage()
        {
            Check(() =>
            {
                loginPage.ClickForgotEmailButton();
                Assert.Contains(forgotEmailPage.SupportText, forgotEmailPage.GetSupportText().Text);
            });
        }

        [Fact]
        public void IsAdvisorAbleToLogin()
        {
            Check(() =>
            {
                loginPage.LoginAsFirstAdvisor();
                Assert.True(loginPage.GetUserMenuAfterLogin().Displayed);
            });
        }

        [Fact]
        public void IsClientAbleToLogin()
        {
            Check(() =>
            {
                loginPage.LoginAsFirstClient();
                Assert.True(loginPage.GetUserMenuAfterLogin().Displayed);
            });
        }

        [Fact]
        public void IsMentorAbleToLogin()
        {
            Check(() =>
            {
                loginPage.LoginAsMentor();
                Assert.True(loginPage.GetUserMenuAfterLogin().Displayed);
            });
        }

        [Fact]
        public void IsRestrictedUserAbleToLogin()
        {
            Check(() =>
            {
                loginPage.LoginAsRestrictedUser();
                Assert.True(loginPage.GetUserMenuAfterLogin().Displayed);
            });
        }

        public static TheoryData<string, string, string> ValidationErrorsTestData =>
            new TheoryData<string, string, string>
            {
                { "", "", "You have not entered a valid email address" },
                { "", "123", "You have not entered a valid email address" },
                { "test@growthwheel.com", "", "You have entered wrong login information" },
                { "alex@growthwheel.com", "123", "You have entered wrong login information" },
                { "test@example.com", "test", "You have entered wrong login information" },
                { "test@example.com", "123", "You have entered wrong login information" },
            };
    }
}
