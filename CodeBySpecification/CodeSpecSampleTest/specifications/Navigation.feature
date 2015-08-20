@UIAutomationTest
Feature: SampleWebProject Tests on Navigation
	In order to check ability of Navigating
	I want to navigate here and there

Scenario: Navigate to SUT
	Given I navigate to SUT

Scenario: Navigate to sub-URL under SUT
	Given I navigate to "TestPages/HTML_Form" of SUT

Scenario: Navigate to a URL
	Given I navigate to "http://google.com"