namespace MS.Services.Identity.Application.Helpers.Utility;

public static class DateTimeHelper
{
    public static DateTime? SetKindUtc(this DateTime? dateTime)
    {
        return dateTime?.SetToUtc();
    }

    public static DateTime SetKindUtc(this DateTime dateTime)
    {
        return dateTime.SetToUtc();
    }
    public static DateTime AddSecondsUtc(int extraTime)
    {
        return DateTime.UtcNow.AddSeconds(extraTime);
    }

    private static DateTime SetToUtc(this DateTime dateTime)
    {
        return dateTime.Kind == DateTimeKind.Utc ? dateTime : DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
    }
}
