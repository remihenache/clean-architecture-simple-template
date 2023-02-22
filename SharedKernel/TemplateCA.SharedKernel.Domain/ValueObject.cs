namespace TemplateCA.SharedKernel.Domain;

public record ValueObject<T>
{
    private readonly T value;

    protected ValueObject(T value)
    {
        this.value = value;
    }

    public static implicit operator T(ValueObject<T> instance)
    {
        return instance.value;
    }


    public override String ToString()
    {
        return this.value!.ToString() ?? String.Empty;
    }
}