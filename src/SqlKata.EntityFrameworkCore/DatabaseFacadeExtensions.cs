namespace SqlKata.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

/// <summary>
/// Extension methods that execute SqlKata queries through a <see cref="DatabaseFacade"/>.
/// </summary>
public static class DatabaseFacadeExtensions
{
    /// <summary>
    /// Executes a raw SQL query against the database.
    /// </summary>
    /// <param name="This">The database facade.</param>
    /// <param name="InCompiledQuery">The compiled query.</param>
    /// <returns>The number of rows affected.</returns>
    public static int ExecuteSqlKata(this DatabaseFacade This, SqlResult InCompiledQuery)
    {
        return This.ExecuteSqlRaw(InCompiledQuery.Sql, InCompiledQuery.Bindings.ToArray());
    }

    /// <summary>
    /// Executes a raw SQL query against the database.
    /// </summary>
    /// <param name="This">The database facade.</param>
    /// <param name="InQuery">The query.</param>
    /// <returns>The number of rows affected.</returns>
    public static int ExecuteSqlKata(this DatabaseFacade This, Query InQuery)
    {
        return ExecuteSqlKata(This, SqlKataEntityFramework.DefaultCompiler.Compile(InQuery));
    }

    /// <summary>
    /// Executes a raw SQL query against the database.
    /// </summary>
    /// <param name="This">The database facade.</param>
    /// <param name="InQuery">The query.</param>
    /// <returns>The number of rows affected.</returns>
    public static int ExecuteSqlKata(this DatabaseFacade This, Func<Query, Query> InQuery)
    {
        return ExecuteSqlKata(This, InQuery(new Query()));
    }

    /// <summary>
    /// Executes a raw SQL query against the database.
    /// </summary>
    /// <param name="This">The database facade.</param>
    /// <param name="InCompiledQuery">The compiled query.</param>
    /// <param name="InCancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>The number of rows affected.</returns>
    public static Task<int> ExecuteSqlKataAsync(this DatabaseFacade This, SqlResult InCompiledQuery, CancellationToken InCancellationToken = default)
    {
        return This.ExecuteSqlRawAsync(InCompiledQuery.Sql, InCompiledQuery.Bindings, InCancellationToken);
    }

    /// <summary>
    /// Executes a raw SQL query against the database.
    /// </summary>
    /// <param name="This">The database facade.</param>
    /// <param name="InQuery">The query.</param>
    /// <param name="InCancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>The number of rows affected.</returns>
    public static Task<int> ExecuteSqlKataAsync(this DatabaseFacade This, Query InQuery, CancellationToken InCancellationToken = default)
    {
        return ExecuteSqlKataAsync(This, SqlKataEntityFramework.DefaultCompiler.Compile(InQuery), InCancellationToken);
    }

    /// <summary>
    /// Executes a raw SQL query against the database.
    /// </summary>
    /// <param name="This">The database facade.</param>
    /// <param name="InQuery">The query.</param>
    /// <param name="InCancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>The number of rows affected.</returns>
    public static Task<int> ExecuteSqlKataAsync(this DatabaseFacade This, Func<Query, Query> InQuery, CancellationToken InCancellationToken = default)
    {
        return ExecuteSqlKataAsync(This, InQuery(new Query()), InCancellationToken);
    }
}
