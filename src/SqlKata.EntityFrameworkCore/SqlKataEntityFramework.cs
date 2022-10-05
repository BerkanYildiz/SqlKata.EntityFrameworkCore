namespace SqlKata.EntityFrameworkCore
{
    using System;

    using SqlKata.Compilers;

    public static class SqlKataEntityFramework
    {
        /// <summary>
        /// Gets the default query compiler.
        /// </summary>
        public static Compiler DefaultCompiler { get; private set; }

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
        /// <exception cref="ArgumentNullException">The compiler is null.</exception>
        public static void SetDefaultCompiler<T>() where T : Compiler, new()
        {
            SetDefaultCompiler(new T());
        }
    }
}
