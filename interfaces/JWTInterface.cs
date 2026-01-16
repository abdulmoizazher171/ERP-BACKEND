using ERP_BACKEND.constracts;
using ERP_BACKEND.dtos;

namespace ERP_BACKEND.interfaces;


public interface IJWTinterface
{
    public Task<Loginresponse>Authenticate (Loginrequest data );
}

