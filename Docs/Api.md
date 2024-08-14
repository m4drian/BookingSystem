## Auth

### Register (Command)

```js
POST {{host}}/auth/register
```

### Register Request (Command)

```json
{
    "firstName": "Jason",
    "lastName": "Medina",
    "email": "jmedina@email.com",
    "password": "UnsafePassword1234@"
}
```

### Register Response (Command)

```js
200 OK
```

```json
{
    "id": "d89c2d9a-eb3e-4075-95ff-b920b55aa104",
    "firstName": "Jason",
    "lastName": "Medina",
    "email": "jmedina@email.com",
    "token": "eyJhb..z9dqcnXoY"
}
```

### Login (Query)

```js
POST {{host}}/auth/login
```

### Login Request (Query)

```json
    "email": "jmedina@email.com",
    "password": "UnsafePassword1234@",
```

### Login Response (Query)

```json
{
    "id": "d89c2d9a-eb3e-4075-95ff-b920b55aa104",
    "firstName": "Jason",
    "lastName": "Medina",
    "email": "jmedina@email.com",
    "token": "eyJhb..hbbQ"
}
```

### add location / add location response (Command)

### get locations request / get locations response (Query)

### update location / update location response (Command)

### delete location / delete location response (Command)

### get desks from location / get desks from location respose (Query)

### add desk / add desk response (Command)

### delete desk / delete desk response (Command)

### update desk / update desk response (Command)

### check desk reservation / check desk reservation response (Query)

### get desks / get desks response (Query)