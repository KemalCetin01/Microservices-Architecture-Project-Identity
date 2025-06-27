using MS.Services.Core.Base.Dtos;

namespace MS.Services.Identity.Application.Handlers.Files.DTOs.Response;

public class FileUploadResponseDto : IResponse
{
    public string Name { get; set; } = null!;
    public string Path { get; set; } = null!;
}