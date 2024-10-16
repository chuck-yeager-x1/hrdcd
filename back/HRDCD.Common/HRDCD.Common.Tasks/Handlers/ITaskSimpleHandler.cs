namespace HRDCD.Common.Tasks.Handlers;

/// <summary>
/// Интерфейс обработчика заданий.
/// </summary>
/// <typeparam name="TArgument">Тип входящего аргумента.</typeparam>
/// <typeparam name="TResult">Тип результата выполнения операции.</typeparam>
public interface ITaskHandler<in TArgument, TResult>
{
    /// <summary>
    /// Асинхронный метод для выполнения обработки заданий.
    /// </summary>
    /// <param name="argument">Входящий аргумент.</param>
    /// <param name="cancellationToken">Запрос на отмену операции.</param>
    /// <returns>Результат выполнения операции.</returns>
    Task<TResult> HandleTaskAsync(TArgument argument, CancellationToken cancellationToken);
}