
# 🧑‍💻 Project Overview

This project demonstrates a clean implementation of CQRS with MediatR in a microservices architecture. It uses well-known Design Patterns such as CQRS+MediatR, Repository and Unit of Work.

---

## 🛠️ Used Technologies

### 🧩 Architecture & Design Patterns

- ⚙️ Target Framework **.NET 9.0**  
  The latest version of the .NET platform providing performance improvements and new features.

- 🧱 **Microservices Architecture**  
  Independent services deployed separately for scalability and flexibility.

- 🧠 **CQRS + MediatR Pattern**  
  Separates read and write operations using MediatR for request/response and event handling.

- 📦 **Repository Pattern**  
  Abstracts data access layer to make business logic independent of data source.

- 🔄 **Unit of Work Pattern**  
  Manages multiple repository changes in a single transaction scope.

### 🧰 Tools & Technologies
- 🗃️ **Redis** — In-memory data store used for caching and fast key-value access  
- 📜 **Serilog** — Structured logging for .NET applications  
- 🧬 **MongoDB** — NoSQL database for flexible document-based data storage  

---

## 🧪 CreateProductCommandHandler

When a create product request is received, the `CreateProductCommandHandler` class takes action. Through the `CreateProduct` method, it maps values from the `CreateProductCommandRequest` object to a `Product` entity and performs the create operation. Then it returns a `CreateProductCommandResponse` to inform the user.

<details>
<summary><strong>Click to view C# code</strong></summary>

```csharp
namespace DAL.CQRS.Handlers.CommandHandlers
{
    public class CreateProductCommandHandler
    {
        public CreateProductCommandResponse CreateProduct(CreateProductCommandRequest createProductCommandRequest)
        {
            var id = Guid.NewGuid();
            ApplicationDbContext.ProductList.Add(new()
            {
                Id = id,
                Name = createProductCommandRequest.Name,
                Price = createProductCommandRequest.Price,
                Quantity = createProductCommandRequest.Quantity,
                CreateTime = DateTime.Now
            });

            return new CreateProductCommandResponse
            {
                IsSuccess = true,
                ProductId = id
            };
        }
    }
}
```

</details>
