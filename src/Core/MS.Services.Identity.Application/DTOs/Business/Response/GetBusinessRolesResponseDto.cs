using MS.Services.Core.Base.Dtos;

namespace MS.Services.Identity.Application.DTOs.Business.Response;

public class GetBusinessRoleResponseDto : IResponse
{
    public string IdentityRefId { get; set; } = null!;
    public string Name { get; set; } = null!;
}