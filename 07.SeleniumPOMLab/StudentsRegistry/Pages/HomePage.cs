using OpenQA.Selenium;

namespace StudentsRegistry.Pages
{
    public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver) : base(driver) 
        {
            
        }

        public override string PageUrl => "http://localhost:8080/";

        public IWebElement ElementStudentsCount => driver.FindElement(By.CssSelector("body > p > b"));

        public int GetStudentsCount()
        {
            string studentCountText = this.ElementStudentsCount.Text;
            return int.Parse(studentCountText);
        }
    }
}
