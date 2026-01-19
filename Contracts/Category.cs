using ERP_BACKEND.constracts;

public class Category
{
    public int CATEGORY_ID { get; set; }
    public string? CATEGORY_NAME { get; set; }

    // Navigation: One Category can have many Assets
    public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();
}