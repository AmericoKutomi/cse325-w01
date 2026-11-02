## 1. Additional Pizza in the Seed Data

The following code from `PizzaService.cs` shows the initial list of pizzas, including the additional pizza **Hawaiian** (Id = 3), and **Pepperoni** (Id = 4).

```csharp
static int nextId = 5;

static PizzaService()
{
    Pizzas = new List<Pizza>
    {
        new Pizza { Id = 1, Name = "Classic Italian", IsGlutenFree = false },
        new Pizza { Id = 2, Name = "Veggie", IsGlutenFree = true },
        new Pizza { Id = 3, Name = "Hawaiian", IsGlutenFree = false },
        new Pizza { Id = 4, Name = "Pepperoni", IsGlutenFree = false }
    };
}
```

## 2. API Testing Evidence

The following examples demonstrate the of the four CRUD operations.

### 2.1 GET – Retrieve Pizzas

**Request:**

```http
GET {{ContosoPizza_HostAddress}}/pizza/
```

**Response:**

HTTP/1.1 200 OK

```json
[
  {
    "id": 1,
    "name": "Classic Italian",
    "isGlutenFree": false
  },
  {
    "id": 2,
    "name": "Veggie",
    "isGlutenFree": true
  },
  {
    "id": 3,
    "name": "Hawaiian",
    "isGlutenFree": false
  },
  {
    "id": 4,
    "name": "Pepperoni",
    "isGlutenFree": false
  }
]
```

**Status Code:** `200`

### 2.2 POST – Create a new Pizza

**Request:**

```http
POST {{ContosoPizza_HostAddress}}/pizza/
Content-Type: application/json
{
    "name": "Margherita",
    "isGlutenFree": false
}
```

**Response:**

HTTP/1.1 201 Created

```json
{
  "id": 5,
  "name": "Margherita",
  "isGlutenFree": false
}
```

**Status Code:** `201`

### 2.3 PUT – Update an existing Pizza

**Request:**

```http
PUT {{ContosoPizza_HostAddress}}/pizza/5
Content-Type: application/json
{
    "id": 5,
    "name": "Margherita Free",
    "isGlutenFree": true
}
```

**Response:**

HTTP/1.1 204 No Content

**Status Code:** `204`

### 2.4 GET/id – Read a specific Pizza

**Request:**

```http
GET {{ContosoPizza_HostAddress}}/pizza/5
```

**Response:**

HTTP/1.1 200 OK

```json
{
  "id": 5,
  "name": "Margherita Free",
  "isGlutenFree": true
}
```

**Status Code:** `200`

### 2.5 DELETE – Delete a specific Pizza

**Request:**

```http
DELETE {{ContosoPizza_HostAddress}}/pizza/5
```

**Response:**

HTTP/1.1 204 No Content

**Status Code:** `204`

### 2.6 GET – Retrieve Pizzas (final check)

**Request:**

```http
GET {{ContosoPizza_HostAddress}}/pizza/
```

**Response:**

HTTP/1.1 200 OK

```json
[
  {
    "id": 1,
    "name": "Classic Italian",
    "isGlutenFree": false
  },
  {
    "id": 2,
    "name": "Veggie",
    "isGlutenFree": true
  },
  {
    "id": 3,
    "name": "Hawaiian",
    "isGlutenFree": false
  },
  {
    "id": 4,
    "name": "Pepperoni",
    "isGlutenFree": false
  }
]
```

**Status Code:** `200`
