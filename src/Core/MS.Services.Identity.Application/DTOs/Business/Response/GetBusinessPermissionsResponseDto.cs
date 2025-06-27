using MS.Services.Core.Base.Dtos;

namespace MS.Services.Identity.Application.DTOs.Business.Response;

public class GetBusinessPermissionsResponseDto : IResponse
{
    public string IdentityRefId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool Selected { get; set; }
}

