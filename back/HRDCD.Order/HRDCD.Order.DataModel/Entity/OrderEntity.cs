namespace HRDCD.Order.DataModel.Entity;

using HRDCD.Common.DataModel.Entity;

/// <summary>
/// Класс для сущности "Заказ", сохраняемой в БД.
/// </summary>
public class OrderEntity : BaseEntity
{
    /// <summary>
    /// Возвращает или задает номер заказа.
    /// </summary>
    public string OrderNumber { get; set; }

    /// <summary>
    /// Возвращает или задает наименование заказа.
    /// </summary>
    public string OrderName { get; set; }

    /// <summary>
    /// Возвращает или задает описание заказа.
    /// </summary>
    public string OrderDescription { get; set; }

    public bool IsSent {get; set;}
}