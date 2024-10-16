namespace HRDCD.Delivery.Consumer;

using HRDCD.Common.DataModel.Queue;
using HRDCD.Common.Tasks.Handlers;
using HRDCD.Delivery.Tasks.DTO.Delivery;
using Tasks.DTO.Order;
using MassTransit;
using Microsoft.Extensions.Options;

/// <summary>
/// Класс для чтения сообщений из очереди RabbitMQ.
/// </summary>
public class MessageConsumer : IConsumer<OrderMessage>
{
    private readonly ITaskHandler<OrderCreateParam, OrderSelectTaskResult> _orderCreateTaskHandler;
    private readonly ITaskHandler<long, DeliveryStartTaskResult> _orderStartDeliveryTaskHandler;
    private readonly IOptions<OrderProcessingSettings> _orderProcessingSettings;

    public MessageConsumer(
        ITaskHandler<OrderCreateParam, OrderSelectTaskResult> orderCreateTaskHandler,
        IOptions<OrderProcessingSettings> orderProcessingSettings,
        ITaskHandler<long, DeliveryStartTaskResult> orderStartDeliveryTaskHandler)
    {
        _orderCreateTaskHandler =
            orderCreateTaskHandler ?? throw new ArgumentNullException(nameof(orderCreateTaskHandler));
        _orderProcessingSettings =
            orderProcessingSettings ?? throw new ArgumentNullException(nameof(orderProcessingSettings));
        _orderStartDeliveryTaskHandler = orderStartDeliveryTaskHandler ??
                                         throw new ArgumentNullException(nameof(orderStartDeliveryTaskHandler));
    }

    /// <summary>
    /// Метод для обработки входящих сообщений RabbitMQ.
    /// </summary>
    /// <param name="context">Входящее сообщение из очереди.</param>
    public async Task Consume(ConsumeContext<OrderMessage> context)
    {
        // создаем объект для записи данных о заказе из очереди в БД.
        var createParam = new OrderCreateParam
        {
            Number = context.Message.OrderNumber,
            Name = context.Message.OrderName,
            Description = context.Message.OrderDescription,
        };

        try
        {
            // сохраняем данные заказа в БД.
            var order = await _orderCreateTaskHandler.HandleTaskAsync(createParam, CancellationToken.None);

            // если в конфигурации указано автоматическое создаание заявки на доставку - то создаем ее. Иначе,
            // заявку нужно будет создавать принудительно, через вызов REST API.
            if (_orderProcessingSettings.Value.Automatic)
            {
                await _orderStartDeliveryTaskHandler.HandleTaskAsync(order.Result.Id, CancellationToken.None);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}