using FluentValidation;
using TemplateCA.SharedKernel.Applications;

namespace TemplateCA.SampleContext.Applications.DoSomething;

public class DoSomethingValidation : Validator<DoSomethingCommand>
{
    public DoSomethingValidation()
    {
        RuleFor(x => x.AggregateId).GreaterThan(0);
    }
}