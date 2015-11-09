@UIAutomationReport
Feature: Login test
	In order to check ability to work with ordered tests
	I want to login to the system and do some stuffs

Scenario: Login to the system
	Given I navigate to "http://cloudresume.herokuapp.com/user/login"
	Given I enter key "username" to the "username" with the "id" of "user_username"
	And I enter key "password" to the "password" with the "id" of "user_password"
	And I click on element "submit" with the "xpath" of "//input[@name='commit']"
	Then The page contains text pattern "User: milindusanoj"
	