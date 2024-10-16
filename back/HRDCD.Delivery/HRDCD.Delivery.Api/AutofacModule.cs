namespace HRDCD.Delivery.Api;

using Autofac;
using HRDCD.Common.Tasks.Handlers;
using HRDCD.Delivery.Tasks.DTO.Delivery;
using Tasks.DTO.Order;
using HRDCD.Delivery.Tasks.Handlers.Delivery;
using Tasks.Handlers.Order;

public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<DeliverySelectSingleTaskHandler>()
            .As<ITaskHandler<long, DeliverySelectTaskResult>>();
        
        builder.RegisterType<DeliverySelectTaskHandler>()
            .As<ITaskHandler<DeliverySelectParam, DeliverySelectTaskMultipleResult>>();
        
        builder.RegisterType<OrderSelectSingleTaskHandler>()
            .As<ITaskHandler<long, OrderSelectTaskResult>>();
        
        builder.RegisterType<OrderSelectTaskHandler>()
            .As<ITaskHandler<OrderSelectParam, OrderSelectTaskMultipleResult>>();

        builder.RegisterType<OrderStartDeliveryTaskHandler>()
            .As<ITaskHandler<long, DeliveryStartTaskResult>>();

        builder.RegisterType<DeliveryChangeStatusTaskHandler>()
            .As<ITaskHandler<DeliveryChangeStatusParam, DeliverySelectTaskResult>>();
    }
}