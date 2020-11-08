using System;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using NUnit.Framework;

namespace CS_Applitools
{
    [TestFixture]
    public class TraditonalTests
    {
        public static bool isOriginalApp = true;
        public readonly string originalAppURL = "https://demo.applitools.com/hackathon.html";
        public readonly string newAppURL = "https://demo.applitools.com/hackathonV2.html";
        public readonly By errorLocator = By.CssSelector(".alert.alert-warning");
        public IWebDriver driver;

        [OneTimeSetUp]
        public void ClassSetUp()
        {

            if (null != Environment.GetEnvironmentVariable("isOriginalApp"))
            {
                isOriginalApp = Convert.ToBoolean(Environment.GetEnvironmentVariable("isOriginalApp"));
            }
        }
        [SetUp]
        public void TestSetup()
        {

            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(originalAppURL);
            //driver.Navigate().GoToUrl(newAppURL);

        }

        [Test]
        [TestCase(TestName = "Validate labels"), Order(1)]
        public void ValidateLabels()
        {
            // Assert Text of Login Form
            Assert.IsTrue(driver.FindElement(By.CssSelector(".auth-header")).Text.Contains("Login Form"));
            Assert.IsTrue(driver.FindElement(By.CssSelector(".auth-header")).Displayed);

            // Assert Text of UserName Label
            Assert.IsTrue(driver.FindElement(By.CssSelector("form > div:nth-child(1) > label")).Text.Contains("Username"));
            Assert.IsTrue(driver.FindElement(By.CssSelector("form > div:nth-child(1) > label")).Displayed);

            // Assert Text of UserName Element
            Assert.IsTrue(driver.FindElement(By.CssSelector("#username")).GetAttribute("placeholder").Contains("Enter your username"));
            Assert.IsTrue(driver.FindElement(By.CssSelector("#username")).Displayed);

            // Assert Text of Password Label
            Assert.IsTrue(driver.FindElement(By.CssSelector("form > div:nth-child(2) > label")).Text.Contains("Password"));
            Assert.IsTrue(driver.FindElement(By.CssSelector("form > div:nth-child(2) > label")).Displayed);

            // Assert Text of Password Element
            Assert.IsTrue(driver.FindElement(By.CssSelector("#password")).GetAttribute("placeholder").Contains("Enter your password"));
            Assert.IsTrue(driver.FindElement(By.CssSelector("#password")).Displayed);

            // Assert Text of Login Element
            Assert.IsTrue(driver.FindElement(By.CssSelector("#log-in")).Text.Contains("Log In"));
            Assert.IsTrue(driver.FindElement(By.CssSelector("#log-in")).Displayed);

            // Assert Text of Remember Me Element
            Assert.IsTrue(driver.FindElement(By.CssSelector(".form-check-label")).Text.Contains("Remember Me"));
            Assert.IsTrue(driver.FindElement(By.CssSelector(".form-check-label")).Displayed);
        }

        [Test]
        [TestCase(TestName = "Validate Images"), Order(2)]
        public void ValidateImages()
        {
            // Assert Logo Icon is Visible
            Assert.IsTrue(driver.FindElement(By.CssSelector(".logo-w>a>img")).Displayed);

            // Assert User Icon is Visible
            Assert.IsTrue(driver.FindElement(By.CssSelector(".pre-icon.os-icon.os-icon-user-male-circle")).Displayed);

            // Assert Fingerprint Icon is Visible
            Assert.IsTrue(driver.FindElement(By.CssSelector(".pre-icon.os-icon.os-icon-fingerprint")).Displayed);

            // Assert Twitter Icon is Visible
            Assert.IsTrue(driver.FindElement(By.CssSelector("a:nth-child(1) > img")).Displayed);

            // Assert Facebook Icon is Visible
            Assert.IsTrue(driver.FindElement(By.CssSelector("a:nth-child(2) > img")).Displayed);

            // Assert Linkdin Icon is Visible
            Assert.IsTrue(driver.FindElement(By.CssSelector("a:nth-child(3) > img")).Displayed);
        }

        [Test]
        [TestCase(TestName = "Validate CheckBox"), Order(3)]
        public void ValidateCheckBox()
        {
            // Assert CheckBox isn't selected
            Assert.IsFalse(driver.FindElement(By.CssSelector(".form-check-input")).Selected);
        }

        // Both Username and Password must be present
        [Test]
        [TestCase(TestName = "Both Username and Password must be present"), Order(4)]
        public void UsernameAndPasswordPresentTest()
        {
            SubmitForm();
            Assert.IsTrue(driver.FindElement(errorLocator).Displayed);
            Assert.IsTrue(driver.FindElement(errorLocator).Text.Contains("Both Username and Password must be present"));
        }

        // Username must be present
        [Test]
        [TestCase(TestName = "Password Must be Present"), Order(5)]
        public void UsernameMustPresentTest()
        {
            driver.FindElement(By.CssSelector("#username")).SendKeys("John Smith");
            SubmitForm();
            Assert.IsTrue(driver.FindElement(errorLocator).Displayed);
            Assert.IsTrue(driver.FindElement(errorLocator).Text.Contains("Password must be present"));
        }

        // Password must be present
        [Test]
        [TestCase(TestName = "Username Must be present"), Order(6)]
        public void PasswordMustPresentTest()
        {
            driver.FindElement(By.CssSelector("#password")).SendKeys("ABC$1@");
            SubmitForm();
            Assert.IsTrue(driver.FindElement(errorLocator).Displayed);
            Assert.IsTrue(driver.FindElement(errorLocator).Text.Contains("Username must be present"));
        }


        // Successful Login
        [Test]
        [TestCase(TestName = "Successful Login"), Order(7)]
        public void SuccessfullLoginTest()
        {
            driver.FindElement(By.CssSelector("#username")).SendKeys("John Smith");
            driver.FindElement(By.CssSelector("#password")).SendKeys("ABC$1@");
            SubmitForm();
            Assert.IsTrue(driver.Title.Contains("ACME demo app"));
        }

        public void SubmitForm()
        {
            driver.FindElement(By.CssSelector("#log-in")).Click();
        }

        [TearDown]
        public void AfterEach()
        {
            driver.Quit();
        }
    }
}