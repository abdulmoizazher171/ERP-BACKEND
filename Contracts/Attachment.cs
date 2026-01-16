namespace ERP_BACKEND.constracts;


public class Attachment
{
    public int AttachmentId { get; set; }
    public int ItemId { get; set; }
    public string AttachmentUrl { get; set; }
    public virtual Asset Asset { get; set; }
}

