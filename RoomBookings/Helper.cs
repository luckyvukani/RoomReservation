using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace RoomBookings
{
    public class Helper : POM
    {
        public UserDetailsForm ReadTestDataJsonFile()
        {
            String JsonPath = AppDomain.CurrentDomain.BaseDirectory.Replace("RoomBookings\\bin\\Debug\\net8.0\\", "") + "TestData.json";
            string jsonTextData = File.ReadAllText(JsonPath);
            UserDetailsForm form = JsonSerializer.Deserialize<UserDetailsForm>(jsonTextData);
            return form;
        }
        
 
        public void NavigateToFillDetailsForm(IWebDriver driver, ExtentTest test)
        {
            try
            {
                driver.FindElement(btnBooking).Click();
                TakeScreenshot(driver, "Booking");
                test.GenerateLog(Status.Pass, "Open Site Url https://automationintesting.online/");
                test.GenerateLog(Status.Pass, "Maximize the window");
                test.GenerateLog(Status.Pass, "Click Booking Button on top right of the page");
                // test.GenerateLog(Status.Pass, "Booking clicked successfully");
                //Check if rooms are available
                if (IsRoomElementVisible(driver))
                {
                    IWebElement rooms1 = driver.FindElement(lRoom);
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", rooms1);
                    Thread.Sleep(1000);
                    TakeScreenshot(driver, "Rooms Available");
                    driver.FindElement(btnBookNow).Click();
                    test.GenerateLog(Status.Pass, "Click Book Now Button");
                    Thread.Sleep(1000);
                    //Click Next button on the calender to check available dates(unfortunately there is a bug, I can not select date)
                    Thread.Sleep(1000);
                    driver.FindElement(btnNext).Click();
                    TakeScreenshot(driver, "Next calender Button");
                    test.GenerateLog(Status.Pass, "Click Next Button to select the date");
                    Thread.Sleep(1000);
                    //Validate that the Total amount for booking display
                    IWebElement txtTotalAmount = driver.FindElement(TotAmount);
                    string elementText = txtTotalAmount.Text;
                    Assert.IsTrue(elementText.Contains("Total"), $"Element text did not contain expected values. Actual text: '{elementText}'");
                    TakeScreenshot(driver, "Total Amount");
                    test.GenerateLog(Status.Pass, "Confirm the Total Amount");
                    //Scroll to the button Reserve Now
                    IWebElement Reserv = driver.FindElement(Reserve);
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", Reserv);
                    Thread.Sleep(1000);
                    //Click button Reserve Now
                    driver.FindElement(btnReserve).Click();
                    test.GenerateLog(Status.Pass, "Click Reserve button to Reserve the Room");

                    Thread.Sleep(1000);
                    //Scroll up to the form
                    IWebElement rForm = driver.FindElement(reserveForm);
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", rForm);
                    test.GenerateLog(Status.Pass, "Navigate to Fill Your Details Form");
                }
                else
                {
                    Console.WriteLine("No rooms available");
                    test.GenerateLog(Status.Info, "No Rooms Available, please check later");
                    driver.Quit();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to Navigate to fill your Details form", e.Message);
                Assert.Fail("FAILED");
            }
        }
        public bool IsRoomElementVisible(IWebDriver driver)
        {
            try
            {
                var element = driver.FindElement(btnBookNow);
                return element.Displayed;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public bool IsMSGElementVisible(IWebDriver driver)
        {
            try
            {
                var element = driver.FindElement(findElement);
                return element.Displayed;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public void fillUpTheForm(IWebDriver driver, ExtentTest test)
        {
            try
            {
                driver.FindElement(firstName).SendKeys(ReadTestDataJsonFile().FirstName);
                test.GenerateLog(Status.Pass, "Enter First Name");
                driver.FindElement(lastName).SendKeys(ReadTestDataJsonFile().LastName);
                test.GenerateLog(Status.Pass, "Enter Last Name");
                driver.FindElement(email).SendKeys(ReadTestDataJsonFile().Email);
                test.GenerateLog(Status.Pass, "Enter Email ");
                driver.FindElement(phone).SendKeys(ReadTestDataJsonFile().PhoneNumber);
                test.GenerateLog(Status.Pass, "Enter Phone Number");

                IWebElement btnReserve = driver.FindElement(btnReserveNow);
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", btnReserve);
                Thread.Sleep(1000);
                TakeScreenshot(driver, "Details form");
                driver.FindElement(btnReserveNow).Click();
                test.GenerateLog(Status.Pass, "Click Reserve Now Button");
            }
            catch (Exception e)
            {
                Console.Write("Failed to submit form", e.Message);
                test.GenerateLog(Status.Pass, "Form Filling");
            }
        }
        public void MissingEmail(IWebDriver driver, ExtentTest test)
        {
            try
            {
                driver.FindElement(firstName).SendKeys(ReadTestDataJsonFile().FirstName);
                test.GenerateLog(Status.Pass, "Enter First Name");
                driver.FindElement(lastName).SendKeys(ReadTestDataJsonFile().LastName);
                test.GenerateLog(Status.Pass, "Enter Last Name");
                driver.FindElement(phone).SendKeys(ReadTestDataJsonFile().PhoneNumber);
                test.GenerateLog(Status.Pass, "Enter Phone Number");
                driver.FindElement(btnReserveNow).Click();
                test.GenerateLog(Status.Pass, "Click Reserve Now Button");
                Thread.Sleep(2000);
                TakeScreenshot(driver, "Details form with missing email");

                //Validate the message
                IWebElement meassge = driver.FindElement(Message);
                String msg = meassge.Text;
                Assert.That(msg, Is.EqualTo("must not be empty"));
                test.GenerateLog(Status.Pass, "Validate the message when the email is empty");
            }
            catch (Exception e)
            {
                Console.WriteLine("Couldn't validate the message", e.Message);
                test.GenerateLog(Status.Fail, "Validate the message when the email is empty");
                test.GenerateLog(Status.Fail, "<pre>" + e.StackTrace + "</pre>");
                TakeScreenshot(driver, "FAILED");
                Assert.Fail("FAILED");
            }
        }
        public void validateBookingMessage(IWebDriver driver, ExtentTest test)
        {
            try
            {
                if (IsMSGElementVisible(driver))
                {

                    IWebElement confimationMessage = driver.FindElement(findElement);
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", btnReserve);
                    string confirm = confimationMessage.Text;
                    //Validate the message correct
                    Assert.That(confirm, Is.EqualTo("Your booking has been confirmed for the following dates:"));
                    test.GenerateLog(Status.Pass, "Message Validation for confirm Booking");
                    TakeScreenshot(driver, "Validation Messsage");
                }
                else
                {
                    Console.WriteLine("Message not found, system might be crashed, please check report");
                    test.GenerateLog(Status.Fail, "Message Validation for confirm Booking");
                    TakeScreenshot(driver, "FAILED");
                    Assert.Fail("Failed, system crashed");
                }
            }
            catch
            (Exception ex)
            {
                Console.WriteLine("Failed, Incorrect Message", ex.Message);
                test.GenerateLog(Status.Fail, "<pre>" + ex.StackTrace + "</pre>");
                TakeScreenshot(driver, "System crashed");
                Assert.Fail("FAILED");
            }
        }
        public void LinkRedirect(IWebDriver driver, ExtentTest test)
        {
            try
            {
                driver.FindElement(rooms).Click();
                Thread.Sleep(1000);
                string currenRoomsUrl = driver.Url;
                Assert.That(currenRoomsUrl, Is.EqualTo("https://automationintesting.online/#rooms"));
                test.GenerateLog(Status.Pass, "Room link redirected successfully");
                TakeScreenshot(driver, "Rooms Redirect");

                driver.FindElement(bookings).Click();
                Thread.Sleep(1000);
                string currenBookingUrl = driver.Url;
                Assert.That(currenBookingUrl, Is.EqualTo("https://automationintesting.online/#booking"));
                test.GenerateLog(Status.Pass, "Booking link redirected successfully");
                TakeScreenshot(driver, "Bookings Redirect");

                driver.FindElement(amenities).Click();
                Thread.Sleep(1000);
                string currentAmenitiestUrl = driver.Url;
                Assert.That(currentAmenitiestUrl, Is.EqualTo("https://automationintesting.online/#amenities"));
                test.GenerateLog(Status.Pass, "amenities link redirected successfully");
                TakeScreenshot(driver, "amenities Redirect");

                driver.FindElement(location).Click();
                Thread.Sleep(1000);
                string currentLocationUrl = driver.Url;
                Assert.That(currentLocationUrl, Is.EqualTo("https://automationintesting.online/#location"));
                test.GenerateLog(Status.Pass, "location link redirected successfully");
                TakeScreenshot(driver, "location Redirect");

                driver.FindElement(contact).Click();
                Thread.Sleep(1000);
                string currentContactUrl = driver.Url;
                Assert.That(currentContactUrl, Is.EqualTo("https://automationintesting.online/#contact"));
                test.GenerateLog(Status.Pass, "contact link redirected successfully");
                TakeScreenshot(driver, "contact Redirect");

                driver.FindElement(admin).Click();
                Thread.Sleep(1000);
                string currentAdminUrl = driver.Url;
                Assert.That(currentAdminUrl, Is.EqualTo("https://automationintesting.online/admin"));
                test.GenerateLog(Status.Pass, "admin link redirected successfully");
                TakeScreenshot(driver, "admin Redirect");
            }
            catch (Exception e)
            {
                Console.WriteLine("one or links not redirected correctly", e.Message);
                test.GenerateLog(Status.Fail, "Links Redirect");
                test.GenerateLog(Status.Fail, "<pre>" + e.StackTrace + "</pre>");
                TakeScreenshot(driver, "FAILED");
            }
        }
        public void TakeScreenshot(IWebDriver driver, String screenshotName)
        {  
            ITakesScreenshot screenshot = (ITakesScreenshot)driver;
            Screenshot screenshot1 = screenshot.GetScreenshot();
            String folderPath = AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\net8.0\\", "") + "Screenshots\\";
            DirectoryInfo dir = Directory.CreateDirectory(folderPath);
            String filePath = dir + screenshotName + DateTime.Now.ToString("yyyyMMddhhmmss") + ".png";
            screenshot1.SaveAsFile(filePath);
        }
    }
}
