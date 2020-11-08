using System;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using NUnit.Framework;
using Applitools.Selenium;
using Applitools;
using Applitools.Utils.Geometry;
using Configuration = Applitools.Selenium.Configuration;

namespace CS_Applitools
{
    [TestFixture]
    public class VisualAITest
    {
        public IWebDriver driver;
        public readonly bool isOriginalApp = true;
        public readonly string originalAppURL = "https://demo.applitools.com/hackathon.html";
        public readonly string newAppURL = "https://demo.applitools.com/hackathonV2.html";
        public Eyes eyes;
        public static EyesRunner runner;
        public static BatchInfo batchInfo;


        [OneTimeSetUp]
        public static void ClassSetup()
        {

            batchInfo = new BatchInfo("VisualAITests");
            runner = new ClassicRunner();

        }
        [SetUp]
        public void TestSetup()
        {
            driver = new ChromeDriver();

            //Original App URL
            driver.Navigate().GoToUrl(originalAppURL);

            //New App URL
            //driver.Navigate().GoToUrl(newAppURL);


            eyes = new Eyes(runner);
            Configuration conf = eyes.GetConfiguration();
            conf.SetStitchMode(StitchModes.CSS);
            conf.SetBatch(batchInfo);

            //conf.SetApiKey("SET_YOUR_API_KEY_HERE");
            //conf.setServerUrl("SET_YOUR_DEDICATED_CLOUD_URL");

            eyes.SetConfiguration(conf);
            eyes.SetLogHandler(new StdoutLogHandler(true));
            eyes.Open(driver, "VisualTest", TestContext.CurrentContext.Test.Name, new RectangleSize(1000, 600));

        }

        // Add visual validation in the following tests replacing all 21 assertions:
        [Test]
        [TestCase(TestName = "Page View"), Order(1)]
        public void UIElementTest()
        {
            //Add visual validation for the entire page here
        }

        // Both Username and Password must be present
        [Test]
        [TestCase(TestName = "Both Username and Password must be present"), Order(2)]
        public void UsernameAndPasswordPresentTest()
        {
            SubmitForm();

            //Add visual validation for both username and password present here
        }

        [Test]
        [TestCase(TestName = "Password Must Be Present"), Order(3)]
        public void UsernameMustPresentTest()
        {
            driver.FindElement(By.CssSelector("#username")).SendKeys("John Smith");
            SubmitForm();

            //Add visual validation for password present here
        }

        [Test]
        [TestCase(TestName = "Username Must Be present"), Order(4)]
        public void PasswordMustPresentTest()
        {
            driver.FindElement(By.CssSelector("#password")).SendKeys("ABC$1@");
            SubmitForm();

            //Add visual validation for username present here
        }

        [Test]
        [TestCase(TestName = "Successful Login"), Order(5)]
        public void SuccessfulLoginTest()
        {
            driver.FindElement(By.CssSelector("#username")).SendKeys("John Smith");
            driver.FindElement(By.CssSelector("#password")).SendKeys("ABC$1@");
            SubmitForm();

            //Add visual validation for successful login here

        }

        public void SubmitForm()
        {
            driver.FindElement(By.CssSelector("#log-in")).Click();
        }


        [TearDown]
        public void TearDown()
        {
            try
            {
                driver.Quit();
                eyes.CloseAsync();
            }
            finally
            {
                eyes.AbortAsync();
            }

        }

        [OneTimeTearDown]
        public static void FinalTearDown()
        {
            TestResultsSummary allTestResults = runner.GetAllTestResults(false);
            Console.WriteLine(allTestResults);
        }
    }
}