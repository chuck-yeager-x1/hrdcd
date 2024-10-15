using HRDCD.Common.Tasks.Handlers;
using HRDCD.Order.Tasks.DTO.Order;
using Microsoft.Extensions.Hosting;

namespace HRDCD.Order.Sender;

public class Worker : IHostedService
{
    private readonly IMessageSender _messageSender;
    private Timer _timer;
    private readonly ITaskHandler<int, OrderSelectUnsentTaskResult> _orderSelectUnsentTaskHandler;
    private readonly ITaskHandler<long, OrderSelectTaskResult> _orderMarkAsSentTaskHandler;

    public Worker(IMessageSender messageSender, ITaskHandler<int, OrderSelectUnsentTaskResult> orderSelectUnsentTaskHandler, ITaskHandler<long, OrderSelectTaskResult> orderMarkAsSentTaskHandler)
    {
        _messageSender = messageSender ?? throw new ArgumentNullException(nameof(messageSender));
        _orderSelectUnsentTaskHandler = orderSelectUnsentTaskHandler ?? throw new ArgumentNullException(nameof(orderSelectUnsentTaskHandler));
        _orderMarkAsSentTaskHandler = orderMarkAsSentTaskHandler ?? throw new ArgumentNullException(nameof(orderMarkAsSentTaskHandler));
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(SendMessages, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
        
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    private async void SendMessages(object state)
    {
        var portion = await _orderSelectUnsentTaskHandler.HandleTaskAsync(1, CancellationToken.None);
        foreach (var orderResultValue in portion.Results.ToList())
        {
            try
            {
                await _messageSender.SendMessageAsync(orderResultValue.OrderNumber, orderResultValue.OrderName,
                    orderResultValue.OrderDescription);
                await _orderMarkAsSentTaskHandler.HandleTaskAsync(orderResultValue.Id, CancellationToken.None);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}