namespace SqlKata.EntityFrameworkCore
{
    using System;
    using System.Linq;

    using Microsoft.EntityFrameworkCore;

    using SqlKata.Compilers;

    public static class DbSetExtensions
    {
        /// <summary>
        /// Executes a strongly-typed raw SQL query against the database.
        /// </summary>
        /// <typeparam name="T">The database set's entity model.</typeparam>
        /// <param name="This">The database set.</param>
        /// <param name="InCompiledQuery">The compiled query.</param>
        public static IQueryable<T> FromSqlKata<T>(this DbSet<T> This, SqlResult InCompiledQuery) where T : class
        {
            return This.FromSqlRaw(InCompiledQuery.Sql, InCompiledQuery.Bindings.ToArray());
        }

        /// <summary>
        /// Executes a strongly-typed raw SQL query against the database.
        /// </summary>
        /// <typeparam name="T">The database set's entity model.</typeparam>
        /// <param name="This">The database set.</param>
        /// <param name="InQuery">The query.</param>
        public static IQueryable<T> FromSqlKata<T>(this DbSet<T> This, Query InQuery) where T : class
        {
            return FromSqlKata<T>(This, SqlKataEntityFramework.DefaultCompiler, InQuery);
        }

        /// <summary>
        /// Executes a strongly-typed raw SQL query against the database.
        /// </summary>
        /// <typeparam name="T">The database set's entity model.</typeparam>
        /// <param name="This">The database set.</param>
        /// <param name="InCompiler">The query compiler.</param>
        /// <param name="InQuery">The query.</param>
        public static IQueryable<T> FromSqlKata<T>(this DbSet<T> This, Compiler InCompiler, Query InQuery) where T : class
        {
            SqlKataEntityFramework.LastUsedCompiler = InCompiler;

            if (!InQuery.HasComponent("from"))
                InQuery.From(This.EntityType.GetTableName());

            return FromSqlKata<T>(This, InCompiler.Compile(InQuery));
        }

        /// <summary>
        /// Executes a strongly-typed raw SQL query against the database.
        /// </summary>
        /// <typeparam name="T">The database set's entity model.</typeparam>
        /// <param name="This">The database set.</param>
        /// <param name="InQuery">The query.</param>
        public static IQueryable<T> FromSqlKata<T>(this DbSet<T> This, Func<Query, Query> InQuery) where T : class
        {
            return FromSqlKata<T>(This, SqlKataEntityFramework.DefaultCompiler, InQuery(new Query()));
        }

        /// <summary>
        /// Executes a strongly-typed raw SQL query against the database.
        /// </summary>
        /// <typeparam name="T">The database set's entity model.</typeparam>
        /// <param name="This">The database set.</param>
        /// <param name="InCompiler">The query compiler.</param>
        /// <param name="InQuery">The query.</param>
        public static IQueryable<T> FromSqlKata<T>(this DbSet<T> This, Compiler InCompiler, Func<Query, Query> InQuery) where T : class
        {
            SqlKataEntityFramework.LastUsedCompiler = InCompiler;

            var Query = InQuery(new Query());
            if (!Query.HasComponent("from"))
                Query.From(This.EntityType.GetTableName());

            return FromSqlKata<T>(This, InCompiler.Compile(Query));
        }
    }
}
