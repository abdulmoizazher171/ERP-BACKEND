using Microsoft.EntityFrameworkCore;
namespace ERP_BACKEND.data;

using ERP_BACKEND.constracts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Asset> Assets { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Turbine> Turbines { get; set; }
    public DbSet<Store> Stores { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Fluent API: Explicitly defining the relationships

        modelBuilder.Entity<Asset>(entity =>
        {
            entity.HasKey(e => e.ItemId);

            entity.HasOne(d => d.Category)
                .WithMany(p => p.Assets)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.SetNull); // If category is deleted, keep asset

            entity.HasOne(d => d.Turbine)
                .WithMany(p => p.Assets)
                .HasForeignKey(d => d.TurbineId);

            entity.HasOne(d => d.Store)
                .WithMany(p => p.Assets)
                .HasForeignKey(d => d.StoreId);
        });

        modelBuilder.Entity<Attachment>(entity =>
        {
            entity.HasOne(d => d.Asset)
                .WithMany(p => p.Attachments)
                .HasForeignKey(d => d.ItemId);
        });

        modelBuilder.Entity<AssetPlacement>(entity =>
{
    entity.HasKey(e => e.AssetPlacementId); // Defining the PK

    entity.HasOne(d => d.Asset)
        .WithMany()
        .HasForeignKey(d => d.ItemId);

    entity.HasOne(d => d.Shelf)
        .WithMany(p => p.Placements)
        .HasForeignKey(d => d.ShelfId);

    entity.HasOne(d => d.Rack)
        .WithMany(p => p.Placements)
        .HasForeignKey(d => d.RackId);
});
    }
}