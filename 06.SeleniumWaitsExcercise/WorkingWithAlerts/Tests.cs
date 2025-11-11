using NuGet.Frameworks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Collections.ObjectModel;

namespace WorkingWithAlerts
{
    public class Tests
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/javascript_alerts");          
        }

        [TearDown]
        public void TearDown()
        {
            driver.Dispose();
        }

        [Test, Order(1)]
        public void HandleBasicAlert()
        {
            driver.FindElement(By.XPath("//button[contains(text(), 'Click for JS Alert')]")).Click();

            IAlert alert = driver.SwitchTo().Alert();

            Assert.That(alert.Text, Is.EqualTo("I am a JS Alert"), "Alert text is not as expected.");

            alert.Accept();

            IWebElement resultElement = driver.FindElement(By.Id("result"));
            Assert.That(resultElement.Text, Is.EqualTo("You successfully clicked an alert"),
                        "Result message is not as expected");
        }

        [Test, Order(2)]
        public void HandleConfirmAlert()
        {
            driver.FindElement(By.XPath("//button[contains(text(), 'Click for JS Confirm')]")).Click();

            IAlert alert = driver.SwitchTo().Alert();

            Assert.That(alert.Text, Is.EqualTo("I am a JS Confirm"), "Alert text is not as expected.");

            alert.Accept();

            IWebElement resultElement = driver.FindElement(By.Id("result"));
            Assert.That(resultElement.Text, Is.EqualTo("You clicked: Ok"),
                        "Result message is not as expected");

            driver.FindElement(By.XPath("//button[contains(text(), 'Click for JS Confirm')]")).Click();

            alert = driver.SwitchTo().Alert();

            Assert.That(alert.Text, Is.EqualTo("I am a JS Confirm"), "Alert text is not as expected.");

            alert.Dismiss();

            resultElement = driver.FindElement(By.Id("result"));
            Assert.That(resultElement.Text, Is.EqualTo("You clicked: Cancel"),
                        "Result message is not as expected");
        }

        [Test, Order(3)]
        public void HandlePromptAlert()
        {
            driver.FindElement(By.XPath("//button[contains(text(), 'Click for JS Prompt')]")).Click();

            IAlert alert = driver.SwitchTo().Alert();

            Assert.That(alert.Text, Is.EqualTo("I am a JS prompt"), "Alert text is not as expected.");

            string inputText = "Test Message!";
            alert.SendKeys(inputText);

            alert.Accept();

            IWebElement resultElement = driver.FindElement(By.Id("result"));
            Assert.That(resultElement.Text, Is.EqualTo($"You entered: {inputText}"),
                        "Result message is not as expected");
        }
    }
}