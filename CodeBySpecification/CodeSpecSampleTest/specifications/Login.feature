@UIAutomationTest
Feature: Facilit Application Login
	As a user I want to login to the system

Scenario: Login Fail
	Given Navigate to SUT
	And The "UsernameTextBox" with the "id" of "username" is shown
	And The "PasswordTextBox" with the "id" of "password" is shown
	Then Enter "ssdfsd" to the "UsernameTextBox"
	And I enter "fdf" to the "PasswordTextBox"
	When The "LoginButton" with the "xpath" of "id('loginform')/div[5]/div" is shown
	Then Click on "LoginButton"
	And "ErrorMessage" with the "id" of "errormessage" is shown
	And The content of "ErrorMessage" contains text "Innlogging mislyktes."

Scenario: Login Sucess
	Given Navigate to SUT
	And The "UsernameTextBox" with the "id" of "username" is shown
	And The "PasswordTextBox" with the "id" of "password" is shown
	Then Enter "br_99x" to the "UsernameTextBox"
	And I enter "facilit99x" to the "PasswordTextBox"
	When The "LoginButton" with the "xpath" of "id('loginform')/div[5]/div" is shown
	Then Click on "LoginButton"
	And I wait for the "UserDetails" with the "xpath" of "id('masterheader')/div[2]/a[4]" to show
	And The content of "UserDetails" contains text "99x Tech (surenr@99x.lk)"
	And 

Scenario: Add a new widget
	Given Navigate to SUT
	And The "UsernameTextBox" with the "id" of "username" is shown
	And The "PasswordTextBox" with the "id" of "password" is shown
	Then Enter "br_99x" to the "UsernameTextBox"
	And I enter "facilit99x" to the "PasswordTextBox"
	When The "LoginButton" with the "xpath" of "id('loginform')/div[5]/div" is shown
	Then Click on "LoginButton"
	And I wait for the "UserDetails" with the "xpath" of "id('masterheader')/div[2]/a[4]" to show
	And The content of "UserDetails" contains text "99x Tech (surenr@99x.lk)"
	Then Click on element "WidgetsButton" with the "xpath" of "id('mastercentercontent')/div/div[3]/table/tbody/tr[3]/td[4]/div/a"
	And I wait for the "AvviksstatusWidget" with the "id" of "DeviationStatusWidget~DeviationStatusWidget001" to show
	And I wait for the "widget_container_003" with the "id" of "widget_container_003" to show
	Then I drag "AvviksstatusWidget" and drop on to "widget_container_003"