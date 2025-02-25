using Microsoft.EntityFrameworkCore;
using PruebaIdGen.Models;

namespace PruebaIdGen.Persistence;

public class ApplicationDbContext : DbContext
{
    public DbSet<Todo> Todos { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Todo>(entity =>
        {
            entity.Property(e => e.Id).HasValueGenerator<GeneratorId>();
            entity.Property(e => e.Title).IsRequired();
            entity.Property(e => e.IsCompleted).IsRequired();
        });
    }

}
