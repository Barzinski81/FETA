using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;

namespace ColorNotePOM
{
    public class ColorNotePOM
    {
        private AndroidDriver _driver;

        public ColorNotePOM(AndroidDriver driver)
        {
            _driver = driver;
        }

        public IWebElement SkipButton => _driver.FindElement(MobileBy.Id
            ("com.socialnmobile.dictapps.notepad.color.note:id/btn_start_skip"));

        public IWebElement AddNoteButton => _driver.FindElement(MobileBy.Id
            ("com.socialnmobile.dictapps.notepad.color.note:id/main_btn1"));

        public IWebElement CreateTextNote => _driver.FindElement(MobileBy.AndroidUIAutomator
            ("new UiSelector().text(\"Text\")"));

        public IWebElement WriteNoteContent => _driver.FindElement(MobileBy.Id
            ("com.socialnmobile.dictapps.notepad.color.note:id/edit_note"));

        public IWebElement BackButton => _driver.FindElement(MobileBy.Id
            ("com.socialnmobile.dictapps.notepad.color.note:id/back_btn"));
        
        public IWebElement NoteTitle(string title) => _driver.FindElement(By.XPath
            ($"//android.widget.TextView[@resource-id='com.socialnmobile.dictapps.notepad.color.note:id/title' and @text='{title}']"));
        public IWebElement EditButton => _driver.FindElement(MobileBy.Id
            ("com.socialnmobile.dictapps.notepad.color.note:id/edit_btn"));

        public IWebElement EditNoteField => _driver.FindElement(MobileBy.Id
            ("com.socialnmobile.dictapps.notepad.color.note:id/edit_note"));

        public IWebElement ThreeDots => _driver.FindElement(MobileBy.AccessibilityId("More"));

        public IWebElement DeleteButton => _driver.FindElement(MobileBy.AndroidUIAutomator
            ("new UiSelector().text(\"Delete\")"));

        public IWebElement DeleteConfirm => _driver.FindElement(MobileBy.Id("android:id/button1"));
       
        public void SkipTutorial()
        {
            SkipButton.Click();
        }

        public void AddNote() => AddNoteButton.Click ();

        public void CreateText() => CreateTextNote.Click ();

        public void WriteNote(string content) => WriteNoteContent.SendKeys(content);

        public void ClickBackButton() => BackButton.Click ();

        public void ClickEditButton() => EditButton.Click();

        public void EditTheNote(string content)
        {
            EditNoteField.Click();
            EditNoteField.Clear();
            EditNoteField.SendKeys(content);
        }

        public void OpenMenu() => ThreeDots.Click ();

        public void ClickDeleteButton() => DeleteButton.Click ();

        public void ConfirmDeletion() => DeleteConfirm.Click ();
    }
}
