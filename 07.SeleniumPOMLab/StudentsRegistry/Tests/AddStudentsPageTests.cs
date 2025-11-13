using OpenQA.Selenium.Support.UI;
using StudentsRegistry.Pages;

namespace StudentsRegistry.Tests
{
     public class AddStudentsPageTests : BaseTests
    {
        [Test, Order(1)]
        public void Test_AddStudentsPageContent()
        {
            var page = new AddStudent(driver);
            page.Open();
            Assert.Multiple(() =>
            {
                Assert.That(page.GetPageTitle(), Is.EqualTo("Add Student"));
                Assert.That(page.GetPageHeadingText(), Is.EqualTo("Register New Student"));
                Assert.That(page.FieldName.Text, Is.EqualTo(""));
                Assert.That(page.FieldEmail.Text, Is.EqualTo(""));
                Assert.That(page.AddButton.Text, Is.EqualTo("Add"));
            });
        }

        [Test, Order(2)]
        public void Test_AddStudentsPageLinks()
        {
            var addStudentsPage = new AddStudent(driver);

            addStudentsPage.Open();
            addStudentsPage.LinkHomePage.Click();
            Assert.That(new HomePage(driver).IsOpen(), Is.True);

            addStudentsPage.Open();
            addStudentsPage.LinkAddStudentsPage.Click();
            Assert.That(new AddStudent(driver).IsOpen(), Is.True);

            addStudentsPage.Open();
            addStudentsPage.LinkViewStudentsPage.Click();
            Assert.That(new ViewStudents(driver).IsOpen(), Is.True);
        }

        [Test, Order(3)]
        public void Test_AddStudentsPageAddValidStudent()
        {
            var page = new AddStudent(driver);
            page.Open();

            string name = GenerateRandomName();
            string email = GenerateRandomEmail(name);

            page.AddStudents(name, email);
            var viewStudents = new ViewStudents(driver);

            new WebDriverWait(driver, TimeSpan.FromSeconds(5))
            .Until(d => d.Url == viewStudents.PageUrl);

            Assert.That(viewStudents.IsOpen(), Is.True);

            var students = viewStudents.GetRegisterStudents();
            string newStudent = $"{name} ({email})";
            Assert.That(students, Does.Contain(newStudent));
        }

        [Test, Order(4)]
        public void Test_AddStudentsPageAddInvalidStudent()
        {
            var page = new AddStudent(driver);
            page.Open();

            string name = "";
            string email = GenerateRandomEmail(name);

            page.AddStudents(name, email);

            Assert.Multiple(() =>
            {
                Assert.That(page.IsOpen(), Is.True);
                Assert.That(page.ErrorMessage.Text, Does.Contain("Cannot add student"));
            });
        }

        private string GenerateRandomName()
        {
            var random = new Random();
            int number = random.Next(100, 999);
            return $"Name_{number}";
        }

        private string GenerateRandomEmail(string name) 
        {
            var random = new Random();
            return $"{name.ToLower()}_{random.Next(100, 999)}@mail.com";
        }
    }
}
