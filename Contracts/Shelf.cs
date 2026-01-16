namespace ERP_BACKEND.constracts;

public class Shelf
{
    public int ShelfId { get; set; }
    public string ShelfCode { get; set; } // e.g., "S-101"
    public virtual ICollection<AssetPlacement> Placements { get; set; }
}