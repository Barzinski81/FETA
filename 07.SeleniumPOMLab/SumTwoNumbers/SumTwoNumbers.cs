using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumTwoNumbers
{
    public class SumTwoNumbers
    {
        private readonly IWebDriver driver;

        public SumTwoNumbers(IWebDriver driver)
        {
            this.driver = driver;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
        }

        const string pageUrl = "http://127.0.0.1:5500/sum-num.html";

        public IWebElement FieldNum1 => driver.FindElement(By.CssSelector("#number1"));
        public IWebElement FieldNum2 => driver.FindElement(By.CssSelector("#number2"));
        public IWebElement ButtonCalc => driver.FindElement(By.CssSelector("#calcButton"));
        public IWebElement ButtonReset => driver.FindElement(By.CssSelector("#resetButton"));
        public IWebElement Sum => driver.FindElement(By.CssSelector("#result"));

        public void OpenPage() 
        { 
            driver.Navigate().GoToUrl(pageUrl);        
        }

        public void ResetForm()
        {
            ButtonReset.Click();
        }

        public bool IsFormEmpty() 
        {
            return FieldNum1.Text + FieldNum2.Text + Sum.Text == "";
        }

        public string AddNumbers(string num1, string num2)
        {
            FieldNum1.SendKeys(num1);
            FieldNum2.SendKeys(num2);
            ButtonCalc.Click();
            string result = Sum.Text;
            return result;
        }
    }
}
