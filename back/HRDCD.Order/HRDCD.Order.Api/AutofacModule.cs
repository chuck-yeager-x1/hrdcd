using Autofac;
using HRDCD.Common.Tasks.Handlers;
using HRDCD.Order.Tasks.DTO.Order;
using HRDCD.Order.Tasks.Handlers;
using HRDCD.Order.Tasks.Handlers.Order;

namespace HRDCD.Order.Api;

public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<OrderCreateTaskHandler>()
            .As<ITaskHandler<OrderCreateParam, OrderCreateTaskResult>>();
        
        builder.RegisterType<OrderSelectTaskHandler>()
            .As<ITaskHandler<OrderSelectParam, OrderSelectTaskMultipleResult>>();
        
        builder.RegisterType<OrderSelectSingleTaskHandler>()
            .As<ITaskHandler<int, OrderSelectTaskResult>>();
    }
}