using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace StudentsRegistry.Tests
{
    public class BaseTests
    {
        protected IWebDriver driver;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            driver = new ChromeDriver();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown() 
        {
            driver.Quit();
            driver.Dispose();
        }

       
    }
}