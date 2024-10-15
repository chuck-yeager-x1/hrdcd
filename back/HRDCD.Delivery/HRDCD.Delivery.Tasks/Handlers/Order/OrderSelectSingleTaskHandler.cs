using HRDCD.Common.Tasks.Handlers;
using HRDCD.Delivery.DataModel;
using HRDCD.Delivery.DataModel.Entity;
using HRDCD.Delivery.Tasks.DTO.Order;
using Microsoft.EntityFrameworkCore;

namespace HRDCD.Delivery.Tasks.Handlers.Order;

public class OrderSelectSingleTaskHandler : ITaskHandler<int, OrderSelectTaskResult>
{
    private readonly DeliveryDbContext _deliveryDbContext;

    public OrderSelectSingleTaskHandler(DeliveryDbContext deliveryDbContext)
    {
        _deliveryDbContext = deliveryDbContext ?? throw new ArgumentNullException(nameof(deliveryDbContext));
    }

    public async Task<OrderSelectTaskResult> HandleTaskAsync(int argument, CancellationToken cancellationToken)
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