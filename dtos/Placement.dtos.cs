namespace ERP_BACKEND.dtos;

public record PlacementReadDto
(
    int PlacementId,
    int ItemId,
    int ShelfId,
    int RackId,
    DateTime PlacedDate,
    string PlacedBy,
    DateTime? WithdrawalDate,
    string WithdrawalBy,
    string Location,
    int Quantity
);

public record PlacementCreateDto 
(
    int ItemId,
    int ShelfId,
    int RackId,
    int TurbineId,
    string PlacedBy,
    string Location,
    int CategoryId,
    int amount,
    DateTime placedDate


);

public record PlacementModifyDto 
(
    int ItemId,
    int ShelfId,
    int RackId,
    string PlacedBy,
    string WithdrawalBy,
    DateTime WithdrawalDate,
    string Location,
    int Quantity


);


