namespace HRDCD.Delivery.Tasks.DTO.Order;

/// <summary>
/// Класс, содержащий входные данные для создания заказа, полученного из очереди сообщений.
/// </summary>
public class OrderCreateParam
{
    /// <summary>
    /// Возвращает или задает номер заказа.
    /// </summary>
    public string Number { get; set; }
    
    /// <summary>
    /// Возвращает или задает наименование заказа.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Возвращает или задает описание заказа.
    /// </summary>
    public string Description { get; set; }
}