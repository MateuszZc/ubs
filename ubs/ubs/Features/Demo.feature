Feature: Demo
	
@regression
Scenario: User is able to make transaction on Demo page
	Given Open ubs webpage
	When User accept all Privacy Settings if its needed
	When User navigate to Trial E-Banking demo page
	When User clicks on New button
	When User clicks on "Account transfer" option
	When User clicks on "Debit from" field
	When User select first element in "Debit from"
	When User clicks on "Credit to" field
	When User select first element in "Credit to"
	When User enter "123" in "Amount" field
	When User enter "Test" in "Booking text" field
	When User clicks on Transfer button
	Then User should see proper message after account transfer

