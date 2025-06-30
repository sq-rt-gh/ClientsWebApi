using Microsoft.EntityFrameworkCore;
using ClientsWebApi.Models;


namespace ClientsWebApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Founder> Founders => Set<Founder>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // связь многие-ко-многим 
        modelBuilder.Entity<Client>()
            .HasMany(c => c.Founders)
            .WithMany()
            .UsingEntity(j => j.ToTable("ClientFounder")); // промежуточная таблица

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // автозаполнение CreatedAt и UpdatedAt
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is Client || e.Entity is Founder);

        foreach (var entry in entries)
        {
            var now = DateTime.UtcNow;

            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Property("CreatedAt").CurrentValue = now;
                    entry.Property("UpdatedAt").CurrentValue = now;
                    break;

                case EntityState.Modified:
                    entry.Property("UpdatedAt").CurrentValue = now;
                    break;

                default:
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
