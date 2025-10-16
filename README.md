# Programming Challenge - Developer Position Elias Silva

First, thanks for **giving me the opportunity** to take part in this process.  
Independent of the result, I'm very grateful for learning a little more about **technical topics** and **time management**.

# Used Technologies and Frameworks

For implementing this **assessment**, I've used several best practices, such as:

1. OOP programming  
2. DDD (Domain Driven Design) using repository, services, and entities  
3. Clean Architecture, UnitOfWork, CQS (Separating Command and Query)  
4. Standardization of unit test names based on BDD  
5. For Unit Tests: XUnit, FluentAssertions, Embedded files, Builder Pattern (for clean unit tests)

I've used a framework called [BasePoint](https://basepointweb.io/), that I'm creating from scratch.  
This framework implements best practices that are used in the market. I've decided to create this framework just for study and **shaping my skills**.

# Little About My Way to Architect Software

The structure of projects in the solution template is:

**ByCodersChallenge.Core:**  
- Agnostic from technology, implements **domain layer**, **application layer**, and declares interfaces that will be implemented in the infrastructure layer (`ByCodersChallenge.Cqrs.Dapper`)

**ByCodersChallenge.Cqrs.Dapper:**  
- Implements **Commands and Queries** from CQS using Dapper. Contains base classes that provide a lot of **common behavior**, including the possibility to **avoid writing insert, update, and delete commands** for persisting entities data, based on entity state.  
  **OBS:** If we want to implement on EntityFramework or ADO, for example, we would create a project `ByCodersChallenge.Cqrs.EntityFramework` or `ByCodersChallenge.Cqrs.ADO`, `ByCodersChallenge.Cqrs.NHibernate`. This separates the technologies and gives the choice to remove or add another easily.

**ByCodersChallenge.Presentation.AspNetCoreApi:**  
- Presentation layer, implemented in ASP.NET Core. If we want to implement with another technology, it would be `ByCodersChallenge.Presentation.TechnologyName`.  

**ByCodersChallenge.Core.Tests:**  
- Unit tests for the layers: application and domain.  
- To test the presentation layer and `ByCodersChallenge.Cqrs.Dapper` we would need integration tests (not implemented in this assessment).

**Curiosity:**  
- `StoreRepository` and `FinancialTransactionRepository` classes **do not implement direct database access**, unlike common developer implementations. They follow the **DDD concept to persist entities depending on their State** ([BaseEntity](https://basepointweb.io/base-entity/)).  
- Data access is implemented in `FinancialTransactionCqrsCommandProvider` and `StoreCqrsCommandProvider`, which are used by repositories. This is because the **control of entity state is technology-independent**, but `FinancialTransactionCqrsCommandProvider` and `StoreCqrsCommandProvider` are **technology-dependent** (Dapper in this case).

# Description of the Application

The application has a **screen with a button and an input to upload the CNAB.txt file**.  
After upload, the data will be persisted in a **MySQL database** on two tables: `store` and `financialtransaction`.  

The upload function also retrieves data from the API and shows it in a table, **separating by store**, and shows the **general total balance** and **values per transaction**.

# Description of the API Endpoints

Endpoint used to import CNAE data to api:
### POST `/api/financial-transactions`

**Body:**
```json
{
    "file": ""
}
```

Endpoint used to retrieve data from the API. Access query stack use case:
### POST `/api/financial-transactions/get-by-filter`

**Body:**
```json
{
  "filters": [
    {
      "filterType": "Equals",
      "filterProperty": "Name",
      "filterValue": "João"
    }
  ],
  "pageNumber": 1,
  "itemsPerPage": 10
}
```

FilterTypes:

```csharp
public enum FilterType
{
    Containing,
    Equals,
    DifferentFrom,
    StartsWith,
    EndsWith,
    LessThan,
    LessThanOrEqualTo,
    GreaterThan,
    GreaterThanOrEqualTo,
    Empty,
    NotEmpty
}
```

# How to setup and run the application.
Just run the command:
docker-compose up --build in the ./ByCodersChallenge folder where docker-compose.yml is located. The containers for MySQL, API, and web application will be started.

Access the application at: http://localhost:3000/