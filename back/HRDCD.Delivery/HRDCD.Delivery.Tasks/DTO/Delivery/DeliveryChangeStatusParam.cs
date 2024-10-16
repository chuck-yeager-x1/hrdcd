using HRDCD.Delivery.DataModel.Entity;

namespace HRDCD.Delivery.Tasks.DTO.Delivery;

/// <summary>
/// Класс, содержащий входные данные для смены статуса заявки на доставку.
/// </summary>
public class DeliveryChangeStatusParam
{
    /// <summary>
    /// Возвращает или задает первичный ключ заявки на доставку, статус которой нужно поменять.
    /// </summary>
    public long DeliveryId { get; set; }
    
    /// <summary>
    /// Возвращает или задает новый статус заявки на доставку.
    /// </summary>
    public DeliveryStatus DeliveryStatus { get; set; }
}