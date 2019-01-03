This is a template for creating API using .NET Core.
The example provided enable user to Create, Read, Update, and Delete a movie collection. 


##Pre Requisite:
 - Have a database installed locally, and update DBConnectionString with your credentials under appSettings.json. 
 - Run 'Database' project, this use Dapper for data access. and creates database if not exist (see stored procedure inside 'scripts' folder)
 - Run 'TestDotNetCore' project and browser list out all available endpoints using Swagger.