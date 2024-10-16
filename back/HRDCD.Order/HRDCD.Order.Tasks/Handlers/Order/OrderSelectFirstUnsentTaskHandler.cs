namespace HRDCD.Order.Tasks.Handlers.Order;

using HRDCD.Common.Tasks.Handlers;
using DataModel;
using HRDCD.Order.Tasks.DTO.Order;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Обработчик задач по получению списка заказов, не отправленных в очередь сообщений.
/// </summary>
public class OrderSelectFirstUnsentTaskHandler : ITaskHandler<int, OrderSelectUnsentTaskResult>
{
    private readonly OrderDbContext _orderDbContext;

    public OrderSelectFirstUnsentTaskHandler(OrderDbContext orderDbContext)
    {
        _orderDbContext = orderDbContext ?? throw new ArgumentNullException(nameof(orderDbContext));
    }

    /// <summary>
    /// Метод для получения списка заказов, не отправленных в очередь сообщений.
    /// </summary>
    /// <param name="argument">Количество запрашиваемых элементов.</param>
    /// <param name="cancellationToken">Запрос на отмену операции.</param>
    /// <returns>Объект, содержащий асинхронную операцию.</returns>
    public async Task<OrderSelectUnsentTaskResult> HandleTaskAsync(int argument, CancellationToken cancellationToken)
    {
        var orders = _orderDbContext.Set<DataModel.Entity.OrderEntity>()
            .Where(_ => _.IsDeleted == false && _.IsSent == false);

        var ordersPaged = await orders
            .Take(argument)
            .Select(_ => new OrderResultValue
            {
                Id = _.Id,
                OrderNumber = _.OrderNumber,
                OrderName = _.OrderName,
                OrderDescription = _.OrderDescription,
                IsSent = _.IsSent,
            }).ToListAsync(cancellationToken);

        return new OrderSelectUnsentTaskResult
        {
            Results = ordersPaged
        };
    }
}