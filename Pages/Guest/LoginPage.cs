using System;
using OpenQA.Selenium;
using GrowthWheel_AutoTests.Configuration;
using OpenQA.Selenium.Support.UI;

namespace GrowthWheel_AutoTests.Pages.Guest
{
    public class LoginPage : SeleniumTestBase
    {
        public LoginPage(SettingUpFixture sb)
            : base(sb)
        {
        }

        public static string URL = config["basic_global_url"];
        public static string DanishURL = config["basic_danish_url"];

        public string PageTitle { get; } = "GrowthWheel Online";

        protected string emailFieldSelector = "#UserUsername";
        protected string passwordFieldSelector = "#UserPassword";
        protected string loginButtonSelector = ".submit-btn";
        protected string supportButtonSelector = "a[href='mailto:support@growthwheel.com']";
        protected string forgotPasswordButtonSelector = "a[href='/users/resetpassword']";
        protected string forgotEmailButtonSelector = "a[href='/users/forgotusername']";
        protected string flashMessageInModalWindowSelector = "#flashMessage";
        protected string validationModalWindowSelector = "#confirmDialog";
        protected string okButtonInModalWindowSelector = "#confirmDialog button[type='submit']";
        protected string userMenuAfterLoginSelector = ".usermenu-top";

        public virtual void SetEmail(string email)
        {
            GetElement(emailFieldSelector).Clear();
            InputValue(emailFieldSelector, email);
        }

        public virtual IWebElement GetEmailField() => GetElement(emailFieldSelector);

        public void SetPassword(string password)
        {
            GetElement(passwordFieldSelector).Clear();
            InputValue(passwordFieldSelector, password);
        }

        public IWebElement GetPasswordField() => GetElement(passwordFieldSelector);

        public IWebElement GetLoginButton() => GetElement(loginButtonSelector);

        public IWebElement GetSupportButton() => GetElement(supportButtonSelector);
        public void ClickSupportButton() => GetElement(supportButtonSelector).Click();

        public IWebElement GetForgotPasswordButton() => GetElement(forgotPasswordButtonSelector);

        public IWebElement GetForgotEmailButton() => GetElement(forgotEmailButtonSelector);

        public IWebElement GetFlashMessageInValidationModalWindow() => GetElement(flashMessageInModalWindowSelector);

        public IWebElement GetUserMenuAfterLogin() => GetElement(userMenuAfterLoginSelector);

        public void SubmitForm() => GetElement(loginButtonSelector).Click();

        public void ClickOkButtonInModalWindow()
        {
            GetElement(okButtonInModalWindowSelector).Click();
            webDriverWait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector(validationModalWindowSelector)));
        }

        public void LoginAsAdmin() => Login(config["admin_login"], config["admin_password"]);

        public void LoginAsFirstAdvisor() => Login(config["simple_advisor_email"], config["simple_advisor_password"]);

        public void LoginAsSecondAdvisor() => Login(config["simple_advisor_2_email"], config["simple_advisor_2_password"]);

        public void LoginAsFirstClient() => Login(config["simple_client_email"], config["simple_client_password"]);

        public void LoginAsSecondClient() => Login(config["simple_client_2_email"], config["simple_client_2_password"]);

        public void LoginAsMentor() => Login(config["simple_mentor_email"], config["simple_mentor_password"]);

        public void LoginAsRestrictedUser() => Login(config["simple_restricted_user_email"], config["simple_restricted_user_password"]);

        public void Logout() => driver.Navigate().GoToUrl(config["basic_global_url"] + "/users/logout");

        public void Login(string email, string password)
        {
            SetEmail(email);
            SetPassword(password);
            SubmitForm();
        }

        public void RemoveRequiredAttributesFromFields()
        {
            string script = "document.getElementById('UserUsername').removeAttribute('required'); " +
                            "document.getElementById('UserPassword').removeAttribute('required');";
            js.ExecuteScript(script);
        }

        public ForgotPasswordPage ClickForgotPasswordButton()
        {
            GetForgotPasswordButton().Click();
            return new ForgotPasswordPage(sb);
        }

        public ForgotEmailPage ClickForgotEmailButton()
        {
            GetForgotEmailButton().Click();
            return new ForgotEmailPage(sb);
        }
    }
}