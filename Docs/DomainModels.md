# model

location
- id string
- name string
- desks [string] //array of desk IDs

desk
- id string
- locationId string
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


public class Reservation

    public int ReservationId { get; set; }
    public int EmployeeId { get; set; }
    public int DeskId { get; set; }
    startDate Date
    endDate Date

one loc to many desks
one desk to many reservations
one user one reservation