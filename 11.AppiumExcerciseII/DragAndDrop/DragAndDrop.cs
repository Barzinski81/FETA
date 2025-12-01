using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;

namespace DragAndDrop
{
    [TestFixture]
    public class DragAndDrop
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
        public void DragAndDropTest()
        {
            var views = driver.FindElement(MobileBy.AccessibilityId("Views"));
            views.Click();

            var dragAndDrop = driver.FindElement(MobileBy.AccessibilityId("Drag and Drop"));
            dragAndDrop.Click();

            var drag = driver.FindElement(By.Id("io.appium.android.apis:id/drag_dot_1"));
            var drop = driver.FindElement(By.Id("io.appium.android.apis:id/drag_dot_2"));

            var scriptArgs = new Dictionary<string, object>
            {
                { "elementId", drag.Id },
                { "endX", drop.Location.X + (drop.Size.Width / 2 ) },
                { "endY", drop.Location.Y + (drop.Size.Height / 2 ) },
                { "speed", 2500 }
            };

            driver.ExecuteScript("mobile: dragGesture", scriptArgs);

            var dropSuccessMessage = driver.FindElement(By.Id("io.appium.android.apis:id/drag_result_text"));
            Assert.That(dropSuccessMessage.Text, Is.EqualTo("Dropped!"), "The drag and drop action failed.");
        }
    }
}