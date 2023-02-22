using TemplateCA.SharedKernel.Applications.Requests;

namespace TemplateCA.SampleContext.Applications.DoSomething;

public record DoSomethingCommand(Int64 AggregateId) : Command;