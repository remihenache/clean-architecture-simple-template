using MediatR;
using TemplateCA.SharedKernel.Applications;
using TemplateCA.SharedKernel.Applications.Presenters;
using TemplateCA.SharedKernel.Applications.Requests;

namespace TemplateCA.Api.Wrappers;

public class MediatorDispatcher : Dispatcher
{
    private readonly IMediator mediator;

    public MediatorDispatcher(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public Task<Presentable<TResponse>> Send<TResponse>(Query<TResponse> request,
        CancellationToken cancellationToken = default)
    {
        return this.mediator.Send(request, cancellationToken);
    }

    public Task<CommandResult> Send(Command request, CancellationToken cancellationToken = default)
    {
        return this.mediator.Send(request, cancellationToken);
    }
}