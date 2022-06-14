# Neural Recommendation Application

This is the Neural Recommendation System. 
The system provides product recommendations for each user based on their previous reviews, 
in particular,  based on the characteristics of the products they liked.
The user interface runs on Android, the server runs from under any Windows family system.

## Stack

- Microsoft Visual Studio 2019
- Express SQL Server 2019
- .NET 5.0
- Unity 2021.2.17f1

## Folder structure

We support a collection of templates, organized in this way:

- [`Prototyping`](./Prototyping) contains diagrams and images.
- [`RecommendationSystemClasses`](./RecommendationSystemClasses) contains 
  class library with all of models of Microsoft SQL Database
- [`RecommendationSystemCore`](./RecommendationSystemCore) contains windows server,
  which accepts API requests (making lists of object, create recommendations list, etc.) and 
  send response to UI
- [`RecommendationSystemUI`](./RecommendationSystemUI) contains android application (.apk),
  which interacts with user and making requests to Core.

## Installation

1. Install [`Core`](./RecommendationSystemCore) to windows system, compile the last version 
   and run RecommendationSystemCore.exe;
2. Install SQL server and execute [`export.sql`](./Prototyping/export.sql)
2. Open [`UI`](./RecommendationSystemUI) project in Unity, change IP address to your [`Core`](./RecommendationSystemCore) in the Database GameObject;
3. Depoly your [`UI application`](./RecommendationSystemUI) and install;
4. Enjoy!

## License

[CC BY-NC 4.0](https://creativecommons.org/licenses/by-nc/4.0/).
