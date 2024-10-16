using HRDCD.Common.Tasks.Handlers;
using HRDCD.Delivery.DataModel;
using HRDCD.Delivery.DataModel.Entity;
using HRDCD.Delivery.Tasks.DTO.Delivery;
using HRDCD.Delivery.Tasks.DTO.Order;
using Microsoft.EntityFrameworkCore;

namespace HRDCD.Delivery.Tasks.Handlers.Order;

/// <summary>
/// Обработчик задач по созданию заявок на доставку.
/// </summary>
public class OrderStartDeliveryTaskHandler : ITaskHandler<long, DeliveryStartTaskResult>
{
    private readonly DeliveryDbContext _deliveryDbContext;

    public OrderStartDeliveryTaskHandler(DeliveryDbContext deliveryDbContext)
    {
        _deliveryDbContext = deliveryDbContext ?? throw new ArgumentNullException(nameof(deliveryDbContext));
    }

    /// <summary>
    /// Метод для создания заявки на доставку.
    /// </summary>
    /// <param name="argument">Первичный ключ записи о заказе в БД, для которой нужно создать заявку на доставку.</param>
    /// <param name="cancellationToken">Запрос на отмену операции.</param>
    /// <returns>Объект, содержащий асинхронную операцию.</returns>
    public async Task<DeliveryStartTaskResult> HandleTaskAsync(long argument, CancellationToken cancellationToken)
    {
        var order = await _deliveryDbContext.Set<OrderEntity>()
            .Include(_ => _.DeliveryEntities)
            .Where(_ => _.IsDeleted == false)
            .SingleOrDefaultAsync(_ => _.Id == argument, cancellationToken);

        // если у заказа есть связанные заявки на доставку, то возвращаем ошибку и завершаем выполнение.
        if (order.DeliveryEntities.Any())
        {
            return new DeliveryStartTaskResult
            {
                IsSuccess = false,
                ErrorMessage = "По данному заказу уже начата процедура доставки"
            };
        }

        var delivery = new DeliveryEntity
        {
            OrderEntityId = order.Id,
            DeliveryStatus = DeliveryStatus.Created,
            InsertDate = DateTime.UtcNow,
            UpdateDate = DateTime.UtcNow,
            DeleteDate = DateTime.UtcNow,
            IsDeleted = false,
        };
        
        await _deliveryDbContext.Set<DeliveryEntity>().AddAsync(delivery, cancellationToken);
        await _deliveryDbContext.SaveChangesAsync(cancellationToken);

        return new DeliveryStartTaskResult
        {
            IsSuccess = true,
            Delivery = new DeliveryResultValue
            {
                DeliveryStatus = delivery.DeliveryStatus,
                OrderNumber = delivery.OrderEntity.OrderNumber,
                OrderName = delivery.OrderEntity.OrderName,
                Id = delivery.Id,
            },
            Order = new OrderResultValue
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                OrderName = order.OrderName,
                OrderDescription = order.OrderDescription
            }
        };
    }
}