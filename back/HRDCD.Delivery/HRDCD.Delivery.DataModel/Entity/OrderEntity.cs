using HRDCD.Common.DataModel.Entity;

namespace HRDCD.Delivery.DataModel.Entity;

public class OrderEntity : BaseEntity
{
    public string OrderNumber { get; set; }
    
    public string OrderName { get; set; }
    
    public string OrderDescription { get; set; }
    
    public ICollection<DeliveryEntity> DeliveryEntities { get; set; }
}