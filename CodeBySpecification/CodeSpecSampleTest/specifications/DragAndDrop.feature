@UIAutomationTest
Feature: SampleWebProject Tests on Drag and Drop
	In order to check ability to work with Drag and Drop
	I want to Drag and Drop some elements

Scenario: Simple Drag and Drop
	Then I navigate to "http://html5demos.com/drag"
	When I drag "one"  \with the "id" of "one" and drop on to "bin" \with the "id" of "bin"


Scenario: Simple Drag and Drop 2
	Then I navigate to "http://localhost:50400/TestPages/DragAndDrop_jQueryUI.cshtml"
	And Wait for the "draggable" with the "id" of "draggable" to show
	And Wait for the "droppable" with the "id" of "droppable" to show
	And Content of "droppableP" with the "id" of "droppableP" has text "Drop here"
	When I drag "draggable" and drop on to "droppable"
	And The page contains text pattern "Drodpped!"
	