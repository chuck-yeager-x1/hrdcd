namespace HRDCD.Order.Tasks.Handlers.Order;

using HRDCD.Common.Tasks.Handlers;
using DataModel;
using HRDCD.Order.Tasks.DTO.Order;

/// <summary>
/// Обработчик задач по созданию заказа и сохранению его в БД.
/// </summary>
public class OrderCreateTaskHandler : ITaskHandler<OrderCreateParam, OrderCreateTaskResult>
{
    private readonly OrderDbContext _orderDbContext;

    public OrderCreateTaskHandler(OrderDbContext orderDbContext)
    {
        _orderDbContext = orderDbContext ?? throw new ArgumentNullException(nameof(orderDbContext));
    }

    /// <summary>
    /// Метод для создания заказа и сохранения его в БД.
    /// </summary>
    /// <param name="argument">Входящий аргумент с данными для формирования заказа.</param>
    /// <param name="cancellationToken">Запрос на отмену операции.</param>
    /// <returns>Объект, содержащий осинхронную операцию.</returns>
    public async Task<OrderCreateTaskResult> HandleTaskAsync(OrderCreateParam argument,
        CancellationToken cancellationToken)
    {
        var order = new DataModel.Entity.OrderEntity
        {
            OrderNumber = argument.Number,
            OrderName = argument.Name,
            InsertDate = DateTime.UtcNow,
            UpdateDate = DateTime.UtcNow,
            DeleteDate = DateTime.UtcNow,
            IsDeleted = false,
            IsSent = false
        };

        await _orderDbContext.Set<DataModel.Entity.OrderEntity>().AddAsync(order, cancellationToken);
        await _orderDbContext.SaveChangesAsync(cancellationToken);

        var taskResult = new OrderCreateTaskResult
        {
            Result = new OrderResultValue
            {
                Id = order.Id,
                OrderName = order.OrderName,
                OrderNumber = order.OrderNumber,
                OrderDescription = order.OrderDescription,
                IsSent = order.IsSent
            }
        };

        return taskResult;
    }
}