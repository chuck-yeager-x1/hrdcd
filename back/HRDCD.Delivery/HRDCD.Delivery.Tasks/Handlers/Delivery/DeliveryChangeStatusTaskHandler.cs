using HRDCD.Common.Tasks.Handlers;
using HRDCD.Delivery.DataModel;
using HRDCD.Delivery.DataModel.Entity;
using HRDCD.Delivery.Tasks.DTO.Delivery;
using Microsoft.EntityFrameworkCore;

namespace HRDCD.Delivery.Tasks.Handlers.Delivery;

public class DeliveryChangeStatusTaskHandler : ITaskHandler<DeliveryChangeStatusParam, DeliverySelectTaskResult>
{
    private readonly DeliveryDbContext _deliveryDbContext;

    public DeliveryChangeStatusTaskHandler(DeliveryDbContext deliveryDbContext)
    {
        _deliveryDbContext = deliveryDbContext ?? throw new ArgumentNullException(nameof(deliveryDbContext));
    }

    public async Task<DeliverySelectTaskResult> HandleTaskAsync(DeliveryChangeStatusParam param, CancellationToken cancellationToken)
    {
        var delivery = await _deliveryDbContext.Set<DeliveryEntity>()
            .Include(deliveryEntity => deliveryEntity.OrderEntity)
            .Where(_ => _.IsDeleted == false)
            .SingleOrDefaultAsync(_ => _.Id == param.DeliveryId, cancellationToken);
        
        delivery.DeliveryStatus = param.DeliveryStatus;
        delivery.UpdateDate = DateTime.UtcNow;
        await _deliveryDbContext.SaveChangesAsync(cancellationToken);

        return new DeliverySelectTaskResult
        {
            IsSuccess = true,
            Result = new DeliveryResultValue
            {
                DeliveryStatus = delivery.DeliveryStatus,
                OrderNumber = delivery.OrderEntity.OrderNumber,
                OrderName = delivery.OrderEntity.OrderName,
                Id = delivery.Id
            }
        };
    }
}

