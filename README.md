# SqlKata.EntityFrameworkCore [![SqlKata.EntityFrameworkCore](https://img.shields.io/nuget/v/SqlKata.EntityFrameworkCore.svg)](https://www.nuget.org/packages/SqlKata.EntityFrameworkCore/) [![SqlKata.EntityFrameworkCore](https://img.shields.io/nuget/dt/SqlKata.EntityFrameworkCore.svg)](https://www.nuget.org/packages/SqlKata.EntityFrameworkCore/) [![build](https://github.com/BerkanYildiz/SqlKata.EntityFrameworkCore/actions/workflows/dotnet.yml/badge.svg)](https://github.com/BerkanYildiz/SqlKata.EntityFrameworkCore/actions/workflows/dotnet.yml)

A lightweight, fluent integration library that seamlessly connects SqlKata's powerful query builder with Entity Framework's DbContext.  
Build complex SQL queries using your existing Entity Framework models without the limitations of LINQ.

## Features

- Directly query EF entities using SqlKata's fluent syntax
- Automatic table name resolution from DbContext
- ~Strong typing support for column selection~
- ~Built-in mapping of query results to entity objects~
- ~Transaction support~
- Comprehensive extension methods for DbContext and DbSet
- Performance optimized with minimal overhead

## Installation

    PM> Install-Package SqlKata.EntityFrameworkCore

## Example without SqlKata.EntityFrameworkCore

Typically, if you're using DbContext, executing a query against your database using SqlKata would look like this:

```csharp
var QueryCompiler = new SqliteCompiler();
var InsertQuery = new Query("users").AsInsert(new { email = "john.doe@example.com", username = "John", password = "Example123!#", created_at = DateTime.UtcNow, updated_at = DateTime.UtcNow });
var CompiledInsertQuery = QueryCompiler.Compile(InsertQuery);
_ = SomeDbContext.Database.ExecuteSqlRaw(CompiledInsertQuery.Sql, CompiledInsertQuery.Bindings.ToArray());
```

There are multiple things annoying with this:
- You have to instantiate a compiler, cache it to avoid constantly re-instantiating.
- You have to create a query, and hardcode the table name you're querying against.
- You have to compile the query, save it into a variable, and pass it to ExecuteSqlRaw.

This is WAY too much work for a simple query, and that's exactly where our library comes into play!

## Example with SqlKata.EntityFrameworkCore

With our library, all you have to do is:
- Specify which compiler you want to use (only one single time).
- The actual query.

```csharp
// Specify the compiler that must be used for all future queries.
SqlKataEntityFramework.SetDefaultCompiler<SqliteCompiler>();

// Insert the user.
_ = Db.Database.ExecuteSqlKata(T => T.From(Db.Users).AsInsert(new { email = "jane.doe@example.com", username = "Jane", password = "Example123!#", created_at = DateTime.UtcNow, updated_at = DateTime.UtcNow }));

// Fetching data is even more simple.
_ = Db.Users.FromSqlKata(T => T.Select("*").Where("id", 1)).Single();
```

# License
You are free to use this library however you or your company wants to.
