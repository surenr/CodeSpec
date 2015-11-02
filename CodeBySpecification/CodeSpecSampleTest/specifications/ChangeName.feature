@UIAutomationReport
Feature: Login test Change name
	In order to check ability to work with ordered tests
	I want to login to the system and change my name


Scenario: Change my name
	Given I navigate to "http://cloudresume.herokuapp.com/user/index"
	And I enter value "Milindu Sanoj" to the "name" with the "id" of "user_name"
	And I click on element "submit" with the "xpath" of "//form[@id = 'edit_user_15']/input[@name='commit']"
	Then The page contains text pattern "User: milindusanoj"
