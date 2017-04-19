# BareMetalApi Heroku_Deploy

> Live example running on Heroku right now. Check out Heroku_Deploy branch for more details

### WHAT

A lightweight and cross platform ASP.Net Core JSON API. CRUD to your PostgreSQL database out of the box thanks to [Npgsql](http://www.npgsql.org/) driver and EF Core dependency injection.  Includes data migration script that runs on startup and seeds initial data. Get started right away!

Data security using tokens,JWT, and [ASP.Net Core Identity](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity), claims based authentication.

Hosted for free on Heroku with additional free Postgres database add-on.


### HOW

**Disclaimer**

This project borrows heavily from the following repos:
   
1. [ToDoApi](https://github.com/aspnet/Docs/tree/master/aspnetcore/mobile/native-mobile-backend/sample/ToDoApi)  blog post [here](https://docs.microsoft.com/en-us/aspnet/core/mobile/native-mobile-backend)
   
2. [ASPNETcoreAngularJWT](https://github.com/Longfld/ASPNETcoreAngularJWT)  

**To run application**

1. Fork repo branch
1. Create Heroku account
2. New Heroku app
3. Resources => Add-on Heroku Postgres
4. Settings => Buildpacks => Add buildpack https://github.com/se/heroku-core
5. Deploy => GitHub (Connect to your forked repo)
6. Deploy => Manual deploy => Deploy Branch

**To use application**

>Live preview on Heroku, send requests to http://bangequal-server.herokuapp.com

1. Get Security Token: Use Postman to login a user.
   * `POST http://bangequal-server.herokuapp.com/blog/account/login`<br/>
      `Body`<br/>
      `x-www-form-urlencoded`<br/>
      `Email`  `larry@ok.com`<br/>
      `Password`  `Abc123!`<br/>
2. Use your security tokens to send JSON GET, POST, PUT, and DELETE requests.<br/>
   * `GET http://bangequal-server.herokuapp.com/blog/blogarticle`<br/>
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
    * `GET http://bangequal-server.herokuapp.com/blog/blogarticle/3` <br/>
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

