using TemplateCA.SharedKernel.Domain;

namespace TemplateCA.SampleContext.Domain;

public record SampleAggregateId : ValueObject<Int64>
{
    public SampleAggregateId(Int64 value) : base(value)
    {
    }
}