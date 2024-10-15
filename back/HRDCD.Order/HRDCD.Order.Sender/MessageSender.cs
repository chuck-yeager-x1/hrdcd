using HRDCD.Common.DataModel.Queue;
using MassTransit;
using Microsoft.Extensions.Options;

namespace HRDCD.Order.Sender;

public class MessageSender : IMessageSender
{
    private readonly IBusControl _busControl;
    private readonly IOptions<QueueSettings> _queueSettings;

    public MessageSender(IBusControl busControl, IOptions<QueueSettings> queueSettings)
    {
        _busControl = busControl ?? throw new ArgumentNullException(nameof(busControl));
        _queueSettings = queueSettings ?? throw new ArgumentNullException(nameof(queueSettings));
    }

    public async Task SendMessageAsync(string orderNumber, string orderName, string orderDescription)
    {
        var message = new OrderMessage
            { OrderNumber = orderNumber, OrderName = orderName, OrderDescription = orderDescription };
        var ep = await _busControl.GetSendEndpoint(new Uri($"queue:{_queueSettings.Value.QueueName}"));
        await ep.Send(message);
        Console.WriteLine($"Sent Message: {message.OrderNumber}");
    }
}