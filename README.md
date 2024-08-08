# PasteBinClone

PasteBinClone is a web application that replicates the core functionality of the popular PasteBin site, allowing users to share text-based posts with various options and configurations. The project is designed with a focus on clean architecture, scalability, and following REST principles. Also, the project uses many different libraries to make development more comfortable and improve the performance of the application.


## Overview

PasteBinClone is designed as a full stack web application with a backend based on WEB APIs and a frontend built using ASP.NET Core MVC. The project utilises a full stack of technologies and follows clean architecture principles to ensure maintainability and separation of concerns.

## Сore Features
+ User Registration & Authentication.
  + Users can register and log in.
  + The user will need to pass Google reCAPTCHA v2.
+ Post Management
  + Create posts with the ability to select category, type, language and expiry date.
  + Create private posts with password protection, available only to you and those people who have a link to the post and password.
  + Edit and delete posts.
+ Interaction with Posts
  + View posts shared by other users.
  + Share or download posts with others.
  + Comment on posts and interact with them by liking or disliking.
+ User Profile
  + Access personal information and statistics related to your posts.
+ Admin features
  + Admins have access to the admin menu to create new filters.
  + Manage user content by deleting posts and comments.

## Technology Stack
+ Backend
  + WEB API
  + Entity Framework Core
  + MS SQL
  + Amazon S3
  + Redis
  + SeriLog
  + AutoMapper
  + Fluent Validation
+ Frontend
  + ASP.NET Core MVC
  + HTML/CSS
  + Bootstrap
+ Authentication
  + Duende Identity Server
  + Google reCAPTCHA v2
  + JWT Bearer
+ Testing
  + xUnit
  + Moq
  + Fluent Assertion
  + In-Memory Database
