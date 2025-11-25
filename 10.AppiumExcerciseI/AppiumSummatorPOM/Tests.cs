using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;

namespace AppiumSummatorPOM
{
    public class Tests
    {
        [TestFixture]
        public class CalculatorTests
        {
            private AndroidDriver driver;
            private AppiumLocalService appiumLocalService;
            private SummatorPOM summatorPOM;

            [OneTimeSetUp]
            public void Setup()
            {
                //This starts Appium server no need to start it via CMD
                appiumLocalService = new AppiumServiceBuilder()
                    .WithIPAddress("127.0.0.1")
                    .UsingPort(4723)
                    .Build();
                
                appiumLocalService.Start();

                var androidOptions = new AppiumOptions();
                androidOptions.PlatformName = "Android";
                androidOptions.AutomationName = "UIAutomator2";
                androidOptions.DeviceName = "Pixel9";
                androidOptions.App = "C:\\Users\\Ivan\\Desktop\\com.example.androidappsummator.apk";

                driver = new AndroidDriver(appiumLocalService.ServiceUrl, androidOptions);
                summatorPOM = new SummatorPOM(driver);
            }

            [OneTimeTearDown]
            public void TearDown()
            {
                driver?.Quit();
                driver?.Dispose();
                appiumLocalService.Dispose();
            }

            [Test, Order(1)]
            public void Test_ValidData_Pom()
            {
                var result = summatorPOM.Calculator("5", "6");
                Assert.That(result, Is.EqualTo("11"));
            }

            [Test, Order(2)]
            public void Test_InvalidData_Pom()
            {
                var result = summatorPOM.Calculator(".", "6");
                Assert.That(result, Is.EqualTo("error"));
            }
        }
    }
}