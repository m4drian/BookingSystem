# Endpoints

## Create Location:

POST /api/locations
Request body: { name: string }

Get All Locations:
GET /api/locations

Update Location:
PUT /api/locations/{locationId}
Request body: { name: string }

Delete Location:
DELETE /api/locations/{locationId}

## Desk Management

Create Desk:
POST /api/locations/{locationId}/desks

Get Desks in Location:
GET /api/locations/{locationId}/desks

Delete Desk:
DELETE /api/desks/{deskId}


## Reservations
Create Reservation:
POST /api/reservations/{reservationId}

Delete Reservation:
DELETE /api/reservations/{reservationId}

## Desk Reservations

Get Desk Reservations for Location:
GET /api/locations/{locationId}/reservations

## Employee Endpoints

Get Available Desks:
GET /api/desks?locationId={locationId}

Book Desk:
POST /api/reservations
Request body: { deskId: string, startDate: Date, endDate: Date }

Get My Reservations:
GET /api/reservations

Change Reservation:
PUT /api/reservations/{reservationId}
Request body: { deskId: string }


# model

location
- id string
- name string
- desks [string] //array of desk IDs

desk
- id string
- locationId string
- available boolean
- reservations
{
    employeeId string
    startDate Date
    endDate Date
}

user
- id string
- firstName string
- lastName string
- email string Unique
- password string Unique
- role string {admin / employee}


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
