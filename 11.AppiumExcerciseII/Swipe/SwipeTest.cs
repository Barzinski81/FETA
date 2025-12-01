using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Interactions;

namespace Swipe
{
    [TestFixture]
    public class Swipe
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
        [Test]
        public void SwipeTest()
        {
            var views = driver.FindElement(MobileBy.AccessibilityId("Views"));
            views.Click();

            var gallery = driver.FindElement(MobileBy.AccessibilityId("Gallery"));
            gallery.Click();

            var photos = driver.FindElement(MobileBy.AccessibilityId("1. Photos"));
            photos.Click();

            var pic1 = driver.FindElements(By.ClassName("android.widget.ImageView"))[0];

            var action = new Actions(driver);
            var swipe = action.ClickAndHold(pic1)
                              .MoveByOffset(-200, 0)
                              .Release()
                              .Build();

            swipe.Perform();

            var pic3 = driver.FindElements(By.ClassName("android.widget.ImageView"))[2];
            Assert.That(pic3, Is.Not.Null, "The hitd picture was not found.");
        }
    }
}