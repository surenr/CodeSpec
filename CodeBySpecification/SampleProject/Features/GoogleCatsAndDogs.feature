@UIAutomationTest
@UIAutomationReport
Feature: Base Test
	In order to check ability of vedio capturing
	I want to google for some stuffs and record them

Scenario: Google for Cats
	Given I navigate to "http://google.com"
	And I enter "Cats" to element "searchBox" with the "id" of "lst-ib"
	And I click on element "searchButton" with the "xpath" of "id('sblsbb')/button" and wait "4" seconds
	Then The page contains text pattern "The domestic cat"
	 
Scenario: Google for Dogs
	Given I navigate to "http://google.com"
	And I enter "Dogs" to element "searchBox" with the "id" of "lst-ib"
	And I click on element "searchButton" with the "xpath" of "id('sblsbb')/button" and wait "4" seconds
	Then The page contains text pattern "The domestic dog"
	Then I take a screenshot
