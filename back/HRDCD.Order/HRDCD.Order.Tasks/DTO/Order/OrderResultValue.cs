namespace HRDCD.Order.Tasks.DTO.Order;

public class OrderResultValue
{
    public long Id { get; set; }
    public string OrderNumber { get; set; }
    public string OrderName { get; set; }
    public string OrderDescription { get; set; }
    
    public bool IsInProgress { get; set; }
    public bool IsSent {get; set;}
}