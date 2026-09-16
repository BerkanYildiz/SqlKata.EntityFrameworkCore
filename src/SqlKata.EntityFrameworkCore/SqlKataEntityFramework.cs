namespace SqlKata.EntityFrameworkCore;

using SqlKata.Compilers;

/// <summary>
/// Global configuration for the SqlKata / Entity Framework Core integration.
/// </summary>
public static class SqlKataEntityFramework
{
    private static Compiler? _defaultCompiler;

    /// <summary>
    /// Gets the default query compiler.
    /// </summary>
    /// <exception cref="InvalidOperationException">No default compiler has been set.</exception>
    public static Compiler DefaultCompiler
    {
        get => _defaultCompiler ?? throw new InvalidOperationException(
            $"No default compiler has been set. Call {nameof(SqlKataEntityFramework)}.{nameof(SetDefaultCompiler)} before executing queries.");
        private set => _defaultCompiler = value;
    }

    /// <summary>
    /// Sets the default query compiler.
    /// </summary>
    /// <param name="InCompiler">The default query compiler.</param>
    /// <exception cref="ArgumentNullException">The compiler is null.</exception>
    public static void SetDefaultCompiler(Compiler InCompiler)
    {
        DefaultCompiler = InCompiler ?? throw new ArgumentNullException(nameof(InCompiler));
    }

    /// <summary>
    /// Sets the default query compiler.
    /// </summary>
    /// <typeparam name="T">The compiler.</typeparam>
    public static void SetDefaultCompiler<T>() where T : Compiler, new()
    {
        SetDefaultCompiler(new T());
    }
}
