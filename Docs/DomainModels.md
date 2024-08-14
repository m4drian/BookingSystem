# model

location
- id string required
- name string required
- description string? optional
- desks [string] //array of desk IDs, can be empty

desk
- id string required
- locationId string required
- userId string? optional
- available boolean required (default true)
- startDate Date?
- endDate Date?

user
- id string required
- firstName string required
- lastName string required
- email string Unique required
- password string required
- role string {admin / employee} required

one location has many desks
one desk can be booked by one user