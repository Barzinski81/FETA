using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;

namespace ColorNotePOM
{
    public class Tests
    {
        private AndroidDriver _driver;
        private AppiumLocalService appiumLocalService;

        private ColorNotePOM colorNotePOM;

        string text = "Test";
        string modifiedText = "Modified";

        [OneTimeSetUp]
        public void RunBeforeAnyTest()
        {
            appiumLocalService = new AppiumServiceBuilder()
                .WithIPAddress("127.0.0.1")
                .UsingPort(4237)
                .Build();
            appiumLocalService.Start();

            var androidOptions = new AppiumOptions();
            androidOptions.PlatformName = "Android";
            androidOptions.AutomationName = "UIAutomator2";
            androidOptions.DeviceName = "Medium_Phone_API_36.1";
            androidOptions.App = "C:\\Users\\Ivan\\Desktop\\Notepad.apk";
            androidOptions.AddAdditionalAppiumOption("autoGrantPermissions", true);

            _driver = new AndroidDriver(appiumLocalService.ServiceUrl, androidOptions);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            colorNotePOM = new ColorNotePOM(_driver);
            colorNotePOM.SkipTutorial();
        }

        [OneTimeTearDown]
        public void RunAfterAnyTest()
        {
            _driver?.Quit();
            _driver?.Dispose();
            appiumLocalService?.Dispose();
        }

        [Test, Order(1)]
        public void Test_CreateNote()
        {
            colorNotePOM.AddNote();
            colorNotePOM.CreateText();
            colorNotePOM.WriteNote(text);
            colorNotePOM.ClickBackButton();
            colorNotePOM.ClickBackButton();

            var note = colorNotePOM.NoteTitle(text);

            Assert.That(note, Is.Not.Null, "The note was not created succesfully.");

            Assert.That(note.Text, Is.EqualTo(text), "The note content does not match.");
        }

        [Test, Order(2)]
        public void Test_EditTheCreatedNote()
        {
            colorNotePOM.NoteTitle(text).Click();
           
            colorNotePOM.ClickEditButton();
            colorNotePOM.EditTheNote(modifiedText);


            colorNotePOM.ClickBackButton();
            colorNotePOM.ClickBackButton();

            var modifiedNote = colorNotePOM.NoteTitle(modifiedText);

            Assert.That(modifiedNote.Text, Is.EqualTo(modifiedText));
        }

        [Test, Order(3)]
        public void Test_DeleteTheEditedNote()
        {
            colorNotePOM.NoteTitle(modifiedText).Click();
            colorNotePOM.OpenMenu();
            colorNotePOM.ClickDeleteButton();
            colorNotePOM.ConfirmDeletion();

            var deletedNote = _driver.FindElements(By.XPath($"//android.widget.TextView[@text={modifiedText}]"));
            Assert.That(deletedNote, Is.Empty, "The note was not deleted successfully.");
        }
    }

    //TO DO -> replace test and modified with string variables !!!
}