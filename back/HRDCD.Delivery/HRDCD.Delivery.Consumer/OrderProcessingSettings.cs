namespace HRDCD.Delivery.Consumer;

/// <summary>
/// Класс для хранения настроек обработки входящих заказов.
/// </summary>
public class OrderProcessingSettings
{
    /// <summary>
    /// Признак автоматической обработки входящих заказов. Если значение <see cref="true"/>, то заявка
    /// создается сразу же, при приемке данных заказа. В случае значения <see cref="false"/> - необходимо
    /// принудительное создание заявки на доставку.
    /// </summary>
    public bool Automatic { get; set; }
}