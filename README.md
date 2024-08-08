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

## Images
Next, I'll demonstrate some of the main pages of the site.
### Login page
![Login](https://github.com/user-attachments/assets/588b44da-f081-4a3d-b0d7-dc61662f7ff6)
### Home page
![Home](https://github.com/user-attachments/assets/5d1a3f4a-da27-473e-961a-3b2f6868220e)
### Crate Paste
![Create](https://github.com/user-attachments/assets/63ea2231-c00d-4553-86ad-a972ff0a0cc3)
### View Paste 1.
![Paste1](https://github.com/user-attachments/assets/c2600a7f-43ec-47c8-ac2b-38535dff5d15)
### View Paste 2.
![Paste2](https://github.com/user-attachments/assets/f6db2671-e9d9-4978-89f9-bab36f73d309)
### User profile
![UserProfile](https://github.com/user-attachments/assets/ca8121e3-eaf2-416d-8a37-c7cb93d5c9e9)
### User Pastes
![UserPastess](https://github.com/user-attachments/assets/50e340cd-21ed-4351-a62d-0ada4485b330)
### Admin menu
![AdminMenu](https://github.com/user-attachments/assets/7ce1f45d-82cb-4085-a026-35c989be3aef)


