using HRDCD.Order.DataModel;
using HRDCD.Order.Tasks.DTO.Order;
using Microsoft.EntityFrameworkCore;

namespace HRDCD.Order.Tasks.Handlers.Order;

public class OrderSelectTaskHandler : ITaskHandler<OrderSelectParam, OrderSelectTaskMultipleResult>
{
    private readonly OrderDbContext _orderDbContext;


    public OrderSelectTaskHandler(OrderDbContext orderDbContext)
    {
        _orderDbContext = orderDbContext ?? throw new ArgumentNullException(nameof(orderDbContext));
    }

    public async Task<OrderSelectTaskMultipleResult> HandleTaskAsync(OrderSelectParam argument,
        CancellationToken cancellationToken)
    {
        var startIndex = (argument.PageNumber - 1) * argument.PageSize;

        var orders = _orderDbContext.Set<DataModel.Entity.OrderEntity>()
            .Where(_ => _.IsDeleted == false);

        var ordersPaged = await orders
            .Skip(startIndex)
            .Take(argument.PageSize)
            .Select(_ => new OrderResultValue
            {
                Id = _.Id,
                OrderNumber = _.OrderNumber,
                OrderName = _.OrderName,
                OrderDescription = _.OrderDescription,
            }).ToListAsync(cancellationToken);

        var total = await orders.CountAsync(cancellationToken);

        return new OrderSelectTaskMultipleResult
        {
            Results = ordersPaged,
            PageNumber = argument.PageNumber,
            PageSize = argument.PageSize,
            Total = total,
            ErrorMessage = "",
            IsSuccess = true
        };
    }
}