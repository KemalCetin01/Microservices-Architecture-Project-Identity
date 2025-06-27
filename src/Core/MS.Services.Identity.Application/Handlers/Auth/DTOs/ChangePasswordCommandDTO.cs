using MS.Services.Core.Base.Dtos;

namespace MS.Services.Identity.Application.Handlers.Auth.DTOs;

public class ChangePasswordCommandDTO : IResponse
{
    public Guid TransactionId { get; set; }
    public string Password { get; set; }
}
