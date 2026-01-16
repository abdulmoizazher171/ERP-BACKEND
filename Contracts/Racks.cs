namespace ERP_BACKEND.constracts;

public class Rack
{
    public int RackId { get; set; }
    public string? RackNumber { get; set; } // e.g., "R-44"
    public virtual ICollection<AssetPlacement> Placements { get; set; }
}