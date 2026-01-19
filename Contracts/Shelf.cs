namespace ERP_BACKEND.constracts;

public class Shelf
{
    public int SHELF_ID { get; set; }
    public string SHELF_CODE { get; set; } =string.Empty;
    public virtual ICollection<AssetPlacement> Placements { get; set; }
}