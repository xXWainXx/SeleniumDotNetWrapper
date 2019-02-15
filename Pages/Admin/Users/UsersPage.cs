using System;
using OpenQA.Selenium;
using GrowthWheel_AutoTests.Configuration;

namespace GrowthWheel_AutoTests.Pages.Admin.Users
{
    public class UsersPage : SeleniumTestBase
    {
        public UsersPage(SettingUpFixture sb)
            :base(sb)
        { }

        public static string URL = config["basic_url"] + "/users";

        protected string addUserButtonSelector = "a[href='/users/add']";
        protected string emailSearchFieldSelector = "#UserFemail";
        protected string submitSearchButtonSelector = "#filterTableMenuSubmit";
        protected string viewUserForDeleteButtonSelector = "a[href^='/users/view/']";
        protected string changeUserPasswordButtonSelector = "a[href^='/users/editpassword/']";
        protected string inputNewPasswordFieldSelector = "#UserPassword1";
        protected string inputNewPasswordConfirmationFieldSelector = "#UserPassword2";
        protected string submitChangePasswordFormButtonSelector = "#UserEditpasswordForm input[type='submit']";

        //needs to be moved to another page
        protected string deleteUserButtonSelector = "#delete-user";
        protected string deleteUserButtonConfirmSelector = "#confirmDialog button[type='submit']";

        public AddEditUserPage GoToAddUserPage()
        {
            GetElement(addUserButtonSelector).Click();
            return new AddEditUserPage(sb);
        }

        public void ChangeUserPassword(string email, string password)
        {
            driver.Navigate().GoToUrl(URL);

            InputValue(emailSearchFieldSelector, email);
            GetElement(submitSearchButtonSelector).Click();
            GetElement(changeUserPasswordButtonSelector).Click();
            InputValue(inputNewPasswordFieldSelector, password);
            InputValue(inputNewPasswordConfirmationFieldSelector, password);
            GetElement(submitChangePasswordFormButtonSelector).Click();
        }

        public void DeleteUser(string email)
        {
            driver.Navigate().GoToUrl(URL);

            InputValue(emailSearchFieldSelector, email);
            GetElement(submitSearchButtonSelector).Click();
            GetElement(viewUserForDeleteButtonSelector).Click();
            GetElement(deleteUserButtonSelector).Click();
            GetElement(deleteUserButtonConfirmSelector).Click();
        }
    }
}
