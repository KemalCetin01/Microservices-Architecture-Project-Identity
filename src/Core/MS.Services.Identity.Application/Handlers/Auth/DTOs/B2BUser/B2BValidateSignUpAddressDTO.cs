using MS.Services.Core.Base.Dtos;

namespace MS.Services.Identity.Application.Handlers.Auth.DTOs.B2BUser;

public class B2BValidateSignUpAddressDTO : IResponse
{
    public int CountryId { get; set; }
    public int CityId { get; set; }
    public string AddressName { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string Region { get; set; }
    public string ZipCode { get; set; }
}