using MS.Services.Core.Base.Dtos;

namespace MS.Services.Identity.Application.Handlers.Auth.DTOs.B2BUser;

public class B2CMoreQuestionsCommandDTO : IResponse
{
    public int SectorId { get; set; }
    public int OccupationId { get; set; }
    public int ActivityAreaId { get; set; }

}