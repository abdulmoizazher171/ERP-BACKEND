namespace ERP_BACKEND.dtos;

public record UserReadDto (
    int userId,
    string Username,
    string PasswordHash,
    string role 
);

public record UserCreateDto (
    string Username,
    string PasswordHash,
    string role 
);


public record Loginrequest (
    string username,
    string PasswordHash
);


public record Loginresponse(
    string role,
    string token
);