using MS.Services.Core.Base.Dtos;

namespace MS.Services.Identity.Application.Handlers.Auth.DTOs;

public class ResetPasswordCommandDTO : IResponse
{
    public string Email { get; set; }
}
