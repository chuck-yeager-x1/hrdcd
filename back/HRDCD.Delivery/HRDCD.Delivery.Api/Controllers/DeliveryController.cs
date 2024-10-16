namespace HRDCD.Delivery.Api.Controllers;

using System.Net.Mime;
using HRDCD.Common.Tasks.Handlers;
using HRDCD.Delivery.Tasks.DTO.Delivery;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class DeliveryController : ControllerBase
{
    private readonly ITaskHandler<long, DeliverySelectTaskResult> _deliverySelectSingleTaskHandler;
    private readonly ITaskHandler<DeliverySelectParam, DeliverySelectTaskMultipleResult> _deliverySelectTaskHandler;
    private readonly ITaskHandler<DeliveryChangeStatusParam, DeliverySelectTaskResult> _deliveryChangeStatusTaskHandler;
    private readonly ILogger<DeliveryController> _logger;

    public DeliveryController(
        ITaskHandler<long, DeliverySelectTaskResult> deliverySelectSingleTaskHandler,
        ITaskHandler<DeliverySelectParam, DeliverySelectTaskMultipleResult> deliverySelectTaskHandler,
        ILogger<DeliveryController> logger,
        ITaskHandler<DeliveryChangeStatusParam, DeliverySelectTaskResult> deliveryChangeStatusTaskHandler)
    {
        _deliverySelectSingleTaskHandler = deliverySelectSingleTaskHandler ??
                                           throw new ArgumentNullException(nameof(deliverySelectSingleTaskHandler));
        _deliverySelectTaskHandler = deliverySelectTaskHandler ??
                                     throw new ArgumentNullException(nameof(deliverySelectTaskHandler));
        _logger = logger;
        _deliveryChangeStatusTaskHandler = deliveryChangeStatusTaskHandler ??
                                           throw new ArgumentNullException(nameof(deliveryChangeStatusTaskHandler));
    }

    [HttpGet]
    [Route("select/{deliveryId}")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<DeliverySelectTaskResult> GetDeliveryAsync(long deliveryId, CancellationToken cancellationToken)
    {
        return await _deliverySelectSingleTaskHandler.HandleTaskAsync(deliveryId, cancellationToken);
    }

    [HttpGet]
    [Route("select")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<DeliverySelectTaskMultipleResult> GetDeliveriesAsync(
        [FromQuery] DeliverySelectParam deliverySelectParam,
        CancellationToken cancellationToken)
    {
        return await _deliverySelectTaskHandler.HandleTaskAsync(deliverySelectParam, cancellationToken);
    }

    [HttpPut]
    [Route("change-status")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<DeliverySelectTaskResult> ChangeStatusAsync(DeliveryChangeStatusParam param,
        CancellationToken cancellationToken)
    {
        return await _deliveryChangeStatusTaskHandler.HandleTaskAsync(param, cancellationToken);
    }
}