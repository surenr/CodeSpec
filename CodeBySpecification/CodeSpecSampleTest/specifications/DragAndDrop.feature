@UIAutomationTest
Feature: SampleWebProject Tests on Drag and Drop
	In order to check ability to work with Drag and Drop
	I want to Drag and Drop some elements

Scenario: Simple Drag and Drop 
	Then I navigate to "TestPages/DragAndDrop_jQueryUI.cshtml" of SUT
	And Wait for the "draggable" with the "id" of "draggable" to show
	And Wait for the "droppable" with the "id" of "droppable" to show
	When I drag "draggable" and drop on to "droppable"

Scenario: Simple Drag and Drop with HTML5
	Given I navigate to "http://html5demos.com/drag"
	And Wait for the "one" with the "id" of "one" to show
	And Wait for the "bin" with the "id" of "bin" to show
	Then I drag "one" and drop on to "bin"
	