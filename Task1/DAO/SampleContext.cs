using Microsoft.EntityFrameworkCore;

namespace Task1.DAO;
public class SampleContext : DbContext
{
    public DbSet<Catalog> Cataloges { get; set; }

    public SampleContext(DbContextOptions<SampleContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.Entity<Catalog>()
            .HasOne(catalog => catalog.Parent)
            .WithMany(catalog => catalog.Children)
            .HasForeignKey(catalog => catalog.Id)
            .OnDelete(DeleteBehavior.Restrict);
}