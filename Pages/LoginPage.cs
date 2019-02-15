using System;
using OpenQA.Selenium;
using GrowthWheel_AutoTests.Configuration;

namespace GrowthWheel_AutoTests.Pages
{
    public class LoginPage : SeleniumTestBase
    {
        public LoginPage(SettingUpFixture sb)
            : base(sb)
        {
        }

        public static string URL = config["basic_url"];

        protected string emailFieldSelector = "#UserUsername";
        protected string passwordFieldSelector = "#UserPassword";
        protected string loginButtonSelector = ".submit-btn";

        public void SetEmail(string email) => InputValue(emailFieldSelector, email);

        public IWebElement GetEmail() => GetElement(emailFieldSelector);

        public void SetPassword(string password) => InputValue(passwordFieldSelector, password);

        public IWebElement GetPassword() => GetElement(passwordFieldSelector);

        public void SubmitLoginForm() => GetElement(loginButtonSelector).Click();

        public void LoginAsAdmin() => Login(config["admin_login"], config["admin_password"]);

        public void LoginAsFirstAdvisor() => Login(config["simple_advisor_email"], config["simple_advisor_password"]);

        public void LoginAsSecondAdvisor() => Login(config["simple_advisor_2_email"], config["simple_advisor_2_password"]);

        public void LoginAsFirstClient() => Login(config["simple_client_email"], config["simple_client_password"]);

        public void LoginAsSecondClient() => Login(config["simple_client_2_email"], config["simple_client_2_password"]);

        public void Login(string email, string password)
        {
            SetEmail(email);
            SetPassword(password);
            SubmitLoginForm();
        }
    }
}