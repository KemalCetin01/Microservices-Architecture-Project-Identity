using MS.Services.Core.Base.Dtos;
using MS.Services.Core.Networking.Http.Models;

namespace MS.Services.Identity.Application.DTOs.Identity.Response;
public class UpdateIdentityUserResponseDto : IResponse
{
    public bool IsSuccess { get; set; }
}
