using MS.Services.Core.Base.Dtos;

namespace MS.Services.Identity.Application.Handlers.Auth.DTOs;

public class AuthenticationDTO : IResponse 
{
    public string AccessToken { get; set; } 
    public int ExpiresIn { get; set; }
    public int RefreshExpiresIn { get; set; }
    public string RefreshToken { get; set; }
    public string TokenType { get; set; }
}
