namespace TemplateCA.SharedKernel.Applications.Presenters;

public interface ListPresenter<TInput, TOutput> : Presenter<IEnumerable<TInput>, IEnumerable<TOutput>>
{
}