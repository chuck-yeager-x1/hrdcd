using HRDCD.Common.Tasks.Handlers;
using HRDCD.Delivery.DataModel;
using HRDCD.Delivery.Tasks.DTO.Delivery;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HRDCD.Delivery.Tasks.Handlers.Delivery;

/// <summary>
/// Обработчик задач по получению списка заявок на доставку в постраничном режиме.
/// </summary>
public class DeliverySelectTaskHandler : ITaskHandler<DeliverySelectParam, DeliverySelectTaskMultipleResult>
{
    private readonly ILogger<DeliverySelectTaskHandler> _logger;
    private readonly DeliveryDbContext _deliveryDbContext;

    public DeliverySelectTaskHandler(ILogger<DeliverySelectTaskHandler> logger, DeliveryDbContext deliveryDbContext)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _deliveryDbContext = deliveryDbContext ?? throw new ArgumentNullException(nameof(deliveryDbContext));
    }

    /// <summary>
    /// Метод для получения списка заявок на доставку в постраничном режиме.
    /// </summary>
    /// <param name="argument">Входящий аргумент с параметрами постраничной выборки.</param>
    /// <param name="cancellationToken">Запрос на отмену операции.</param>
    /// <returns>Объект, содержащий асинхронную операцию.</returns>
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