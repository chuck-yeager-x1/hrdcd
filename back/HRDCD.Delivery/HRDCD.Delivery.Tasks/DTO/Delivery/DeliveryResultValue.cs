using HRDCD.Delivery.DataModel.Entity;

namespace HRDCD.Delivery.Tasks.DTO.Delivery;

public class DeliveryResultValue
{
    public long Id { get; set; }
    
    public string OrderNumber { get; set; }
    
    public string OrderName { get; set; }
    
    public DeliveryStatus DeliveryStatus { get; set; }
}