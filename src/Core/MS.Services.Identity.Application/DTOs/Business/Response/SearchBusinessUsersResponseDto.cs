using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Application.DTOs.Business.Response;

public class SearchBusinessUsersResponseDto
{
    public Guid UserId { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }

}
