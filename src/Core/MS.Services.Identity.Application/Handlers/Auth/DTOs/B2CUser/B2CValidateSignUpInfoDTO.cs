using MS.Services.Core.Base.Dtos;

namespace MS.Services.Identity.Application.Handlers.Auth.DTOs.B2BUser;

public class B2CValidateSignUpInfoDTO : IResponse
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Suffix { get; set; }
}