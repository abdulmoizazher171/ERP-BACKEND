namespace ERP_BACKEND.constracts;

public class Rack
{
    public int RACK_ID { get; set; }
    public string? RACK_NUMBER { get; set; } // e.g., "R-44"
    public virtual ICollection<AssetPlacement>? AssetPlacements { get; set; }
}