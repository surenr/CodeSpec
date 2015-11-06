@UIAutomationTest
Feature: SampleWebProject Tests on Form Elements
	In order to check ability ofworking with form eements
	I want to do some actions with form elements


Scenario: Select element in dropdown
	Given I navigate to "https://www.cs.tut.fi/~jkorpela/www/testel.html"
	Then Select "one" of the "ddb" with the "id" of "f10"

	