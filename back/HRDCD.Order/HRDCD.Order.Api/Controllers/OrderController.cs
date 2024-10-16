namespace HRDCD.Order.Api.Controllers;

using System.Net.Mime;
using HRDCD.Common.Tasks.Handlers;
using HRDCD.Order.Tasks.DTO.Order;
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
        _orderCreateHandler = orderCreateHandler ?? throw new ArgumentNullException(nameof(orderCreateHandler));
        _orderSelectHandler = orderSelectHandler ?? throw new ArgumentNullException(nameof(orderSelectHandler));
        _orderSelectSingleHandler = orderSelectSingleHandler ??
                                    throw new ArgumentNullException(nameof(orderSelectSingleHandler));
    }

    [HttpPost]
    [Route("create")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<OrderCreateTaskResult> CreateOrderAsync(OrderCreateParam orderCreateParam,
        CancellationToken cancellationToken)
    {
        return await _orderCreateHandler.HandleTaskAsync(orderCreateParam, cancellationToken);
    }

    [HttpGet]
    [Route("select")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<OrderSelectTaskMultipleResult> SelectOrdersAsync([FromQuery] OrderSelectParam orderSelectParam,
        CancellationToken cancellationToken)
    {
        return await _orderSelectHandler.HandleTaskAsync(orderSelectParam, cancellationToken);
    }

    [HttpGet]
    [Route("select/{orderId}")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<OrderSelectTaskResult> SelectOrderAsync(int orderId, CancellationToken cancellationToken)
    {
        return await _orderSelectSingleHandler.HandleTaskAsync(orderId, cancellationToken);
    }
}