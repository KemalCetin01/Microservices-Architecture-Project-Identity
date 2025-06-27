using MS.Services.Core.Base.Dtos;
using MS.Services.Core.Base.Dtos.Response;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Application.DTOs.Business.Response;

public class GetBusinessRepresentativeResponseDto
{
    public Guid Id { get; set;}
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string FullName { 
        get {
            return String.Concat(this.FirstName, " ", this.LastName);
        }
    }
}

