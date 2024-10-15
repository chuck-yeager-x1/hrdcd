using HRDCD.Common.Tasks.Handlers;
using HRDCD.Delivery.DataModel;
using HRDCD.Delivery.Tasks.DTO.Order;

namespace HRDCD.Delivery.Tasks.Handlers.Order;

public class OrderCreateTaskHandler  : ITaskHandler<OrderCreateParam, OrderSelectTaskResult>
{
    private readonly DeliveryDbContext _deliveryDbContext;

    public OrderCreateTaskHandler(DeliveryDbContext deliveryDbContext)
    {
        _deliveryDbContext = deliveryDbContext ?? throw new ArgumentNullException(nameof(deliveryDbContext));
    }

    public async Task<OrderSelectTaskResult> HandleTaskAsync(OrderCreateParam argument, CancellationToken cancellationToken)
    {
        var order = new DataModel.Entity.OrderEntity
        {
            OrderNumber = argument.Number,
            OrderName = argument.Name,
            InsertDate = DateTime.UtcNow,
            UpdateDate = DateTime.UtcNow,
            DeleteDate = DateTime.UtcNow,
            IsDeleted = false,
        };

        await _deliveryDbContext.Set<DataModel.Entity.OrderEntity>().AddAsync(order, cancellationToken);
        await _deliveryDbContext.SaveChangesAsync(cancellationToken);

        var taskResult = new OrderSelectTaskResult
        {
            Result = new OrderResultValue
            {
                Id = order.Id, 
                OrderName = order.OrderName, 
                OrderNumber = order.OrderNumber,
                OrderDescription = order.OrderDescription,
            }
        };

        return taskResult;
    }
}