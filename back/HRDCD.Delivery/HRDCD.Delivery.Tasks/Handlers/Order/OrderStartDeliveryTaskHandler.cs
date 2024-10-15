using HRDCD.Common.Tasks.Handlers;
using HRDCD.Delivery.DataModel;
using HRDCD.Delivery.DataModel.Entity;
using HRDCD.Delivery.Tasks.DTO.Delivery;
using Microsoft.EntityFrameworkCore;

namespace HRDCD.Delivery.Tasks.Handlers.Order;

public class OrderStartDeliveryTaskHandler : ITaskHandler<int, DeliverySelectTaskResult>
{
    private readonly DeliveryDbContext _deliveryDbContext;

    public OrderStartDeliveryTaskHandler(DeliveryDbContext deliveryDbContext)
    {
        _deliveryDbContext = deliveryDbContext ?? throw new ArgumentNullException(nameof(deliveryDbContext));
    }

    public async Task<DeliverySelectTaskResult> HandleTaskAsync(int argument, CancellationToken cancellationToken)
    {
        var order = await _deliveryDbContext.Set<OrderEntity>()
            .Where(_ => _.IsDeleted == false)
            .SingleOrDefaultAsync(_ => _.Id == argument, cancellationToken);

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

        return new DeliverySelectTaskResult
        {
            IsSuccess = true,
            Result = new DeliveryResultValue
            {
                DeliveryStatus = delivery.DeliveryStatus,
                OrderNumber = delivery.OrderEntity.OrderNumber,
                OrderName = delivery.OrderEntity.OrderName,
                Id = delivery.Id,
            }
        };
    }
}