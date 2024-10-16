namespace HRDCD.Common.Tasks.Handlers;

/// <summary>
/// Класс для представления результата выполнения операции, состоящего из одного элемента.
/// </summary>
/// <typeparam name="TResultEntity"></typeparam>
public abstract class TaskSingleResult<TResultEntity> : TaskBaseResult
{
    /// <summary>
    /// Возвращает или задает переменную типа <see cref="TResultEntity"/>,
    /// возвращаемую в результате выполнения операции.
    /// </summary>
    public TResultEntity Result { get; set; }
}