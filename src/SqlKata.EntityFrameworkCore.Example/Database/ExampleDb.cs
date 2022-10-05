namespace SqlKata.EntityFrameworkCore.Example.Database
{
    using Microsoft.EntityFrameworkCore;

    using SqlKata.EntityFrameworkCore.Example.Database.Models;

    internal class ExampleDb : DbContext
    {
        internal ExampleDb() { }
        internal ExampleDb(DbContextOptions<ExampleDb> InOptions) : base(InOptions) { }
        internal virtual DbSet<UserDbModel> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_unicode_ci");
            modelBuilder.Entity<UserDbModel>(T =>
            {
                T.ToTable("users");
                T.HasIndex(e => e.Email, "users_email_unique").IsUnique();
                T.HasIndex(e => e.Username, "users_username_unique").IsUnique();

                T.Property(e => e.Id).HasColumnType("integer").HasColumnName("id");
                T.Property(e => e.Email).IsRequired().HasColumnName("email");
                T.Property(e => e.Username).IsRequired().HasColumnName("username");
                T.Property(e => e.Password).IsRequired().HasMaxLength(255).HasColumnName("password");
                T.Property(e => e.CreatedAt).HasColumnType("timestamp").HasColumnName("created_at");
                T.Property(e => e.UpdatedAt).HasColumnType("timestamp").HasColumnName("updated_at");
            });
        }
    }
}
