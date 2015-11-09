@UIAutomationTest
Feature: SampleWebProject Tests on Tables
	In order to check ability of working with tables,
	I want to get values from table cells


Scenario: Read value from table cell
	Given I navigate to "https://mdn.mozillademos.org/en-US/docs/Web/HTML/Element/table$samples/More_Examples?revision=905727"
	Then "1"st row, "1"nd column of table "SimpleTable" contains value "Body content 1"
	Then "1"st row, "2"nd column of table "SimpleTable" contains value "Body content 2"
	Then "1"st row,