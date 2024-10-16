namespace HRDCD.Delivery.Api.Controllers;

using System.Net.Mime;
using HRDCD.Common.Tasks.Handlers;
using HRDCD.Delivery.Tasks.DTO.Delivery;
using Tasks.DTO.Order;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly ITaskHandler<long, OrderSelectTaskResult> _orderSelectSigleTaskHandler;
    private readonly ITaskHandler<OrderSelectParam, OrderSelectTaskMultipleResult> _orderSelectMultipleTaskHandler;
    private readonly ITaskHandler<long, DeliveryStartTaskResult> _orderStartDeliveryTaskHandler;

    public OrderController(
        ITaskHandler<long, OrderSelectTaskResult> orderSelectSigleTaskHandler,
        ITaskHandler<OrderSelectParam, OrderSelectTaskMultipleResult> orderSelectMultipleTaskHandler,
        ITaskHandler<long, DeliveryStartTaskResult> orderStartDeliveryTaskHandler)
    {
        _orderSelectSigleTaskHandler = orderSelectSigleTaskHandler;
        _orderSelectMultipleTaskHandler = orderSelectMultipleTaskHandler;
        _orderStartDeliveryTaskHandler = orderStartDeliveryTaskHandler;
    }

    [HttpGet]
    [Route("select")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<OrderSelectTaskMultipleResult> SelectOrdersAsync([FromQuery] OrderSelectParam orderSelectParam,
        CancellationToken cancellationToken)
    {
        return await _orderSelectMultipleTaskHandler.HandleTaskAsync(orderSelectParam, cancellationToken);
    }

    [HttpGet]
    [Route("select/{orderId}")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<OrderSelectTaskResult> SelectOrderAsync(int orderId, CancellationToken cancellationToken)
    {
        return await _orderSelectSigleTaskHandler.HandleTaskAsync(orderId, cancellationToken);
    }

    [HttpPost]
    [Route("create-delivery")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<DeliveryStartTaskResult> CreateDeliveryAsync([FromQuery] int orderId,
        CancellationToken cancellationToken)
    {
        return await _orderStartDeliveryTaskHandler.HandleTaskAsync(orderId, cancellationToken);
    }
}