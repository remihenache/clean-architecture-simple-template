using TemplateCA.SharedKernel.Domain;

namespace TemplateCA.SampleContext.Domain;

public class SampleAggregate : EventAggregate
{
    private Boolean somethingAlreadyHappen;

    public SampleAggregate(SampleAggregateId id)
        : base(ArraySegment<DomainEvent>.Empty)
    {
        Id = id;
    }

    public void DoSomething()
    {
        if (this.somethingAlreadyHappen)
            return;
        SomethingDone somethingDone = new();
        ApplyEvent(somethingDone);
        AddEvent(somethingDone);
    }

    protected override void ApplyEvent(DomainEvent @event)
    {
        if (@event is SomethingDone somethingDone)
            this.somethingAlreadyHappen = true;
    }
}