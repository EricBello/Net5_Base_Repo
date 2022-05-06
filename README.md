# # Net 5.0 -- API Base Project

This a base project to start fast with a new c#.net 5.0 API

## Getting Started


### Installing

Install or update all the packages

### Executing program

1. In the infrastructure project is  DbScriptSql file, to run in your local instance of SQL server, contain only one table EmployeeTable.
2. Change the CnString in the appSetting.js file to the new DB recently created.

### Libraries and special packages used

1. Swagger
2. Serilog
3. FluentValidations
4. AutoMapper

### Important

This project is using middleware to handle all exceptions and track the log. Is in the api project : LogsAndErrorHandlerMiddleware
