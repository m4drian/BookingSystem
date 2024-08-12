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
