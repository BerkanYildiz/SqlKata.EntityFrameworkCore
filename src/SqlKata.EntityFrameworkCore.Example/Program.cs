namespace SqlKata.EntityFrameworkCore.Example
{
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Diagnostics;

    using Newtonsoft.Json;

    using SqlKata.Compilers;
    using SqlKata.EntityFrameworkCore.Example.Database;
    using SqlKata.EntityFrameworkCore.Example.Database.Models;

    internal static class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="InArgs">The launch arguments.</param>
        private static void Main(string[] InArgs)
        {
            SqlKataEntityFramework.SetDefaultCompiler<SqlServerCompiler>();

            // 
            // Build a testing database context.
            // 

            var SqLiteConnection = new SqliteConnection("Filename=:memory:");
            SqLiteConnection.Open();

            using var Db = new ExampleDb(new DbContextOptionsBuilder<ExampleDb>()
                                    .UseSqlite(SqLiteConnection).Options);
            
            Db.Database.EnsureCreated();

            // 
            // Test if library syntax and function parameters match properly.
            // 

            Console.WriteLine("Inserting a user...");
            Db.Database.ExecuteSqlKata(T => T.From(Db.Users).AsInsert(new { id = 1, email = "john.doe@example.com", username = "John", password = "Example123!#", created_at = DateTime.UtcNow, updated_at = DateTime.UtcNow }));
            
            Console.Write("Querying the user: ");
            var ExampleSelectUser = Db.Users.FromSqlKata(T => T.Select("*").Where("id", 1));
            Console.WriteLine(JsonConvert.SerializeObject(ExampleSelectUser, Formatting.None));

            // 
            // Freeze the console.
            // 

            Console.WriteLine(Environment.NewLine + "Exiting...");
            Thread.Sleep(Timeout.InfiniteTimeSpan);
        }
    }
}