namespace ERP_BACKEND.dtos;

public record readCategoryDto
(
    int CategoryId,
    string? CategoryName
);

public record createCategoryDto
(
    int CategoryId,
    string? CategoryName
);