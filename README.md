This is a room booking website automated test project.

It covers the following: 
	- Room booking flow.
	- Booking with missing email.
	- Checks if the links are redirecting as expected.

To get this project to run you need the following:
	
	-Install Visual studio 2022, It can be visual studio community 2019
	- .Net 8.0
        Install the following nuGet packagies from the NuGet Package Manager
	    - Selenium.webdriver
            - Selenium.WebDrivr.ChromeDriver
	    - ExtentReports 
            - NUnit
        Clone the project from this URL https://github.com/luckyvukani/RoomReservation.git.
        Open the project
        Once the project is open, you will see 4 classes under my project called RoomBookings
                                                        -BaseClass.cs for extentReport and driver
                                                        -Booking.cs  for Tests
                                                        -Helper.cs for Methods
                                                        -POM.cs for Elements
       To run the tests, click on view and click on test explorer.
       On the Test explorer, expand roomBooking>Bookings and run those 4 tests(BookRoom,BookRoomMissingEai,delete booking, and LinksRedirect).
