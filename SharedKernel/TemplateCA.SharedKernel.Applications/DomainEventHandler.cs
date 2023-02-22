using JetBrains.Annotations;
using MediatR;
using TemplateCA.SharedKernel.Domain;

namespace TemplateCA.SharedKernel.Applications;

[UsedImplicitly(ImplicitUseTargetFlags.WithInheritors)]
public interface DomainEventHandler<TEvent> : INotificationHandler<TEvent>
    where TEvent : DomainEvent
{
}