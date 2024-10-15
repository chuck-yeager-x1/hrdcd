namespace HRDCD.Common.Tasks.Handlers;

/// <summary>
/// Обобщенный интерфейс обработки заданий.
/// </summary>
/// <typeparam name="TArgument">Входящий аргумент.</typeparam>
/// <typeparam name="TResult">Результат выполнения.</typeparam>
public interface ITaskHandler<in TArgument, TResult>
{
    /// <summary>
    /// Асинхронный метод для выполнения обработки заданий.
    /// </summary>
    /// <param name="argument">Аргумент.</param>
    /// <param name="cancellationToken">Токен.</param>
    /// <returns>Результат.</returns>
    Task<TResult> HandleTaskAsync(TArgument argument, CancellationToken cancellationToken);
}