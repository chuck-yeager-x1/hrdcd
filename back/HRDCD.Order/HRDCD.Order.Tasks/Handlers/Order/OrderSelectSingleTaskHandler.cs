namespace HRDCD.Order.Tasks.Handlers.Order;

using HRDCD.Common.Tasks.Handlers;
using DataModel;
using DataModel.Entity;
using HRDCD.Order.Tasks.DTO.Order;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Обработчик задач по получению данных о заказе по идентификатору (первичному ключу).
/// </summary>
public class OrderSelectSingleTaskHandler : ITaskHandler<int, OrderSelectTaskResult>
{
    private readonly OrderDbContext _orderDbContext;

    public OrderSelectSingleTaskHandler(OrderDbContext orderDbContext)
    {
        _orderDbContext = orderDbContext ?? throw new ArgumentNullException(nameof(orderDbContext));
    }

    /// <summary>
    /// Метод для получения данных о заказе по первичному ключу.
    /// </summary>
    /// <param name="argument">Первичный ключ записи о заказе в БД.</param>
    /// <param name="cancellationToken">Запрос на отмену операции.</param>
    /// <returns>Объект, содержащий асинхронную операцию.</returns>
    public async Task<OrderSelectTaskResult> HandleTaskAsync(int argument, CancellationToken cancellationToken)
    {
        var order = await _orderDbContext.Set<OrderEntity>()
            .Where(_ => _.IsDeleted == false)
            .SingleOrDefaultAsync(_ => _.Id == argument, cancellationToken);

        return new OrderSelectTaskResult
        {
            IsSuccess = order != null,
            Result = new OrderResultValue
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                OrderName = order.OrderName,
                OrderDescription = order.OrderDescription,
                IsSent = order.IsSent
            }
        };
    }
}