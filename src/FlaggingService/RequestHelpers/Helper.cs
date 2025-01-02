namespace FlaggingService.RequestHelpers;

public static class Helper
{
    public static DateTime ConvertToUtc(DateTime dateTime)
    {
        return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
    }
}
