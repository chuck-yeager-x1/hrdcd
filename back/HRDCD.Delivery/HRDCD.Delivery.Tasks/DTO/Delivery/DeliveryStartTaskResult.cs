using HRDCD.Common.Tasks.Handlers;
using HRDCD.Delivery.Tasks.DTO.Order;

namespace HRDCD.Delivery.Tasks.DTO.Delivery;

/// <summary>
/// Класс, содержащий информацию о результате операции создания заявки на доставку заказа.
/// </summary>
public class DeliveryStartTaskResult : TaskBaseResult
{
    /// <summary>
    /// Возвращает или задает данные о созданной заявке на доставку.
    /// </summary>
    public DeliveryResultValue Delivery { get; set; }
    
    /// <summary>
    /// Возвращает или задает данные о заказе, для которого была создана заявка.
    /// </summary>
    public OrderResultValue Order { get; set; }
}