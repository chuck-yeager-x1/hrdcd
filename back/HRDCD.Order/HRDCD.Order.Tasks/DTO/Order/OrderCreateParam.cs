namespace HRDCD.Order.Tasks.DTO.Order;

/// <summary>
/// Класс, содержащий входные данные для создания заказа.
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
}