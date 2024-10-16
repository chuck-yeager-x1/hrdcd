namespace HRDCD.Order.Sender;

/// <summary>
/// Интерфейс класс для отправки сообщений в очередь.
/// </summary>
public interface IMessageSender
{
    /// <summary>
    /// Метод для отправки в очередь данных о заказе.
    /// </summary>
    /// <remarks>Можно использовать не отдельные строковые параметры, а заключить их в класс-параметр.</remarks>
    /// <param name="orderNumber">Номер заказа.</param>
    /// <param name="orderName">Наименование заказа.</param>
    /// <param name="orderDescription">Описание заказа.</param>
    /// <returns>Объект, содержащий асинхронную операцию.</returns>
    Task SendMessageAsync(string orderNumber, string orderName, string orderDescription);
}