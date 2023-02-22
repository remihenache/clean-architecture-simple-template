using JetBrains.Annotations;
using MediatR;
using TemplateCA.SharedKernel.Applications.Presenters;

namespace TemplateCA.SharedKernel.Applications.Requests;

[UsedImplicitly(ImplicitUseTargetFlags.WithInheritors)]
public class PresenterQueryHandler<TQuery, TOutput> : IRequestHandler<TQuery, Presentable<TOutput>>
    where TQuery : Query<TOutput>
{
    private readonly QueryHandler<TQuery, TOutput> queryHandler;
    private readonly IServiceProvider serviceProvider;

    public PresenterQueryHandler(IServiceProvider serviceProvider, QueryHandler<TQuery, TOutput> queryHandler)
    {
        this.serviceProvider = serviceProvider;
        this.queryHandler = queryHandler;
    }

    public async Task<Presentable<TOutput>> Handle(TQuery request, CancellationToken cancellationToken)
    {
        TOutput output = await this.queryHandler.Handle(request, cancellationToken);
        return new GenericPresentable<TOutput>(output, this.serviceProvider);
    }
}