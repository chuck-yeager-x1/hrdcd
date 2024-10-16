using HRDCD.Common.Tasks.Handlers;
using HRDCD.Delivery.Tasks.DTO.Delivery;
using HRDCD.Delivery.Tasks.DTO.Order;
using Microsoft.AspNetCore.Mvc;

namespace HRDCD.Delivery.Api.Controllers;

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
    public async Task<OrderSelectTaskMultipleResult> SelectOrdersAsync([FromQuery] OrderSelectParam orderSelectParam,
        CancellationToken cancellationToken)
    {
        return await _orderSelectMultipleTaskHandler.HandleTaskAsync(orderSelectParam, cancellationToken);
    }

    [HttpGet]
    [Route("select/{orderId}")]
    [ProducesResponseType(typeof(OrderSelectTaskResult), StatusCodes.Status200OK)]
    public async Task<OrderSelectTaskResult> SelectOrderAsync(int orderId, CancellationToken cancellationToken)
    {
        return await _orderSelectSigleTaskHandler.HandleTaskAsync(orderId, cancellationToken);
    }

    [HttpPost]
    [Route("create-delivery")]
    public async Task<DeliveryStartTaskResult> CreateDeliveryAsync([FromQuery]int orderId, CancellationToken cancellationToken)
    {
        return await _orderStartDeliveryTaskHandler.HandleTaskAsync(orderId, cancellationToken);
    }
}