using ERP_BACKEND.constracts;

public class Turbine
{
    public int TURBINE_ID{ get; set; }
    public int SYSTEM_NUMBER { get; set; }

    // Navigation: One Turbine can house many Assets
    public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();
}