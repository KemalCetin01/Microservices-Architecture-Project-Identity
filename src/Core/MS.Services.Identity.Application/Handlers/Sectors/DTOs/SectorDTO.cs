using MS.Services.Identity.Domain.Enums;
using MS.Services.Core.Base.Dtos;
//using static MS.Services.Identity.Application.Mappers.LocalizationMapper;

namespace MS.Services.Identity.Application.Handlers.Sectors.DTOs;

public class SectorDTO : IResponse
{
    public int Id { get; init; }
    //[Localize]
    public string Name { get; set; } = null!;
    public StatusEnum Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid? CreatedBy { get; set; }
}