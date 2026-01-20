namespace ERP_BACKEND.dtos;

public record readRackDto
(
    int RackId,
    string? RackNumber
);

public record createRackDto
(
   
    string? RackNumber
);