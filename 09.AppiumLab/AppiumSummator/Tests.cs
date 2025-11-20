using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;

namespace AppiumSummator
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
            androidOptions.DeviceName = "Pixel9";
            androidOptions.App = "C:\\Users\\Ivan\\Desktop\\com.example.androidappsummator.apk";

            driver = new AndroidDriver(appiumLocalService.ServiceUrl, androidOptions);
        }

        [OneTimeTearDown]
        public void RunAfterAnyTest()
        {
            driver?.Quit();
            driver?.Dispose();
            appiumLocalService?.Dispose();
        }

        [Test, Order(1)]
        public void Test_ValidData()
        {
            var field1 = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));
            field1.Clear();
            field1.SendKeys("5");

            var field2 = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText2"));
            field2.Clear();
            field2.SendKeys("6");

            var calcButton = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/buttonCalcSum"));
            calcButton.Click();

            var result = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum")).Text;

            Assert.That(result, Is.EqualTo("11"));
        }

        [Test, Order(2)]
        public void Test_InvalidData()
        {
            var field1 = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));
            field1.Clear();
            field1.SendKeys(".");

            var field2 = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText2"));
            field2.Clear();
            field2.SendKeys("6");

            var calcButton = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/buttonCalcSum"));
            calcButton.Click();

            var result = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum")).Text;

            Assert.That(result, Is.EqualTo("error"));
        }
    }
}