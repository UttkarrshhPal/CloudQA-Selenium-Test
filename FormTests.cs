using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using OpenQA.Selenium.Support.Extensions;

namespace CloudQATests
{
    [TestClass]
    public class FormTests
    {
        private IWebDriver? driver;
        private WebDriverWait? wait;
        private const string BaseUrl = "https://app.cloudqa.io/home/AutomationPracticeForm";

        [TestInitialize]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--disable-extensions");
            
            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            // Increase wait time to 20 seconds
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        [TestMethod]
        public void TestFormFields()
        {
            try
            {
                // Navigate to the form
                driver?.Navigate().GoToUrl(BaseUrl);

                // Wait for page to load completely
                wait?.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));

                // Test 1: First Name field
                TestFirstNameField();

                // Test 2: Email field
                TestEmailField();

                // Test 3: Gender radio buttons
                TestGenderRadioButtons();
            }
            catch (Exception ex)
            {
                // Take screenshot on failure
                if (driver != null)
                {
                    var screenshot = driver.TakeScreenshot();
                    screenshot.SaveAsFile("test-failure.png");
                }
                throw new Exception($"Test failed: {ex.Message}", ex);
            }
        }

        private void TestFirstNameField()
        {
            // Try multiple selectors for the first name field
            var firstNameField = wait?.Until(d => 
            {
                try
                {
                    // Try different selectors
                    var element = d.FindElement(By.CssSelector("input[name='firstName']"));
                    if (element != null && element.Displayed) return element;
                }
                catch { }

                try
                {
                    var element = d.FindElement(By.CssSelector("input[type='text']"));
                    if (element != null && element.Displayed) return element;
                }
                catch { }

                try
                {
                    var element = d.FindElement(By.XPath("//input[contains(@placeholder, 'First Name')]"));
                    if (element != null && element.Displayed) return element;
                }
                catch { }

                return null;
            });

            Assert.IsNotNull(firstNameField, "First Name field should be found");
            Assert.IsTrue(firstNameField?.Displayed ?? false, "First Name field should be displayed");
            
            // Test input
            firstNameField?.Clear();
            firstNameField?.SendKeys("John");
            Assert.AreEqual("John", firstNameField?.GetAttribute("value"), "First Name field should accept input");
        }

        private void TestEmailField()
        {
            // Try multiple selectors for the email field
            var emailField = wait?.Until(d => 
            {
                try
                {
                    var element = d.FindElement(By.CssSelector("input[type='email']"));
                    if (element != null && element.Displayed) return element;
                }
                catch { }

                try
                {
                    var element = d.FindElement(By.XPath("//input[contains(@placeholder, 'Email')]"));
                    if (element != null && element.Displayed) return element;
                }
                catch { }

                return null;
            });

            Assert.IsNotNull(emailField, "Email field should be found");
            Assert.IsTrue(emailField?.Displayed ?? false, "Email field should be displayed");
            
            // Test valid email input
            emailField?.Clear();
            emailField?.SendKeys("test@example.com");
            Assert.AreEqual("test@example.com", emailField?.GetAttribute("value"), "Email field should accept valid input");
        }

        private void TestGenderRadioButtons()
        {
            // Try multiple selectors for gender radio buttons
            var maleRadio = wait?.Until(d => 
            {
                try
                {
                    var element = d.FindElement(By.CssSelector("input[value='Male']"));
                    if (element != null && element.Displayed) return element;
                }
                catch { }

                try
                {
                    var element = d.FindElement(By.XPath("//input[@type='radio' and contains(., 'Male')]"));
                    if (element != null && element.Displayed) return element;
                }
                catch { }

                return null;
            });

            var femaleRadio = wait?.Until(d => 
            {
                try
                {
                    var element = d.FindElement(By.CssSelector("input[value='Female']"));
                    if (element != null && element.Displayed) return element;
                }
                catch { }

                try
                {
                    var element = d.FindElement(By.XPath("//input[@type='radio' and contains(., 'Female')]"));
                    if (element != null && element.Displayed) return element;
                }
                catch { }

                return null;
            });
            
            Assert.IsNotNull(maleRadio, "Male radio button should be found");
            Assert.IsNotNull(femaleRadio, "Female radio button should be found");
            Assert.IsTrue(maleRadio?.Displayed ?? false, "Male radio button should be displayed");
            Assert.IsTrue(femaleRadio?.Displayed ?? false, "Female radio button should be displayed");
            
            // Test radio button selection
            maleRadio?.Click();
            Assert.IsTrue(maleRadio?.Selected ?? false, "Male radio button should be selectable");
            
            femaleRadio?.Click();
            Assert.IsTrue(femaleRadio?.Selected ?? false, "Female radio button should be selectable");
            Assert.IsFalse(maleRadio?.Selected ?? true, "Male radio button should be deselected when Female is selected");
        }

        [TestCleanup]
        public void Cleanup()
        {
            driver?.Quit();
        }
    }
} 