# Using MongoDB with .NET Core WebAPI
this repo is based on this blog post - [http://qappdesign.com/using-mongodb-with-net-core-webapi](http://qappdesign.com/using-mongodb-with-net-core-webapi)

in this project i tried to implement a dot net core web api project 
with mongodb database integration
and tried to cover as many topics of dotnet core


#### Topics Covered
- Technology stack
- Configuration model
- Options model
- Dependency injection
- MongoDb – using MongoDB C# Driver v.2
- Make a full ASP.NET WebApi project, connected async to MongoDB
- Allowing Cross Domain Calls (CORS)
- Update entire MongoDB documents
- Exception management
- Swagger UI

 #### How to run it
 - Download or clone this project locally 
 - Install the tools - see here more details: [http://qappdesign.com/using-mongodb-with-net-core-webapi](http://qappdesign.com/using-mongodb-with-net-core-webapi)
 - create a database user with this code:
 `
    use admin
    db.createUser(
    {
        user: "admin",
        pwd: "abc123!",
        roles: [ { role: "root", db: "admin" } ]
    }
    );
    exit;
 `
 - and run the project in Visual Studio

