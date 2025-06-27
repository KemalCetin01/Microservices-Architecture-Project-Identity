using MS.Services.Core.Base.Dtos;

namespace MS.Services.Identity.Application.Handlers.Auth.DTOs.B2CUser;

public class B2CLoginDTO : IResponse
{
    public string EmailOrPhone { get; set; }
    public string Password { get; set; }
}