namespace HRDCD.Order.Sender;

using HRDCD.Common.DataModel.Queue;
using MassTransit;
using Microsoft.Extensions.Options;

/// <summary>
/// <inheritdoc cref="IMessageSender"/>.
/// </summary>
public class MessageSender : IMessageSender
{
    private readonly IBusControl _busControl;
    private readonly IOptions<QueueSettings> _queueSettings;

    public MessageSender(IBusControl busControl, IOptions<QueueSettings> queueSettings)
    {
        _busControl = busControl ?? throw new ArgumentNullException(nameof(busControl));
        _queueSettings = queueSettings ?? throw new ArgumentNullException(nameof(queueSettings));
    }

    /// <summary>
    /// <inheritdoc cref="IMessageSender.SendMessageAsync"/>
    /// </summary>
    public async Task SendMessageAsync(string orderNumber, string orderName, string orderDescription)
    {
        // формируем сообщение из строковых параметров метода.
        var message = new OrderMessage
            { OrderNumber = orderNumber, OrderName = orderName, OrderDescription = orderDescription };

        // получаем нужную очередь, и отправляем в нее сообщение.
        var ep = await _busControl.GetSendEndpoint(new Uri($"queue:{_queueSettings.Value.QueueName}"));
        await ep.Send(message);
    }
}