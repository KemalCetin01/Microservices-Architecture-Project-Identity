using Microsoft.AspNetCore.Http;
using MS.Services.Core.Base.Dtos;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Application.Handlers.Auth.DTOs.B2BUser;

public class B2BSignupCommandDTO : IResponse
{
    #region information
    public string CompanyName { get; set; } = null!;
    public int SectorId { get; set; }
    public int NumberOfEmployeeId { get; set; }
    public int ActivityAreaId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Suffix { get; set; } = null!;
    public int PositionId { get; set; }
    public int OccupationId { get; set; } //meslek
    #endregion

    #region Address
    public int CountryId { get; set; }
    public int CityId { get; set; }
    public string AddressName { get; set; } = null!;
    public string AddressLine1 { get; set; } = null!;
    public string AddressLine2 { get; set; } = null!;
    public string Region { get; set; } = null!;
    public string ZipCode { get; set; } = null!;
    #endregion

    #region register
    public string Email { get; set; } = null!;
    public string PhoneCountryCode { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string TaxNumber { get; set; } = null!;
    public ICollection<IFormFile>? TaxCertificates { get; set; }
    public string Password { get; set; }=null!;
    public List<ConfirmRegisterEnum> ConfirmRegisters { get; set; }

    #endregion

}