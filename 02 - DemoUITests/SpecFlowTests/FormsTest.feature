Feature: FormsTest
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@tessler-reset-database-true
Scenario: Add a user
	Given I am on the Forms page
	When I have entered emailaddress xanderr@infosupport.com
	And I have entered name Xander van Rijn
	When I press submit
	Then A new user has been added with emailaddress xanderr@infosupport.com and name Xander van Rijn