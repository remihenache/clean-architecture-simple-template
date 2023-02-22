namespace TemplateCA.SharedKernel.Domain;

public record EntityId : ValueObject<String>
{
    protected EntityId(String value)
        : base(value)
    {
    }

    public static T CreateNew<T>() where T : EntityId
    {
        return (Activator.CreateInstance(typeof(T), Guid.NewGuid().ToString()) as T)!;
    }
}