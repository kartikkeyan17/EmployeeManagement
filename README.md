# EmployeeManagement

Overview of project
Managing employee data and functionality provie to view,Add and update employee Data

Technology used
.Net core web api version 8
ORM is entity framework core (Databse first Approach)
Database Sql server

Instruction

*First run the sql scripts in local database

*Set profiler IIS Express to run the application use swagger to test Api

*Here use globalexception middelware to handle exception change
For your information
change the filepath in GlobalExceptionMiddleware class
private const string _filePath = @"C:/Users/karti/source/repos/EmployeeManagement/logs.txt";
