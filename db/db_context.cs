using Microsoft.EntityFrameworkCore;
namespace ERP_BACKEND.data;

using ERP_BACKEND.constracts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Asset> Assets { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<Turbine> Turbines { get; set; }
    public DbSet<Store> Store { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<User> Users { get; set; }

    public DbSet<Rack> Rack {get; set;}

    public DbSet<Shelf> Shelf {get; set;}

    public DbSet<AssetPlacement> Asset_Placement {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Fluent API: Explicitly defining the relationships

        modelBuilder.Entity<Asset>(entity =>
        {
            entity.HasKey(e => e.ITEM_ID);

            entity.HasOne(d => d.Category)
                .WithMany(p => p.Assets)
                .HasForeignKey(d => d.CATEGORY_ID)
                .OnDelete(DeleteBehavior.SetNull); // If category is deleted, keep asset

            entity.HasOne(d => d.Turbine)
                .WithMany(p => p.Assets)
                .HasForeignKey(d => d.TURBINE_ID);

            entity.HasOne(d => d.Store)
                .WithMany(p => p.Assets)
                .HasForeignKey(d => d.STORE_ID);
        });

        modelBuilder.Entity<Attachment>(entity =>
        {
            entity.HasOne(d => d.Asset)
                .WithMany(p => p.Attachments)
                .HasForeignKey(d => d.ITEM_ID);
        });

        modelBuilder.Entity<AssetPlacement>(entity =>
{
    entity.HasKey(e => e.ASSET_PLACEMENT_ID); // Defining the PK

    entity.HasOne(d => d.Asset)
        .WithMany()
        .HasForeignKey(d => d.ITEM_ID);

    entity.HasOne(d => d.Shelf)
        .WithMany(p => p.Placements)
        .HasForeignKey(d => d.SHELF_ID);

    entity.HasOne(d => d.Rack)
        .WithMany(p => p.AssetPlacements)
        .HasForeignKey(d => d.RACK_ID);
});

    modelBuilder.Entity<User>()
        .HasKey(u => u.USER_ID);


    modelBuilder.Entity<Category>()
        .HasKey(c=>c.CATEGORY_ID);

    

    modelBuilder.Entity<Attachment>()
        .HasKey(a=>a.ATTACHMENT_ID);

        
    modelBuilder.Entity<Shelf>()
        .HasKey(s=>s.SHELF_ID);

    
    modelBuilder.Entity<Turbine>()
    .HasKey(t=>t.TURBINE_ID);

    modelBuilder.Entity<Store>()
    .HasKey(s=>s.STORE_ID);
    
    
    
    modelBuilder.Entity<Rack>()
    .HasKey(r=>r.RACK_ID);
    
    
    
    
    }

    

    
}
