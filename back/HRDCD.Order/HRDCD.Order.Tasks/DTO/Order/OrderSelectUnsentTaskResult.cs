namespace HRDCD.Order.Tasks.DTO.Order;

/// <summary>
/// Класс, содержащий результат выполнения операции по получению списка заказов, не отправленных
/// в очередь сообщений.
/// </summary>
/// <remarks>Содержит количество заказов, указанных для получения при выполнении операции.</remarks>
public class OrderSelectUnsentTaskResult
{
    public IList<OrderResultValue> Results { get; set; }
}