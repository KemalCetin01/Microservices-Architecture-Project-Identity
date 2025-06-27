using MS.Services.Core.Base.IoC;
using MS.Services.Identity.Application.Handlers.Files.DTOs.Request;

namespace MS.Services.Identity.Application.Core.Infrastructure.Services.File;

public interface IFileService : IScopedService
{
    Task<string> UploadFileToCdnAsync(FileUploadRequestDto request, CancellationToken cancellationToken = default);
    public Task<string> GetFilePath(string path, DateTime expires);
}