namespace TemplateCA.SampleContext.Domain;

public interface SampleAggregateRepository
{
    Task<SampleAggregate> GetById(SampleAggregateId id);

    Task Persist(SampleAggregate aggregate);
}