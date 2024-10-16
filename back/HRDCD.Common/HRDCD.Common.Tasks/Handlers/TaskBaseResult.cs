namespace HRDCD.Common.Tasks.Handlers;

/// <summary>
/// Класс для представления результата выполнения операции.
/// </summary>
public abstract class TaskBaseResult
{
    /// <summary>
    /// Возвращает или задает текстовое сообщение о ошибке.
    /// </summary>
    public string ErrorMessage { get; set; }
    
    /// <summary>
    /// Возвращает или задает признак (не)успешности выполнения операции.
    /// </summary>
    public bool IsSuccess { get; set; }
}