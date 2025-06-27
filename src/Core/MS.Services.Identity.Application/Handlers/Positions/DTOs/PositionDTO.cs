using MS.Services.Identity.Domain.Enums;
using MS.Services.Core.Base.Dtos;

namespace MS.Services.Identity.Application.Handlers.Positions.DTOs;

public class PositionDTO : IResponse
{
    public int Id { get; init; }
    public string Name { get; set; } = null!;
    public StatusEnum Status { get; set; }
    public DateTime CreatedDate { get; set; }

    public Guid? CreatedBy { get; set; }
}