# model

location
- id string
- name string
- description string?
- desks [string] //array of desk IDs

desk
- id string
- locationId string
- userId string
- available boolean
- startDate Date
- endDate Date

user
- id string
- firstName string
- lastName string
- email string Unique
- password string Unique
- role string {admin / employee}

one loc to many desks
one desk to one user