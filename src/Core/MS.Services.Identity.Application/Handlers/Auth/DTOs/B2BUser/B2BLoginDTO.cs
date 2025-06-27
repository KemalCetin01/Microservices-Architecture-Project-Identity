using MS.Services.Core.Base.Dtos;

namespace MS.Services.Identity.Application.Handlers.Auth.DTOs.B2BUser;

public class B2BLoginDTO : IResponse
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}