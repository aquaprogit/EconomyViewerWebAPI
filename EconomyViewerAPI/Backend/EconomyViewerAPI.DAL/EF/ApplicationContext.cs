using EconomyViewerAPI.DAL.Entities;

using Microsoft.EntityFrameworkCore;

namespace EconomyViewerAPI.DAL.EF;
public class ApplicationContext : DbContext
{
    public virtual DbSet<Item> Items { get; set; }
    public virtual DbSet<Server> Servers { get; set; }

    public ApplicationContext()
    {
        Database.EnsureCreated();
    }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(@"Data Source=" + Path.GetDirectoryName(Environment.ProcessPath) + $@"\economy.db;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Server>()
                    .HasMany(server => server.Items)
                    .WithOne();
    }
}
