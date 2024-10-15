using System.Net.Mime;
using HRDCD.Common.Tasks.Handlers;
using HRDCD.Delivery.Tasks.DTO;
using HRDCD.Delivery.Tasks.DTO.Delivery;
using Microsoft.AspNetCore.Mvc;

namespace HRDCD.Delivery.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeliveryController : ControllerBase
{
    private readonly ITaskHandler<int, DeliverySelectTaskResult> _deliverySelectSingleTaskHandler;
    private readonly ITaskHandler<DeliverySelectParam, DeliverySelectTaskMultipleResult> _deliverySelectTaskHandler;
    private readonly ILogger<DeliveryController> _logger;

    public DeliveryController(
        ITaskHandler<int, DeliverySelectTaskResult> deliverySelectSingleTaskHandler,
        ITaskHandler<DeliverySelectParam, DeliverySelectTaskMultipleResult> deliverySelectTaskHandler,
        ILogger<DeliveryController> logger)
    {
        _deliverySelectSingleTaskHandler = deliverySelectSingleTaskHandler;
        _deliverySelectTaskHandler = deliverySelectTaskHandler;
        _logger = logger;
    }

    [HttpGet]
    [Route("select/{deliveryId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeliverySelectTaskResult))]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<DeliverySelectTaskResult> GetDeliveryAsync(int deliveryId, CancellationToken cancellationToken)
    {
        return await _deliverySelectSingleTaskHandler.HandleTaskAsync(deliveryId, cancellationToken);
    }

    [HttpGet]
    [Route("select")]
    public async Task<DeliverySelectTaskMultipleResult> GetDeliveriesAsync([FromQuery]DeliverySelectParam deliverySelectParam,
        CancellationToken cancellationToken)
    {
        return await _deliverySelectTaskHandler.HandleTaskAsync(deliverySelectParam, cancellationToken);
    }
}