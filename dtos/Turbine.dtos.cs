namespace ERP_BACKEND.dtos;

public record readTurbineDto
(
    int TurbineId,
    int SystemNumber
);


public record createTurbineDto
(
   
    int SystemNumber
);