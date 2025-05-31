# CloudQA Automation Practice Form Tests

This project contains automated tests for the CloudQA Automation Practice Form using C# and Selenium WebDriver.

## Prerequisites

- .NET 6.0 or later
- Chrome browser installed

## Required NuGet Packages

- Selenium.WebDriver
- Selenium.Support
- MSTest.TestFramework
- MSTest.TestAdapter

## Test Description

The tests verify three fields on the CloudQA Automation Practice Form:
1. First Name field
2. Email field
3. Gender radio buttons

The tests are designed to be resilient to changes in the HTML structure by:
- Using multiple selector strategies for each element
- Implementing proper wait mechanisms
- Including error handling with screenshots
- Using flexible element location methods

## How to Run

1. Clone this repository
2. Open the solution in Visual Studio or VS Code
3. Restore NuGet packages
4. Run the tests using Test Explorer or `dotnet test` command 
