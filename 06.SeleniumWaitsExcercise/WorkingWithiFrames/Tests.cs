using NuGet.Frameworks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Collections.ObjectModel;

namespace WorkingWithiFrames
{
    public class Tests
    {
        IWebDriver driver;
        WebDriverWait wait;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            driver.Navigate().GoToUrl("https://codepen.io/pervillalva/full/abPoNLd");

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Dispose();
        }

        [Test, Order(1)]
        public void TestFrameByIndex()
        {
            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.TagName("iframe")));

            var dropdownButton = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".dropbtn")));
            dropdownButton.Click();

            var dropdownLinks = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy
                        (By.CssSelector(".dropdown-content a")));

            foreach (var link in dropdownLinks)
            {
                Console.WriteLine(link.Text);
                Assert.That(link.Displayed, Is.True, "Link inside the dropdown is not diplayed as expected.");
            }

            driver.SwitchTo().DefaultContent();
        }

        [Test, Order(2)]
        public void TestFrameById()
        {
            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt("result"));

            var dropdownButton = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".dropbtn")));
            dropdownButton.Click();

            var dropdownLinks = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy
                        (By.CssSelector(".dropdown-content a")));

            foreach (var link in dropdownLinks)
            {
                Console.WriteLine(link.Text);
                Assert.That(link.Displayed, Is.True, "Link inside the dropdown is not diplayed as expected.");
            }

            driver.SwitchTo().DefaultContent();
        }

        [Test, Order(3)]
        public void TestFrameByWebElement()
        {
            var frameElement = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#result")));

            driver.SwitchTo().Frame(frameElement);

            var dropdownButton = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".dropbtn")));
            dropdownButton.Click();

            var dropdownLinks = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy
                        (By.CssSelector(".dropdown-content a")));

            foreach (var link in dropdownLinks)
            {
                Console.WriteLine(link.Text);
                Assert.That(link.Displayed, Is.True, "Link inside the dropdown is not diplayed as expected.");
            }

            driver.SwitchTo().DefaultContent();
        }
    }
}