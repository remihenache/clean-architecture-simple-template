namespace TemplateCA.SharedKernel.Applications.Presenters;

public interface ItemPresenter<in TInput, TOutput>
{
    Task<TOutput> Present(TInput input);
}