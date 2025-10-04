# 🧩 CQRSDecorate.Net

**CQRSDecorate.Net** is a lightweight and extensible library for implementing the **CQRS (Command Query Responsibility Segregation)** pattern with full **Decorator Pattern** support in .NET.

It provides a clean, maintainable, and testable approach to separating **Command** and **Query** logic while allowing the injection of cross-cutting concerns such as **Logging, Validation, Caching, and Authorization** through decorators — keeping your handlers clean and focused.

---

## 🚀 Key Features

- ✅ Full **CQRS** pattern implementation  
- ⚙️ Built-in **Decorator Pattern** support  
- 🧱 Simple integration with **Dependency Injection**  
- 📦 Works with **.NET 6+ and C# 10+**  
- 🧩 Clean, testable architecture  
- 🔄 Supports **Commands**, **Queries**, and **Pipeline Behaviors**

---

## 🧠 Concept

Instead of mixing read and write logic in the same service, `CQRSDecorate.Net` separates responsibilities clearly:

- **Commands** → change state (e.g., create, update, delete)  
- **Queries** → retrieve data (without modifying state)

Each Command or Query can be processed through one or more **decorators** before reaching the main handler, giving full control over execution pipelines.

---
## 🧰Why Use This Library?

- **Reduces boilerplate code** 
- **Promotes Single Responsibility Principle** 
- **Enables easy unit testing of each handler** 
- **Supports pipeline decorators similar to MediatR — but with a cleaner, lighter implementation** 





## 🧩 Basic Structure

```csharp
public record CreateUserCommand(string Name, string Email) : ICommand<Guid>;

public class CreateUserHandler : ICommandHandler<CreateUserCommand, Guid>
{
    public async Task<Guid> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        // User creation logic
        var userId = Guid.NewGuid();
        Console.WriteLine($"User created: {command.Name}");
        return await Task.FromResult(userId);
    }
}
 

