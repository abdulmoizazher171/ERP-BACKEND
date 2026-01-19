namespace ERP_BACKEND.constracts;

public class Asset
{
    public int ITEM_ID { get; set; }
    public int? TURBINE_ID { get; set; }
    public int? CATEGORY_ID { get; set; }
    public int? STORE_ID { get; set; }

    public int? RACK_ID {get; set;}
    public string? ITEM_NAME { get; set; } 
    public string? DESCRIPTION { get; set; }
   

    // Navigation Properties
    public virtual Turbine Turbine { get; set; }
    public virtual Category Category { get; set; }
    public virtual Store Store { get; set; }
    public virtual ICollection<Attachment> Attachments { get; set; }
}

