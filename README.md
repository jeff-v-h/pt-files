# PT Files

A project to practice a combination of React, Redux, TypScript, .NET Core with Clean Architecture and deployment on AWS. Physiotherapy management software for entering and storing data for consultations.

## Getting started

### Requirements

#### Front-end

- TypeScript 3.8
- React 16.3
- Redux 4

#### Back-end

- C# .NET Core 3.1
- Entity Framework Core 3.1
- SQL Server 2019

#### Additional

- Docker

### Installation

The TypeScript api models are automatically generated/updated every time the back-end successfully builds. This created file (client/src/api/generated.ts) is committed to source control but should it not be up-to-date, it may be necessary to build the backend first.

There are two methods of getting up and running, with and without docker. For instructions with docker-compose scroll down to the "With Docker" section.

#### Without Docker

##### Database

1. Download SQL Server and ensure the default MSSQLSERVER is present. If you wish to use another SQL server, please change the connection string in appsetting.development.json
2. Ensure method CreateAndSeedDbIfNotExists in Program.cs Main method is uncommented to ensure the database schema is created on back-end startup.

##### API

1. Open Solution with Visual Studio.
2. Target PTFiles.Web and Run project on IIS Express.
3. A new browser should open up to [swagger](http://localhost:5555/swagger) when successfully built, otherwise open browser to [localhost:5555/swagger)](http://localhost:5555/swagger). It also serves static content from wwwroot if there are front-end built files existing in there. You may navigate to view this static content at [localhost:5555](http://localhost:5555).

##### Client

1. Navigate to client project from root `cd client`.
2. Install dependencies `npm install`.
3. Run the frontend client `npm start`. (It may be necessary to `npm rebuild node-sass` beforehand)
4. Visit [localhost:3000](http://localhost:3000) in your browser.

#### With Docker

Docker Compose tool is used with Nginx reverse proxy to create a multi-container application.

**Note:** Hot module replacement is not setup by default. You will need to uncomment devServer.watchOptions in webpack.dev.js and may need to do additional configuration to get it working.

1. Open command line to the root of the entire project where docker-compose file sits.
2. Run `docker-compose up`
3. Open browser to [localhost:3000](http://localhost:3000). If using Windows Home (Docker Toolbox), you may need to visit [192.168.99.100:3000](http://192.168.99.100:3000) (Docker's default ip address) instead.

## Production

CI/CD is currently setup with Travis CI.

1. Merge/push any changes to master branch to initiate CI/CD process.

## Authors

Jeffrey Huang - jeffvh@outlook.com
