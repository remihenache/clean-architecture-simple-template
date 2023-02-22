namespace TemplateCA.SharedKernel.Applications.Presenters;

public static class PresentableExtension
{
    public static async Task<TOutput> Present<TOutput>(this Task presentable)
    {
        if (!presentable.GetType().GetGenericTypeDefinition().IsAssignableTo(typeof(Task)))
            throw new InvalidCastException();
        await presentable;
        Presentable value = presentable.GetType().GetProperty("Result")?.GetValue(presentable) as Presentable ??
                            throw new InvalidCastException();

        return await value.Present<TOutput>();
    }

    public static async Task<T> AsItSelf<T>(this Task<Presentable<T>> presentable)
    {
        return await (await presentable).AsItSelf();
    }
}