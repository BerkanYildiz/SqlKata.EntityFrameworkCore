namespace SqlKata.EntityFrameworkCore
{
    using System;
    using System.Linq;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;

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
            if (!InQuery.HasComponent("from"))
                InQuery.From(This.EntityType.GetSchemaQualifiedTableName());

            return FromSqlKata<T>(This, SqlKataEntityFramework.DefaultCompiler.Compile(InQuery));
        }

        /// <summary>
        /// Executes a strongly-typed raw SQL query against the database.
        /// </summary>
        /// <typeparam name="T">The database set's entity model.</typeparam>
        /// <param name="This">The database set.</param>
        /// <param name="InQuery">The query.</param>
        public static IQueryable<T> FromSqlKata<T>(this DbSet<T> This, Func<Query, Query> InQuery) where T : class
        {
            return FromSqlKata(This, InQuery(new Query()));
        }
    }
}
