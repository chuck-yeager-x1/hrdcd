using HRDCD.Common.Tasks.Handlers;
using HRDCD.Order.DataModel;
using HRDCD.Order.DataModel.Entity;
using HRDCD.Order.Tasks.DTO.Order;
using Microsoft.EntityFrameworkCore;

namespace HRDCD.Order.Tasks.Handlers.Order;

public class OrderMarkAsSentTaskHandler : ITaskHandler<long, OrderSelectTaskResult>
{
    private readonly OrderDbContext _orderDbContext;

    public OrderMarkAsSentTaskHandler(OrderDbContext orderDbContext)
    {
        _orderDbContext = orderDbContext ?? throw new ArgumentNullException(nameof(orderDbContext));
    }

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