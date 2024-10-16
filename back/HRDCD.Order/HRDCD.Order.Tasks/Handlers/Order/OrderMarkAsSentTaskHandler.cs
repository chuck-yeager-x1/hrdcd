namespace HRDCD.Order.Tasks.Handlers.Order;

using HRDCD.Common.Tasks.Handlers;
using DataModel;
using DataModel.Entity;
using HRDCD.Order.Tasks.DTO.Order;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Обработчик задач по установке для заказов признака завершения отправки в очередь.
/// </summary>
public class OrderMarkAsSentTaskHandler : ITaskHandler<long, OrderSelectTaskResult>
{
    private readonly OrderDbContext _orderDbContext;

    public OrderMarkAsSentTaskHandler(OrderDbContext orderDbContext)
    {
        _orderDbContext = orderDbContext ?? throw new ArgumentNullException(nameof(orderDbContext));
    }

    /// <summary>
    /// Метод для установки у заказа признака завершения отправки в очередь.
    /// </summary>
    /// <param name="argument">Первичный ключ заказа в БД.</param>
    /// <param name="cancellationToken">Запрос на отмену операции.</param>
    /// <returns>Объект, содержащий асинхронную операцию.</returns>
    public async Task<OrderSelectTaskResult> HandleTaskAsync(long argument, CancellationToken cancellationToken)
    {
        var order = await _orderDbContext.Set<OrderEntity>()
            .Where(_ => _.IsDeleted == false)
            .SingleOrDefaultAsync(_ => _.Id == argument, cancellationToken);

        order.IsSent = true;
        await _orderDbContext.SaveChangesAsync(cancellationToken);

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