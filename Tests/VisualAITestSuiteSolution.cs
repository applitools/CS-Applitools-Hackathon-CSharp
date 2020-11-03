using System;
using System.IO;
using System.Configuration;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using NUnit.Framework;
using Applitools.Selenium;
using Applitools;
using Applitools.Utils.Geometry;
using Configuration = Applitools.Selenium.Configuration;

namespace CS_Applitools
{
    public class VisualAITest
    {
        public IWebDriver driver;
        public readonly bool isOriginalApp = true;
        public readonly String OriginalAppURL = "https://demo.applitools.com/hackathon.html";
        public readonly String NewAppURL = "https://demo.applitools.com/hackathonV2.html";
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
            Console.WriteLine(Environment.GetEnvironmentVariable("isOriginalApp"));
            //driver.Navigate().GoToUrl(OriginalAppURL);
            driver.Navigate().GoToUrl(NewAppURL);
            //if (Convert.ToBoolean(Environment.GetEnvironmentVariable("isOriginalApp")))
            //{

            //    driver.Navigate().GoToUrl(OriginalAppURL);
            //}
            //else
            //{
            //    driver.Navigate().GoToUrl(NewAppURL);

            //}

            eyes = new Eyes(runner);
            Configuration conf = eyes.GetConfiguration();
            conf.SetStitchMode(StitchModes.CSS);
            conf.SetBatch(batchInfo);
            conf.SetViewportSize(new RectangleSize(1000, 600));
            ///
            //        conf.SetApiKey("");
            //        conf.setServerUrl("SET_YOUR_DEDICATED_CLOUD_URL");

            eyes.SetConfiguration(conf);
            eyes.SetLogHandler(new StdoutLogHandler(true));
            eyes.Open(driver, "VisualTest", TestContext.CurrentContext.Test.FullName);

        }


        [Test]
        [TestCase(TestName = "Page View"), Order(1)]
        public void UIElementTest()
        {
            // Add visual validation here replacing all 21 assertions in the following tests:
            // validateLabels
            // validateImages
            // validateCheckBox
            eyes.Check("LoginPage", Target.Window().Fully(true));
        }

        [Test]
        [TestCase(TestName = "Validate Images"), Order(2)]
        public void usernameAndPasswordMustPresentTest()
        {
            SubmitForm();
            eyes.Check("Username and password must be present", Target.Window().Fully());
        }

        [Test]
        [TestCase(TestName = "Username Mhust Be Present"), Order(3)]
        public void usernameMustPresentTest()
        {
            driver.FindElement(By.CssSelector("#username")).SendKeys("John Smith");
            SubmitForm();
            eyes.Check("Username must be present", Target.Window().Fully());
        }

        [Test]
        [TestCase(TestName = "Password Must Be present"), Order(4)]
        public void passwordMustPresentTest()
        {
            driver.FindElement(By.CssSelector("#password")).SendKeys("ABC$1@");
            SubmitForm();
            eyes.Check("Password must be present", Target.Window().Fully());
        }

        [Test]
        [TestCase(TestName = "Successful Login"), Order(5)]
        public void successfulLoginTest()
        {
            driver.FindElement(By.CssSelector("#username")).SendKeys("John Smith");
            driver.FindElement(By.CssSelector("#password")).SendKeys("ABC$1@");
            SubmitForm();
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
        public static void finalTearDown()
        {
            TestResultsSummary allTestResults = runner.GetAllTestResults(false);
            Console.WriteLine(allTestResults);
        }
    }
}
