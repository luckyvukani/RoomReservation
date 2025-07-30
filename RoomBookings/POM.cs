using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RoomBookings
{
    public class POM
    {
        public IWebDriver _driver;
        public void HomePage(IWebDriver driver)
        {
            _driver = driver;
        }
        // Locators
        public By btnBooking => By.XPath("//a[@href='/#booking']");
        public By btnBookNow => By.XPath("//a[@class='btn btn-primary']");
        public By btnNext => By.XPath("//button[normalize-space()='Next']");
        public By btnReserve => By.XPath("//button[@id='doReservation']");
        public By firstName => By.XPath("//input[@placeholder='Firstname']");
        public By lastName => By.XPath("//input[@placeholder='Lastname']");
        public By email => By.XPath("//input[@placeholder='Email']");
        public By phone => By.XPath("//input[@placeholder='Phone']");
        public By btnReserveNow => By.XPath("//button[@class='btn btn-primary w-100 mb-3']");
        public By rooms => By.XPath("//a[@href='/#rooms']");
        public By bookings => By.XPath("//a[@href='/#booking']");
        public By amenities => By.XPath("//a[@href='/#amenities']");
        public By location => By.XPath("//a[@href='/#location']");
        public By contact => By.XPath("//a[@href='/#contact']");
        public By admin => By.XPath("//a[@href='/admin']");
        public By Message => By.XPath("//*[@id=\"root-container\"]/div/div[2]/div/div[2]/div/div/form/div[5]/ul/li");
        public By TotAmount => By.XPath("//div[@class='d-flex justify-content-between fw-bold']");
        public By reserveForm => By.XPath("//input[@placeholder='Firstname']");
        public By Reserve => By.XPath("//button[@id='doReservation']");
        public By lRoom => By.XPath("//a[@class='btn btn-primary']");
        public By findElement => By.XPath("//*[@id=\"root-container\"]/div/div[2]/div/div[2]/div/div/p[1]");
        /////////////////////////////////////////////////////////////////////
        public By Username => By.XPath("//input[@id='username']");
        public By Password => By.XPath("//input[@id='password']");
        public By Login => By.XPath("//button[@id='doLogin']");
        public By Messages => By.XPath("//a[@href='/admin/message']");
        public By RoomCreated => By.XPath("//div[@data-testid=\'roomlisting\']");
        public By DeleteButton => By.XPath("//span[@class=\"fa fa-trash bookingDelete\"]");

    }
}

    
