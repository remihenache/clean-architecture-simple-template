using JetBrains.Annotations;
using MediatR;
using TemplateCA.SharedKernel.Domain;

namespace TemplateCA.SharedKernel.Applications.Requests;

[UsedImplicitly(ImplicitUseTargetFlags.WithInheritors)]
public abstract class CommandHandler<TCommand> : IRequestHandler<TCommand, CommandResult>
    where TCommand : Command
{
    public async Task<CommandResult> Handle(TCommand request, CancellationToken cancellationToken)
    {
        return CommandResult.FromAggregate(await Execute(request, cancellationToken));
    }

    protected abstract Task<Aggregate> Execute(TCommand request, CancellationToken cancellationToken);
}