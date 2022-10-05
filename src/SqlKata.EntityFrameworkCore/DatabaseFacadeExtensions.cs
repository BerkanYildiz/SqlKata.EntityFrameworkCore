namespace SqlKata.EntityFrameworkCore
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;

    public static class DatabaseFacadeExtensions
    {
        /// <summary>
        /// Executes a raw SQL query against the database.
        /// </summary>
        /// <param name="This">The database facade.</param>
        /// <param name="InCompiledQuery">The compiled query.</param>
        public static int ExecuteSqlKata(this DatabaseFacade This, SqlResult InCompiledQuery)
        {
            return This.ExecuteSqlRaw(InCompiledQuery.Sql, InCompiledQuery.Bindings.ToArray());
        }

        /// <summary>
        /// Executes a raw SQL query against the database.
        /// </summary>
        /// <param name="This">The database facade.</param>
        /// <param name="InQuery">The query.</param>
        public static int ExecuteSqlKata(this DatabaseFacade This, Query InQuery)
        {
            return ExecuteSqlKata(This, SqlKataEntityFramework.DefaultCompiler.Compile(InQuery));
        }

        /// <summary>
        /// Executes a raw SQL query against the database.
        /// </summary>
        /// <param name="This">The database facade.</param>
        /// <param name="InQuery">The query.</param>
        public static int ExecuteSqlKata(this DatabaseFacade This, Func<Query, Query> InQuery)
        {
            return ExecuteSqlKata(This, InQuery(new Query()));
        }

        /// <summary>
        /// Executes a raw SQL query against the database.
        /// </summary>
        /// <param name="This">The database facade.</param>
        /// <param name="InCompiledQuery">The compiled query.</param>
        public static Task<int> ExecuteSqlKataAsync(this DatabaseFacade This, SqlResult InCompiledQuery)
        {
            return This.ExecuteSqlRawAsync(InCompiledQuery.Sql, InCompiledQuery.Bindings.ToArray());
        }

        /// <summary>
        /// Executes a raw SQL query against the database.
        /// </summary>
        /// <param name="This">The database facade.</param>
        /// <param name="InQuery">The query.</param>
        public static Task<int> ExecuteSqlKataAsync(this DatabaseFacade This, Query InQuery)
        {
            return ExecuteSqlKataAsync(This, SqlKataEntityFramework.DefaultCompiler.Compile(InQuery));
        }

        /// <summary>
        /// Executes a raw SQL query against the database.
        /// </summary>
        /// <param name="This">The database facade.</param>
        /// <param name="InQuery">The query.</param>
        public static Task<int> ExecuteSqlKataAsync(this DatabaseFacade This, Func<Query, Query> InQuery)
        {
            return ExecuteSqlKataAsync(This, InQuery(new Query()));
        }
    }
}
