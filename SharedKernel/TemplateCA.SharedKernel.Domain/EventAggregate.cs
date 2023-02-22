namespace TemplateCA.SharedKernel.Domain;

public abstract class Aggregate
{
    public Int64 Id { get; protected set; }
}

public abstract class EventAggregate : Aggregate
{
    private readonly List<DomainEvent> domainEvents = new();

    public EventAggregate(IEnumerable<DomainEvent> events)
    {
        foreach (DomainEvent @event in events)
            // ReSharper disable once VirtualMemberCallInConstructor
            ApplyEvent(@event);
    }

    public virtual IReadOnlyCollection<DomainEvent> Events => this.domainEvents;

    protected abstract void ApplyEvent(DomainEvent @event);

    protected void AddEvent(DomainEvent @event)
    {
        this.domainEvents.Add(@event);
    }

    protected void AddEvents(IEnumerable<DomainEvent> events)
    {
        this.domainEvents.AddRange(events);
    }

    public virtual void ClearEvents()
    {
        this.domainEvents.Clear();
    }
}