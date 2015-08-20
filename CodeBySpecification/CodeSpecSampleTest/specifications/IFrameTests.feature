@UIAutomationTest
Feature: SampleWebProject Tests on IFrames
	In order to check ability to work with IFrams
	I want to switch into Iframes and do some work there

Scenario: Switch to an IFrame
	Given Navigate to SUT 
	Then I navigate to "http://localhost:50400/TestPages/IFrames"
	And switched to iframe with the "id" of "iframe_test_one" 
	Then I enter value "Typing in textbox one" to the "Text1" with the "id" of "Text1"

Scenario: Switch to an IFrame inside IFrame
	Given Navigate to SUT 
	Then I navigate to "http://localhost:50400/TestPages/IFrames"
	And switched to iframe with the "id" of "iframe_test_two" 
	Then I enter value "Typing in textbox one" to the "Text1" with the "id" of "Text1"
	And switched to iframe with the "id" of "iframe_test_two_2" 
	Then I enter value "Typing in textbox one" to the "Text1" with the "id" of "Text1"

Scenario: Switch to an IFrame inside IFrame then switch to Default content
	Given Navigate to SUT 
	Then I navigate to "http://localhost:50400/TestPages/IFrames"
	And switched to iframe with the "id" of "iframe_test_two" 
	Then I enter value "Typing in textbox one" to the "Text1" with the "id" of "Text1"
	And switched to iframe with the "id" of "iframe_test_two_2" 
	Then I enter value "Typing in textbox one" to the "Text1" with the "id" of "Text1"
	And I switched to default content
	Then I enter value "Typed in textbox one in IFrame inside IFrame Two" to the "Text1" with the "id" of "Text1" 