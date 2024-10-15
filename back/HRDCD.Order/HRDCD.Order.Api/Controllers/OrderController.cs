using System.Net.Mime;
using HRDCD.Common.Tasks.Handlers;
using HRDCD.Order.Tasks.DTO.Order;
using HRDCD.Order.Tasks.Handlers;

namespace HRDCD.Order.Api.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly ITaskHandler<OrderCreateParam, OrderCreateTaskResult> _orderCreateHandler;
    private readonly ITaskHandler<OrderSelectParam, OrderSelectTaskMultipleResult> _orderSelectHandler;
    private readonly ITaskHandler<int, OrderSelectTaskResult> _orderSelectSingleHandler;

    public OrderController(
        ITaskHandler<OrderCreateParam, OrderCreateTaskResult> orderCreateHandler,
        ITaskHandler<OrderSelectParam, OrderSelectTaskMultipleResult> orderSelectHandler,
        ITaskHandler<int, OrderSelectTaskResult> orderSelectSingleHandler)
    {
        _orderCreateHandler = orderCreateHandler;
        _orderSelectHandler = orderSelectHandler;
        _orderSelectSingleHandler = orderSelectSingleHandler;
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(OrderCreateTaskResult), StatusCodes.Status201Created)]
    [Route("create")]
    public async Task<OrderCreateTaskResult> CreateOrderAsync(OrderCreateParam orderCreateParam,
        CancellationToken cancellationToken)
    {
        return await _orderCreateHandler.HandleTaskAsync(orderCreateParam, cancellationToken);
    }

    [HttpGet]
    [Route("select")]
    public async Task<OrderSelectTaskMultipleResult> SelectOrdersAsync([FromQuery] OrderSelectParam orderSelectParam,
        CancellationToken cancellationToken)
    {
        return await _orderSelectHandler.HandleTaskAsync(orderSelectParam, cancellationToken);
    }

    [HttpGet]
    [Route("select/{orderId}")]
    [ProducesResponseType(typeof(OrderSelectTaskResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<OrderSelectTaskResult> SelectOrderAsync(int orderId, CancellationToken cancellationToken)
    {
        return await _orderSelectSingleHandler.HandleTaskAsync(orderId, cancellationToken);
    }
}