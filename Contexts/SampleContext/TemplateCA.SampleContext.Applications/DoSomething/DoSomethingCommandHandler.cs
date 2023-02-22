using TemplateCA.SampleContext.Domain;
using TemplateCA.SharedKernel.Applications.Requests;
using TemplateCA.SharedKernel.Domain;

namespace TemplateCA.SampleContext.Applications.DoSomething;

public class DoSomethingCommandHandler : CommandHandler<DoSomethingCommand>
{
    private readonly SampleAggregateRepository sampleAggregateRepository;

    public DoSomethingCommandHandler(SampleAggregateRepository sampleAggregateRepository)
    {
        this.sampleAggregateRepository = sampleAggregateRepository;
    }

    protected override async Task<Aggregate> Execute(DoSomethingCommand request, CancellationToken cancellationToken)
    {
        SampleAggregate aggregate =
            await this.sampleAggregateRepository.GetById(new SampleAggregateId(request.AggregateId));
        aggregate.DoSomething();
        await this.sampleAggregateRepository.Persist(aggregate);
        return aggregate;
    }
}