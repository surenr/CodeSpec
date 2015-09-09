@UIAutomationTest
Feature: SampleWebProject Tests on Navigation
	In order to check ability of Navigating
	I want to navigate here and there

Scenario: Navigate to SUT
	Given I navigate to SUT
	Then The page contains text pattern "Available tests"

Scenario: Navigate to SUT error
	Given I navigate to SUT
	Then The page contains text pattern "Available testsdrs"

@record
Scenario: Navigate to sub-URL under SUT
	Given I navigate to "forms" of SUT

Scenario: Navigate to a URL
	Given I navigate to "http://google.com"