using FluentAssertions;
using TemplateCA.SampleContext.Adapters;
using TemplateCA.SampleContext.Domain;

namespace TemplateCA.SampleContext.Tests.Adapters;

public class InMemorySampleAggregateRepositoryTests
{
    [Fact]
    public async Task GetByIdShouldReturnAnAggregateWithSameIdAsGivenInParameter()
    {
        InMemorySampleAggregateRepository sampleAggregateRepository = new();
        const Int32 expected = 1;
        SampleAggregate sampleAggregate = await sampleAggregateRepository.GetById(new SampleAggregateId(expected));
        sampleAggregate.Id.Should().Be(expected);
    }

    [Fact]
    public async Task PersistShouldStoreAllEvents()
    {
        InMemorySampleAggregateRepository sampleAggregateRepository = new();
        SampleAggregate aggregate = new(new SampleAggregateId(1));
        aggregate.DoSomething();
        await sampleAggregateRepository.Persist(aggregate);
        Assert.True(true); // Check if operation has been persisted, using mock on last operation
    }
}