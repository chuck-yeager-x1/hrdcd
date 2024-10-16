namespace HRDCD.Common.DataModel.Queue;

/// <summary>
/// Интерфейс сообщения о сущности "Заказ", передаваемого через очередь сообщений.
/// </summary>
public interface IOrderMessage
{
    /// <summary>
    /// Возвращает или задает номер заказа.
    /// </summary>
    string OrderNumber { get; set; }

    /// <summary>
    /// Возвращает или задает наименование заказа.
    /// </summary>
    string OrderName { get; set; }
    
    /// <summary>
    /// Возвращает или задает описание заказа.
    /// </summary>
    string OrderDescription { get; set; }
}