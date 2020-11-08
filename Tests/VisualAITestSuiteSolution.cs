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
    public class VisualAITestSolution
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
           // driver.Navigate().GoToUrl(originalAppURL);

            //New App URL
            driver.Navigate().GoToUrl(newAppURL);


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

        //Visual validation in the following tests replacing all 21 assertions:
        [Test]
        [TestCase(TestName = "Page View"), Order(1)]
        public void UIElementTest()
        {
            //Visual validation for the entire page:
            eyes.Check("LoginPage", Target.Window().Fully(true));
        }

        // Both Username and Password must be present
        [Test]
        [TestCase(TestName = "Both Username and Password must be present"), Order(2)]
        public void UsernameAndPasswordPresentTest()
        {
            SubmitForm();

            //Visual validation for both username and password present:
            eyes.Check("Username and password must be present", Target.Window().Fully());
        }

        [Test]
        [TestCase(TestName = "Password Must Be Present"), Order(3)]
        public void UsernameMustPresentTest()
        {
            driver.FindElement(By.CssSelector("#username")).SendKeys("John Smith");
            SubmitForm();

            //Visual validation for password present:
            eyes.Check("Password must be present", Target.Window().Fully());
        }

        [Test]
        [TestCase(TestName = "Username Must Be present"), Order(4)]
        public void PasswordMustPresentTest()
        {
            driver.FindElement(By.CssSelector("#password")).SendKeys("ABC$1@");
            SubmitForm();

            //Visual validation for username present:
            eyes.Check("Username must be present", Target.Window().Fully());
        }

        [Test]
        [TestCase(TestName = "Successful Login"), Order(5)]
        public void SuccessfulLoginTest()
        {
            driver.FindElement(By.CssSelector("#username")).SendKeys("John Smith");
            driver.FindElement(By.CssSelector("#password")).SendKeys("ABC$1@");
            SubmitForm();

            //Visual validation for successful login:
            eyes.Check("Successful login", Target.Window().Fully());

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