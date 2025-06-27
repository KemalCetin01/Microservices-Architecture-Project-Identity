using System.Collections;
using MS.Services.Identity.Application.DTOs.Business.Request;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Application.Extensions;

public static class EntityMapperExtensions
{
    
    public static Business CreateBusiness(this CreateBusinessRequestDTO request, string businessCode, Guid identityRefId, ReviewStatusEnum reviewStatus = ReviewStatusEnum.Approved)
    {
        return new Business() {
          Code = businessCode,
          IdentityRefId = identityRefId,
          ReviewStatus = ReviewStatusEnum.Approved,
          Name = request.Name,
          BusinessStatusId = (int) request.BusinessStatus,
          RepresentativeId = request.RepresentativeId,
          SectorId = request.SectorId,
          ActivityAreaId = request.ActivityAreaId,
          NumberOfEmployeeId = request.NumberOfEmployeeId,
          Phone = request.Phone,
          PhoneCountryCode = request.PhoneCountryCode,
          FaxNumber = request.FaxNumber,
          DiscountRate = request.DiscountRate
        };
    }

}