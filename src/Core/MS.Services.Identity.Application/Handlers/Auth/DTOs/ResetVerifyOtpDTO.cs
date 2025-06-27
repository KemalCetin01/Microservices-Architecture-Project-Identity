using MS.Services.Core.Base.Dtos;

namespace MS.Services.Identity.Application.Handlers.Auth.DTOs;

public class ResetVerifyOtpDTO : IResponse
{
    public Guid TransactionId { get; set; }
}