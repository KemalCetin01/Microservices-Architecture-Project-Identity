using MS.Services.Core.Base.Dtos;

namespace MS.Services.Identity.Application.Handlers.Auth.DTOs;

public class VerifyOtpDTO : IResponse
{
    public string OtpCode { get; set; }
    public Guid TransactionId { get; set; }
}