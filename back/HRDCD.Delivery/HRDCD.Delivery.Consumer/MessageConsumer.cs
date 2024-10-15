using HRDCD.Common.DataModel.Queue;
using HRDCD.Common.Tasks.Handlers;
using HRDCD.Delivery.Tasks.DTO.Delivery;
using HRDCD.Delivery.Tasks.DTO.Order;
using MassTransit;
using Microsoft.Extensions.Options;

namespace HRDCD.Delivery.Consumer;

public class MessageConsumer : IConsumer<OrderMessage>
{
    private readonly ITaskHandler<OrderCreateParam, OrderSelectTaskResult> _orderCreateTaskHandler;
    private readonly ITaskHandler<long, DeliverySelectTaskResult> _orderStartDeliveryTaskHandler;
    private readonly IOptions<OrderProcessingSettings> _orderProcessingSettings;
    
    public MessageConsumer(
        ITaskHandler<OrderCreateParam, OrderSelectTaskResult> orderCreateTaskHandler, 
        IOptions<OrderProcessingSettings> orderProcessingSettings, 
        ITaskHandler<long, DeliverySelectTaskResult> orderStartDeliveryTaskHandler)
    {
        _orderCreateTaskHandler = orderCreateTaskHandler ?? throw new ArgumentNullException(nameof(orderCreateTaskHandler));
        _orderProcessingSettings = orderProcessingSettings ?? throw new ArgumentNullException(nameof(orderProcessingSettings));
        _orderStartDeliveryTaskHandler = orderStartDeliveryTaskHandler ?? throw new ArgumentNullException(nameof(orderStartDeliveryTaskHandler));
    }

    public async Task Consume(ConsumeContext<OrderMessage> context)
    {
        var createParam = new OrderCreateParam
        {
            Number = context.Message.OrderNumber,
            Name = context.Message.OrderName,
            Description = context.Message.OrderDescription,
        };

        try
        {
            var order = await _orderCreateTaskHandler.HandleTaskAsync(createParam, CancellationToken.None);

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