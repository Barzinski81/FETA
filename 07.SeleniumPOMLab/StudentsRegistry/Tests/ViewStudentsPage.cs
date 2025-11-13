using StudentsRegistry.Pages;

namespace StudentsRegistry.Tests
{
    public class ViewStudentsPage : BaseTests
    {
        [Test, Order(1)]
        public void Test_ViewStudentsPageContent() 
        {
            var page = new ViewStudents(driver);
            page.Open();

            Assert.Multiple(() =>
            {
                Assert.That(page.GetPageTitle(), Is.EqualTo("Students"));
                Assert.That(page.GetPageHeadingText(), Is.EqualTo("Registered Students"));
            });

            var students = page.GetRegisterStudents();

            foreach (var student in students)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(student.IndexOf("(") > 0, Is.True);
                    Assert.That(student.LastIndexOf(")") == student.Length -1 , Is.True);
                });
            }         
        }

        [Test, Order(2)]

        public void Test_ViewStudentsPageLinks()
        {
            var viewStudentsPage = new ViewStudents(driver);

            viewStudentsPage.Open();
            viewStudentsPage.LinkHomePage.Click();
            Assert.That(new HomePage(driver).IsOpen(), Is.True);

            viewStudentsPage.Open();
            viewStudentsPage.LinkAddStudentsPage.Click();
            Assert.That(new AddStudent(driver).IsOpen(), Is.True);

            viewStudentsPage.Open();
            viewStudentsPage.LinkViewStudentsPage.Click();
            Assert.That(new ViewStudents(driver).IsOpen(), Is.True);
        }
    }
}
