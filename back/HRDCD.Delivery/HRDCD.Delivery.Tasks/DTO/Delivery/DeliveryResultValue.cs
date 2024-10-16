using HRDCD.Delivery.DataModel.Entity;

namespace HRDCD.Delivery.Tasks.DTO.Delivery;

/// <summary>
/// Класс, содержащий данные о заявке на доставку. Используется в качестве значения, возвращаемого из операции.
/// </summary>
public class DeliveryResultValue
{
    /// <summary>
    /// Возвращает или задает первичный ключ заявки на доставку в БД.
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Возвращает или задает номер заказа, с которым связана заявка на доставку.
    /// </summary>
    public string OrderNumber { get; set; }
    
    /// <summary>
    /// Возвращает или задает наименование заказа, с которым связана заявка на доставку.
    /// </summary>
    public string OrderName { get; set; }
    
    /// <summary>
    /// Возвращает или задает статус заявки на доставку.
    /// </summary>
    public DeliveryStatus DeliveryStatus { get; set; }
}