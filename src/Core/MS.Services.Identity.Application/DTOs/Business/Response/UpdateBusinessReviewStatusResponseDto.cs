using MS.Services.Core.Base.Dtos;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Application.DTOs.Business.Response;

public class UpdateBusinessReviewStatusResponseDto : IResponse
{
    public Guid Id { get; set; }
    public ReviewStatusEnum ReviewStatus { get; set; }
    public Guid? ReviewApprovedBy { get; set; }
    public Guid? ReviewRejectedBy { get; set; }
    public DateTime? ReviewApprovedDate { get; set; }
    public DateTime? ReviewRejectedDate { get; set; }
}
