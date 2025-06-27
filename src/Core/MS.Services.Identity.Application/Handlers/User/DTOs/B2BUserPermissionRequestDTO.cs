using MS.Services.Core.Networking.Http.Models;

namespace MS.Services.Identity.Application.Handlers.User.DTOs;

public class B2BUserPermissionRequestDTO : IRestResponse
{
    public string id { get; set; }
    public string name { get; set; }
}