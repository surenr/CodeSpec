# CodeSpec User Guide

This document guids you how to write UI testing using CodeSpec UI testing framework on Microsoft Visual Studio . 

## Creating new project

1. Create a standard MSTest Unit Test Project
2. Delete the Generated UnitTest1.cs class
3. Add CodeSpec dependency via NuGet ( This will generate the needed directory strucute and install other dependencie )
4. Create a new folder for your specifications, let's call this folder `Specifications`.
5. Create a new SpecFlow Feature File inside `Specifications` folder ( For this you shold have SpecFlow plugin installed. By default this SpecFlow Feature File contains Feature section Scenario section. )
6. Annotate the Feature with UIAutomationTest tag 
  ``` 
  
  @UIAutomationTest
  Feature: MyWebOne Application Login
  As a user I want to login to the system
  
  ```
7. Update the Feature section accordingly. 
8. Define the scenario. You can define multiple scenarios in one feature file. 
  ```
 
  Scenario: Login Sucess
  	Given Navigate to SUT
  	And The "UsernameTextBox" with the "id" of "username" is shown
  	And The "PasswordTextBox" with the "id" of "password" is shown
  	Then Enter "admin" to the "UsernameTextBox"
  	And I enter "abc123" to the "PasswordTextBox"
  	When The "LoginButton" with the "xpath" of "id('loginButton')/a" is shown
  	Then Click on "LoginButton"
  	And I wait for the "UserDetails" with the "xpath" of "id('User')/div/b" to show
  	And The content of "UserDetails" contains text "Admin user"
 	
  ```
9. In your App.config change the `UI.Tests.SUT.Url` to the URL of your SUT( Site Under Test )
10. Right click on your project and click Run SpecFlow Scenarios to run your scenarios. 
