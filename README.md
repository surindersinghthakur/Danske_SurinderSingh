# Danske_SurinderSingh
Main API Endoint https://localhost:44367/api

Technologies used:
-- .NetCore 3.0
-- SQL Server 2014 or higher
-- ORM: Dapper (Free nuget package)
-- xUnit
-- Postman -- For API endpoint testing
-- Dbup - Tried it but I got conflict with package and does not look relevant for this task as it is test task.
      -- tried to use to highlight that we can use this package for auto db upgrades.


What is Done:
-- Add, Edit, List, Get by add endpoints for Municipality and Tax Scheduling
-- Get Tax Rate for any municipality for any date
-- Tests -- Postman tests and XUnit (have added only 1 test there, as it is already there on postman)
-- DI (Default from .NetCore, have not used Unity or any other container)
-- Documentation on Interface methods itself


What is not done:
 -- Authentication/Authorization -- I have used mostly Auth0 for that, have not tried here, however have kept code in startup.cs
 -- XUnit full tests - have written one to demonstrate that I am aware of that
 -- Mocking -- have not mocked Db objects in test
 -- Picking validation messages from resource or appsettings/app.config
 

Setup:
-- Create new empty database
-- Run script file from DbUp -> Scripts -> CreateSchema.sql
-- Update connection string in "appsettings.json" or we can add in User Secrets file
-- Run below Postman tests, replace endpoint if different:
   All endpoints are available in Postman with samples and TESTS as well, below is link:
   https://www.getpostman.com/collections/bdfac9236bd3e3060fd0

Add single municipality:
  https://localhost:44367/api/municipality/add
  
  Add multiple tax schedules, it will add 4 mentioned in document:
  https://localhost:44367/api/taxes/add/list
  
  Fetch tax rates for different parameters:
  https://localhost:44367/api/TaxRate/get?municipality=Copenhagen&taxDate=2016-01-01
  https://localhost:44367/api/TaxRate/get?municipality=Copenhagen&taxDate=2016-05-02
  https://localhost:44367/api/TaxRate/get?municipality=Copenhagen&taxDate=2016-07-10
  https://localhost:44367/api/TaxRate/get?municipality=Copenhagen&taxDate=2016-03-16
  

Main Methods:
Municipality: Add, Add List, Get by Id, Get List
Tax Schedules: Add, Add List, Get by Id, Get List
Get Tax Rates: Get based on Municipality and Tax Date                                  
                                  


Explain the logic in the readme file
     -- Get tax rate for particular municipality for a specific date
		     Checks if daily rate is available for passed date and municipality
         If not then checks for weekly, if not then checks monthly and lastly checks yearly
         Below is order of Tax Rate returned: 
          Daily, Weekly, Monthly, Yearly
·         

