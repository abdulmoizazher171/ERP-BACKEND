using ERP_BACKEND.constracts;

public class Turbine
{
    public int TurbineId { get; set; }
    public int SystemNumber { get; set; }

    // Navigation: One Turbine can house many Assets
    public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();
}