using HRDCD.Common.Tasks.Handlers;
using HRDCD.Delivery.DataModel;
using HRDCD.Delivery.DataModel.Entity;
using HRDCD.Delivery.Tasks.DTO;
using HRDCD.Delivery.Tasks.DTO.Delivery;
using Microsoft.EntityFrameworkCore;

namespace HRDCD.Delivery.Tasks.Handlers.Delivery;

public class DeliverySelectSingleTaskHandler : ITaskHandler<int, DeliverySelectTaskResult>
{
    private readonly DeliveryDbContext _deliveryDbContext;

    public DeliverySelectSingleTaskHandler(DeliveryDbContext deliveryDbContext)
    {
        _deliveryDbContext = deliveryDbContext ?? throw new ArgumentNullException(nameof(deliveryDbContext));
    }

    public async Task<DeliverySelectTaskResult> HandleTaskAsync(int argument, CancellationToken cancellationToken)
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