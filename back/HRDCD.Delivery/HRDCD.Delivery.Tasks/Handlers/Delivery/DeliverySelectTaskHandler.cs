using HRDCD.Common.Tasks.Handlers;
using HRDCD.Delivery.DataModel;
using HRDCD.Delivery.Tasks.DTO;
using HRDCD.Delivery.Tasks.DTO.Delivery;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HRDCD.Delivery.Tasks.Handlers.Delivery;

public class DeliverySelectTaskHandler : ITaskHandler<DeliverySelectParam, DeliverySelectTaskMultipleResult>
{
    private readonly ILogger<DeliverySelectTaskHandler> _logger;
    private readonly DeliveryDbContext _deliveryDbContext;

    public DeliverySelectTaskHandler(ILogger<DeliverySelectTaskHandler> logger, DeliveryDbContext deliveryDbContext)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _deliveryDbContext = deliveryDbContext ?? throw new ArgumentNullException(nameof(deliveryDbContext));
    }

    public async Task<DeliverySelectTaskMultipleResult> HandleTaskAsync(DeliverySelectParam argument, CancellationToken cancellationToken)
    {
        var startIndex = (argument.PageNumber - 1) * argument.PageSize;

        var deliveries = _deliveryDbContext.Set<DataModel.Entity.DeliveryEntity>()
            .Where(_ => _.IsDeleted == false);
        
        var total = await deliveries.CountAsync(cancellationToken);

        var deliveriesPaged = await deliveries
            .Skip(startIndex)
            .Take(argument.PageSize)
            .Select(_ => new DeliveryResultValue
            {
                Id = _.Id,
                OrderNumber = _.OrderEntity.OrderNumber,
                OrderName = _.OrderEntity.OrderName,
                DeliveryStatus = _.DeliveryStatus,
            }).ToListAsync(cancellationToken);

        return new DeliverySelectTaskMultipleResult
        {
            Results = deliveriesPaged,
            PageNumber = argument.PageNumber,
            PageSize = argument.PageSize,
            Total = total,
            ErrorMessage = "",
            IsSuccess = true
        };
    }
}