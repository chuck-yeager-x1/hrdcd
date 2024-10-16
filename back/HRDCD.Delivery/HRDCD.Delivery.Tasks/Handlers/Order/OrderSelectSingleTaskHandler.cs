using HRDCD.Common.Tasks.Handlers;
using HRDCD.Delivery.DataModel;
using HRDCD.Delivery.DataModel.Entity;
using HRDCD.Delivery.Tasks.DTO.Order;
using Microsoft.EntityFrameworkCore;

namespace HRDCD.Delivery.Tasks.Handlers.Order;

/// <summary>
/// Обработчик задач по получению данных о заказе по идентификатору (первичному ключу).
/// </summary>
public class OrderSelectSingleTaskHandler : ITaskHandler<long, OrderSelectTaskResult>
{
    private readonly DeliveryDbContext _deliveryDbContext;

    public OrderSelectSingleTaskHandler(DeliveryDbContext deliveryDbContext)
    {
        _deliveryDbContext = deliveryDbContext ?? throw new ArgumentNullException(nameof(deliveryDbContext));
    }

    /// <summary>
    /// Метод для получения данных о заказе по первичному ключу.
    /// </summary>
    /// <param name="argument">Первичный ключ записи о заказе в БД.</param>
    /// <param name="cancellationToken">Запрос на отмену операции.</param>
    /// <returns>Объект, содержащий асинхронную операцию.</returns>
    public async Task<OrderSelectTaskResult> HandleTaskAsync(long argument, CancellationToken cancellationToken)
    {
        var order = await _deliveryDbContext.Set<OrderEntity>()
            .Where(_ => _.IsDeleted == false)
            .SingleOrDefaultAsync(_ => _.Id == argument, cancellationToken);

        return new OrderSelectTaskResult
        {
            IsSuccess = order != null,
            Result = new OrderResultValue
            {
                OrderNumber = order.OrderNumber,
                OrderName = order.OrderName,
                OrderDescription = order.OrderDescription,
            }
        };
    }
}