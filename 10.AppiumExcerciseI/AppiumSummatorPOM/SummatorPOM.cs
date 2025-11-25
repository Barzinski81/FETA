using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;

namespace AppiumSummatorPOM
{
    public class SummatorPOM
    {
        private readonly AndroidDriver _driver;

        public SummatorPOM(AndroidDriver driver)
        {
            _driver = driver;
        }

        public IWebElement Field1 => _driver.FindElement(MobileBy.Id
            ("com.example.androidappsummator:id/editText1"));

        public IWebElement Field2 => _driver.FindElement(MobileBy.Id
            ("com.example.androidappsummator:id/editText2"));

        public IWebElement CalcButton => _driver.FindElement(MobileBy.Id
            ("com.example.androidappsummator:id/buttonCalcSum"));

        public IWebElement Result => _driver.FindElement(MobileBy.Id
            ("com.example.androidappsummator:id/editTextSum"));

        public string Calculator (string num1, string num2)
        {
            Field1.Clear();
            Field1.SendKeys(num1);
            Field2.Clear();
            Field2.SendKeys(num2);
            CalcButton.Click();

            return Result.Text;
        }
    }
}
