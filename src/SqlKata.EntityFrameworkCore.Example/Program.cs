namespace SqlKata.EntityFrameworkCore.Example;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SqlKata.Compilers;
using SqlKata.EntityFrameworkCore.Example.Database;

internal static class Program
{
    private static bool EventDatabaseIsCreated { get; set; }

    /// <summary>
    /// Defines the entry point of the application.
    /// </summary>
    private static async Task Main()
    {
        SqlKataEntityFramework.SetDefaultCompiler<SqliteCompiler>();

        //
        // Build a testing database context.
        //

        await using var SqLiteConnection = new SqliteConnection("Filename=:memory:");
        await SqLiteConnection.OpenAsync();

        await using var Db = new ExampleDb(new DbContextOptionsBuilder<ExampleDb>()
            .UseSqlite(SqLiteConnection)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors()
            .LogTo(FilteredLog, LogLevel.Information)
            .Options);

        await Db.Database.EnsureCreatedAsync();
        EventDatabaseIsCreated = true;

        //
        // Test if library syntax and function parameters match properly.
        //

        Console.WriteLine(Environment.NewLine + "Inserting a user...");
        var Inserted = await Db.Database.ExecuteSqlKataAsync(T => T.From(Db.Users).AsInsert(new { id = 1, email = "john.doe@example.com", username = "John", password = "Example123!#", created_at = DateTime.UtcNow, updated_at = DateTime.UtcNow }));
        Console.WriteLine($"Rows affected: {Inserted}");

        Console.WriteLine(Environment.NewLine + "Querying the user...");
        var User = await Db.Users.FromSqlKata(T => T.Select("*").Where("id", 1)).SingleAsync();
        Console.WriteLine($"Fetched user #{User.Id}: {User.Username} <{User.Email}>");
    }

    /// <summary>
    /// Log filter that ignores irrelevant events.
    /// </summary>
    /// <param name="InMessage">The log message.</param>
    private static void FilteredLog(string InMessage)
    {
        var SkippedEventsArray = new[]
        {
            "EntityFrameworkCore.ChangeTracking",
            "EntityFrameworkCore.Database.Transaction",
            "CoreEventId.ContextInitialized",
            "CoreEventId.SensitiveDataLoggingEnabledWarning",
            "RelationalEventId.DataReaderDisposing",
            "RelationalEventId.ConnectionOpening",
            "RelationalEventId.ConnectionOpened",
            "RelationalEventId.CommandCreating",
            "RelationalEventId.CommandCreated"
        };

        if (!EventDatabaseIsCreated || SkippedEventsArray.Any(InMessage.Contains))
            return;

        Console.WriteLine(InMessage);
    }
}
