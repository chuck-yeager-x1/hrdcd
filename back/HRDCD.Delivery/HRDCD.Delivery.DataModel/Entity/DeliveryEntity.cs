using HRDCD.Common.DataModel.Entity;

namespace HRDCD.Delivery.DataModel.Entity;

public class DeliveryEntity : BaseEntity
{
    public DeliveryStatus DeliveryStatus { get; set; }
    
    public long OrderEntityId {get; set;}
    public OrderEntity OrderEntity {get; set;}
}