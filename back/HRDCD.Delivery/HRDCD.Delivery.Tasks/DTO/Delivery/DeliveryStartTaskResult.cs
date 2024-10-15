using HRDCD.Delivery.Tasks.DTO.Order;

namespace HRDCD.Delivery.Tasks.DTO.Delivery;

public class DeliveryStartTaskResult
{
    public bool IsSuccess { get; set; }
    
    public string Message { get; set; }
    
    public DeliveryResultValue Delivery { get; set; }
    
    public OrderResultValue Order { get; set; }
}