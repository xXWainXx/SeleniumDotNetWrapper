using System;
using OpenQA.Selenium;
using GrowthWheel_AutoTests.Configuration;

namespace GrowthWheel_AutoTests.Pages.Advisor.MyToolbox
{
    class PersonalToolsPage : SeleniumTestBase
    {
        public PersonalToolsPage(SettingUpFixture sb)
            : base(sb)
        { }

        public static string URL = config["basic_global_url"] + "/files";

        //personal tools table
        protected string addToolButtonSelector = "#UploadFile";
        protected string addLinkButtonSelector = "#AddLink";
        protected string toolNameListSelector = "#category_body .cat-list-item-title a[target=_blank]";
        protected string toolThumbnailListSelector = "#category_body .cat-list-item .cat-list-item-img img.listcattablewidth";
        protected string toolDeleteButtonsListSelector = "#category_body .cat-list-item .fileEmailCart a[data-original-title='Remove File']";
        protected string toolDownloadButtonsListSelector = "#category_body .cat-list-item .fileEmailCart a[data-original-title='Download']";

        //addTool PopUp
        protected string addToolPopupTitleSelector = "#UploadForm h4.modal-title";
        protected string nameFieldSelector = "#Filename";
        protected string uploadFileFieldSelector = "#UploadFileField input[type='file']";
        protected string crossButtonSelector = "#UploadForm .modal-cross.no";
        protected string cancelButtonSelector = "#UploadForm .modal-footer .btn-cancel";
        protected string saveButtonSelector = "#UploadForm .modal-footer button[type='submit']";
        protected string okButtonSelector = "#confirmDialog .modal-footer button[type='submit']";

        //person tools table
        public IWebElement GetAddToolButton() => GetElement(addToolButtonSelector);

        public IWebElement GetToolNameField(string name) => GetElementInListByText(toolNameListSelector, name);

        public IWebElement GetToolDownloadButton(string name)
        {
            var elementIndex = GetElementIndexInListByText(toolNameListSelector, name);
            string selector = "#category_body .cat-list-item:nth-child(" + elementIndex + ") .fileEmailCart a[data-original-title='Download']";
            try
            {
                return GetElement(selector);
            }
            catch
            {
                throw new Exception("Download button not found. Maybe you tries to download link instead of tool");
            }
        }

        public IWebElement GetDeleteButton(string name)
        {
            var elementIndex = GetElementIndexInListByText(toolNameListSelector, name);
            string selector = "#category_body .cat-list-item:nth-child(" + elementIndex + ") .fileEmailCart a[data-original-title='Remove File']";
            try
            {
                return GetElement(selector);
            }
            catch
            {
                throw new Exception("Delete button not found");
            }
        }

        //addTool PopUp
        public IWebElement GetAddToolPopupTitle() => GetElement(addToolPopupTitleSelector);

        public void SetName(string name) => InputValue(nameFieldSelector, name);
        public IWebElement GetNameField() => GetElement(nameFieldSelector);
        
        public void UploadFile(string url) => InputValue(uploadFileFieldSelector, url);
        public IWebElement GetUploadFileField() => GetElement(uploadFileFieldSelector);

        public IWebElement GetCancelButton() => GetElement(cancelButtonSelector);

        public IWebElement GetCrossButton() => GetElement(crossButtonSelector);

        public IWebElement GetSaveButton() => GetElement(saveButtonSelector);

        public IWebElement GetOkButton() => GetElement(okButtonSelector);
    }
}
