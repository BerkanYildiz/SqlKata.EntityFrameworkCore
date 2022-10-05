namespace SqlKata.EntityFrameworkCore.Example
{
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using Microsoft.Extensions.Logging;

    using Newtonsoft.Json;

    using SqlKata.Compilers;
    using SqlKata.EntityFrameworkCore.Example.Database;
    using SqlKata.EntityFrameworkCore.Example.Database.Models;

    internal static class Program
    {
        private static bool EventDatabaseIsCreated { get; set; }

        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="InArgs">The launch arguments.</param>
        private static void Main(string[] InArgs)
        {
            SqlKataEntityFramework.SetDefaultCompiler<SqliteCompiler>();

            // 
            // Build a testing database context.
            // 

            var SqLiteConnection = new SqliteConnection("Filename=:memory:");
            SqLiteConnection.Open();

            using var Db = new ExampleDb(new DbContextOptionsBuilder<ExampleDb>()
                                    .UseSqlite(SqLiteConnection)
                                    .EnableSensitiveDataLogging()
                                    .EnableDetailedErrors()
                                    .LogTo(FilteredLog, LogLevel.Information).Options);
            
            Db.Database.EnsureCreated();
            EventDatabaseIsCreated = true;

            // 
            // Test if library syntax and function parameters match properly.
            // 

            Console.WriteLine(Environment.NewLine + "Inserting a user...");
            _ = Db.Database.ExecuteSqlKata(T => T.From(Db.Users).AsInsert(new { id = 1, email = "john.doe@example.com", username = "John", password = "Example123!#", created_at = DateTime.UtcNow, updated_at = DateTime.UtcNow }));
            
            Console.WriteLine(Environment.NewLine + "Querying the user...");
            _ = Db.Users.FromSqlKata(T => T.Select("*").Where("id", 1)).Single();

            // 
            // Freeze the console.
            // 
            
            Thread.Sleep(Timeout.InfiniteTimeSpan);
        }

        /// <summary>
        /// Log filter that ignores irrelevant events.
        /// </summary>
        /// <param name="InMessage">The log message.</param>
        private static void FilteredLog(string InMessage)
        {
            var SkippedEventsArray = new string[]
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
}