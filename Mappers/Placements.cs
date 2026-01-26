namespace ERP_BACKEND.mappers;

public class PlacementMapper
{
    public int PlacementId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public string RackNumber { get; set; } = string.Empty;
    public string ShelfName { get; set; } = string.Empty;
    public DateTime PlacedDate { get; set; }
    public string PlacedBy { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string? WithdrawnBy { get; set; }
    public DateTime WithdrawalDate { get; set; }
    public int Quantity { get; set; }

    public string CategoryName { get; set; } = string.Empty;
    public string SystemNumber { get; set; } = string.Empty;

}