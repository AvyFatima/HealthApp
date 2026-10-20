namespace HealthApp.Interfaces;

public interface IXmlStore<T>
{
    Task<IReadOnlyList<T>> LoadAsync(
        CancellationToken cancellationToken = default);

    Task SaveAsync(
        IReadOnlyList<T> items,
        CancellationToken cancellationToken = default);
}
