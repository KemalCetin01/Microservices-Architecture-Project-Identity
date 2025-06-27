using Microsoft.AspNetCore.Http;
using MS.Services.Core.Base.Dtos;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Application.Handlers.Auth.DTOs.B2BUser;

public class TaxCertificateDTO
{
    public TaxCertificateTypeEnum TaxCertificateType { get; set; }
    public IFormFile TaxCertificate { get; set; }

}