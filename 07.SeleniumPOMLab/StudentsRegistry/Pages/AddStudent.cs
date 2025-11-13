using OpenQA.Selenium;

namespace StudentsRegistry.Pages
{
    public class AddStudent : BasePage
    {
        public AddStudent(IWebDriver driver) : base(driver) 
        {
            
        }

        public override string PageUrl => "http://localhost:8080/add-student";

        public IWebElement FieldName => driver.FindElement(By.CssSelector("#name"));
        public IWebElement FieldEmail => driver.FindElement(By.CssSelector("#email"));
        public IWebElement AddButton => driver.FindElement(By.XPath("//button[@type='submit']"));
        public IWebElement ErrorMessage => driver.FindElement(By.XPath("//div[contains(@style, 'background:red')]"));

        public void AddStudents(string name, string email)
        {
            this.FieldName.SendKeys(name);
            this.FieldEmail.SendKeys(email);
            this.AddButton.Click();
        }
    }
}
