@UIAutomationTest
Feature: basic
	In order to demonstrate Code Spec I want to write a UI Automation scenario 

Scenario: Search Google
	Given I navigate to "http://www.google.com"
	And I enter value "Cats" to element "SearchBox" with the "id" of "lst-ib"
	And I click on element "searchButton" with the "xpath" of "id('sblsbb')/button" and wait "4" seconds
	Then The page contains text pattern "The domestic cat is"