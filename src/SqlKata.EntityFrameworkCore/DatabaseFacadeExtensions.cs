namespace SqlKata.EntityFrameworkCore
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;

    using SqlKata.Compilers;

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
            return ExecuteSqlKata(This, SqlKataEntityFramework.DefaultCompiler, InQuery);
        }

        /// <summary>
        /// Executes a raw SQL query against the database.
        /// </summary>
        /// <param name="This">The database facade.</param>
        /// <param name="InCompiler">The query compiler.</param>
        /// <param name="InQuery">The query.</param>
        public static int ExecuteSqlKata(this DatabaseFacade This, Compiler InCompiler, Query InQuery)
        {
            SqlKataEntityFramework.LastUsedCompiler = InCompiler;
            return ExecuteSqlKata(This, InCompiler.Compile(InQuery));
        }

        /// <summary>
        /// Executes a raw SQL query against the database.
        /// </summary>
        /// <param name="This">The database facade.</param>
        /// <param name="InQuery">The query.</param>
        public static int ExecuteSqlKata(this DatabaseFacade This, Func<Query, Query> InQuery)
        {
            return ExecuteSqlKata(This, SqlKataEntityFramework.DefaultCompiler, InQuery(new Query()));
        }

        /// <summary>
        /// Executes a raw SQL query against the database.
        /// </summary>
        /// <param name="This">The database facade.</param>
        /// <param name="InCompiler">The query compiler.</param>
        /// <param name="InQuery">The query.</param>
        public static int ExecuteSqlKata(this DatabaseFacade This, Compiler InCompiler, Func<Query, Query> InQuery)
        {
            SqlKataEntityFramework.LastUsedCompiler = InCompiler;
            return ExecuteSqlKata(This, InCompiler.Compile(InQuery(new Query())));
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
            return ExecuteSqlKataAsync(This, SqlKataEntityFramework.DefaultCompiler, InQuery);
        }

        /// <summary>
        /// Executes a raw SQL query against the database.
        /// </summary>
        /// <param name="This">The database facade.</param>
        /// <param name="InCompiler">The query compiler.</param>
        /// <param name="InQuery">The query.</param>
        public static Task<int> ExecuteSqlKataAsync(this DatabaseFacade This, Compiler InCompiler, Query InQuery)
        {
            SqlKataEntityFramework.LastUsedCompiler = InCompiler;
            return ExecuteSqlKataAsync(This, InCompiler.Compile(InQuery));
        }

        /// <summary>
        /// Executes a raw SQL query against the database.
        /// </summary>
        /// <param name="This">The database facade.</param>
        /// <param name="InQuery">The query.</param>
        public static Task<int> ExecuteSqlKataAsync(this DatabaseFacade This, Func<Query, Query> InQuery)
        {
            return ExecuteSqlKataAsync(This, SqlKataEntityFramework.DefaultCompiler, InQuery(new Query()));
        }

        /// <summary>
        /// Executes a raw SQL query against the database.
        /// </summary>
        /// <param name="This">The database facade.</param>
        /// <param name="InCompiler">The query compiler.</param>
        /// <param name="InQuery">The query.</param>
        public static Task<int> ExecuteSqlKataAsync(this DatabaseFacade This, Compiler InCompiler, Func<Query, Query> InQuery)
        {
            SqlKataEntityFramework.LastUsedCompiler = InCompiler;
            return ExecuteSqlKataAsync(This, InCompiler.Compile(InQuery(new Query())));
        }
    }
}
