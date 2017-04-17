# BareMetalApi

> Live example running on Heroku right now. Check out Heroku_Deploy branch for more details

### WHAT

A lightweight and cross platform ASP.Net Core JSON API. CRUD to your PostgreSQL database out of the box thanks to [Npgsql](http://www.npgsql.org/) driver and EF Core dependency injection.  Includes data migration script that runs on startup and seeds initial data. Get started right away!

Data security using tokens,JWT, and [ASP.Net Core Identity](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity), claims based authentication.


### HOW

**Disclaimer**

This project borrows heavily from the following repos:
   
1. [ToDoApi](https://github.com/aspnet/Docs/tree/master/aspnetcore/mobile/native-mobile-backend/sample/ToDoApi)  blog post [here](https://docs.microsoft.com/en-us/aspnet/core/mobile/native-mobile-backend)
   
2. [ASPNETcoreAngularJWT](https://github.com/Longfld/ASPNETcoreAngularJWT)  

**Prerequisites**

1. Install .NET Core  (https://www.microsoft.com/net/core)
2. Install PostgreSQL (https://www.postgresql.org/)
3. Add an appsettings.json file to /src/BareMetalApi, which will define your db connection string.<br/>
   * `{` <br/>
         `"ConnectionStrings": {` <br/>
            `"DefaultConnection": "User ID=postgres;Password=password;Host=localhost;Port=5432;Database=Blog;Pooling=true;"` <br/>
            `}` <br/>
    `}` <br/>

**To run application**

1. Download repository
2. Open command prompt and navigate to /src/BareMetalApi
3. Run command "dotnet restore"
   * Please create new [issue](https://github.com/hatoro/BareMetalApi/issues/new?title=Restore_Issue&assignee=hatoro&body=My%20Platform:______%20<br/>%20Operating%20System:_______%20<br/>%20DotNet%20Core%20Version:_____) if you are having trouble downloading dependencies
4. Run command "dotnet run"
   * App will compile then run, wait for message `Application started. Press Ctrl+C to shut down.`
5. First Register a User: Use Postman to send a POST request in order to register your first user.
    * `POST http://localhost:5000/blog/account/register`<br/>
      `{"Email" : "YourName@ok.com", "PasswordHash" : "Abc!"}`
6. Get Security Token: Use Postman to login your user.
   * `POST http://localhost:5000/blog/account/login`<br/>
      `Body`<br/>
      `x-www-form-urlencoded`<br/>
      `Email`  `YourName@ok.com`<br/>
      `Password`  `Abc123!`<br/>
7. Use your security tokens to send JSON GET, POST, PUT, and DELETE requests.<br/>
   * `GET http://localhost:5000/blog/blogarticle`<br/>
      `Headers`<br/>
      `Authorization`   `Bearer eyJhbGc...FULL TOKEN...RrXfOA`<br/>
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
      `Headers`<br/>
      `Authorization`   `Bearer eyJhbGc...FULL TOKEN...RrXfOA`<br/>
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

