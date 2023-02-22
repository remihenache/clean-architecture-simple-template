namespace TemplateCA.SharedKernel.Domain.Extensions;

public static class DateExtensions
{
    public static DateTime ToDateTimeUtc(this String instance, String format = "dd-MM-yyyy HH:mm")
    {
        return DateTime.SpecifyKind(DateTime.ParseExact(instance, format, null), DateTimeKind.Utc);
    }
}