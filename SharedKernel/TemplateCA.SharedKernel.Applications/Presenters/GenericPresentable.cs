using Microsoft.Extensions.DependencyInjection;

namespace TemplateCA.SharedKernel.Applications.Presenters;

public class GenericPresentable<TInput> : Presentable<TInput>
{
    private readonly TInput input;
    private readonly IServiceProvider serviceProvider;

    public GenericPresentable(TInput input, IServiceProvider serviceProvider)
    {
        this.input = input;
        this.serviceProvider = serviceProvider;
    }

    public Task<TOutput> Present<TOutput>()
    {
        return this.serviceProvider.GetService<Presenter<TInput, TOutput>>()?.Present(this.input) ??
               throw new NotSupportedException();
    }

    Task<TInput> Presentable<TInput>.AsItSelf()
    {
        return Task.FromResult(this.input);
    }
}