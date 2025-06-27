using MS.Services.Core.Base.Dtos;

namespace MS.Services.Identity.Application.Handlers.Auth.DTOs;

public class ResendOtpDTO : IResponse
{
    public Guid? TransactionId { get; set; }
    public string? OTPCode { get; set; }
}