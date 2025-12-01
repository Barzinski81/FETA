using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Interactions;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Interactions;

namespace SeekBar
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

        private void ScrollToText(string text)
        {
            driver.FindElement(MobileBy.AndroidUIAutomator(
                "new UiScrollable(new UiSelector().scrollable(true)).scrollIntoView(new UiSelector().text(\"" + text + "\"))"));
        }


        [Test]
        public void SeekBarTest()
        {
            var views = driver.FindElement(MobileBy.AccessibilityId("Views"));
            views.Click();

            ScrollToText("Seek Bar");

            var seekBar = driver.FindElement(MobileBy.AccessibilityId("Seek Bar"));
            seekBar.Click();

            MoveSeekBar(541, 230, 1038, 230);

            var seekBarValueElement = driver.FindElement(By.Id("io.appium.android.apis:id/progress"));
            var seekBarValueText = seekBarValueElement.Text;

            Assert.That(seekBarValueText, Is.EqualTo("100 from touch=true"));

        }

            private void MoveSeekBar(int startX, int startY, int endX, int endY)
            {
                var finger = new OpenQA.Selenium.Interactions.PointerInputDevice(PointerKind.Touch);
                var start = new System.Drawing.Point(startX, startY);
                var end = new System.Drawing.Point(endX, endY);
                var swipe = new ActionSequence(finger);

                swipe.AddAction(finger.CreatePointerMove(CoordinateOrigin.Viewport, startX, startY, TimeSpan.Zero));
                swipe.AddAction(finger.CreatePointerDown(MouseButton.Left));
                swipe.AddAction(finger.CreatePointerMove(CoordinateOrigin.Viewport, endX, endY, TimeSpan.FromSeconds(1)));
                swipe.AddAction(finger.CreatePointerUp(MouseButton.Left));

                driver.PerformActions(new List<ActionSequence> { swipe });

            }
        
    }
}