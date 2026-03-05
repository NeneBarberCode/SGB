using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SGB.Domain.Entities;

namespace SGB.Infrastructure.Persistence;

public class SgbDbContextFactory : IDesignTimeDbContextFactory<SgbDbContext>
{
   public SgbDbContext CreateDbContext(string[] args)
    {
        var basePath = Directory.GetCurrentDirectory();

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        var optionsBuilder = new DbContextOptionsBuilder<SgbDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new SgbDbContext(optionsBuilder.Options);
    }
}

public class SgbDbContext : DbContext
{
    public SgbDbContext(DbContextOptions<SgbDbContext> options)
        : base(options)
    {
    }

    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Copy> Copies => Set<Copy>();
    public DbSet<Borrowing> Borrowings => Set<Borrowing>();
    public DbSet<Configuration> Configurations => Set<Configuration>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Employee>()
            .HasIndex(e => e.Email)
            .IsUnique();

        modelBuilder.Entity<Customer>()
            .HasIndex(c => c.Email)
            .IsUnique();

        modelBuilder.Entity<Book>()
            .HasIndex(l => l.ISBN)
            .IsUnique();
    }
}
