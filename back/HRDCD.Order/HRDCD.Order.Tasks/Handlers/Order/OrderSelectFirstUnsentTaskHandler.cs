using HRDCD.Common.Tasks.Handlers;
using HRDCD.Order.DataModel;
using HRDCD.Order.Tasks.DTO.Order;
using Microsoft.EntityFrameworkCore;

namespace HRDCD.Order.Tasks.Handlers.Order;

public class OrderSelectFirstUnsentTaskHandler : ITaskHandler<int, OrderSelectUnsentTaskResult>
{
    private readonly OrderDbContext _orderDbContext;

    public OrderSelectFirstUnsentTaskHandler(OrderDbContext orderDbContext)
    {
        _orderDbContext = orderDbContext ?? throw new ArgumentNullException(nameof(orderDbContext));
    }

    public async Task<OrderSelectUnsentTaskResult> HandleTaskAsync(int argument, CancellationToken cancellationToken)
    {
        var orders = _orderDbContext.Set<DataModel.Entity.OrderEntity>()
            .Where(_ => _.IsDeleted == false && _.IsSent == false);

        var ordersPaged = await orders
            .Take(argument)
            .Select(_ => new OrderResultValue
            {
                Id = _.Id,
                OrderNumber = _.OrderNumber,
                OrderName = _.OrderName,
                OrderDescription = _.OrderDescription,
                IsSent = _.IsSent,
            }).ToListAsync(cancellationToken);

        return new OrderSelectUnsentTaskResult
        {
            Results = ordersPaged
        };
    }
}