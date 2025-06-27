namespace MS.Services.Identity.Application.Helpers.Options;

public class CdnOptions
{
    public string Path { get; set; } = null!;
    public string ApiKey { get; set; } = null!;
    public string BucketName { get; set; } = null!;
    public string SecretKey { get; set; } = null!;
    public string ServiceUrl { get; set; } = null!;
}