namespace ERP_BACKEND.constracts;


public class Attachment
{
    public int ATTACHMENT_ID{ get; set; }
    public int ITEM_ID { get; set; }
    public string? ATTACHMENT_URL { get; set; }
    public virtual Asset? Asset { get; set; }
}

