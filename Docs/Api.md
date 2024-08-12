## Auth

### Register

```js
POST {{host}}/auth/register
```

### Register Request

```json
{
    "firstName": "Jason",
    "lastName": "Medina",
    "email": "jmedina@email.com",
    "password": "UnsafePassword1234@"
}
```

### Register Response

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

### Login

```js
POST {{host}}/auth/login
```

### Login Request

```json
    "email": "jmedina@email.com",
    "password": "UnsafePassword1234@",
```

### Login Response

```json
{
    "id": "d89c2d9a-eb3e-4075-95ff-b920b55aa104",
    "firstName": "Jason",
    "lastName": "Medina",
    "email": "jmedina@email.com",
    "token": "eyJhb..hbbQ"
}
```