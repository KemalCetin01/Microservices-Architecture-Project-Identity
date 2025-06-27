namespace MS.Services.Identity.Application.Extensions;

public static class FileExtensions
{
    private static readonly List<string> NotAllowedFileExtensions = new()
    {
        ".exe",
        ".dll"
    };

    public static bool IsAllowedExtension(this string fileName)
    {
        return !NotAllowedFileExtensions.Any(s => s.Equals(fileName, StringComparison.OrdinalIgnoreCase));
    }
}