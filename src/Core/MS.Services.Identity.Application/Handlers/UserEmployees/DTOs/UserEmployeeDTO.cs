using MS.Services.Core.Base.Dtos;

namespace MS.Services.Identity.Application.Handlers.UserEmployees.DTOs;
public class UserEmployeeDTO : IResponse
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Role { get; set; } = null!;
    public Guid RoleId { get; set; }
    public int? DiscountRate { get; set; }
    public DateTime? LastDateEntry { get; set; }
    public Guid IdentityRefId { get; set; }

}
