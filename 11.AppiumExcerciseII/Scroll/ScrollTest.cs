using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;

namespace Scroll
{
    [TestFixture]
    public class Scroll
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
            androidOptions.App = "C:\\Users\\Ivan\\Desktop\\ApiDemos-debug.apk";

            driver = new AndroidDriver(appiumLocalService.ServiceUrl, androidOptions);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

        }

        [OneTimeTearDown]
        public void RunAfterAnyTest()
        {
            driver?.Quit();
            driver?.Dispose();
            appiumLocalService?.Dispose();
        }

        private void ScrollToText(string text)
        {
            driver.FindElement(MobileBy.AndroidUIAutomator(
                "new UiScrollable(new UiSelector().scrollable(true)).scrollIntoView(new UiSelector().text(\"" + text + "\"))"));
        }

        [Test]
        public void ScrollTest()
        {
            var views = driver.FindElement(MobileBy.AccessibilityId("Views"));
            views.Click();

            ScrollToText("Lists");

            var lists = driver.FindElement(MobileBy.AccessibilityId("Lists"));

            Assert.That(lists, Is.Not.Null, "The 'Lists' element was not found.");

            lists.Click();

            var elementInList = driver.FindElement(MobileBy.AccessibilityId("10. Single choice list"));
            Assert.That(elementInList, Is.Not.Null, "The expected element was not found.");
        }
    }
}