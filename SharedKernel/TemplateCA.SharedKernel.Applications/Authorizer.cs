using JetBrains.Annotations;
using MediatR;

namespace TemplateCA.SharedKernel.Applications;

[UsedImplicitly(ImplicitUseTargetFlags.WithInheritors)]
public interface Authorizer<in T>
    where T : IBaseRequest
{
    Task AuthorizeAsync(T instance, CancellationToken cancellation = default);
}