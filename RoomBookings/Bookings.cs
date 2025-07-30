using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Linq;

namespace RoomBookings
{
    public class Bookings : BaseClass
    {
        [Test]
        public void BookRoom()
        {
            test = extent.CreateTest("BookRoom");
            Helper book = new Helper();
            try
            {
                book.NavigateToFillDetailsForm(driver, test);
                book.TakeScreenshot(driver, "NavigateToForm");
                //Fill up the form
                book.fillUpTheForm(driver, test);
                book.validateBookingMessage(driver, test);
            }
            catch(Exception e)
            {
                test.GenerateLog(Status.Fail, "Test Failed: " + e.Message);
                test.GenerateLog(Status.Fail, "<pre>" + e.StackTrace + "</pre>");
            }
        }
        [Test]
        public void BookRoomMissingEmail()
        {
            test = extent.CreateTest("BookRoomMissingEmail");
            try
            {
                Helper noEmail = new Helper();
                noEmail.NavigateToFillDetailsForm(driver, test);
                //Fill up the form with missing email
                noEmail.MissingEmail(driver, test);
            }
            catch(Exception e)
            {
                test.Log(Status.Fail, "Test Failed: " + e.Message);
                test.Log(Status.Fail, "<pre>" + e.StackTrace + "</pre>");
            }
        }
        [Test]
        public void DeleteBooking()
        {
            test = extent.CreateTest("Delete Booking");
            try
            {
                Helper admin = new Helper();
                admin.adminLogin(driver, test);
                admin.DeleteBooking(driver, test);
            }
            catch (Exception e)
            {
                test.Log(Status.Fail, "Test Failed: " + e.Message);
                test.Log(Status.Fail, "<pre>" + e.StackTrace + "</pre>");
            }
        }
        
        [Test]
        public void linksRedirect()
        {
            test = extent.CreateTest("linksRedirect");
            Helper redirect = new Helper();
            try
            {  
                redirect.LinkRedirect(driver, test);
            }
                catch(Exception e)
                {
                test.Log(Status.Fail, "Test Failed: " + e.Message);
                test.Log(Status.Fail, "<pre>" + e.StackTrace + "</pre>");
                redirect.TakeScreenshot(driver, "FAILED");
            }
        }
    }
}