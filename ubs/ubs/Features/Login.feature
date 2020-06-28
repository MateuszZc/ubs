Feature: Login

@smoke
Scenario: User is able to open digital Banking Demo
	Given Open ubs webpage
	When User accept all Privacy Settings if its needed
	When User click on "UBS logins" button
	Then User should see dropdown "Choose your UBS login"
	When User clicks on "UBS E-Banking Switzerland" options in UBS logins dropdown
	Then User should see E-Banking login page 
	When User clicks on "Trial E-Banking demo" option on E-Banking login page
	Then User should see Account and custody account overview page

@regression
Scenario: User is not able to login with incorrect correct Number
	Given Open ubs webpage
	When User accept all Privacy Settings if its needed
	When User click on "UBS logins" button
	Then User should see dropdown "Choose your UBS login"
	When User clicks on "UBS E-Banking Switzerland" options in UBS logins dropdown
	Then User should see E-Banking login page
	When User enter "TEST123TEST" value in contract Number field
	When User clicks on Continue button
	Then User should see warning message after incorrect try