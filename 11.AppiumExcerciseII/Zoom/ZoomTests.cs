using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Interactions;

namespace Zoom
{
    public class Zoom
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

        [Test, Order(1)]
        public void ZoomInTest()
        {
            var views = driver.FindElement(MobileBy.AccessibilityId("Views"));
            views.Click();

            ScrollToText("WebView");

            var seekBar = driver.FindElement(MobileBy.AccessibilityId("WebView"));
            seekBar.Click();

            PerformZoom(516, 626, 548, 192, 502, 814, 463, 1181);
        }

        [Test, Order(2)]
        public void ZoomOutTest()
        {
            PerformZoom(636, 259, 438, 1004, 177, 1980, 368, 1329);
        }

        private void PerformZoom(int startX1, int startY1, int endX1, int endY1,
                                 int startX2, int startY2, int endX2, int endY2)
        {
            var finger1 = new PointerInputDevice(PointerKind.Touch);
            var finger2 = new PointerInputDevice(PointerKind.Touch);

            var zoom1 = new ActionSequence(finger1);
            zoom1.AddAction(finger1.CreatePointerMove(CoordinateOrigin.Viewport, startX1, startY1, TimeSpan.Zero));
            zoom1.AddAction(finger1.CreatePointerDown(MouseButton.Left));
            zoom1.AddAction(finger1.CreatePointerMove(CoordinateOrigin.Viewport, endX1, endY1, TimeSpan.FromSeconds(1)));
            zoom1.AddAction(finger1.CreatePointerUp(MouseButton.Left));

            var zoom2 = new ActionSequence(finger2);
            zoom2.AddAction(finger2.CreatePointerMove(CoordinateOrigin.Viewport, startX2, startY2, TimeSpan.Zero));
            zoom2.AddAction(finger2.CreatePointerDown(MouseButton.Left));
            zoom2.AddAction(finger2.CreatePointerMove(CoordinateOrigin.Viewport, endX2, endY2, TimeSpan.FromSeconds(1)));
            zoom2.AddAction(finger2.CreatePointerUp(MouseButton.Left));

            driver.PerformActions(new List<ActionSequence> { zoom1, zoom2 });
        }
    }
}