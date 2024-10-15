using HRDCD.Common.Tasks.Handlers;
using HRDCD.Delivery.DataModel;
using HRDCD.Delivery.Tasks.DTO.Order;
using Microsoft.EntityFrameworkCore;

namespace HRDCD.Delivery.Tasks.Handlers.Order;

public class OrderSelectTaskHandler : ITaskHandler<OrderSelectParam, OrderSelectTaskMultipleResult>
{
    private readonly DeliveryDbContext _deliveryDbContext;

    public OrderSelectTaskHandler(DeliveryDbContext deliveryDbContext)
    {
        _deliveryDbContext = deliveryDbContext ?? throw new ArgumentNullException(nameof(deliveryDbContext));
    }

    public async Task<OrderSelectTaskMultipleResult> HandleTaskAsync(OrderSelectParam argument, CancellationToken cancellationToken)
    {
        var startIndex = (argument.PageNumber - 1) * argument.PageSize;

        var orders = _deliveryDbContext.Set<DataModel.Entity.OrderEntity>()
            .Where(_ => _.IsDeleted == false);
        
        var total = await orders.CountAsync(cancellationToken);

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