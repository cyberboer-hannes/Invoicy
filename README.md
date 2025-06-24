# Invoicy

Invoicy is a demo full-stack invoicing application built with Clean Architecture principles using an Angular frontend and ASP.NET Core backend.  
This is a demonstration project and not intended for production use.

## Features

- Manage customers
- Create invoices linked to customers
- Automatic invoice number generation
- Customer deduplication based on phone number
- Inline customer search while creating invoices
- Full validation and error handling
- Responsive frontend design

## Tech Stack

### Backend

- ASP.NET Core 8, C#
- Clean Architecture (Domain, Application, Infrastructure, API layers)
- Entity Framework Core
- SQL Server
- Dependency Injection
- Domain-Driven Design (DDD)

### Frontend

- Angular 17 standalone components
- Angular Routing
- Reactive Forms
- Toastr for notifications
- Bootstrap for styling

## Architecture

The solution follows Clean Architecture:

- **Domain Layer:** Business logic and entities (Customer, Invoice, Address)
- **Application Layer:** Commands, Queries, Services, DTO mappings, Validators
- **Infrastructure Layer:** EF Core persistence, Repositories, Migrations
- **API Layer:** Controllers, REST API endpoints

The frontend communicates with the backend through a REST API.

## Setup

### Backend

- ASP.NET Core 8 project using EF Core
- Run EF Core migrations to create the database schema

### Frontend

- Angular 17 standalone app
- Run `npm install` and `ng serve` to start the frontend

## Notes

- Full CRUD for customers
- Invoices can be created and viewed (editing not implemented)
- Phone number uniqueness is enforced for customer records

---

*This project is for demo purposes only and is not a complete production system.*
