using HRDCD.Delivery.DataModel.Entity;

namespace HRDCD.Delivery.Tasks.DTO.Delivery;

public class DeliveryChangeStatusParam
{
    public long DeliveryId { get; set; }
    
    public DeliveryStatus DeliveryStatus { get; set; }
}