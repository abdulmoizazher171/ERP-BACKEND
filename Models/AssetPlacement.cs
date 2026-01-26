namespace ERP_BACKEND.constracts;

    public class AssetPlacement
    {
        // Since this is a bridge/placement table, you still need a Primary Key
        // even if it's not in your SQL, EF Core requires one. 
        // If your SQL doesn't have an ID, we can use a Composite Key or add an ID.
        public int PLACEMENT_ID { get; set; } 

        public int ITEM_ID { get; set; }
        public int SHELF_ID { get; set; }
        public int RACK_ID { get; set; }
        public DateTime PLACED_DATE { get; set; }
        public string PLACED_BY { get; set; } = string.Empty;
        public DateTime? WITHDRAWAL_DATE { get; set; }
        public string? WITHDRAWN_BY { get; set; } 
        public string LOCATION { get; set; } = string.Empty;

        public int QUANTITY { get; set; }
        // Navigation Properties
        public virtual Asset Asset { get; set; }
        public virtual Shelf Shelf { get; set; }
        public virtual Rack Rack { get; set; }
    }
