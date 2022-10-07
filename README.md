# SqlKata.EntityFrameworkCore [![SqlKata.EntityFrameworkCore](https://img.shields.io/nuget/v/SqlKata.EntityFrameworkCore.svg)](https://www.nuget.org/packages/SqlKata.EntityFrameworkCore/) [![SqlKata.EntityFrameworkCore](https://img.shields.io/nuget/dt/SqlKata.EntityFrameworkCore.svg)](https://www.nuget.org/packages/SqlKata.EntityFrameworkCore/) [![build](https://github.com/BerkanYildiz/SqlKata.EntityFrameworkCore/actions/workflows/dotnet.yml/badge.svg)](https://github.com/BerkanYildiz/SqlKata.EntityFrameworkCore/actions/workflows/dotnet.yml)

.NET library that aims to facilitate the combination of DbContexts (EntityFrameworkCore) and SqlKata queries.

## Installation

    PM> Install-Package SqlKata.EntityFrameworkCore

## Example

```csharp
using SqlKata;
using SqlKata.Compilers;
using SqlKata.EntityFrameworkCore;


using var Db = new MyDbContext();

// Set the compiler.
SqlKataEntityFramework.SetDefaultCompiler(new MySqlCompiler());

// Example 1
var BerkanLogins = Db.UsersLogins.FromSqlKata(
	Query => Query.From("users_logins").Where("user_id", 1).Limit(0).OrderByDesc("id")).ToList();
Console.WriteLine($"BerkanLogins: Admin logged in {BerkanLogins.Count} times, last login from {BerkanLogins.FirstOrDefault()?.IpAddress}");

// Example 2
var UserLogin2 = Db.UsersLogins.FromSqlKata(new Query("users_logins")
	.Where("user_id", "6")
	.Limit(1))
	.FirstOrDefault();
Console.WriteLine($"UserLogin2: {UserLogin2?.IpAddress}");

// Example 3 - Executing commands
var LoginsDeleted = Db.Database.ExecuteSqlKata(new Query("users_logins")
	.Where("ip_address", "LIKE", "127.0.0.1")
	.OrderBy("id")
	.AsDelete());
Console.WriteLine($"{LoginsDeleted} logins were deleted!");

// Example 4 - Executing commands
var RowsUpdated = Db.Database.ExecuteSqlKata(new Query("users_groups_links")
	.AsUpdate(new { user_group_id = 7 })
	.Where("user_id", 1)
	.Where("user_group_id", 6));
Console.WriteLine($"{RowsUpdated} rows were affected!");

```

# License
You are free to use this library however you or your company wants to.
