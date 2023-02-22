namespace TemplateCA.SharedKernel.Applications.Presenters;

public interface Presenter<in TInput, TOutput>
{
    Task<TOutput> Present(TInput input);
}