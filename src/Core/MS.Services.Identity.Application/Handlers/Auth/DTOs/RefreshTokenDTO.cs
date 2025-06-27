using MS.Services.Core.Base.Dtos;

namespace MS.Services.Identity.Application.Handlers.Auth.DTOs;

public class RefreshTokenDTO : IResponse
{
    public string RefreshToken { get; set; }
}