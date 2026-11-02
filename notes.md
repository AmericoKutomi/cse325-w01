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

**Status Code:** `200 OK`
