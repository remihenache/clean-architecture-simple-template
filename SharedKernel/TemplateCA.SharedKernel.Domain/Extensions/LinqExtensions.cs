namespace TemplateCA.SharedKernel.Domain.Extensions;

public static class LinqExtensions
{
    public static async Task<IEnumerable<TResult>> SelectAsync<TSource, TResult>(this IEnumerable<TSource> source,
        Func<TSource, Task<TResult>> method)
    {
        return await Task.WhenAll(source.Select(s => method(s)));
    }

    public static async Task<List<TSource>> ToListAsync<TSource>(this Task<IEnumerable<TSource>> source)
    {
        return (await source).ToList();
    }
}