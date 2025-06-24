# Invoicy

Invoicy is a full-stack demo invoicing application built with Clean Architecture principles using an Angular frontend and ASP.NET Core backend.  
This is a demonstration project for learning purposes and not intended for production use.

---

## Features

- Manage customers
- Create invoices linked to customers
- Automatic invoice number generation
- Customer deduplication based on phone number
- Inline customer search while creating invoices
- Full validation and error handling
- Responsive frontend design

---

## Tech Stack

### Backend

- ASP.NET Core 8 (C#)
- Clean Architecture (Domain, Application, Infrastructure, API layers)
- Entity Framework Core
- SQL Server
- Dependency Injection
- Domain-Driven Design (DDD)

### Frontend

- Angular 17 (Standalone Components)
- Angular Routing
- Reactive Forms
- Toastr for notifications
- Bootstrap for styling

---

## Project Structure
/src
/Invoicy.Api - ASP.NET Core API (backend entry point)

/Invoicy.Application - Application layer (commands, queries, services)

/Invoicy.Domain - Domain layer (entities, business rules)

/Invoicy.Infrastructure - Infrastructure layer (EF Core, repositories)

/invoicy-web - Angular frontend application

---

## Setup

### Backend

- ASP.NET Core 8 project using EF Core
- Run EF Core migrations to create the database schema
- Update connection string as needed in `appsettings.json`

### Frontend

- Navigate to `src/invoicy-web`
- Run `npm install`
- Start frontend with `ng serve`

---

## Notes

- Full CRUD operations available for Customers
- Invoices can be created and viewed (editing not yet implemented)
- Customer phone number uniqueness enforced
- Clean Architecture applied across entire solution

---

*This project is for educational and demo purposes only.*
