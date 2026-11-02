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

The following examples demonstrate the four CRUD operations on Pizzas list.

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

## 3. Sales Summary Function

The following code from `Program.cs` implements the Sales Summary functionality. It reads the JSON files, extracts the sales amounts, calculates the grand total, and generates the `SalesSummary.txt` file on salesTotalDir folder.
The program considers the attributes Total or OverallTotal in the files. The record SalesTotal considers both attributes.

```csharp
record SalesTotal(double? Total, double? OverallTotal);

static void GenerateSalesSummary(IEnumerable<string> salesFiles)
{
    double totalSales = 0;

    // StringBuilders for accumulating details
    StringBuilder salesDetailsBuilder = new StringBuilder();
    salesDetailsBuilder.AppendLine("Sales Details:");
    salesDetailsBuilder.AppendLine("----------------------------");

    // Loop over each file path in salesFiles
    foreach (var file in salesFiles)
    {
        // Read the contents of the file
        string salesJson = File.ReadAllText(file);

        // Parse the contents as JSON
        SalesTotal? data = JsonConvert.DeserializeObject<SalesTotal?>(salesJson);

        // Add the amount found in the Total attribute or OverallTotal attribute to the totalSales variable
        double fileTotal = data?.Total ?? data?.OverallTotal ?? 0;
        totalSales += fileTotal;
        salesDetailsBuilder.AppendLine($"{file}: {fileTotal:C}");
    }

    // Declare a StringBuilder for the General Summary
    StringBuilder summaryBuilder = new StringBuilder();
    summaryBuilder.AppendLine("Sales Summary:");
    summaryBuilder.AppendLine("----------------------------");
    summaryBuilder.AppendLine($"  Total Sales: {totalSales:C}");
    summaryBuilder.AppendLine();

    File.WriteAllText(
        Path.Combine("salesTotalDir", "SalesSummary.txt"),
        summaryBuilder.ToString());

    File.AppendAllText(
        Path.Combine("salesTotalDir", "SalesSummary.txt"),
        salesDetailsBuilder.ToString());
}

record SalesTotal(double? Total, double? OverallTotal);
```
