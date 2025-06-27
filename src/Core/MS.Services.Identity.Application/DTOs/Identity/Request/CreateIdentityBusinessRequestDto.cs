using Newtonsoft.Json;
using MS.Services.Core.Networking.Http.Models;

namespace MS.Services.Identity.Application.DTOs.Identity.Request;
public class CreateIdentityBusinessRequestDto
{
    public CreateIdentityBusinessRequestDto(string businessCode)
    {
        BusinessCode = businessCode;
    }

    public string BusinessCode { get; set; } = null!;

}