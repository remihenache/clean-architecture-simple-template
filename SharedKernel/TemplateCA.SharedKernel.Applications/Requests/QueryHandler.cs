using JetBrains.Annotations;

namespace TemplateCA.SharedKernel.Applications.Requests;

[UsedImplicitly(ImplicitUseTargetFlags.WithInheritors)]
public interface QueryHandler<TQuery, TOutput>
    where TQuery : Query<TOutput>
{
    Task<TOutput> Handle(TQuery request, CancellationToken cancellationToken);
}