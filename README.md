# Booking System API

Project implementing clean architecture, partially inspired by Amichai Mantinband's guidelines for clean architecture.

Main libraries used:
xUnit, FluentValidation, MediatR, DependencyInjection, Jwt

# Project Requirements
Administration:
- Manage locations (add/remove, can't remove if desk exists in location)
- Manage desk in locations (add/remove if no reservation/make unavailable)

Employees
- Determine which desks are available to book or unavailable.
- Filter desks based on location
- Book a desk for the day.
- Allow reserving a desk for multiple days but no more than a week.
- Allow to change desk, but not later than the 24h before reservation.
- Administrators can see who reserves a desk in location, where Employees can see only that specific desk is unavailable.

## How to build and run project

```
dotnet build
```

```
dotnet run --project .\src\BookingSystem.Api\ 
```

To run unit tests:
```
dotnet test
```

Tested specifically "BookingSystem.Application.UnitTests.Desks.Commands"

Various endpoints can be tested with REST Client for Visual Studio Code, requests available in folder "Requests"

# Endpoints

## User Management

- Registration:
```json
POST /api/auth/register
```

- Login:
```json
POST /api/auth/login
```

## Location Management

- Create Location (admin only):
```json
POST /api/locations
```

- Get All Locations:
```json
GET /api/locations/all
```

- Get Desks in Location (admins can see who reserved them, employees can only see if they are aviable):
```json
GET /api/locations/desks/{locationName}
```

- Update Location (admin only):
```json
PUT /api/locations/{locationName}
```

- Delete Location (admin only):
```json
DELETE /api/locations/{locationName}
```

## Desk Management

- Create Desk (admin only):
```json
POST /api/desks/{locationName}
```

- Check all desks (admin only):
```json
GET /api/desks/all
```

- Check if desk is reserved:
```json
GET /api/desks/{deskId}
```

- Update Desk Reservation
(admins can remove reservation and change desk details except the dates, employees can reserve desk or cancel their own reservation):
```json
PUT /api/desks/reservation/{deskId}
```
```json
PUT /api/desks/reservation/admin/{deskId}
```

- Delete Desk (admin only):
```json
DELETE /api/desks/{deskId}
```

## Information about models

location
- Id string required
- Name string required
- Description string? optional
- Desks [string] //array of desk IDs, can be empty

desk
- Id string required
- LocationId string required
- UserEmail string? optional
- Available boolean required (default true)
- StartDate date? optional
- EndDate date? optional

user
- Id string required
- FirstName string required
- LastName string required
- Email string unique required
- Password string required
- Role string {admin / employee} required

one location has many desks
one desk can be booked by one user
user can book only one desk
