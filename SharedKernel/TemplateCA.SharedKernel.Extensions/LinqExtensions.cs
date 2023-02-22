namespace TemplateCA.SharedKernel.Extensions;

public static class LinqExtensions
{
    public static async Task<IEnumerable<TResult>> SelectAsync<TSource, TResult>(this IEnumerable<TSource> source,
        Func<TSource, Task<TResult>> method)
    {
        return await Task.WhenAll(source.Select(method));
    }

    public static async Task<List<TSource>> ToListAsync<TSource>(this Task<IEnumerable<TSource>> source)
    {
        return (await source).ToList();
    }

    public static async Task<IEnumerable<TResult>> SelectManyAsync<TSource, TResult>(this IEnumerable<TSource> source,
        Func<TSource, Task<IEnumerable<TResult>>> method)
    {
        List<TResult> list = new();
        foreach (TSource item in source)
        {
            IEnumerable<TResult> results = await method(item);
            list.AddRange(results);
        }

        return list;
    }


    public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
    {
        foreach (TSource element in source) action(element);
    }

    public static async Task ForEachAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, Task> action)
    {
        foreach (TSource element in source) await action(element);
    }
}