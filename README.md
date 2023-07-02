# aspnetcoremicroservice
Sample Asp.NET Core Microservice project for online shopping tools

There are 4 microservices
1. Basket Microservice using ASP.NET core with Redis Cache
2. Catalog Microservice using ASP.NET core with MongoDB
3. Discount GRPC Service using ASP.NET core gRPC and PostreSQL
4. Order Microservice
   
   4.1.  Asp.NET core using Clean Architecture
   
   4.2.  CQRS with Repository Pattern using MediatR
   
   4.3.  EF Core Migrations using SQL Server
   
   4.4.  Auto Mapper
   
   4.5.  Database Migrations
   
   4.6.  Sending emails using Sendgrid
   
   4.7.  Pre and post processing using Pipeline behaviour
   
   4.8.  Model validations using FluentValidations
   
   4.9.  Centralized Exception handling
   
   4.10. Distributed dependency injection for different projects
   

Along with the services it consists docker orchestration support which contains the insturctions for hosting these services with all of its dependencies in docker.

Each services are created for learning purpose only using different possible approach. This project is for proof of concept only and not to be refernced in any real time project.
