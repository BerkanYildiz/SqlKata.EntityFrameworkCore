namespace SqlKata.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

/// <summary>
/// Extension methods that bind Entity Framework metadata to a SqlKata <see cref="Query"/>.
/// </summary>
public static class QueryExtensions
{
    /// <summary>
    /// Binds a table to the query.
    /// </summary>
    /// <param name="This">The query.</param>
    /// <param name="InEntityType">The entity model.</param>
    /// <exception cref="InvalidOperationException">The entity type is not mapped to a table.</exception>
    public static Query From(this Query This, IEntityType InEntityType)
    {
        return This.From(InEntityType.GetRequiredTableName());
    }

    /// <summary>
    /// Binds a table to the query.
    /// </summary>
    /// <typeparam name="T">The entity model of the database set.</typeparam>
    /// <param name="This">The query.</param>
    /// <param name="InDbSet">The database set.</param>
    /// <exception cref="InvalidOperationException">The entity type is not mapped to a table.</exception>
    public static Query From<T>(this Query This, DbSet<T> InDbSet) where T : class
    {
        return This.From(InDbSet.EntityType);
    }

    /// <summary>
    /// Gets the schema-qualified table name of an entity type, or throws if it is not mapped to a table.
    /// </summary>
    /// <param name="InEntityType">The entity model.</param>
    /// <exception cref="InvalidOperationException">The entity type is not mapped to a table.</exception>
    internal static string GetRequiredTableName(this IEntityType InEntityType)
    {
        return InEntityType.GetSchemaQualifiedTableName()
            ?? throw new InvalidOperationException($"The entity type '{InEntityType.DisplayName()}' is not mapped to a table.");
    }
}
