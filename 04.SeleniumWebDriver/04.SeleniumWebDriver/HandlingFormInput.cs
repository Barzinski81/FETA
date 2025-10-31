using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace HandlingFormInput
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

        [Test]
        public void Test_UserRegistration()
        {
            driver.FindElement(By.LinkText("My Account")).Click();
            driver.FindElement(By.LinkText("Continue")).Click();
            Random random = new Random();

            int randomNumber = random.Next(1000, 9999);
            string randomEmail = $"email_{randomNumber}@mail.com";
            string randomFirstName = $"First_Name_{randomNumber}";
            string randomLastName = $"Last_Name_{randomNumber}";


            driver.FindElement(By.CssSelector("input[type='radio'][value='m']")).Click();
            driver.FindElement(By.Name("firstname")).SendKeys(randomFirstName);
            driver.FindElement(By.Name("lastname")).SendKeys(randomLastName);
            driver.FindElement(By.Name("dob")).SendKeys("08/12/1991");
            driver.FindElement(By.Name("email_address")).SendKeys(randomEmail);

            driver.FindElement(By.Name("company")).SendKeys(randomNumber.ToString());

            driver.FindElement(By.Name("street_address")).SendKeys("22 Acacia Avenue");
            driver.FindElement(By.Name("suburb")).SendKeys("London");
            driver.FindElement(By.Name("postcode")).SendKeys("25052");
            driver.FindElement(By.Name("city")).SendKeys("London");
            driver.FindElement(By.Name("state")).SendKeys("London");

            new SelectElement(driver.FindElement(By.Name("country"))).SelectByText("United Kingdom");

            driver.FindElement(By.Name("telephone")).SendKeys("+4489641464");
            driver.FindElement(By.Name("fax")).SendKeys("+4489641464");
            driver.FindElement(By.Name("newsletter")).Click();

            driver.FindElement(By.Name("password")).SendKeys("123456");
            driver.FindElement(By.Name("confirmation")).SendKeys("123456");

            driver.FindElement(By.Id("tdb4")).Click();

            Assert.IsTrue(driver.PageSource.Contains("Your Account Has Been Created!"), "Account creation failed");

            driver.FindElement(By.LinkText("Log Off")).Click();
            driver.FindElement(By.LinkText("Continue")).Click();

            Console.WriteLine($"User Account Created with email: {randomEmail}");

        }
    }
}