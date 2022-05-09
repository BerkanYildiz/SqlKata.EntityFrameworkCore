namespace SqlKata.EntityFrameworkCore
{
    using System;

    using SqlKata.Compilers;

    public static class SqlKataEntityFramework
    {
        /// <summary>
        /// Gets the default query compiler.
        /// </summary>
        public static Compiler DefaultCompiler
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the last used query compiler.
        /// </summary>
        public static Compiler LastUsedCompiler
        {
            get;
            internal set;
        }

        /// <summary>
        /// Sets the default query compiler.
        /// </summary>
        /// <param name="InCompiler">The default query compiler.</param>
        public static void SetDefaultCompiler(Compiler InCompiler)
        {
            DefaultCompiler = InCompiler ?? throw new ArgumentNullException(nameof(InCompiler));
        }
    }
}
