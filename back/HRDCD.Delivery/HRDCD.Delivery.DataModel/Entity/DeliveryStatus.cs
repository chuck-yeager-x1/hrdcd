namespace HRDCD.Delivery.DataModel.Entity;

/// <summary>
/// Перечисление с вариантами статусов доставки.
/// </summary>
public enum DeliveryStatus
{
    /// <summary>
    /// Статус "Создан".
    /// </summary>
    Created = 1,
    /// <summary>
    /// Статус "В работе".
    /// </summary>
    InProgress = 2,
    /// <summary>
    /// Статус "Отменено".
    /// </summary>
    Cancelled = 3,
    /// <summary>
    /// Статус "Возобновлено".
    /// </summary>
    Renewed = 4,
    /// <summary>
    /// Статус "Завершено".
    /// </summary>
    Done = 5
}