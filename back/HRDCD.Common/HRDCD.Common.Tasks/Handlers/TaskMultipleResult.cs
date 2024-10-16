namespace HRDCD.Common.Tasks.Handlers;

/// <summary>
/// Класс для представления результата выполнения операции, состоящего из множества элементов с возможностью
/// постраничного просмотра.
/// </summary>
/// <typeparam name="TResultEntity">Тип элементов, возвращаемых при выполнения операции.</typeparam>
public abstract class TaskMultipleResult<TResultEntity> : TaskBaseResult
{
    /// <summary>
    /// Возвращает или задает коллекцию результатов операции типа <see cref="TResultEntity"/>.
    /// </summary>
    public IList<TResultEntity> Results { get; set; }
    
    /// <summary>
    /// Возвращает или задает общее количество записей, доступных для постраничного просмотра.
    /// </summary>
    public int Total { get; set; }
    
    /// <summary>
    /// Возвращает или задает номер текущей страницы в постраничном просмотре (начиная с 1).
    /// </summary>
    public int PageNumber { get; set; }
    
    /// <summary>
    /// Возвращает или задает количество записей на странице.
    /// </summary>
    public int PageSize { get; set; }
}