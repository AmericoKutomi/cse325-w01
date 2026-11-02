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
