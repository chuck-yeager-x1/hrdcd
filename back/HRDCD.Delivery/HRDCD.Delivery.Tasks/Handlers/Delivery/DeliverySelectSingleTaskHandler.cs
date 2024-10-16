using HRDCD.Common.Tasks.Handlers;
using HRDCD.Delivery.DataModel;
using HRDCD.Delivery.DataModel.Entity;
using HRDCD.Delivery.Tasks.DTO.Delivery;
using Microsoft.EntityFrameworkCore;

namespace HRDCD.Delivery.Tasks.Handlers.Delivery;

/// <summary>
/// Обработчик задач на получение данных по заявке на доставку по идентификатору (первичному ключу).
/// </summary>
public class DeliverySelectSingleTaskHandler : ITaskHandler<long, DeliverySelectTaskResult>
{
    private readonly DeliveryDbContext _deliveryDbContext;

    public DeliverySelectSingleTaskHandler(DeliveryDbContext deliveryDbContext)
    {
        _deliveryDbContext = deliveryDbContext ?? throw new ArgumentNullException(nameof(deliveryDbContext));
    }

    /// <summary>
    /// Метод для получения данных о заявке на доставку по первичному ключу.
    /// </summary>
    /// <param name="argument">Первичный ключ записи о заявке на доставку в БД.</param>
    /// <param name="cancellationToken">Запрос на отмену операции.</param>
    /// <returns>Объект, содержащий асинхронную операцию.</returns>
    public async Task<DeliverySelectTaskResult> HandleTaskAsync(long argument, CancellationToken cancellationToken)
    {
        var delivery = await _deliveryDbContext.Set<DeliveryEntity>()
            .Include(deliveryEntity => deliveryEntity.OrderEntity)
            .Where(_ => _.IsDeleted == false)
            .SingleOrDefaultAsync(_ => _.Id == argument, cancellationToken);

        return new DeliverySelectTaskResult
        {
            IsSuccess = delivery != null,
            Result = new DeliveryResultValue
            {
                Id = delivery.Id,
                DeliveryStatus = delivery.DeliveryStatus,
                OrderNumber = delivery.OrderEntity.OrderNumber,
                OrderName = delivery.OrderEntity.OrderName
            }
        };
    }
}