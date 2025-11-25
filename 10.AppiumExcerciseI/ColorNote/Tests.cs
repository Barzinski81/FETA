using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;

namespace ColorNote
{
    public class Tests
    {
        private AndroidDriver driver;
        private AppiumLocalService appiumLocalService;

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

            driver = new AndroidDriver(appiumLocalService.ServiceUrl, androidOptions);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            var skipButton = driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/btn_start_skip"));
            skipButton.Click();
        }

        [OneTimeTearDown]
        public void RunAfterAnyTest()
        {
            driver?.Quit();
            driver?.Dispose();
            appiumLocalService?.Dispose();
        }

        [Test, Order(1)]
        public void Test_CreateNote()
        {
            var AddNote = driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/main_btn1"));
            AddNote.Click();

            var createTextNote = driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"Text\")"));
            createTextNote.Click();

            var writeNote = driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/edit_note"));
            writeNote.SendKeys("Test_1");

            var back = driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/back_btn"));
            back.Click();
            back.Click();

            var note = driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/title"));

            Assert.That(note, Is.Not.Null, "The note was not created succesfully.");

            Assert.That(note.Text, Is.EqualTo("Test_1"), "The note content does not match.");
        }

        [Test, Order(2)]
        public void Test_EditTheCreatedNote()
        {
            var note = driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"Test_1\")"));
            note.Click();

            var editButton = driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/edit_btn"));
            editButton.Click();

            var editNote = driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/edit_note"));
            editNote.Click();
            editNote.Clear();
            editNote.SendKeys("Modified");

            var back = driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/back_btn"));
            back.Click();
            back.Click();

            var modifiedNote = driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"Modified\")"));

            Assert.That(modifiedNote.Text, Is.EqualTo("Modified"));
        }

        [Test, Order(3)]
        public void Test_DeleteTheEditedNote()
        {
            var note = driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"Modified\")"));
            note.Click();

            var threeDots = driver.FindElement(MobileBy.AccessibilityId("More"));
            threeDots.Click();

            var deleteButton = driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"Delete\")"));
            deleteButton.Click();

            var deleteConfirm = driver.FindElement(MobileBy.Id("android:id/button1"));
            deleteConfirm.Click();

            var deletedNote = driver.FindElements(By.XPath("//android.widget.TextView[@text='Modified']"));
            Assert.That(deletedNote, Is.Empty, "The note was not deleted successfully.");
        }
    }
}