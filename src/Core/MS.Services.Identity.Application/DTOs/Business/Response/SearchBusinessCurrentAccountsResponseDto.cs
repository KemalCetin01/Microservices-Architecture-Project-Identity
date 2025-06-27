using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Application.DTOs.Business.Response;

public class SearchBusinessCurrentAccountsResponseDto
{
    public Guid Id { get; set; }
    public string? CurrentAccountName { get; set; }
    public string? Code { get; set; }
    public string? ErpRefId { get; set; }
}
