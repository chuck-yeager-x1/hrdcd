namespace HRDCD.Delivery.DataModel.Entity;

using HRDCD.Common.DataModel.Entity;

/// <summary>
/// Класс для сущности "Доставка", сохраняемой в БД.
/// </summary>
public class DeliveryEntity : BaseEntity
{
    /// <summary>
    /// Возвращает или задает статус доставки.
    /// </summary>
    public DeliveryStatus DeliveryStatus { get; set; }

    /// <summary>
    /// Возвращает или задате ссылку на первичный ключ связанной сущности <see cref="OrderEntity"/>.
    /// </summary>
    public long OrderEntityId { get; set; }
    
    /// <summary>
    /// Связанная сущность <see cref="OrderEntity"/>.
    /// </summary>
    public OrderEntity OrderEntity { get; set; }
}