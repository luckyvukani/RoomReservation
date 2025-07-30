using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBookings
{
    public class BaseClass
    {
        public IWebDriver driver;
        public void HomePage(IWebDriver _driver)
        {
            driver = _driver;
        }

        public static ExtentReports extent;
        public static ExtentTest test;

        Helper om=new Helper();

        [SetUp]
        public void Setup()
        {

                string reportpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports", "TestReport.html");
                var htmlReporter = new ExtentSparkReporter(reportpath);

                extent = new ExtentReports();
                extent.AttachReporter(htmlReporter);

                // Launch browser
                driver = new ChromeDriver();
              
                driver.Navigate().GoToUrl("https://automationintesting.online/");
                Thread.Sleep(2000);
                om.TakeScreenshot(driver, "Home Page");
                driver.Manage().Window.Maximize();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            }

        [TearDown]
        public void Teardown()
        {
                if (driver != null)
                {
                    driver.Quit();    
                    driver.Dispose();
                }
                extent.Flush();
            }
    }
}
