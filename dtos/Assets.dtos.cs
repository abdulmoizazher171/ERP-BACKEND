namespace ERP_BACKEND.dtos;

public record AssetReadDto(
int ItemId,
string ItemName,
string CategoryName,
int? SystemNumber,
string StoreName,
List<string> AttachmentUrls
);

public record AssetCreateDto(
    string ItemName,
    int CategoryId,
    int TurbineId,
    int StoreId,
    string Description
);