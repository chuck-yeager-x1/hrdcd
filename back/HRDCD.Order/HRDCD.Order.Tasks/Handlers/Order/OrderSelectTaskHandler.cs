namespace HRDCD.Order.Tasks.Handlers.Order;

using HRDCD.Common.Tasks.Handlers;
using DataModel;
using HRDCD.Order.Tasks.DTO.Order;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Обработчик задач по получению списка заказов в постраничном режиме.
/// </summary>
public class OrderSelectTaskHandler : ITaskHandler<OrderSelectParam, OrderSelectTaskMultipleResult>
{
    private readonly OrderDbContext _orderDbContext;

    public OrderSelectTaskHandler(OrderDbContext orderDbContext)
    {
        _orderDbContext = orderDbContext ?? throw new ArgumentNullException(nameof(orderDbContext));
    }

    /// <summary>
    /// Метод для получения списка заказов в постраничном режиме.
    /// </summary>
    /// <param name="argument">Входящий аргумент с параметрами постраничной выборки.</param>
    /// <param name="cancellationToken">Запрос на отмену операции.</param>
    /// <returns>Объект, содержащий асинхронную операцию.</returns>
    public async Task<OrderSelectTaskMultipleResult> HandleTaskAsync(OrderSelectParam argument,
        CancellationToken cancellationToken)
    {
        var startIndex = (argument.PageNumber - 1) * argument.PageSize;

        var orders = _orderDbContext.Set<DataModel.Entity.OrderEntity>()
            .Where(_ => _.IsDeleted == false);

        var total = await orders.CountAsync(cancellationToken);

        var ordersPaged = await orders
            .Skip(startIndex)
            .Take(argument.PageSize)
            .Select(_ => new OrderResultValue
            {
                Id = _.Id,
                OrderNumber = _.OrderNumber,
                OrderName = _.OrderName,
                OrderDescription = _.OrderDescription,
                IsSent = _.IsSent,
            }).ToListAsync(cancellationToken);

        return new OrderSelectTaskMultipleResult
        {
            Results = ordersPaged,
            PageNumber = argument.PageNumber,
            PageSize = argument.PageSize,
            Total = total,
            IsSuccess = true
        };
    }
}