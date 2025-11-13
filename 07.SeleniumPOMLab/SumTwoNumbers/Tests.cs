using OpenQA.Selenium.Chrome;

namespace SumTwoNumbers
{
    public class Tests
    {
        private ChromeDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [TearDown]
        public void Close()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
        }

        [Test, Order(1)]
        public void Test_SumValidNumbers()
        {
            var sumpage = new SumTwoNumbers(driver);
            sumpage.OpenPage();
            var result = sumpage.AddNumbers("3", "4");
            Assert.That(result, Is.EqualTo("Sum: 7"));
        }

        [Test, Order(2)]
        public void Test_SumInvalidNumbers()
        {
            var sumpage = new SumTwoNumbers(driver);
            sumpage.OpenPage();
            var result = sumpage.AddNumbers("text", "test");
            Assert.That(result, Is.EqualTo("Sum: invalid input"));
        }

        [Test, Order(3)]
        public void Test_AddTwoNumbersAndReset()
        {
            var sumpage = new SumTwoNumbers(driver);
            sumpage.OpenPage();
            var result = sumpage.AddNumbers("3", "4");
            bool isFormEmpty = sumpage.IsFormEmpty();
            Assert.That(isFormEmpty, Is.False);

            sumpage.ResetForm();
            isFormEmpty = sumpage.IsFormEmpty();
            Assert.That(isFormEmpty, Is.True);
        }
    }
}