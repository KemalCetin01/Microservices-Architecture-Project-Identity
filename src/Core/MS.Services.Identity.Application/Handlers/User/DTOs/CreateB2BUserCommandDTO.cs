using MS.Services.Core.Base.Dtos;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Application.Handlers.User.DTOs;

public class CreateB2BUserCommandDTO:IResponse
{
    public int? CountryId { get; set; }
    public int? CityId { get; set; }
    public int? TownId { get; set; }
    public Guid? RepresentativeId { get; set; }
    public Guid BusinessId { get; set; }
    public int? SectorId { get; set; } 
    public int? ActivityAreaId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string RePassword { get; set; } = null!;
    public string PhoneCountryCode { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string? PanelPrivileges { get; set; } 
    public Guid? UserGroupRoleId { get; set; }
    public SiteStatusEnum SiteStatus { get; set; }
    public UserStatusEnum UserStatus { get; set; }
}
