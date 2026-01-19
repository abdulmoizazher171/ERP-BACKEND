namespace ERP_BACKEND.constracts;

public class Store
{
    public int STORE_ID { get; set; }
    public string? STORE_NAME { get; set; }

    // Navigation: One Store can provide many Assets
    public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();
}