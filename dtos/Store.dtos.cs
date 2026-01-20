namespace ERP_BACKEND.dtos;

public record readStoreDto
(
    int StoreId,
    string? StoreName
);

public record createStoreDto
(
  
    string? StoreName
);