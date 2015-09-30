# CodeSpec User Guide

This document guides you how to write UI testing using CodeSpec UI testing framework on Microsoft Visual Studio .

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

  Scenario: Login Success
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

#Continious integration with Jenkins

*NOTE: If you want to record the tests, then make sure you run Jenkins using the 'java -jar {directoryOfJenkinsFile}/jenkins.war' command, and not running as a service in Windows.*


1. Create a new project or you can use your existing MS Test project on Jenkins.
2. We have to download the dependencies, you can use the following for that.

Add a new build step of "Execute Windows batch command" and give the following the command

```
path\to\nuget.exe restore SolutionName\ProjectName.sln
```

An example would be,

```
tools\nuget.exe restore CodeBySpecification\CodeBySpecification.sln
```

3. We can now build the solution, you can use the following for that.

Add a new build step of "Build a Visual Studio project or solution using MSBuild" and give the following the params.

MSBuild Version : Whatever the version you set in the Jenkins configurations for MSBuild plugin.

MSBuild Build File : ${WORKSPACE}\SolutionName\ProjectName.sln

Command Line Arguments : /p:VisualStudioVersion=12.0 /p:DefineConstants=DEBUG

4. Add a new build step of "Execute Windows batch command" and give the following the command

```
"path\to\MSTest.exe" /testcontainer:SolutionName\ProjectName\TestsFolder\TestFile.orderedtest
```

An example would be,

```
"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\mstest" /testcontainer:CodeBySpecification\CodeSpecSampleTest\OrderedTests\GoogleCatsAndDogs.orderedtest
```

Alternatively you can use the "Run unit tests with MSTest" if you have installed the "MSTestRunner plugin"
