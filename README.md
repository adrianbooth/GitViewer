# GitViewer
Simple mvc site to load user info from github api

## Prerequisites
To run the web application you will need .NET Framework Runtime v4.7.2 

To run the tests from visual studio you will need a test runner compatible with NUnit 3 tests for Example NUnit 3 test Adapter. info here: [NUnit test runner] (https://github.com/nunit/docs/wiki/Visual-Studio-Test-Adapter)

## Structure
While this is a simple application with limited functionality the application has been structured to split out the different concerns to aid expansion later. Anything presentational such as views and view models are contained in the main GitViewer web project, logic and mappings in the service layer, data access (although via api now) is contained in the repository/data layer. Cross cutting concerns such as models and logging are held in the Domain/core project.

## Testing
The unit tests included in the project use NUnit and Moq. 

## Task
This project was completed to satisfy the following [Task](/task.md)
