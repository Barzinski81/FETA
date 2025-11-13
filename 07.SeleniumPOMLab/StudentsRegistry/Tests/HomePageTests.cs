using StudentsRegistry.Pages;

namespace StudentsRegistry.Tests
{
    public class HomePageTests : BaseTests
    {
        [Test, Order(1)]
        public void Test_HomePageContent()
        {
            var page = new HomePage(driver);
            page.Open();

            Assert.Multiple(() =>
            {
                Assert.That(page.GetPageTitle(), Is.EqualTo("MVC Example"));
                Assert.That(page.GetPageHeadingText, Is.EqualTo("Students Registry"));
            });

            page.GetStudentsCount();
        }

        [Test, Order(2)]

        public void Test_HomePageLinks() 
        { 
            var homePage = new HomePage(driver);

            homePage.Open();
            homePage.LinkHomePage.Click();
            Assert.That(new HomePage(driver).IsOpen(), Is.True);

            homePage.Open();
            homePage.LinkAddStudentsPage.Click();
            Assert.That(new AddStudent(driver).IsOpen(), Is.True);

            homePage.Open();
            homePage.LinkViewStudentsPage.Click();
            Assert.That(new ViewStudents(driver).IsOpen(), Is.True);
        }
    }
}
