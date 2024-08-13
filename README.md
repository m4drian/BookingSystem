# Endpoints

## User Management

- Registration:
POST /api/auth/register

- Login:
POST /api/auth/login

## Location Management

- Create Location (admin only):
POST /api/locations

- Get All Locations:
GET /api/locations

- Update Location (admin only):
PUT /api/locations/{locationId}

- Delete Location:
DELETE /api/locations/{locationId}

- Get Desks in Location (admins can see who reserved them, employees can only see if they are aviable):
GET /api/locations/{locationId}/desks

## Desk Management

- Create Desk (admin only):
POST /api/desks/{locationId}

- Delete Desk (admin only):
DELETE /api/desks/{deskId}

- Update Desk Reservation
(admins can remove reservation and change desk details except date, employees can reserve desk or cancel their own reservation):
POST /api/desks/reservation

Check if desk is reserved:
GET /api/desks/{deskId}

Check all desks (admin only):
GET /api/desks


# Information about models

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

## Requirements
Administration:
- Manage locations (add/remove, can't remove if desk exists in location)
- Manage desk in locations (add/remove if no reservation/make unavailable)

Employees
- Determine which desks are available to book or unavailable.
- Filter desks based on location
- Book a desk for the day.
- Allow reserving a desk for multiple days but now more than a week.
- Allow to change desk, but not later than the 24h before reservation.
- Administrators can see who reserves a desk in location, where Employees can see only that specific desk is unavailable.
