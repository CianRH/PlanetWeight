using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTests
{

    [TestClass]
    public class UATests
    {
        // .runsettings file contains test run parameters
        // e.g. URI for app
        // test context for this run

        private TestContext testContextInstance;

        // test harness uses this property to initliase test context
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        // URI for web app being tested
        private string webAppUrl;

        // .runsettings property overriden in vsts test runner
        // release task
        [TestInitialize]                // run before each unit test
        public void Setup()
        {
            this.webAppUrl = testContextInstance.Properties["webAppUrl"].ToString();
        }

        [TestMethod]
        public void TestMars55()
        {
            //Arrange

            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");

            IWebDriver chromeBrowser = new ChromeDriver(chromeOptions);
            //chromeBrowser.Navigate().GoToUrl("https://planet-weight-qa-cian.azurewebsites.net/");
            chromeBrowser.Navigate().GoToUrl(this.webAppUrl);

            chromeBrowser.FindElement(By.Id("planetWeight_weight")).Click();
            chromeBrowser.FindElement(By.Id("planetWeight_weight")).SendKeys("55");
            chromeBrowser.FindElement(By.Id("planetWeight_planet")).Click();
            {
                var dropdown = chromeBrowser.FindElement(By.Id("planetWeight_planet"));
                dropdown.FindElement(By.XPath("//option[. = 'Mars']")).Click();
            }
            //Act
            chromeBrowser.FindElement(By.CssSelector(".btn")).Click();

            //Assert
            Assert.AreEqual(chromeBrowser.FindElement(By.CssSelector(".form-group:nth-child(4)")).Text, "Your Weight on that Planet: 20.735");

            chromeBrowser.Close();
            chromeBrowser.Quit();
        }
    }
}