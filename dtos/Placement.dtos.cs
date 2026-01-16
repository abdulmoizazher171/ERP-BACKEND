namespace ERP_BACKEND.dtos;

public record PlacementReadDto
(
    int AssetPlacementId,
    int ItemId,
    int ShelfId,
    int RackId,
    DateTime PlacedDate,
    string PlacedBy
);

public record PlacementCreateDto 
(
    int ItemId,
    int ShelfId,
    int RackId,
    string PlacedBy


);

