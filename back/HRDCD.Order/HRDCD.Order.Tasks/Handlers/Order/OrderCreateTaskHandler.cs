using HRDCD.Common.Tasks.Handlers;
using HRDCD.Order.DataModel;
using HRDCD.Order.Tasks.DTO.Order;

namespace HRDCD.Order.Tasks.Handlers.Order;

public class OrderCreateTaskHandler : ITaskHandler<OrderCreateParam, OrderCreateTaskResult>
{
    private readonly OrderDbContext _orderDbContext;

    public OrderCreateTaskHandler(OrderDbContext orderDbContext)
    {
        _orderDbContext = orderDbContext ?? throw new ArgumentNullException(nameof(orderDbContext));
    }

    public async Task<OrderCreateTaskResult> HandleTaskAsync(OrderCreateParam argument,
        CancellationToken cancellationToken)
    {
        var order = new DataModel.Entity.OrderEntity
        {
            OrderNumber = argument.Number,
            OrderName = argument.Name,
            InsertDate = DateTime.UtcNow,
            UpdateDate = DateTime.UtcNow,
            DeleteDate = DateTime.UtcNow,
            IsDeleted = false,
            IsSent = false
        };

        await _orderDbContext.Set<DataModel.Entity.OrderEntity>().AddAsync(order, cancellationToken);
        await _orderDbContext.SaveChangesAsync(cancellationToken);

        var taskResult = new OrderCreateTaskResult
        {
            Result = new OrderResultValue
            {
                Id = order.Id, 
                OrderName = order.OrderName, 
                OrderNumber = order.OrderNumber,
                OrderDescription = order.OrderDescription,
                IsSent = order.IsSent
            }
        };

        return taskResult;
    }
}