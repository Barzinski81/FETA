using NuGet.Frameworks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SearchProductWithExplicitWait
{
    public class Tests
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

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

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);

            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                IWebElement buyNowLink = wait.Until(e => e.FindElement(By.LinkText("Buy Now")));

                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                buyNowLink.Click();

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

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);

            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                IWebElement buyNowLink = wait.Until(e => e.FindElement(By.LinkText("Buy Now")));

                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                buyNowLink.Click();

                Assert.Fail("The 'Buy Now' link was found for a non-existing product.");
            }

            catch (WebDriverTimeoutException)
            {
                Assert.Pass("Expected WebDriverTimeoutException was thrown");
            }

            catch (Exception ex)
            {
                Assert.Fail($"Unexpected exception: {ex.Message}");
            }

            finally
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            }

        }
    }
}