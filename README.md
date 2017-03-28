# BareMetalApi

### WHAT

ASP.Net Core JSON API that is lightweight and cross platform. CRUD to your PostgreSQL database out of the box thanks to [Npgsql](http://www.npgsql.org/) driver and EF Core dependency injection.  Includes data migration script that runs on startup and seeds initial data. Get started right away!

Security by [ASP.Net Core Identity](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity) claims based authentication system that is token based, JWT.


### HOW

**Disclaimer**

This project borrows heavily from the following repos:
   
1. [ToDoApi](https://github.com/aspnet/Docs/tree/master/aspnetcore/mobile/native-mobile-backend/sample/ToDoApi)  blog post [here](https://docs.microsoft.com/en-us/aspnet/core/mobile/native-mobile-backend)
   
2. [BareMetalApi](https://github.com/benfoster/BareMetalApi)  blog post [here](http://benfoster.io/blog/bare-metal-apis-with-aspnet-core-mvc)

3. [ASPNetCoreAngular2YoExample](https://github.com/osya/ASPNetCoreAngular2YoExample)  

**Prerequisites**

1. Install .NET Core  (https://www.microsoft.com/net/core)
2. Install PostgreSQL (https://www.postgresql.org/)
3. Update lines 22 and 23 in the file [Models/ApplicationDbContext.cs](https://github.com/hatoro/BareMetalApi/blob/master/src/BareMetalApi/Models/ApplicatonDbContext.cs) with your PostgreSQL user name and password

To run application

1. Download repository
2. Open command prompt and navigate to /src/BareMetalApi
3. Run command "dotnet restore"
   * Please create new [issue](https://github.com/hatoro/BareMetalApi/issues/new?title=Restore_Issue&assignee=hatoro&body=My%20Platform:______%20<br/>%20Operating%20System:_______%20<br/>%20DotNet%20Core%20Version:_____) if you are having trouble downloading dependencies
4. Run command "dotnet run"
5. Use Postman to send JSON GET, POST, PUT, and DELETE requests.
   * `GET http://localhost:5000/blog/blogarticle`<br/>
      {<br/>
       "Id": 1, <br/>
       "ArticleTitle": "How to Dabb", <br/>
       "ArticleContent": "First tuck you head down..." <br/>
      }, <br/>
      { <br/>
      "Id": 2, <br/>
      "ArticleTitle": "How to Whip", <br/>
      "ArticleContent": "Rock back and forth..." <br/>
      }, <br/>
      { <br/>
      "Id": 3, <br/>
      "ArticleTitle": "How to Nae Nae", <br/>
      "ArticleContent": "Add a connecting move..." <br/>
      }, <br/>
      { <br/>
      "Id": 4, <br/>
      "ArticleTitle": "How to Dougie", <br/>
      "ArticleContent": "Pass your hand through..." <br/>
      }, <br/>
      { <br/>
      "Id": 5, <br/>
      "ArticleTitle": "How to Wop", <br/>
      "ArticleContent": "Worm your upper body..." <br/>
      } <br/>
      <br/>
    * `GET http://localhost:5000/blog/blogarticle/3` <br/>
      { <br/>
      "Id": 3, <br/>
      "ArticleTitle": "How to Nae Nae", <br/>
      "ArticleContent": "Add a connecting move..." <br/>
      } <br/>
      <br/>
    * `POST http://localhost:5000/blog/blogarticle` <br/>
      `{"ArticleTitle":"How to Running Man","ArticleContent":"Lift your right foot and..."}` <br/>
      <br/>
    * `PUT http://localhost:5000/blog/blogarticle/4` <br/>
       `{"ArticleTitle":"How to Moonwalk","ArticleContent":"Place one foot directly..."}` <br/>
       <br/>
    * `DELETE http://localhost:5000/blog/blogarticle/5`

