using Microsoft.EntityFrameworkCore;

namespace PasswordsBunker;

public partial class PasswordsDbContext : DbContext
{
    public DbSet<Password> Passwords => Set<Password>();
    public PasswordsDbContext() => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=passwords.db");
    }
}
