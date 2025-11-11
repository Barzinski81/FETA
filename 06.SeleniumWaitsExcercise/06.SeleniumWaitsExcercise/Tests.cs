using NuGet.Frameworks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SearchProductWithImplicitWait
{
    public class Tests
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();

            driver.Navigate().GoToUrl("http://practice.bpbonline.com/");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Dispose();
        }

        [Test, Order(1)]
        public void Search_Product_Keyboard_ShouldAddToCart()
        {
            driver.FindElement(By.Name("keywords")).SendKeys("keyboard");

            driver.FindElement(By.XPath("//input[@alt='Quick Find']")).Click();

            try
            {
                driver.FindElement(By.LinkText("Buy Now")).Click();

                Assert.That(driver.PageSource, Does.Contain("keyboard"),
                            "The product 'keyboard' was not found in the cart.");
                Console.WriteLine("Scenario completed");
            }

            catch (Exception ex)
            {
                Assert.Fail($"Unexpected exception: {ex.Message}");
            }
            
        }

        [Test, Order(2)]
        public void Search_Product_Junk_ShouldThrowNoSuchElementException()
        {
            driver.FindElement(By.Name("keywords")).SendKeys("junk");

            driver.FindElement(By.XPath("//input[@alt='Quick Find']")).Click();

            try
            {
                driver.FindElement(By.LinkText("Buy Now")).Click();  
            }

            catch (NoSuchElementException ex)
            {
                Assert.Pass("Expected NoSuchElementException was thrown");
                Console.WriteLine($"Timeout - {ex.Message}");
            }

            catch (Exception ex)
            {
                Assert.Fail($"Unexpected exception: {ex.Message}");
            }

        }
    }
}