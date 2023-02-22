using TemplateCA.SampleContext.Domain;
using TemplateCA.SharedKernel.Domain;
using TemplateCA.SharedKernel.Extensions;

namespace TemplateCA.SampleContext.Adapters;

public class InMemorySampleAggregateRepository : SampleAggregateRepository
{
    public Task<SampleAggregate> GetById(SampleAggregateId id)
    {
        return Task.FromResult(new SampleAggregate(id));
    }

    public Task Persist(SampleAggregate aggregate)
    {
        return aggregate.Events.ForEachAsync(TreatEvent);
    }

    private Task TreatEvent(DomainEvent @event)
    {
        return @event switch
        {
            SomethingDone somethingDone => PersistSomethingDone(somethingDone),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private Task PersistSomethingDone(SomethingDone somethingDone)
    {
        // make some database or file operation
        return Task.CompletedTask;
    }
}