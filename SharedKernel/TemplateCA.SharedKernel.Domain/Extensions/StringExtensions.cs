using System.Text;

namespace TemplateCA.SharedKernel.Domain.Extensions;

public static class StringExtensions
{
    public static T ToEnum<T>(this String instance)
        where T : struct, Enum
    {
        return Enum.Parse<T>(instance);
    }

    public static String FirstLetterToUpperCase(this String instance)
    {
        return instance.Substring(0, 1).ToUpperInvariant();
    }


    public static Byte[] Base64ToBytes(this String instance)
    {
        return Convert.FromBase64String(instance);
    }

    public static Byte[] ToBytes(this String instance)
    {
        return Encoding.UTF8.GetBytes(instance);
    }

    public static String StringToBase64(this String instance)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(instance));
    }

    public static String Base64ToString(this String instance)
    {
        return Encoding.UTF8.GetString(Convert.FromBase64String(instance));
    }
}