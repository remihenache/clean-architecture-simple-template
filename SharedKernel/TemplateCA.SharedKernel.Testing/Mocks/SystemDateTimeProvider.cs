using TemplateCA.SharedKernel.Domain;

namespace TemplateCA.SharedKernel.Testing.Mocks;

public class SystemDateTimeProvider : DateTimeProvider
{
    public DateTime GetNowUtc()
    {
        return DateTime.UtcNow;
    }
}