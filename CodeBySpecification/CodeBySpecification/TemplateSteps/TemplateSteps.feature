Feature: TemplateSteps
	In Order to do zero code UI testing, define the template steps required

@mytag
Scenario: Template Steps for Navigations
	Given I navigate to System Under Test
	And Navigate to System Under Test
	And I navigate to SUT
	And Navigate to SUT
	And I navigate to "<url>"
	And Navigate to "<url>"
	And I navigate to URL stored in "<variable_name>"
	And Navigate to URL stored in "<variable_name>"
	And I switch to tab "<tab_number>"
	And I close tab "<tab_number>"

Scenario: Template Steps for Element Visibility
	Given "<element_key>" is visible
	And The "<element_key>" is visible
	And I wait for the "<element_key>" to appear
	And Wait for the "<element_key>" to appear
	And The "<element_key>" with the "<selection_method>" of "<XPATH or ID>" is shown
	And "<element_key>" with the "<selection_method>" of "<XPATH or ID>" is shown
	And I wait for the "<element_key>" with the "<selection_method>" of "<XPATH or ID>" to show
	And Wait for the "<element_key>" with the "<selection_method>" of "<XPATH or ID>" to show

Scenario: Template Steps for Reading content from elements
	Given Read the content of "<element_key>"
	And Get the content of "<element_key>" with the "<selection_method>" of "<XPATH or ID>"
	And Content of "<element_key>" contains text "<expected_string>"
	And The content of "<element_key>" contains text "<expected_string>"
	And Content of "<element_key>" with the "<selection_method>" of "<XPATH or ID>" has text "<expected_string>"
	And The content of "<element_key>" with the "<selection_method>" of "<XPATH or ID>" has text "<expected_string>"
	And "<element_key>" contains the text pattern "<Regx Pattern>"
	And The "<element_key>" contains text pattern "<Regx Pattern>"
	And The content of the "<element_key>" contains text pattern "<Regx Pattern>"

Scenario: Template Steps for Element Clicks
	Given "<element_key>" contains the text pattern "<Regx Pattern>"
	And The "<element_key>" contains text pattern "<Regx Pattern>"
	And The content of the "<element_key>" contains text pattern "<Regx Pattern>"
	And I click on "<element_key>"
	And Click on "<element_key>"
	And I click on element "<element_key>" with the "<selection_method>" of "<XPATH or ID>"
	And Click on element "<element_key>" with the "<selection_method>" of "<XPATH or ID>"
	And I click on "<element_key>" and wait "<seconds>" seconds
	And Click on "<element_key>" and wait "<seconds>" seconds
	And I click on element "<element_key>" with the "<selection_method>" of "<XPATH or ID>" and wait "<seconds>" seconds
	And Click on element "<element_key>" with the "<selection_method>" of "<XPATH or ID>" and wait "<seconds>" seconds	

Scenario: Template Steps for Entering text to element
	Given I enter "<string>" to the "<element_key>"
	And Enter "<string>" to the "<element_key>"
	And I enter value "<string>" to the "<element_key>" with the "<selection_method>" of "<XPATH or ID>"
	And Enter value "<string>" to the "<element_key>" with the "<selection_method>" of "<XPATH or ID>"

Scenario: Template Steps for Confirming browser generated alerts
	Given I accept the confirmation
	And I accept the confirmation alert
	And Accept the confirmation
	And Accept the confirmation alert

Scenario: Tempalte Steps for Table manipulation
	Given Table "<element_key>" has "<number>" of rows
	And Row in table "<element_key>" has "<number>" coloums
	And "<number>"st row, "<number>"st column of table "<element_key>" contains value "<value>"

Scenario: Template Steps for Variable manipulation
	Given I enter value of variable "<variable_name>" to the "<element_key>"
	And Enter value of variable "<variable_name>" to the "<element_key>"
	And I enter variable value "<variable_name>" to the "<element_key>" with the "<selection_method>" of "<XPATH or ID>"
	And Enter variable value "<variable_name>" to the "<element_key>" with the "<selection_method>" of "<XPATH or ID>"
	And I read the URL and store in "<variable_name>" variable
	And Read the URL and store in "<variable_name>" variable
	And I read the content of element "<element_key>" and store in variable "<variable_name>"
	And Read the content of element "<element_key>" and store in variable "<variable_name>"
	And I read the "<URL_Param_Numbrer>"st element of the URL path and store in "<variable_name>" variable
	And Read the "<URL_Param_Numbrer>"st element of the URL path and store in "<variable_name>" variable
	And I read the "<URL_Param_Numbrer>"nd element of the URL path and store in "<variable_name>" variable
	And Read the "<URL_Param_Numbrer>"nd element of the URL path and store in "<variable_name>" variable
	And I read the "<URL_Param_Numbrer>"th element of the URL path and store in "<variable_name>" variable
	And Read the "<URL_Param_Numbrer>"th element of the URL path and store in "<variable_name>" variable
	And The value of variable "<variable_name>" is equal to "<element_key>"
	And Value of variable "<variable_name>" is equal to "<element_key>"