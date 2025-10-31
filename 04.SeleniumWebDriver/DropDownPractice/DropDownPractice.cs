using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

namespace DropDownPractice
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
        public void Test_DropDown()
        {
            string path = Directory.GetCurrentDirectory() + "/manufacturer.txt";

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            SelectElement manufDropdown = new SelectElement(driver.FindElement(By.Name("manufacturers_id")));

            IList<IWebElement> allManufacturers = manufDropdown.Options;

            List<string> manufNames = new List<string>();

            foreach (IWebElement manufName in allManufacturers)
            {
                manufNames.Add(manufName.Text);
            }
            
            manufNames.RemoveAt(0);

            foreach (string mname in manufNames)
            {
                manufDropdown.SelectByText(mname);
                manufDropdown = new SelectElement(driver.FindElement(By.XPath("//select[@name='manufacturers_id']")));

                if (driver.PageSource.Contains("There are no products available in this category."))
                {
                    File.AppendAllText(path, $"The manufacturer {mname} has no products\n");
                }
                else
                {
                    // Create the table element
                    IWebElement productTable = driver.FindElement(By.ClassName("productListingData"));

                    // Fetch all table rows
                    File.AppendAllText(path, $"\n\nThe manufacturer {mname} products are listed--\n");
                    ReadOnlyCollection<IWebElement> rows = productTable.FindElements(By.XPath("//tbody/tr"));

                    // Print the product information in the file
                    foreach (IWebElement row in rows)
                    {
                        File.AppendAllText(path, row.Text + "\n");
                    }
                }
            }
        }
    }
}