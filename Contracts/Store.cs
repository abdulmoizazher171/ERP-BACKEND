namespace ERP_BACKEND.constracts;

public class Store
{
    public int StoreId { get; set; }
    public string? StoreName { get; set; }

    // Navigation: One Store can provide many Assets
    public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();
}