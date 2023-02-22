namespace TemplateCA.SharedKernel.Domain;

public interface DateTimeProvider
{
    DateTime GetNowUtc();
}