namespace ERP_BACKEND.constracts;
public class User
{
    public int USER_ID { get; set; }
    public string? USERNAME { get; set; }
    public string? PASSWORD_HASH { get; set; }
    public string? ROLE { get; set; }
}

