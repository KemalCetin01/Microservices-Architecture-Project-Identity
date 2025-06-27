using MS.Services.Identity.Domain.Enums;
using MS.Services.Core.Base.Dtos;

namespace MS.Services.Identity.Application.Handlers.Occupations.DTOs;

public class OccupationDTO : IResponse
{
    public int Id { get; init; }
    public string Name { get; set; } = null!;
    public StatusEnum Status { get; set; }
    public DateTime CreatedDate { get; set; }

    public Guid? CreatedBy { get; set; }
}