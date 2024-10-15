namespace HRDCD.Common.Tasks.DTO;

/// <summary>
/// Класс для представления параметров, необходимых при запросе постраничного просмотра списка.
/// </summary>
public class SelectParamBase
{
    /// <summary>
    /// Возвращает или задает размер страницы в постраничном просмотре списка.
    /// </summary>
    public int PageSize { get; set; }
    
    /// <summary>
    /// Возвращает или задает номер страницы в постраничном просмотре списка.
    /// </summary>
    public int PageNumber { get; set; }
}