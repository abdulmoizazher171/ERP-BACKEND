using ERP_BACKEND.constracts;

public class Category
{
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }

    // Navigation: One Category can have many Assets
    public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();
}