namespace HRDCD.Order.Sender;

using HRDCD.Common.Tasks.Handlers;
using HRDCD.Order.Tasks.DTO.Order;
using Microsoft.Extensions.Hosting;

public class Worker : IHostedService
{
    private readonly IMessageSender _messageSender;
    private readonly ITaskHandler<int, OrderSelectUnsentTaskResult> _orderSelectUnsentTaskHandler;
    private readonly ITaskHandler<long, OrderSelectTaskResult> _orderMarkAsSentTaskHandler;
    
    private Timer _timer;

    public Worker(IMessageSender messageSender,
        ITaskHandler<int, OrderSelectUnsentTaskResult> orderSelectUnsentTaskHandler,
        ITaskHandler<long, OrderSelectTaskResult> orderMarkAsSentTaskHandler)
    {
        _messageSender = messageSender ?? throw new ArgumentNullException(nameof(messageSender));
        _orderSelectUnsentTaskHandler = orderSelectUnsentTaskHandler ??
                                        throw new ArgumentNullException(nameof(orderSelectUnsentTaskHandler));
        _orderMarkAsSentTaskHandler = orderMarkAsSentTaskHandler ??
                                      throw new ArgumentNullException(nameof(orderMarkAsSentTaskHandler));
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

    /// <summary>
    /// Выполняемый по таймеру метод, отправляющий данные о заказах в очередь сообщений.
    /// </summary>
    /// <param name="state"></param>
    private async void SendMessages(object state)
    {
        // получаем определенное количество необработанных сообщений (т.е. не отправленных в очередь)
        var portion = await _orderSelectUnsentTaskHandler.HandleTaskAsync(1, CancellationToken.None);
        
        // каждое сообщение отправляем в очередь и помечаем для него в БД факт отправки.
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