namespace SqlKata.EntityFrameworkCore
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;

    public static class QueryExtensions
    {
        /// <summary>
        /// Binds a table to the query.
        /// </summary>
        /// <param name="This">The query.</param>
        /// <param name="InEntityType">The entity model.</param>
        public static Query From(this Query This, IEntityType InEntityType)
        {
            return This.From(InEntityType.GetSchemaQualifiedTableName());
        }

        /// <summary>
        /// Binds a table to the query.
        /// </summary>
        /// <typeparam name="T">The database set's entity model.</typeparam>
        /// <param name="This">The query.</param>
        /// <param name="InDbSet">The database set.</param>
        public static Query From<T>(this Query This, DbSet<T> InDbSet) where T : class
        {
            return This.From(InDbSet.EntityType);
        }
    }
}
