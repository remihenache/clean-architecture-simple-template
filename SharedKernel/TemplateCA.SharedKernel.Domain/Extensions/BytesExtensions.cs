using System.Text;

namespace TemplateCA.SharedKernel.Domain.Extensions;

public static class BytesExtensions
{
    public static String BytesToBase64(this Byte[] instance)
    {
        return Convert.ToBase64String(instance);
    }

    public static String BytesToString(this Byte[] instance)
    {
        return Encoding.UTF8.GetString(instance);
    }
}