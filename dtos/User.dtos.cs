namespace ERP_BACKEND.dtos;

public record UserReadDto (
    int userId,
    string Username,
    string PasswordHash,
    string role 
);

public record UserCreateDto (
    string Username,
    string Password,
    string role 
);


public record Loginrequest (
    string username,
    string Password,
    string role
);


public record Loginresponse(
    string role,
    string token,
    int tokenExpiryTimeStamp
);