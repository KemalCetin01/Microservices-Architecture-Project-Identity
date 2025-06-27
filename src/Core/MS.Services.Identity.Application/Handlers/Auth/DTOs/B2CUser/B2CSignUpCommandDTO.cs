using MS.Services.Core.Base.Dtos;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Application.Handlers.Auth.DTOs.B2BUser;

public class B2CSignUpCommandDTO : IResponse
{
    #region information
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Suffix { get; set; } = null!;
    #endregion

    #region signup
    public int CountryId { get; set; }
    public string CountryCode { get; set; }=null!;
    public int? CityId { get; set; }
    public string Email { get; set; } = null!;
    public string PhoneCountryCode { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string RePassword { get; set; } = null!;
    public VerificationTypeEnum VerificationType { get; set; }
    public List<ConfirmRegisterEnum> ConfirmRegisters { get; set; }
    #endregion

}