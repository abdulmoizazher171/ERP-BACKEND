namespace ERP_BACKEND.constracts;

    public class AssetPlacement
    {
        // Since this is a bridge/placement table, you still need a Primary Key
        // even if it's not in your SQL, EF Core requires one. 
        // If your SQL doesn't have an ID, we can use a Composite Key or add an ID.
        public int AssetPlacementId { get; set; } 

        public int ItemId { get; set; }
        public int ShelfId { get; set; }
        public int RackId { get; set; }
        public DateTime PlacedDate { get; set; }
        public string PlacedBy { get; set; }

        // Navigation Properties
        public virtual Asset Asset { get; set; }
        public virtual Shelf Shelf { get; set; }
        public virtual Rack Rack { get; set; }
    }
