namespace ERP_BACKEND.constracts;

public class Asset
{
    public int ItemId { get; set; }
    public int? TurbineId { get; set; }
    public int? CategoryId { get; set; }
    public int? StoreId { get; set; }
    public string? ItemName { get; set; } 
    public string? Description { get; set; }
   

    // Navigation Properties
    public virtual Turbine Turbine { get; set; }
    public virtual Category Category { get; set; }
    public virtual Store Store { get; set; }
    public virtual ICollection<Attachment> Attachments { get; set; }
}

