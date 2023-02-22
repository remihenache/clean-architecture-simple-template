using TemplateCA.SampleContext.Domain;

namespace TemplateCA.SampleContext.Tests.UseCases;

internal class SampleAggregateRepositoryMock : SampleAggregateRepository
{
    public Task<SampleAggregate> GetById(SampleAggregateId id)
    {
        return Task.FromResult(new SampleAggregate(id));
    }

    public Task Persist(SampleAggregate aggregate)
    {
        return Task.CompletedTask;
    }
}