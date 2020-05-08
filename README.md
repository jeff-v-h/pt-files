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

### Installation

The TypeScript api models are automatically generated/updated every time the back-end successfully builds. This file (client/src/api/generated.ts) is committed to source control but should it not be up-to-date, it may be necessary to build the backend first.

#### Database

1. Download SQL Server and ensure the default MSSQLSERVER is present. If you wish to use another SQL server, please chagne the connection string appsetting.development.json
2. Ensure method CreateAndSeedDbIfNotExists is uncommented to ensure the database schema is created on back-end startup. Uncomment DbContextSeed.Initialise method to seed database.

#### API

1. Open Solution with Visual Studio.
2. Target PTFiles.Web and Run project on IIS Express.
3. A new browser should open up to [localhost:5555](http://localhost:5555) when successfully built, otherwis open browser to this url. By default it serves static content from wwwroot if there are front-end built files existing in there. Otherwise you may navigate to view [swagger](http://localhost:5555/swagger).

#### Client

1. Navigate to client project from root `cd client`.
2. Install dependencies `npm install`.
3. Run the frontend client `npm start`.

## Production

CICD is currently not setup. The following method manually builds and uploads a zip onto AWS.

1. Build front-end client first: Navigate to client project from root `cd client` and build with `npm run build`. This will build files into src directory's wwwroot folder.
2. Once front-end is built, navigate back to root `cd ..`.
3. Build application `dotnet publish -c Release -o publish`. This will create a build the files into a directory named 'publish'. Old files will be replaced if this directory already exists.
4. Zip the contents of this publish folder by selecting everything inside (Either use command line or use you local machine's zip package). Make sure you zip the inner contents, not the 'publish' directory itself.
5. Login to AWS and navigate to the correct environment (Please see author for instructions and credentials).
6. Click 'Upload and Deploy' and select the recently created zip to upload.

## Authors

Jeffrey Huang - jeffvh@outlook.com
