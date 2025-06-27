using MS.Services.Core.Base.Dtos;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Application.Handlers.Auth.DTOs;

public class SignUpDTO : IResponse
{
    public Guid? TransactionId { get; set; }
    public string? OTPCode { get; set; }
    public AuthenticationDTO authenticationDTO { get; set; }
}