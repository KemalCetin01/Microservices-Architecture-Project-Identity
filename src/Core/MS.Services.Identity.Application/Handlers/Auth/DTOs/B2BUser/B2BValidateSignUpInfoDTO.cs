using MS.Services.Core.Base.Dtos;

namespace MS.Services.Identity.Application.Handlers.Auth.DTOs.B2BUser;

public class B2BValidateSignUpInfoDTO : IResponse
{
    public string CompanyName { get; set; }
    public int SectorId { get; set; } 
    public int NumberOfEmployeeId { get; set; }
    public int ActivityAreaId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Suffix { get; set; }
    public int PositionId { get; set; }
    public int OccupationId { get; set; } //meslek

}