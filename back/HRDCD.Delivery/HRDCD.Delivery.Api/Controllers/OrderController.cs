using HRDCD.Common.Tasks.Handlers;
using HRDCD.Delivery.Tasks.DTO.Delivery;
using HRDCD.Delivery.Tasks.DTO.Order;
using Microsoft.AspNetCore.Mvc;

namespace HRDCD.Delivery.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly ITaskHandler<int, OrderSelectTaskResult> _orderSelectSigleTaskHandler;
    private readonly ITaskHandler<OrderSelectParam, OrderSelectTaskMultipleResult> _orderSelectMultipleTaskHandler;
    private readonly ITaskHandler<int, DeliverySelectTaskResult> _orderStartDeliveryTaskHandler;

    public OrderController(
        ITaskHandler<int, OrderSelectTaskResult> orderSelectSigleTaskHandler, 
        ITaskHandler<OrderSelectParam, OrderSelectTaskMultipleResult> orderSelectMultipleTaskHandler, 
        ITaskHandler<int, DeliverySelectTaskResult> orderStartDeliveryTaskHandler)
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
    [Route("create")]
    public async Task<DeliverySelectTaskResult> CreateDeliveryAsync(int orderId, CancellationToken cancellationToken)
    {
        return await _orderStartDeliveryTaskHandler.HandleTaskAsync(orderId, cancellationToken);
    }
}