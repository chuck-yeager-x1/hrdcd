namespace HRDCD.Order.Tasks.DTO.Order;

/// <summary>
/// Класс, содержащий данные о заказе. Используется в качестве значения, возвращаемого из операции. 
/// </summary>
public class OrderResultValue
{
    /// <summary>
    /// Возвращает или задает первичный ключ заказа в БД.
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Возвращает или задает номер заказа.
    /// </summary>
    public string OrderNumber { get; set; }
    
    /// <summary>
    /// Возвращает или задает наименование заказа.
    /// </summary>
    public string OrderName { get; set; }
    
    /// <summary>
    /// ВОзвращает или задает описание заказа.
    /// </summary>
    public string OrderDescription { get; set; }
    
    /// <summary>
    /// Возвращает или задает признак (не)отправки данных о заказе в очередь сообщений.
    /// </summary>
    public bool IsSent {get; set;}
}