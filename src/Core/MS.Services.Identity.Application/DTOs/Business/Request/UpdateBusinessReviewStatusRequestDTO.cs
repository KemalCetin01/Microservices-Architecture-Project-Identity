using MS.Services.Core.Base.Dtos;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Application.DTOs.Business.Request;

public class UpdateBusinessReviewStatusRequestDto
{
    public Guid Id { get; set; }
    public ReviewStatusEnum ReviewStatus { get; set; }
}
